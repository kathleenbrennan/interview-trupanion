using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PetPolicyClientConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().Wait();

            //PolicyAndOwnerSummary policyAndOwnerSummary = 
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:62792/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            IEnumerable<PolicyAndOwnerSummary> policyAndOwnerSummary = null;

            try
            {

                var url = new Uri("http://localhost:62792/api/owner/3/policy");
                //// Create a new product
                //Product product = new Product { Name = "Gizmo", Price = 100, Category = "Widgets" };

                //var url = await CreateProductAsync(product);
                //Console.WriteLine($"Created at {url}");

                // Get the product
                policyAndOwnerSummary = await GetPolicyAndOwnerSummaryAsync(url.PathAndQuery);
                ShowPolicyAndOwnerSummary(policyAndOwnerSummary);

                //// Update the product
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                //// Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                //// Delete the product
                //var statusCode = await DeleteProductAsync(product.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        static void ShowPolicyAndOwnerSummary(IEnumerable<PolicyAndOwnerSummary> summaries)
        {
            foreach (var summary in summaries)
            {
                Console.WriteLine($"PolicyId: {summary.PolicyId}\tPolicyNumber: {summary.PolicyNumber}");
            }
        }


        static async Task<IEnumerable<PolicyAndOwnerSummary>> GetPolicyAndOwnerSummaryAsync(string path)
        {
            IEnumerable<PolicyAndOwnerSummary> policyAndOwnerSummary = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                policyAndOwnerSummary = await response.Content.ReadAsAsync<IEnumerable<PolicyAndOwnerSummary>>();
            }
            return policyAndOwnerSummary;
        }

    }
}
