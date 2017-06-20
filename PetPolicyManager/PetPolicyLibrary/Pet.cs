using System;
using PetPolicyDataProvider;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{
    public static class PetFactory
    {
        public static IPet AddPet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth)
        {
            return new Pet(ownerId, petName, speciesId, breedName, petDateOfBirth);
        }
    }

    public class Pet : IPet
    {
        public int OwnerId { get; }
        public int PetId { get; }
        public string PetName { get; }
        public int SpeciesId { get; }
        public string BreedName { get; }
        public DateTime PetDateOfBirth { get; }

        public Pet()
        {
            //hide constructor so unable to create without initialization 
        }

        internal Pet(int ownerId, string petName, int speciesId, string breedName, DateTime petDateOfBirth)
        {
            //use dependency injection to determine 
            //  whether or not to use database
            var useDatabaseConfigSetting =
                System.Configuration.ConfigurationManager.AppSettings["useDatabase"];

            bool result;
            var useDatabase =
                Boolean.TryParse(useDatabaseConfigSetting, out result)
                && result;

            IPetPolicyDataProvider provider =
                PetPolicyDataProviderFactory.GetProvider(useDatabase: useDatabase);
            var dto = provider.AddPet(ownerId, petName, speciesId, breedName, petDateOfBirth);
            PetId = dto.PetId;
            OwnerId = dto.OwnerId;
            PetName = dto.PetName;
            BreedName = dto.BreedName;
            SpeciesId = dto.SpeciesId;
            PetDateOfBirth = dto.PetDateOfBirth;

        }


    }


}