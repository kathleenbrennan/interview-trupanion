using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public override List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryList()
        {
            var queryString = "SELECT * from vwPolicyAndOwner";
            return GetPetPolicySummaryDtos(queryString);
        }

        public override List<PolicyAndOwnerSummaryDto> GetPolicyAndOwnerSummaryListByOwner(int ownerId)
        {
            var queryString =
                $"SELECT * from vwPolicyAndOwner WHERE OwnerId = {ownerId}";
            return GetPetPolicySummaryDtos(queryString);
        }

        public override DateTime? AddPetToPolicy(int petId, int policyId)
        {
            using (var cmd = new SqlCommand("dbo.spPetPolicyInsert", _sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@petId", petId);
                cmd.Parameters.AddWithValue("@policyId", policyId);
                cmd.Parameters.Add("@petPolicyAddDate", SqlDbType.Date).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var addDate = DateTime.Parse(cmd.Parameters["@petPolicyAddDate"].Value.ToString());
                return addDate;
            }
        }

        public override DateTime? RemovePetFromPolicy(int petId, int policyId)
        {
            using (var cmd = new SqlCommand("dbo.spPetPolicyUpdateRemovalDate", _sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@petId", petId);
                cmd.Parameters.AddWithValue("@policyId", policyId);
                cmd.Parameters.Add("@petPolicyRemovalDate", SqlDbType.Date).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var removalDate = DateTime.Parse(cmd.Parameters["@petPolicyRemovalDate"].Value.ToString());
                return removalDate;
            }
        }


        public override List<PolicyAndOwnerSummaryDto> GetPetPolicySummaryListById(int policyId)
        {
            var queryString =
                $"SELECT * from vwPolicyAndOwner WHERE PolicyId = {policyId}";
            return GetPetPolicySummaryDtos(queryString);
        }

        private List<PolicyAndOwnerSummaryDto> GetPetPolicySummaryDtos(string queryString)
        {
            var adapter = new SqlDataAdapter(queryString, _sqlConnection);
            var ds = new DataSet();
            adapter.Fill(ds, "PetPolicySummaryList");

            var petPolicySummaryList = ds.Tables[0].AsEnumerable()
                .Select
                (
                    dr =>
                    {
                        var dto = new PolicyAndOwnerSummaryDto
                        {
                            OwnerName = dr.Field<string>("OwnerName"),
                            CountryIso3LetterCode = dr.Field<string>("CountryIso3LetterCode"),
                            CountryId = dr.Field<int>("CountryId"),
                            OwnerId = dr.Field<int>("OwnerId"),
                            PolicyCancellationDate = dr.Field<DateTime?>("PolicyCancellationDate"),
                            PolicyEnrollmentDate = dr.Field<DateTime>("PolicyEnrollmentDate"),
                            PolicyNumber = dr.Field<string>("PolicyNumber"),
                            PolicyId = dr.Field<int>("PolicyId")
                        };
                        return dto;
                    }).ToList();
            return petPolicySummaryList;
        }
    }
}