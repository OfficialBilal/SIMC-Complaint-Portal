using Complaint_Portal.MVVM.ViewModel;
using Firebase.Database;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Complaint_Portal;

public partial class SolverDashboard : ContentPage
{
    private readonly MainViewModel mainViewModel;
    private readonly FirebaseClient firebaseClient;

    public SolverDashboard(FirebaseClient firebaseClient, MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = this.mainViewModel = mainViewModel;
        this.firebaseClient = firebaseClient;
    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await mainViewModel.LoadDataAsync();

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