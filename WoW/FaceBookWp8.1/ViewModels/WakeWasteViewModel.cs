using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceBookWp8._1.ViewModels
{
    public class WakeWasteViewModel : ViewModelBase
    {
        public int NumberOfDay { get; set; }

        public string Time { get; set; }
        public string ButtonName { get; set; }

        public string Color { get; set; }

        public void Init()
        {
            var randomizer = new Random();
            if (randomizer.Next(10) % 2 == 0)
            {
                this.Time = "06:30";
                this.ButtonName = "Wake";
                this.Color = "#FF6E8BDD";
            }
            else
            {
                this.Time = "07:30";
                this.ButtonName = "Waste";
                this.Color = "#FFFF6800";
            }
        }
    }
}
