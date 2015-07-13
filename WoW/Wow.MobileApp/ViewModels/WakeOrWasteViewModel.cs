using Microsoft.Phone.Scheduler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Wow.MobileApp.ViewModels
{
    public class WakeOrWasteViewModel : ViewModelBase
    {
        private bool _isLoadCompleted;

        public bool IsLoadCompleted
        {
            get { return _isLoadCompleted; }
            set
            {
                _isLoadCompleted = value;
                base.OnPropertyChanged("IsLoadCompleted");
            }
        }

        private UserProfile _userProfile;

        public UserProfile MyUserProfile
        {
            get { return _userProfile; }
            set
            {
                _userProfile = value;
                base.OnPropertyChanged("MyUserProfile");
            }
        }

        private Challenge _challenge;

        public Challenge MyChallenge
        {
            get { return _challenge; }
            set
            {
                _challenge = value;
                base.OnPropertyChanged("MyChallenge");
            }
        }

        public void Initializer()
        {
            this.IsLoadCompleted = false;
            WebClient webClient = new WebClient();
            webClient.DownloadStringAsync(new Uri(GET_USER_SOCIAL));
            webClient.DownloadStringCompleted += webClient_LoadProfileCompleted;
        }


        #region private


        private string GET_USER_SOCIAL = "http://wakeorwaste.azurewebsites.net/api/public/GetUserSocial?clientId=940690715953478";
        private string ALARM_NAME = "Wake Or Waste";
        private void SetAlarm(DateTime wakeupTime, int duration = 1)
        {
            Alarm alarm = new Alarm(ALARM_NAME);
            alarm.Content = "Wake uppp";
            alarm.Sound = new Uri("/Ringtones/cuckoo__clock.mp3", UriKind.Relative);
            alarm.BeginTime = wakeupTime;
            alarm.ExpirationTime = DateTime.Now.AddDays(duration);
            alarm.RecurrenceType = RecurrenceInterval.Daily;
            if (!(ScheduledActionService.Find(ALARM_NAME) == null))
            {
                ScheduledActionService.Remove(ALARM_NAME);
            }

            ScheduledActionService.Add(alarm);

        }
        void webClient_LoadProfileCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JObject o = (JObject)JToken.Parse(e.Result);

                UserProfile userProfile = new UserProfile()
                {
                    Name = o["Name"].ToString(),
                    ProfileImageUrl = o["ImageUrl"].ToString()
                };
                this.MyUserProfile = userProfile;

                JArray challenges = (JArray)o["Challenges"];

                if (challenges != null && challenges.Count > 0)
                {
                    JObject objChallenge = (JObject)challenges[0];
                    Challenge myChallenge = new Challenge();
                    myChallenge.Message = objChallenge["Messages"].HasValues ? objChallenge["Messages"].ToString() : string.Empty;
                    var strTime = objChallenge["WakeUpTime"].ToString();

                    myChallenge.WakeupTime = Convert.ToDateTime(strTime);
                    myChallenge.ChallengeId = Convert.ToInt16(objChallenge["Id"]);
                    myChallenge.DonateId = objChallenge["DonationId"].HasValues ? Convert.ToInt16(objChallenge["DonationId"]) : -1;
                    myChallenge.Duration = objChallenge["Duration"].HasValues ? Convert.ToInt16(objChallenge["Duration"]) : 1;
                    //temp data for changle 
                    this.MyChallenge = myChallenge;
                    SetAlarm(myChallenge.WakeupTime, myChallenge.Duration);
                }

            }
            catch
            {
                SetAlarm(DateTime.Now.AddMinutes(1));
            }
            this.IsLoadCompleted = true;


        }
        #endregion
    }
}
