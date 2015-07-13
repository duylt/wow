using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wow.MobileApp
{
    public class ToggleWakeWasteViewModel : ViewModelBase
    {
        public int ChallengeId { get; set; }
        public int NumberOfDay { get; set; }

        public string Time { get; set; }
        public string ButtonName { get; set; }

        public string Color { get; set; }
        public int CountDay { get; set; }

        public bool IsWake()
        {
            var myChallenge = App.MyViewModel.MyChallenge;
            if (DateTime.Now.AddMinutes(-60).TimeOfDay <= myChallenge.WakeupTime.TimeOfDay &&
        DateTime.Now.AddMinutes(15).TimeOfDay >= myChallenge.WakeupTime.TimeOfDay)
            {
                return true;
            }
            return false;
        }
        public void Init()
        {
            var myChallenge = App.MyViewModel.MyChallenge;
            ChallengeId = myChallenge.ChallengeId;

            this.Time = myChallenge.WakeupTime.ToString("HH:mm");
            if (IsWake())
            {
                this.ButtonName = "Wake";
                this.Color = "#FF6E8BDD";
            }
            else
            {

                this.ButtonName = "Waste";
                this.Color = "#FFFF6800";
            }
        }


    }
}
