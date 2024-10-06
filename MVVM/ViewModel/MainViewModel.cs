using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Complaint_Portal.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaint_Portal.MVVM.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly FirebaseClient firebaseClient;

        public MainViewModel(FirebaseClient firebaseClient)
        {
            this.firebaseClient = firebaseClient;
        }

        [ObservableProperty]
        ObservableCollection<SIMC> complainlist = new();

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            complainlist.Clear();

            // Constructing the query without passing any parameters, ordering by descending timestamp
            var query = this.firebaseClient
                .Child("SIMC")
                .OrderBy("Timestamp")
                .LimitToLast(50);  // Load last 50 records (you can adjust the limit as per your need)

            // Asynchronously getting the data
            var result = await query.OnceAsync<SIMC>();

            // Firebase returns data in ascending order, so we reverse it for descending order
            var sortedResult = result.Reverse();  // Reverse the result to get descending order

            // Adding items to the list
            foreach (var item in sortedResult)
            {
                if (item.Object != null)
                {
                    item.Object.Date = item.Object.Date;
                    item.Object.Name = item.Object.Name;
                    item.Object.Id = item.Key;
                    complainlist.Add(item.Object);
                }
            }
        }

        [RelayCommand]
        public async Task SolveComplainAsync(SIMC complain)
        {
            if (complain == null || string.IsNullOrEmpty(complain.Id))
                return;

            // Fetch the current complaint from Firebase to avoid overwriting other fields
            var existingComplain = await this.firebaseClient
                .Child("SIMC")
                .Child(complain.Id)
                .OnceSingleAsync<SIMC>();

            if (existingComplain != null)
            {
                // Update only the status field, keeping the rest unchanged
                existingComplain.Status = "Solved";

                // Save the updated complaint back to Firebase
                await this.firebaseClient
                    .Child("SIMC")
                    .Child(complain.Id)
                    .PutAsync(existingComplain);  // Put the entire updated object back to Firebase

                // Optionally, reload the data if needed
                await LoadDataAsync();
            }

        }

        [RelayCommand]
        public async Task ComplainTypeAsync(SIMC complain)
        {
            if (complain == null || string.IsNullOrEmpty(complain.Id))
                return;

            // Fetch the current complaint from Firebase to avoid overwriting other fields
            var existingComplain = await this.firebaseClient
                .Child("SIMC")
                .Child(complain.Id)
                .OnceSingleAsync<SIMC>();

            if (existingComplain != null)
            {
                // Update only the status field, keeping the rest unchanged
                existingComplain.ComplainType = "Urgent";

                // Save the updated complaint back to Firebase
                await this.firebaseClient
                    .Child("SIMC")
                    .Child(complain.Id)
                    .PutAsync(existingComplain);  // Put the entire updated object back to Firebase

                // Optionally, reload the data if needed
                await LoadDataAsync();

            }
        }

    }
}