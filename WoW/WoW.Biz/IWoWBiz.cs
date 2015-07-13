using System;
using System.Collections.Generic;
using WoW.Core;
using WoW.Core.Enum;

namespace WoW.Biz
{
    public interface IWoWBiz
    {
        bool CreateNewUser(WoW.Core.User user);
        WoW.Core.User GetUserByClientId(string clientId);
        bool UpdateSocialToken(int socialId, string token);
        bool UpdateUser(WoW.Core.User user);


        bool AddChallenge(Challenge challenge);
        Challenge GetChallenge(int challengeId);
        List<Challenge> GetChallengeByUserId(int userId);
        Challenge GetActiveChallengeByUserId(int userId);
        bool UpdateChallenge(Challenge challenge);
        bool RemoveChallenge(int challengeId);
        List<Challenge> GetBatchActiveChallengeByPage(int pageIndex, int size);
        List<Challenge> GetAllActiveChallenge();

        #region Donation
        List<Donation> GetAllDonation();
        #endregion

        #region Message
        Message GetDefaultWakeMessage();
        Message GetDefaultWasteMessage();
        #endregion

        void CompletedChallenge(int challengeId);
        bool ChallengeWake(Challenge challenge, bool processed = false);
        bool ChallengeWaste(Challenge challenge, bool processed = false);


        void AddChallengeDay(ChallegeDay challengeDay);
    }
}
