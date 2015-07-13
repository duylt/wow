using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WoW.Core.Enum;

namespace WoW.Core
{
    public interface IWowCore
    {
        #region User
        User GetUser(int userId);
        User GetUserByClientId(string clientId);
        bool AddUser(User user);
        bool RemoveUser(int userId);
        bool UpdateUser(User user);
        #endregion

        #region Social
        bool UpdateSocialToken(int socialId, string token);

        #endregion

        #region Message
        Message GetMessage(int messageId);
        List<Message> GetMessagesByChallegen(int challengId);
        bool AddMessage(Message message);
        bool RemoveMessage(int messageId);
        bool UpdateMessage(Message message);
        Message GetDefaultWakeMessage();
        Message GetDefaultWasteMessage();
        Message GetMessageByUserType(int userId, MessageType type);
        #endregion

        #region Donation
        List<Donation> GetAllDonation();
        bool AddDonation(Donation donation);
        bool RemoveDonation(int donationId);
        bool UpdateDonation(Donation donation);
        #endregion

        #region Challenge
        bool AddChallenge(Challenge challenge);
        Challenge GetChallenge(int challengeId);
        List<Challenge> GetChallengeByUserId(int userId);
        Challenge GetActiveChallengeByUserId(int userId);
        bool UpdateChallenge(Challenge challenge);
        bool RemoveChallenge(int challengeId);
        List<Challenge> GetBatchActiveChallengeByPage(int pageIndex, int size);
        List<Challenge> GetAllActiveChallenge();
        #endregion

        #region ChallengeDay
        bool AddChallengeDay(ChallegeDay challengeDay);
        bool UpdateChallengeDay(ChallegeDay challengeDay);
        bool RemoveChallengeDay(int challengeDayId);
        List<ChallegeDay> GetChallengeDayByUser(int userId, int challengeId);
        #endregion


        void CompletedChallenge(int challengeId);
    }
}
