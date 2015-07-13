using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WoW.Core;

namespace WoW.Biz
{
    public class WoWBiz : IWoWBiz
    {
        private IWowCore _core;
        public WoWBiz()
        {
            _core = new WoWCore();
        }

        public User GetUserByClientId(string clientId) // by client ID include social 
        {
            return _core.GetUserByClientId(clientId);
        }

        public bool CreateNewUser(User user)
        {
            return _core.AddUser(user);
        }

        /// <summary>
        /// Update Social Token by socialId
        /// </summary>
        /// <param name="socialId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool UpdateSocialToken(int socialId, string token)
        {
            return _core.UpdateSocialToken(socialId, token);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(User user)
        {
            return _core.UpdateUser(user);
        }

        public bool AddChallenge(Challenge challenge)
        {
            return _core.AddChallenge(challenge);
        }

        public Challenge GetChallenge(int challengeId)
        {
            return _core.GetChallenge(challengeId);
        }

        public List<Challenge> GetChallengeByUserId(int userId)
        {
            return _core.GetChallengeByUserId(userId);
        }

        public Challenge GetActiveChallengeByUserId(int userId)
        {
            return _core.GetActiveChallengeByUserId(userId);
        }

        public bool UpdateChallenge(Challenge challenge)
        {
            return _core.UpdateChallenge(challenge);
        }

        public bool RemoveChallenge(int challengeId)
        {
            return _core.RemoveChallenge(challengeId);
        }

        public List<Challenge> GetBatchActiveChallengeByPage(int pageIndex, int size)
        {
            return _core.GetBatchActiveChallengeByPage(pageIndex, size);
        }

        public List<Challenge> GetAllActiveChallenge()
        {
            return _core.GetAllActiveChallenge();
        }

        #region Challenge Progress
        public bool ChallengeWake(Challenge challenge, bool processed = false)
        {
            challenge = this.GetChallenge(challenge.Id);
            var todayChallenge = challenge.ChallegeDays.Where(cld => cld.Date.Date.Equals(DateTime.Now.Date)).FirstOrDefault();
            if (todayChallenge == null)
            {
                ChallegeDay day = new ChallegeDay
                {
                    ChallengeId = challenge.Id,
                    Date = DateTime.Now,
                    Succeed = true,
                    UserId = challenge.UserId,
                    IsProcessed = processed
                };
                this._core.AddChallengeDay(day);
                challenge.ChallegeDays.Add(day);
            }
            //Process to post facebook challenge
            if (!processed)
            {
                TempBiz tempBiz = new TempBiz();
                tempBiz.ChallengeProcess(challenge);
            }
            return true;
        }
        public bool ChallengeWaste(Challenge challenge, bool processed = false)
        {
            challenge = this.GetChallenge(challenge.Id);
            var todayChallenge = challenge.ChallegeDays.Where(cld => cld.Date.Date.Equals(DateTime.Now.Date)).FirstOrDefault();
            if (todayChallenge == null)
            {
                ChallegeDay day = new ChallegeDay
                {
                    ChallengeId = challenge.Id,
                    Date = DateTime.Now,
                    Succeed = false,
                    UserId = challenge.UserId,
                    IsProcessed = processed
                };
                this._core.AddChallengeDay(day);
            }
            //Process to post facebook challenge
            TempBiz tempBiz = new TempBiz();
            if (!processed)
                tempBiz.ChallengeProcess(challenge);
            return true;
        }
        #endregion

        public List<Donation> GetAllDonation()
        {
            return this._core.GetAllDonation();
        }


        public Message GetDefaultWakeMessage()
        {
            return this._core.GetDefaultWakeMessage();
        }

        public Message GetDefaultWasteMessage()
        {
            return this._core.GetDefaultWasteMessage();
        }

        public bool UpdateChallengeDay(ChallegeDay challengeday)
        {
            return this._core.UpdateChallengeDay(challengeday);
        }

        public void CompletedChallenge(int challengeId)
        {
            _core.CompletedChallenge(challengeId);
        }


        public void AddChallengeDay(ChallegeDay challengeDay)
        {
            _core.AddChallengeDay(challengeDay);
        }
    }
}
