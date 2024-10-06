namespace Complaint_Portal.Pages;

public partial class NoInternet : ContentPage
{
	public NoInternet()
	{
		InitializeComponent();
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