using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wow.MobileApp.ViewModels;

namespace Wow.MobileApp
{
    public class Challenge : ViewModelBase
    {
        #region Properties

        private int _challengeId;

        public int ChallengeId
        {
            get { return _challengeId; }
            set
            {
                _challengeId = value;
                OnPropertyChanged("ChallengeId");
            }
        }

        private DateTime _wakeupTime;

        public DateTime WakeupTime
        {
            get { return _wakeupTime; }
            set
            {
                _wakeupTime = value;
                OnPropertyChanged("WakeupTime");
            }
        }

        private int _duration;

        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged("Duration");
            }
        }



        private int _donateId;

        public int DonateId
        {
            get { return _donateId; }
            set
            {
                _donateId = value;
                OnPropertyChanged("DonateId");
            }
        }

        private int _donateAmount;

        public int DonateAmount
        {
            get { return _donateAmount; }
            set
            {
                _donateAmount = value;
                OnPropertyChanged("DonateAmount");
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private int _countDay;

        public int CountDay
        {
            get { return _countDay; }
            set
            {
                _countDay = value;
                OnPropertyChanged("CountDay");
            }
        }

        private bool _isCompleted;

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnPropertyChanged("IsCompleted");
            }
        }

        private ObservableCollection<ChallengeDayDetail> _challengeDayDetails;
        public ObservableCollection<ChallengeDayDetail> ChallengeDayDetails
        {
            get { return _challengeDayDetails; }
            set
            {
                _challengeDayDetails = value;
                OnPropertyChanged("ChallengeDayDetails");
            }
        }

        #endregion

        public Challenge()
        {
            this.ChallengeDayDetails = new ObservableCollection<ChallengeDayDetail>();
        }


    }
}
