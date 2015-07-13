using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.Phone.Scheduler;

namespace FaceBookWp8._1
{
    public class WakeOrWasteBL
    {

        public Challenge GetMyChallenge()
        {
            // TEMP DATA
            Challenge myChallenge = new Challenge();
            myChallenge.WakeUpTime = DateTime.Now.AddMinutes(2);
            myChallenge.Duration = 21;
            myChallenge.DonationAmount = 1;
            return myChallenge;
        }

        public void SetAlarm()
        {
            Alarm alarm = new Alarm("Wake or waste");
            alarm.Content = "Wake uppp";
            alarm.Sound = new Uri("/Ringtones/baroqueloop90z.wma", UriKind.Relative);
            alarm.BeginTime = DateTime.Now.AddMinutes(1);
            alarm.ExpirationTime = DateTime.Now.AddDays(21);
            alarm.RecurrenceType = RecurrenceInterval.Daily;

            ScheduledActionService.Add(alarm);
        }

    }
}
