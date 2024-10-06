using Complaint_Portal.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Timers;
using UraniumUI;

namespace Complaint_Portal.Pages;

public partial class SubmitComplaint : ContentPage
{
    private readonly FirebaseClient firebaseClient;

    public SubmitComplaint(FirebaseClient firebaseClient)
	{
		InitializeComponent();
        this.firebaseClient = firebaseClient;

        CurrentDate.Text = DateTime.Today.ToString("dd - MMM - yyyy");
        CurrentTime.Text = DateTime.Now.ToString("hh:mm tt");


        string[] departments = new string[]
        {
            "Oral Biology", "Oral Pathology", "Dental Materials", "Community Dentistry", "Periodontology",
            "Operative Dentistry", "Oral & Maxillofacial Surgery", "Orthodontics", "Prosthodontics", "Oral Medicine",
            "HR Office Dental", "Principal Office Dental", "Reception Dental", "Radiology Dental", "Director Office",
            "Accounts Office", "Accounts Hospital", "HR Medical", "Student Affairs", "Principal Medical",
            "DME Office", "Principal DPT", "Principal Pharm-D", "Biochemistry", "Physiology", "Anatomy",
            "Pathology", "Forensic Medicine", "Community Medicine", "Pharmacology", "MS Office Medical",
            "MS Office Dental", "Pathology Lab", "Reception Medical", "Reception Hospital Matran Office",
            "Principal Nursing", "Nursing College", "Pharmacy College", "DPT College", "Paramedical College",
            "Boys Hostel", "Girls Hostel", "Faculty Apartment", "Security", "Administrator Office",
            "Electrician Department", "Carpenter Department", "Plumber Department", "Medicine", "Surgery",
            "Gynecology & Obstetrics", "Pediatric", "Ophthalmology", "ENT", "Orthopedic", "Urology",
            "Neurosurgery", "Plastic Surgery", "Cardiac Surgery", "Dermatology", "Psychiatry", "Cardiology",
            "Nephrology", "Gastroenterology", "Pulmonology", "Anesthesia", "Radiology", "Physiotherapy",
            "Accident & Emergency", "State of Art Endoscopy & ERCP", "Angiography Suit", "Dialysis Center",
            "Liver Transplant Facility", "Renal Transplant Facility"
        };

        Array.Sort(departments);

        foreach (var department in departments)
        {
            DepartmentPicker.Items.Add(department);
        }

        DepartmentPicker.SelectedItem = "";


        BlockPicker.Items.Add("Medical College");
        BlockPicker.Items.Add("Dental College");
        BlockPicker.Items.Add("Nursing College");
        BlockPicker.Items.Add("Hospital");
        BlockPicker.Items.Add("Girls Hostel");
        BlockPicker.Items.Add("Boys Hostel");
        BlockPicker.Items.Add("Faculty");
        BlockPicker.Items.Add("Other");

        BlockPicker.SelectedItem = "";


        WorkRelated.Items.Add("Carpanter");
        WorkRelated.Items.Add("Colour Painter");
        WorkRelated.Items.Add("Electrician");
        WorkRelated.Items.Add("Gardner (Mali)");
        WorkRelated.Items.Add("Information Technology (IT)");
        WorkRelated.Items.Add("Masson (Mistri)");
        WorkRelated.Items.Add("Plumber");
        WorkRelated.Items.Add("Security");
        
        WorkRelated.SelectedItem = "";

    }

    private void OnDepartmentSelection(object sender, EventArgs e)
    {
        if (DepartmentPicker.SelectedIndex != -1)
        {
            string selectedItem = DepartmentPicker.Items[DepartmentPicker.SelectedIndex];
        }
    }

    private void OnBlockSelection(object sender, EventArgs e)
    {
        if (BlockPicker.SelectedIndex != -1)
        {
            string selectedItem = BlockPicker.Items[BlockPicker.SelectedIndex];
        }
    }

    private void OnWorkSelection(object sender, EventArgs e)
    {
        if (DepartmentPicker.SelectedIndex == -1)
        {
            DisplayAlert("Department/Block not selected", "Please select department/block first.", "OK");
        }
        else
        {
            string selectedItem = WorkRelated.Items[WorkRelated.SelectedIndex];
        }

    }

    // Event handler for the Submit Button
    private void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        // Get the entered details
        string name = NameEntry.Text;
        string phone = PhoneEntry.Text;
        string complaintDetails = ComplaintEditor.Text;
        string Department = DepartmentPicker.SelectedItem?.ToString();
        string Block = BlockPicker.SelectedItem?.ToString();
        string Work = WorkRelated.SelectedItem?.ToString();



        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(Department) || string.IsNullOrEmpty(Block) || string.IsNullOrEmpty(Work) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(complaintDetails))
           {
            DisplayAlert("Error", "Please fill out all fields.", "OK");
            return;

        }
        else if (!name.All(char.IsLetter))
        {
            DisplayAlert("Name Error", "Nume should only contain letters", "OK");
        }
        else if (!phone.StartsWith("03"))
        {
            DisplayAlert("Number Error", "Phone number must start with 03", "OK");
        }
        else if (phone.Length != 11)
        {
            DisplayAlert("Number Error", "Phone number must contain 11 digits.", "OK");
        }
        else
        {
            this.firebaseClient.Child("SIMC").PostAsync(new SIMC
            {
                Date = CurrentDate.Text,
                Time = CurrentTime.Text,
                Department = (DepartmentPicker.SelectedItem?.ToString() + " Department"),
                Name = NameEntry.Text,
                PhoneNumber = PhoneEntry.Text,
                WorkRelated = WorkRelated.SelectedItem?.ToString(),
                Block = BlockPicker.SelectedItem?.ToString(),
                ComplaintDetails = ComplaintEditor.Text,


            });
                NameEntry.Text = string.Empty;
                PhoneEntry.Text = string.Empty;
                ComplaintEditor.Text = string.Empty;
                DisplayAlert("Success", "Your Complaint Submited Succcessfully.", "OK");
        }
    }

    private void ShowPopup(object sender, EventArgs e)
    {
        Overlay.IsVisible = true;
        MyFrame.IsVisible = true;
    }

    private void ClosePopup_Clicked(object sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        MyFrame.IsVisible = false;
    }

    private async void OpenNewPage_Clicked(object sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        MyFrame.IsVisible = false;

        await Navigation.PushAsync(new Page());
    }
}





