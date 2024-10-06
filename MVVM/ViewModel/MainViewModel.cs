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
                    complainlist.Add(item.Object);
                }
            }
        }

    }
}