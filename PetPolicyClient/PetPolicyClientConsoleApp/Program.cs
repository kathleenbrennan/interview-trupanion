using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PetPolicyClientConsoleApp
{
    internal class Program
    {
        private static readonly HttpClient _client = new HttpClient();
        private static string _countryCode;
        private static string _ownerName;
        private static int _ownerId;
        private static int _policyId;
        private static string _policyNumber;

        private static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            _client.BaseAddress = new Uri("http://localhost:62792/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                if (GetCountryCode())
                {
                    _ownerName = GetOwnerName();
                    var ownerUri = CreateOwner().Result;

                    var policyUri = EnrollPolicyForOwner().Result;
                    var policyAndOwnerSummary = await GetPolicyAndOwnerSummaryAsync(policyUri.PathAndQuery);
                    ShowPolicyAndOwnerSummary(policyAndOwnerSummary);

                    var policyAndPetsUri = AddPetsToPolicy().Result;
                    var policyAndPetsSummary = await GetPolicyAndPetsSummaryAsync(policyAndPetsUri.PathAndQuery);
                    ShowPolicyAndPetsSummary(policyAndPetsSummary);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static bool GetCountryCode()
        {
            string[] validCountryCodes = {"usa", "gbr", "fra", "deu", "jpn"};
            var stringList = string.Join(", ", validCountryCodes);
            Console.WriteLine($"Enter a 3-letter country code from the following list: {stringList}");
            var countryCode = Console.ReadLine();
            if (!validCountryCodes.Contains(countryCode))
            {
                Console.WriteLine($"{countryCode} is not a valid country code");
                return false;
            }
            _countryCode = countryCode;
            return true;
        }

        private static string GetOwnerName()
        {
            Console.WriteLine($"Enter the name of the pet owner.");
            return Console.ReadLine();
        }

        private static async Task<Uri> CreateOwner()
        {
            Console.WriteLine("Creating Owner. Please wait.");
            var path = new Uri(_client.BaseAddress, "/api/owner");
            OwnerModel ownerModel = new OwnerModel
            {
                OwnerName = _ownerName,
                CountryIso3LetterCode = _countryCode
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync(path, ownerModel);
            response.EnsureSuccessStatusCode();
            _ownerId = response.Content.ReadAsAsync<OwnerModel>().Result.OwnerId;
            Console.WriteLine($"Owner Created. Resource at {response.Headers.Location}");
            // Return the URI of the created resource.
            return response.Headers.Location;
        }

        private static async Task<Uri> EnrollPolicyForOwner()
        {
            Console.WriteLine("Creating Policy for owner. Please wait.");
            var path = new Uri(_client.BaseAddress, $"/api/owner/{_ownerId}/policy/countryCode={_countryCode}");
            HttpResponseMessage response = await _client.PutAsync(path,null);
            var policyModel = response.Content.ReadAsAsync<PolicyModel>().Result;
            _policyId = policyModel.PolicyId;
            _policyNumber = policyModel.PolicyNumber;
            Console.WriteLine($"Policy {_policyNumber} Created.  Resource at {response.Headers.Location}");
            // Return the URI of the created resource.
            return response.Headers.Location;
        }

        private static async Task<PolicyAndOwnerSummary> GetPolicyAndOwnerSummaryAsync(string path)
        {
            IEnumerable<PolicyAndOwnerSummary> policyAndOwnerSummary = null;

            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;

            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                policyAndOwnerSummary = await response.Content.ReadAsAsync<IEnumerable<PolicyAndOwnerSummary>>();
            }
            return policyAndOwnerSummary.FirstOrDefault();
        }

        private static async Task<Uri> AddPetsToPolicy()
        {
            Console.WriteLine($"Adding pets to policy #{_policyNumber}");
            Console.WriteLine("How many pets to add to policy?");
            Uri location = null;
            try
            {
                int petCount = Int32.Parse(Console.ReadLine());
                for (int i = 1; i <= petCount; i++)
                {
                    Console.WriteLine($"Enter Pet #{i}");
                    Console.WriteLine("Enter cat or dog.");
                    string species = Console.ReadLine().Trim().ToLower();
                    if (!(species == "cat" || species == "dog"))
                    {
                        throw new ApplicationException();
                    }
                    Console.WriteLine("Enter pet name.");
                    string petName = Console.ReadLine();
                    Console.WriteLine("Enter pet breed name (e.g., Siamese, Labrador Retriever");
                    string breedName = Console.ReadLine();
                    Console.WriteLine("Enter pet date of birth in the format mm/dd/yyyy");
                    DateTime petDateOfBirth = DateTime.Parse(Console.ReadLine());
                    var petModel = new PetModel
                    {
                        OwnerId = _ownerId,
                        PetName = petName,
                        Species = species,
                        BreedName = breedName,
                        PetDateOfBirth = petDateOfBirth
                    };

                    location = await AddPetToPolicy(petModel);
                }
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("No pets to add.");
            }
            catch (ApplicationException appEx)
            {
                Console.WriteLine("Invalid data entered. Stopped adding pets.");
            }
            return location;
        }

        private static async Task<Uri> AddPetToPolicy(PetModel petModel)
        {
            Console.WriteLine("Adding pet. Please wait.");
            var path = new Uri(_client.BaseAddress, $"/api/policy/{_policyId}/pets");
            HttpResponseMessage response = await _client.PutAsJsonAsync(path, petModel);
            int petId = response.Content.ReadAsAsync<PetModel>().Result.PetId;
            var location = response.Headers.Location;
            Console.WriteLine($"Pet {petId} Created.  Resource at {location}");

            return location;
        }

        private static async Task<IEnumerable<PolicyAndPetSummary>> GetPolicyAndPetsSummaryAsync(string path)
        {
            IEnumerable<PolicyAndPetSummary> policyAndPetSummary = null;

            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;

            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                policyAndPetSummary = await response.Content.ReadAsAsync<IEnumerable<PolicyAndPetSummary>>();
            }
            return policyAndPetSummary;
        }

        private static void ShowPolicyAndOwnerSummary(PolicyAndOwnerSummary summary)
        {
                Console.WriteLine($"PolicyId: {summary.PolicyId}\tPolicyNumber: {summary.PolicyNumber}");
        }

        private static void ShowPolicyAndPetsSummary(IEnumerable<PolicyAndPetSummary> policyAndPetsSummary)
        {
            foreach (var summary in policyAndPetsSummary)
            {
                Console.WriteLine($"PolicyId: {summary.PolicyId}" +
                                  $"\tPolicyNumber: {summary.PolicyNumber}" +
                                  $"\tPetId: {summary.PetId}" +
                                  $"\tPetName: {summary.PetName}" +
                                  $"\tSpeciesName: {summary.SpeciesName}" +
                                  $"\tBreedName: {summary.BreedName}" +
                                  $"\tPetDateOfBirth: {summary.PetDateOfBirth.Date}" +
                                  $"\tAddToPolicyDate: {summary.AddToPolicyDate.Date}");

            }
        }
    }
}
