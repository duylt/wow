using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wow.MobileApp.ViewModels
{
    public class RemindPageViewModel : ViewModelBase
    {
        public string Time { get; set; }
        public string Message { get; set; }
        public bool IsDonate { get; set; }
        public string DonateAmount { get; set; }

        public int CountDay { get; set; }

        public List<ChallengeDayDetail> ChallengeDayDetails { get; set; }

        public void Init()
        {
            var myChallenge = App.MyViewModel.MyChallenge;
           
            this.Time = myChallenge.WakeupTime.ToString("HH:mm");
            this.Message = myChallenge.Message;
            this.CountDay = 16;
            this.ChallengeDayDetails = new List<ChallengeDayDetail>();
            var randomizer = new Random();
            for (int i = 0; i < 16; i++)
            {
                this.ChallengeDayDetails.Add(new ChallengeDayDetail
                {
                    Date = DateTime.Now,
                    IsCompleted = randomizer.Next(10) % 2 == 0
                });
            }
        }


    }

    public class ChallengeDayDetail
    {
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
    }
}
