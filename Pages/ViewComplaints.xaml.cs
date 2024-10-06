using Complaint_Portal.MVVM.ViewModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Complaint_Portal;

public partial class ViewComplaints : ContentPage
{
    private readonly MainViewModel mainViewModel;
    public ViewComplaints(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = this.mainViewModel = mainViewModel;
    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        // Start the ActivityIndicator
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        base.OnNavigatedTo(args);

        // Load data asynchronously
        await mainViewModel.LoadDataAsync();

        // Introduce a 10-second delay
        await Task.Delay(8000);  // 10000 milliseconds = 10 seconds

        // Stop the ActivityIndicator after 10 seconds
        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }



}
