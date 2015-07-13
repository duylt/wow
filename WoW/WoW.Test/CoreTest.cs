using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WoW.Core;
using WoW.Core.Enum;

namespace WoW.Test
{
    [TestClass]
    public class CoreTest
    {
        [TestMethod]
        public void TestMethodAddUser()
        {
            Social facebook = new Social()
            {
                Name = "Hieu Pious",
                Email = "hieupious@ymail.com",
                Token = "aaaaaaaaaaaaaaaaaaaa",
                Type = (int)SocialType.Facebook,
                ClientId_ = "32131231312"
            };

            User newUser = new User() {
                Name = "Hieu Pious",
                ImageUrl = "abc",                
            };

            newUser.Socials.Add(facebook);
            
            
            IWowCore core = new WoWCore();
            Assert.AreEqual(true, core.AddUser(newUser));
            
        }

        [TestMethod]
        public void TestUpdateSocial()
        {
            IWowCore core = new WoWCore();
            Assert.AreEqual(true, core.UpdateSocialToken(1, "1232"));
        }

        [TestMethod]
        public void TestGetUserBySocialId()
        {
            IWowCore core = new WoWCore();
            var user = core.GetUserByClientId("1234");
            Assert.AreEqual(3, user.Id);
            Assert.AreEqual(1, user.Socials.Count);
        }

        [TestMethod]
        public void AddDonation()
        {
            // charity foundation
            //Donation aarp = new Donation()
            //{
            //    Name = "American Association of Retired Persons",
            //    IconUrl = "https://en.wikipedia.org/wiki/File:American_Association_of_Retired_Persons_(logo).png"
            //};

            Donation donation = new Donation()
            {
                Name = "Bill & Melinda Gates Foundation",
                IconUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/Bill_%26_Melinda_Gates_Foundation_logo.svg/175px-Bill_%26_Melinda_Gates_Foundation_logo.svg.png"
            };

            IWowCore core = new WoWCore();
            Assert.AreEqual(true, core.AddDonation(donation));
        }
    }
}
