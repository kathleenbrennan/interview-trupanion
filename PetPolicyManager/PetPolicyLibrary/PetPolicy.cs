﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPolicyObjectSchema;
using System.Configuration;

namespace PetPolicyLibrary
{
    public static class PetPolicyFactory
    {

        public static IPetPolicy Enroll(string countryCode, int? ownerId)
        {
            if (String.IsNullOrWhiteSpace(countryCode) || countryCode.Length < 3 || countryCode.Length > 3)
            {
                throw new UnableToCreatePolicyException("A three-letter country code is required to enroll a policy.");
            }
            if (!ownerId.HasValue)
            {
                throw new UnableToCreatePolicyException("A non-null owner ID is required to enroll a policy.");
            }
            return new PetPolicy(countryCode, ownerId.Value);
        }

        public static List<IPolicyAndOwnerSummary> GetPolicyAndOwnerSummaryList()
        {
            return PolicyAndOwnerSummaryList.GetPolicyAndOwnerSummaryList();
        }

        public static List<IPolicyAndOwnerSummary> GetPolicyAndOwnerSummaryListByOwner(int ownerId)
        {
            return PolicyAndOwnerSummaryList.GetPolicyAndOwnerSummaryListByOwner(ownerId);
        }

        public static List<IPolicyAndOwnerSummary> GetPolicyAndOwnerSummaryListById(int policyId)
        {
            return PolicyAndOwnerSummaryList.GetPolicyAndOwnerSummaryListById(policyId);
        }


        public static List<IPolicyAndPetSummary> GetPolicyAndPetSummaryListByPolicyId(int policyId)
        {
            return PolicyAndPetSummaryList.GetPolicyAndPetSummaryList(policyId);
        }

        public static List<IPolicyAndPetSummary> GetPolicyAndPetSummaryListByPolicyIdAndPetId(int policyId, int petId)
        {
            return PolicyAndPetSummaryList.GetPolicyAndPetSummaryList(policyId, petId);
        }


        public static List<IPolicyAndPetSummary> GetPolicyAndPetSummaryListByOwnerId(int ownerId)
        {
            return PolicyAndPetSummaryList.GetPolicyAndPetSummaryListByOwnerId(ownerId);
        }

        public class PetPolicy : IPetPolicy
        {
            public int PolicyId { get; set; }
            public string PolicyNumber { get; set; }

            private PetPolicy()
            {
                //hide constructor so unable to create without initialization 
            }

            internal PetPolicy(string countryCode, int ownerId)
            {
                var provider = DataProviderFactory.GetDataProvider();
                var dto = provider.EnrollPolicy(countryCode, ownerId);
                PolicyId = dto.PolicyId;
                PolicyNumber = dto.PolicyNumber;
            }
        }

        public static int AddPet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth)
        {
            var provider = DataProviderFactory.GetDataProvider();
            var petDto = provider.AddPet(ownerId, petName, speciesId, breedName, petDateOfBirth);
            return petDto.PetId;
        }

        public static DateTime? AddPetToPolicy(int petId, int policyId)
        {
            //todo: check if pet already has a policy and if it does 
            //  return the policy add date
            try
            {
                var provider = DataProviderFactory.GetDataProvider();
                DateTime? addDate = provider.AddPetToPolicy(petId, policyId);
                //todo: return the policy add date
                return addDate;
            }
            catch (Exception ex)
            {
                throw ex;
                //todo: would have better error handling here in a real system
            }
        }

        public static DateTime? RemovePetFromPolicy(int petId, int policyId)
        {
            //todo: return error if this pet is not on this policy
            //todo: return policy remove date if successful
            try
            {
                var provider = DataProviderFactory.GetDataProvider();
                DateTime? removeDate = provider.RemovePetFromPolicy(petId, policyId);
                return removeDate;
            }
            catch (Exception ex)
            {
                throw ex;
                //todo: would have better error handling here in a real system
            }
        }


    }
}
