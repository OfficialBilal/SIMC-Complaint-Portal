using Complaint_Portal.MVVM.ViewModel;
using Firebase.Database;

namespace Complaint_Portal.Pages;

public partial class AdminDashboard : ContentPage
{
    private readonly FirebaseClient firebaseClient;
    private readonly MainViewModel mainViewModel;

    public AdminDashboard(FirebaseClient firebaseClient, MainViewModel mainViewModel)
	{
		InitializeComponent();
        this.firebaseClient = firebaseClient;
        this.mainViewModel = mainViewModel;
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

    private void Submit_Complaint(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new SubmitComplaint(firebaseClient));
    }

    private void Active_Complaints(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new ActiveComplaints());
    }

    private void Solved_Complaints(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new SolvedComplaints());
    }
}