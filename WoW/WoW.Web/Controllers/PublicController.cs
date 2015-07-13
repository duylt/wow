using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WoW.Biz;
using WoW.Core;

namespace WoW.Web.Controllers
{
    public class PublicController : ApiController
    {
        private WoW.Biz.WoWBiz biz = new Biz.WoWBiz();

        public WoW.Core.User GetUserSocial(string clientId)
        {
            var user = (WoW.Core.User)biz.GetUserByClientId(clientId);
            user.Challenges = new List<Challenge>()
            {
                biz.GetActiveChallengeByUserId(user.Id)
            };

            user.Socials.First().User = null;
            return user;
        }

        /// <summary>
        /// It take so long time to make a background service
        /// go on live
        /// This API is just a ugly solution to replace background service
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool AppWorker()
        {
            TempBiz challengeLogic = new TempBiz();
            //Get All Challenge
            int page = 1;
            int size = 10;
            while (true)
            {
                var challenges = biz.GetBatchActiveChallengeByPage(page, size);
                //No more data
                if (challenges.Count == 0)
                {
                    break;
                }

                foreach (var challenge in challenges)
                {
                    challengeLogic.ChallengeProcess(challenge);
                }
                if (challenges.Count < size)
                {
                    break;
                }
                page++;
            }
            return true;
        }
    }
}
