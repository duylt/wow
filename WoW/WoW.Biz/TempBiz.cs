using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WoW.Biz.Social;
using WoW.Core;

namespace WoW.Biz
{
    public class TempBiz
    {
        WoWBiz Biz = new WoWBiz();
        FacebookConnector Fb = new FacebookConnector();

        public void ChallengeProcess(Challenge challenge)
        {
            try
            {
                var todayChallenge = challenge.ChallegeDays.Where(c => c.Date.Date.Equals(DateTime.Now.Date)).FirstOrDefault();
                #region Fail Message
                if (todayChallenge == null)
                {
                    var settingTime = DateTime.Now.Date;
                    settingTime = settingTime.AddHours(challenge.WakeUpTime.Value.Hours);
                    settingTime = settingTime.AddMinutes(challenge.WakeUpTime.Value.Minutes);

                    //Fail challenge when dont complete challenge before 10minutes after goal time
                    settingTime = settingTime.AddMinutes(10);
                    if (settingTime < DateTime.Now)
                    {
                        //Over 10 minutes but user still dont complete day challenge
                        Biz.ChallengeWaste(challenge, true);
                        PostFailMessage(challenge);
                        todayChallenge.IsProcessed = true;
                        Biz.UpdateChallengeDay(todayChallenge);
                        //#############################################
                    }
                }
                else
                {
                    if (todayChallenge.Succeed == false && todayChallenge.IsProcessed == false)
                    {
                        PostFailMessage(challenge);
                        todayChallenge.IsProcessed = true;
                        Biz.UpdateChallengeDay(todayChallenge);
                    }
                }
                //#############################################
                
                //Challenge InProgress
                #endregion


                //Success message
                if (challenge != null && todayChallenge != null)
                {
                    if (todayChallenge.Succeed && !todayChallenge.IsProcessed)
                    {
                        var successChallenges = challenge.ChallegeDays.Where(c => c.Succeed == true).ToList();

                        //Announcement 1st success
                        if (successChallenges.Count() == 1)
                        {
                            PostStatus(challenge, "I completed my very first challenge to wake up earlier!");
                            todayChallenge.IsProcessed = true;
                            Biz.UpdateChallengeDay(todayChallenge);
                        }

                        //Announcement 50% progress
                        if ((successChallenges.Count() / challenge.Duration.Value) >= 0.5)
                        {
                            PostStatus(challenge, "I completed 50% challenge to wake up earlier!");
                            todayChallenge.IsProcessed = true;
                            Biz.UpdateChallengeDay(todayChallenge);
                        }

                        //Perfect
                        if (successChallenges.Count() == challenge.Duration.Value)
                        {
                            PostStatus(challenge, "I completed 100% challenge to wake up earlier! I changed my habit!");
                            todayChallenge.IsProcessed = true;
                            Biz.UpdateChallengeDay(todayChallenge);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void PostStatus(Challenge challenge, string message)
        {
            var clientId = challenge.User.Socials.First().ClientId_;
            var clientToken = challenge.User.Socials.First().Token;
            Fb.Post(clientId, clientToken, message);
        }

        private void PostFailMessage(Challenge challenge)
        {
            List<string> failMessage = new List<string>
            {
                "I MISSED MY GOAL of waking up today. Please give your likes and comments below to encourage me to move on!",
            };

            List<string> failMessageDonate = new List<string>
            {
                "As part of the challenge on wakeorwaste.com, I have donated ${0} to the charity foundation {1}"
            };

            if (challenge.Donation != null)
            {
                PostStatus(challenge, string.Format(failMessageDonate[0], challenge.DonationAmount.Value.ToString(), challenge.Donation.Name));
            }
            else
            {
                PostStatus(challenge, failMessage[0]);
            }
        }
    }
}
