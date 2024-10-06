using Complaint_Portal.MVVM.ViewModel;
using Complaint_Portal.Pages;
using Firebase.Database;

namespace Complaint_Portal
{
    public partial class App : Application
    {
        private readonly MainViewModel mainViewModel;
        private readonly FirebaseClient firebaseClient;
        public App(FirebaseClient firebaseClient, MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.firebaseClient = firebaseClient;
            this.mainViewModel = mainViewModel;

            MainPage = new NavigationPage(new MainPage(firebaseClient, mainViewModel)); 
        }
    }
}
