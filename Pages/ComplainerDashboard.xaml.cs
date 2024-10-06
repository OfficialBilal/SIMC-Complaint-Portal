using Complaint_Portal.MVVM.ViewModel;
using Firebase.Database;
using Microsoft.Maui.Controls;

namespace Complaint_Portal.Pages;

public partial class ComplainerDashboard : ContentPage
{
    private readonly MainViewModel mainViewModel;
    private readonly FirebaseClient firebaseClient;
    public ComplainerDashboard(FirebaseClient firebaseClient, MainViewModel mainViewModel)
	{
		InitializeComponent();
        this.firebaseClient = firebaseClient;
        this.mainViewModel = mainViewModel;
    }

    private async void Submit_Complaint(object sender, TappedEventArgs e)
    {
        if (!await IsInternetConnectedAsync())
        {
            // Navigate to the "No Internet" page
            await Navigation.PushAsync(new NoInternet());
        }
        else if (await IsInternetConnectedAsync())
        {
            Navigation.PushAsync(new SubmitComplaint(firebaseClient));
        }
    }

    private async void View_My_Complaint(object sender, TappedEventArgs e)
    {
        if (!await IsInternetConnectedAsync())
        {
            // Navigate to the "No Internet" page
            await Navigation.PushAsync(new NoInternet());
        }
        else if (await IsInternetConnectedAsync())
        {
            Navigation.PushAsync(new ViewComplaints(mainViewModel));
        }
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

    protected override bool OnBackButtonPressed()
    {
        CloseApplication();
        return true;
    }

    private void CloseApplication()
    {
        // Close the app
        Application.Current.Quit();
    }
}