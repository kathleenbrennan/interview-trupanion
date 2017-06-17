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

        public override string GeneratePolicyNumber(string countryCode, int ownerId)
        {
            using (var cmd = new SqlCommand("dbo.EnrollPolicy", _sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // set up the parameters
                cmd.Parameters.Add("@petOwnerId", SqlDbType.Int);
                cmd.Parameters.Add("@countryIso3LetterCode", SqlDbType.Char, 3);
                cmd.Parameters.Add("@policyNumber", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@petOwnerId"].Value = ownerId;
                cmd.Parameters["@countryIso3LetterCode"].Value = countryCode;

                try
                {
                    //todo: update the sproc so that we can catch the error here and raise it to the client
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                var policyNumber = cmd.Parameters["@policyNumber"].Value.ToString();
                return policyNumber;
            }

        }
    }
}