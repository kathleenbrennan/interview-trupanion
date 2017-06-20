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
            using (var cmd = new SqlCommand("dbo.spPolicyInsert", _sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // set up the parameters
                cmd.Parameters.Add("@ownerId", SqlDbType.Int);
                cmd.Parameters.Add("@countryIso3LetterCode", SqlDbType.Char, 3);
                cmd.Parameters.Add("@policyNumber", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@ownerId"].Value = ownerId;
                cmd.Parameters["@countryIso3LetterCode"].Value = countryCode;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("Unable to create policy due to an error accessing the data store.", ex);
                }


                var policyNumber = cmd.Parameters["@policyNumber"].Value.ToString();
                return policyNumber;
            }

        }

        public override PetOwnerDto RegisterOwner(string countryCode, string ownerName)
        {
            using (var cmd = new SqlCommand("dbo.spOwnerInsert", _sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // set up the parameters
                cmd.Parameters.Add("@ownerName", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@countryIso3LetterCode", SqlDbType.Char, 3);
                cmd.Parameters.Add("@ownerId", SqlDbType.Int).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@ownerName"].Value = ownerName;
                cmd.Parameters["@countryIso3LetterCode"].Value = countryCode;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("Unable to register owner due to an error accessing the data store.", ex);
                }


                var ownerId = int.Parse(cmd.Parameters["@ownerId"].Value.ToString());
                return new PetOwnerDto
                {
                    OwnerId = ownerId,
                    OwnerName = ownerName
                };
                
            }
        }

        public override PetDto AddPet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth)
        {
            using (var cmd = new SqlCommand("dbo.spPetInsert", _sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // set up the parameters
                cmd.Parameters.Add("@ownerId", SqlDbType.Int);
                cmd.Parameters.Add("@petName", SqlDbType.VarChar, 200);
                cmd.Parameters.Add("@speciesId", SqlDbType.Int);
                cmd.Parameters.Add("@breedName", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@petDateOfBirth", SqlDbType.Date);
                cmd.Parameters.Add("@petId", SqlDbType.Int).Direction = ParameterDirection.Output;

                // set parameter values
                cmd.Parameters["@ownerId"].Value = ownerId;
                cmd.Parameters["@petName"].Value = petName;
                cmd.Parameters["@speciesId"].Value = speciesId;
                cmd.Parameters["@breedName"].Value = breedName;
                cmd.Parameters["@petDateOfBirth"].Value = petDateOfBirth.Date; //chop off any time component since the sql db just stores date


                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException("Unable to add pet due to an error accessing the data store.", ex);
                }


                var petId = int.Parse(cmd.Parameters["@petId"].Value.ToString());
                return new PetDto
                {
                    PetId = petId,
                    OwnerId = ownerId,
                    PetName = petName,
                    SpeciesId = speciesId,
                    BreedName = breedName,
                    PetDateOfBirth = petDateOfBirth

                };

            }
        }
    }
}