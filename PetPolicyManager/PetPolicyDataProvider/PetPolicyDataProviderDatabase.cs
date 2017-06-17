using System;
using System.Data.SqlClient;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public class PetPolicyDataProviderDatabase : PetPolicyDataProviderAbstract, IDisposable
    {
        private readonly SqlConnection _sqlConnection;

        public PetPolicyDataProviderDatabase()
        {
            _sqlConnection = new SqlConnection(
                @"Server=(localdb)\ProjectsV13;Database=PetPolicySqlDb;Trusted_Connection=true");
            using (_sqlConnection)
            {
                _sqlConnection.Open();
                
            }
        }

        public void Dispose()
        {
            _sqlConnection?.Close();
            _sqlConnection?.Dispose();
        }

        public override string GeneratePolicyNumber(string countryCode)
        {
            //todo: replace owner with real data from the client
            int petOwnerId = 1;

            throw new NotImplementedException();
        }
    }
}