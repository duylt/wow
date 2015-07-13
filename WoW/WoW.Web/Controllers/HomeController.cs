using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WoW.Biz;
using WoW.Core;

namespace WoW.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Resource
        private Biz.IWoWBiz biz = new WoWBiz();
        private Core.User CurrentUser
        {
            get
            {
                var user = (Core.User)Session["User"];
                if (user == null)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect(Request.Url.AbsolutePath);
                }
                return user;
            }
        }

        private bool IsFirstWakeWaste
        {
            get
            {
                return Session["FirstWakeWaste"] == null ? true : (bool)Session["FirstWakeWaste"];
            }
            set
            {
                Session["FirstWakeWaste"] = value;
            }
        }
        #endregion

        /// <summary>
        /// index page show challegen detail
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var user = CurrentUser;
            var challenge = GetCurrentChallenge(true);
            if (challenge == null)
            {
                return RedirectToActionPermanent("CreateChallenge");
            }

            // Go to wake or wake page when current time is after 1 hour before
            var settingTime = DateTime.Now.Date;
            settingTime = settingTime.AddHours(challenge.WakeUpTime.Value.Hours);
            settingTime = settingTime.AddMinutes(challenge.WakeUpTime.Value.Minutes);
            if (settingTime.AddHours(-1) <= DateTime.Now)
            {
                if (IsFirstWakeWaste)
                {
                    var todayChallenge = challenge.ChallegeDays.Where(cd => cd.Date.Date.Equals(DateTime.Now.Date)).FirstOrDefault();
                    //var isLastDate = this.IsLastDate(challenge);
                    //ViewBag.IsLastDate = isLastDate;
                    if (todayChallenge == null || (todayChallenge != null && !todayChallenge.Succeed))
                    {
                        return RedirectToAction("WakeOrWaste");
                    }
                }
            }

            return View(challenge);
        }

        public ActionResult CompleteChallenge()
        {
            var challenge = GetCurrentChallenge(true);
            if (challenge == null)
            {
                return RedirectToActionPermanent("CreateChallenge");
            }
            if(IsLastDate(challenge))
            {
                var todayChallenge = challenge.ChallegeDays.Where(cd => cd.Date.Date.Equals(DateTime.Now.Date)).FirstOrDefault();
                if (IsFirstWakeWaste && todayChallenge == null)
                {
                    var settingTime = DateTime.Now;
                    settingTime = settingTime.AddHours(challenge.WakeUpTime.Value.Hours);
                    settingTime = settingTime.AddMinutes(challenge.WakeUpTime.Value.Minutes);
                    ChallegeDay lastDate = new ChallegeDay()
                    {
                        ChallengeId = challenge.Id,
                        Date = DateTime.Now,
                        IsProcessed = true,
                        UserId = CurrentUser.Id,
                    };
                    if ((settingTime.AddHours(-1) <= DateTime.Now) && (settingTime.AddMinutes(10) >= DateTime.Now))
                    {
                        lastDate.Succeed = true;
                    }
                    else
                    {
                        lastDate.Succeed = false;
                    }
                    biz.AddChallengeDay(lastDate);
                    IsFirstWakeWaste = false;
                }
                Session["FromComleted"] = true;
                return RedirectToActionPermanent("Statistics");
            }
                return RedirectToActionPermanent("Index");
            }

        public ActionResult FinalizeChallenge()
        {
            var challenge = GetCurrentChallenge(true);
            if (challenge == null)
            {
                return RedirectToActionPermanent("CreateChallenge");
            }
            biz.CompletedChallenge(challenge.Id);
            // process payment, post to facebook, email .....
            return RedirectToActionPermanent("Index");
        }

        public ActionResult WakeOrWaste()
        {
            var user = CurrentUser;
            var challenge = GetCurrentChallenge(true);
            if (challenge == null)
            {
                return RedirectToActionPermanent("CreateChallenge");
            }
            return View(challenge);
        }

        public ActionResult Wake()
        {
            var user = CurrentUser;
            var challenge = GetCurrentChallenge(false);
            if (challenge == null)
            {
                return RedirectToActionPermanent("CreateChallenge");
            }
            biz.ChallengeWake(challenge);
            IsFirstWakeWaste = false;
            return RedirectToAction("Index");
        }

        public ActionResult Waste()
        {
            var user = CurrentUser;
            var challenge = GetCurrentChallenge(false);
            if (challenge == null)
            {
                return RedirectToActionPermanent("CreateChallenge");
            }
            biz.ChallengeWaste(challenge);
            IsFirstWakeWaste = false;
            return RedirectToAction("Index");
        }

        public ActionResult Statistics()
        {
            var user = CurrentUser;
            var challenge = GetCurrentChallenge(true);
            if (challenge == null)
            {
                return RedirectToActionPermanent("CreateChallenge");
            }
            var fromCompleted = (bool)Session["FromComleted"];
            if (!fromCompleted)
            {
                return RedirectToActionPermanent("Index");
            }
            Session["FromComleted"] = null;
            ViewBag.Message = "Statistics";

            ViewBag.FullyAccomplished = IsFullyAccomplished(challenge);
            ViewBag.TotalDonationAmount = TotalDonationAmount(challenge);

            return View(challenge);
        }

        public ActionResult TestPage()
        {

            return View();
        }

        [HttpGet]
        public ActionResult CreateChallenge()
        {
            var user = CurrentUser;
            var challenge = GetCurrentChallenge();
            if (challenge == null)
            {
                IWoWBiz biz = new WoWBiz();
                List<Core.Donation> donations = biz.GetAllDonation();
                ViewBag.Donations = donations;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
                //return View();
            }
        }

        [HttpPost]
        public ActionResult CreateChallenge(Core.Challenge challenge, string WakeUpTime)
        {
            var user = CurrentUser;
            DateTime dt = Convert.ToDateTime(WakeUpTime);
            if (user != null)
            {
                Core.Challenge newChallenge = new Core.Challenge()
                {
                    UserId = user.Id,
                    WakeUpTime = new TimeSpan(0, dt.Hour, dt.Minute, 0, 0),
                    Duration = challenge.Duration == null ? 21 : challenge.Duration,
                    DonationId = challenge.DonationId,
                    DonationAmount = challenge.DonationAmount,
                    StartTime = DateTime.Now,
                    isActive = true
                };
                IWoWBiz biz = new WoWBiz();
                biz.AddChallenge(newChallenge);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Dashboard()
        {
            var user = CurrentUser;
            return View((Core.User)Session["User"]);
        }

        #region Utilities
        private Challenge GetCurrentChallenge(bool refresh = true)
        {
            Challenge challenge = refresh ? null : (Challenge)Session["challenge"];
            if (challenge == null)
            {
                challenge = biz.GetActiveChallengeByUserId(CurrentUser.Id);
                Session["challenge"] = challenge;
            }
            return challenge;
        }

        private bool IsLastDate(Challenge challenge)
        {
            var lastDate = challenge.StartTime.Value.AddDays((double)challenge.Duration);
            if (DateTime.Now.Date < lastDate.Date && DateTime.Now.Month <= lastDate.Month)
            {
                return false;
            }
            return true;
        }

        private bool IsFullyAccomplished(Challenge challenge)
        {
            var successfulDayNo = GetSuccessfulDayNo(challenge);
            if (successfulDayNo == challenge.Duration)
                return true;
            return false;
        }

        private int TotalDonationAmount(Challenge challenge)
        {
            var failedDayNo = GetFailedDayNo(challenge);
            if (challenge.DonationAmount.HasValue)
                return failedDayNo * (int)challenge.DonationAmount;
            return 0;
        }

        private int GetSuccessfulDayNo(Challenge challenge)
        {
            return challenge.ChallegeDays.Count(cd => cd.Succeed);
        }

        private int GetFailedDayNo(Challenge challenge)
        {
            return challenge.ChallegeDays.Count(cd => !cd.Succeed);
        }
        #endregion
    }
}
