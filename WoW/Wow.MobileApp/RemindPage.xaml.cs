using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Wow.MobileApp.ViewModels;

namespace Wow.MobileApp
{
    public partial class RemindPage : PhoneApplicationPage
    {
        public RemindPage()
        {
            InitializeComponent();
            Loaded += RemindPage_Loaded;
            
        }

        private RemindPageViewModel _viewModel;
        public RemindPageViewModel MyViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new RemindPageViewModel();
                    _viewModel.Init();

                }
                return _viewModel;
            }
        }
        void RemindPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = MyViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }
        }
    }
}