using Complaint_Portal.Pages;
using Complaint_Portal;
using Firebase.Database;
using System;
using Complaint_Portal.MVVM.ViewModel;

namespace Complaint_Portal
{
    public partial class MainPage : ContentPage
    {
        private readonly FirebaseClient firebaseClient;
        private readonly MainViewModel mainViewModel;

        public MainPage(FirebaseClient firebaseClient, MainViewModel mainViewModel )
        {
            InitializeComponent();
            this.firebaseClient = firebaseClient;
            this.mainViewModel = mainViewModel;
        }

        private async Task<bool> IsInternetConnectedAsync()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                return current == NetworkAccess.Internet;
            }
            catch
            {
                return false;
            }
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            var isAuthenticated = await FirebaseAuthService.SignInWithEmailPassword(email, password);

            if ((!await IsInternetConnectedAsync())) 
            {
                await Navigation.PushAsync(new NoInternet());
            }

            else if (isAuthenticated && email.EndsWith("@complainer.com"))
            {
               await Navigation.PushAsync(new ComplainerDashboard(firebaseClient, mainViewModel));
            }

            else if (isAuthenticated && email.EndsWith("@solver.com"))
            {
                await Navigation.PushAsync(new SolverDashboard(firebaseClient, mainViewModel));
            }

            else if (isAuthenticated && email.EndsWith("@admin.com"))
            {
                await Navigation.PushAsync(new AdminDashboard(firebaseClient, mainViewModel));
            }
            else
            {
                DisplayAlert("Error", "Enter Correct Email & Password", "OK");
            }
        }
    }
}
