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
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        base.OnNavigatedTo(args);
        await mainViewModel.LoadDataAsync();
    }



}
