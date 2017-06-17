using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PetPolicyObjectSchema;

namespace PetPolicyDataProvider
{
    public class PetPolicyDataProviderDatabase : PetPolicyDataProviderAbstract, IDisposable
    {
        private readonly SqlConnection _sqlConnection;

        public PetPolicyDataProviderDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PetPolicyDbLocal"].ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);

            _sqlConnection.Open();
                
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

            //throw new NotImplementedException();

            using (SqlCommand cmd = new SqlCommand("dbo.EnrollPolicy", _sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // set up the parameters
                cmd.Parameters.Add("@petOwnerId", SqlDbType.Int);
                cmd.Parameters.Add("@countryIso3LetterCode", SqlDbType.NChar, 3);
                cmd.Parameters.Add("@policyNumber", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@petOwnerId"].Value = petOwnerId;
                cmd.Parameters["@countryIso3LetterCode"].Value = countryCode;

                cmd.ExecuteNonQuery();

                string policyNumber = cmd.Parameters["@policyNumber"].Value.ToString();
                return policyNumber;
            }

        }
    }
}