using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using WoW.Core.Enum;

namespace WoW.Core
{
    public class WoWCore : IWowCore
    {
        public WoWCore()
        {

        }

        #region User
        public User GetUser(int userId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Users.Find(userId);
            }
        }

        public User GetUserByClientId(string clientId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var social = entity.Socials.Include(s => s.User).FirstOrDefault(s => s.ClientId_.Equals(clientId));
                if (social != null)
                {
                    return entity.Users.Include(u => u.Socials).FirstOrDefault(u => u.Id == social.UserId);
                }
                return null;
            }
        }

        public bool AddUser(User user)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var rs = entity.Users.Add(user);
                entity.SaveChanges();
                return rs != null;
            }
        }

        public bool RemoveUser(int userId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var user = entity.Users.Find(userId);
                if (user != null)
                {
                    var rs = entity.Users.Remove(user);
                    entity.SaveChanges();
                    return rs != null;
                }
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var userToUpdated = entity.Users.FirstOrDefault(u => u.Id == user.Id);
                if (userToUpdated != null)
                {
                    userToUpdated.Name = user.Name;
                    userToUpdated.ImageUrl = user.ImageUrl;
                    entity.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        #endregion
        

        public Message GetMessage(int messageId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Messages.Find(messageId);
            }
        }

        public List<Message> GetMessagesByChallegen(int challengId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Messages.Where(m => m.ChallengeId == challengId).ToList();
            }
        }

        public bool AddMessage(Message message)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var rs = entity.Messages.Add(message);
                entity.SaveChanges();
                return rs != null;
            }
        }

        public bool RemoveMessage(int messageId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var message = entity.Messages.Find(messageId);
                if (message != null)
                {
                    var rs = entity.Messages.Remove(message);
                    entity.SaveChanges();
                    return rs != null;
                }
                return false;
            }
        }

        public bool UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Message GetDefaultWakeMessage()
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Messages.FirstOrDefault(m => m.ChallengeId == null && m.UserId == null && m.Type == (int)MessageType.Wake);
            }
        }

        public Message GetDefaultWasteMessage()
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Messages.FirstOrDefault(m => m.ChallengeId == null && m.UserId == null && m.Type == (int)MessageType.Waste);
            }
        }

        public Message GetMessageByUserType(int userId, Enum.MessageType type)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Messages.FirstOrDefault(m => m.UserId == userId && m.Type == (int)type);
            }
        }

        public List<Donation> GetAllDonation()
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Donations.ToList();
            }
        }

        public bool AddDonation(Donation donation)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var rs = entity.Donations.Add(donation);
                entity.SaveChanges();
                return rs != null;
            }
        }

        public bool RemoveDonation(int donationId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var donation = entity.Donations.Find(donationId);
                if (donation != null)
                {
                    var rs = entity.Donations.Remove(donation);
                    entity.SaveChanges();
                    return rs != null;
                }
                return false;
            }
        }

        public bool UpdateDonation(Donation donation)
        {
            throw new NotImplementedException();
        }

        public bool AddChallenge(Challenge challegen)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var rs = entity.Challenges.Add(challegen);
                entity.SaveChanges();
                return rs != null;
            }
        }

        public Challenge GetChallenge(int challengeId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Challenges.Include(c => c.User.Socials).Include(c => c.Donation).Include(cl => cl.ChallegeDays).Where(cl => cl.Id == challengeId).FirstOrDefault();
            }
        }

        public List<Challenge> GetChallengeByUserId(int userId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Challenges.Where(c => c.UserId == userId).ToList();
            }
        }

        public bool UpdateChallenge(Challenge challenge)
        {
            throw new NotImplementedException();
        }

        public bool RemoveChallenge(int challengeId)
        {
            throw new NotImplementedException();
        }

        public bool AddChallengeDay(ChallegeDay challengeDay)
        {
            
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var rs = entity.ChallegeDays.Add(challengeDay);
                entity.SaveChanges();
                return rs != null;
            }
        }

        public bool UpdateChallengeDay(ChallegeDay challengeDay)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                var coreValue = entity.ChallegeDays.FirstOrDefault(c => c.Id == challengeDay.Id);
                if (coreValue != null)
                {
                    coreValue.IsProcessed = challengeDay.IsProcessed;
                }
                entity.SaveChanges();
                return true;
            }
        }

        public bool RemoveChallengeDay(int challengeDayId)
        {
            throw new NotImplementedException();
        }

        public List<ChallegeDay> GetChallengeDayByUser(int userId, int challengeId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.ChallegeDays.Where(c => c.UserId == userId && c.ChallengeId == challengeId).ToList();
            }
        }


        public bool UpdateSocialToken(int socialId, string token)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var social = entity.Socials.Find(socialId);
                if (social != null)
                {
                    social.Token = token;
                    entity.SaveChanges();
                    return true;
                }
                return false;
            }
        }


        public Challenge GetActiveChallengeByUserId(int userId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var challenge = entity.Challenges.Include(cl => cl.ChallegeDays).Include(c => c.Donation).FirstOrDefault(c => c.UserId == userId && c.isActive == true);
                //Remove the circle
                if (challenge != null)
                {
                    foreach (var item in challenge.ChallegeDays)
                    {
                        item.Challenge = null;
                    }

                    if (challenge.Donation != null)
                    {
                        challenge.Donation.Challenges = null;
                    }
                }
                
                return challenge;
            }
        }

        public List<Challenge> GetBatchActiveChallengeByPage(int pageIndex, int size)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Challenges.OrderBy(c => c.StartTime).Include(c=>c.Donation).Include(cl => cl.ChallegeDays).Include(c => c.User.Socials).Where(c => c.isActive == true).Skip((pageIndex - 1) * size).Take(size).ToList();
            }
        }

        public List<Challenge> GetAllActiveChallenge()
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Challenges.Where(c => c.isActive == true).ToList();
            }
        }



        public void CompletedChallenge(int challengeId)
        {
            using (var entity = new WakeOrWasteEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                var completedChallenge = entity.Challenges.Find(challengeId);
                completedChallenge.isActive = false;
                entity.SaveChanges();
            }
        }
    }
}
