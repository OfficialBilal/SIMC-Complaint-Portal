using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Complaint_Portal
{
    public static class FirebaseAuthService
    {
        private static readonly string FirebaseApiKey = "AIzaSyCHCu5ECIyxy7W1ujCwxzNdzU29hnq327I";  // Replace with your actual Firebase API Key

        // Firebase sign-in URL
        private static readonly string SignInUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={FirebaseApiKey}";

        // Sign-in method using Firebase REST API
        public static async Task<bool> SignInWithEmailPassword(string email, string password)
        {
            try
            {
                // Creating the request object for Firebase
                var requestBody = new
                {
                    email = email,
                    password = password,
                    returnSecureToken = true
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Sending HTTP POST request to Firebase
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(SignInUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Success - User authenticated
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var authResult = JsonConvert.DeserializeObject<FirebaseAuthResponse>(responseContent);
                        Console.WriteLine($"User Token: {authResult.IdToken}");
                        return true; // Authentication successful
                    }
                    else
                    {
                        // Handle failed response
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Firebase Error: {errorContent}");
                        return false; // Authentication failed
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle general errors
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }

    // Firebase Auth Response object to handle the response from Firebase
    public class FirebaseAuthResponse
    {
        [JsonProperty("idToken")]
        public string IdToken { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("expiresIn")]
        public string ExpiresIn { get; set; }

        [JsonProperty("localId")]
        public string LocalId { get; set; }
    }
}
