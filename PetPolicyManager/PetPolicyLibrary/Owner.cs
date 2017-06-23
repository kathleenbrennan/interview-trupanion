using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using PetPolicyDataProvider;
using PetPolicyObjectSchema;

namespace PetPolicyLibrary
{

    public static class OwnerFactory
    {
        public static IOwner RegisterOwner(string countryCode, string ownerName)
        {
            return new Owner(countryCode, ownerName);
        }

        public static IOwner GetOwner(int ownerId)
        {
            return new Owner(ownerId);
        }
    }
    public class Owner : IOwner
    {
        public int OwnerId { get; }
        public string OwnerName { get;  }
        public int CountryId { get; }

        private Owner()
        {
            //hide constructor so unable to create without initialization 
        }

        //get owner
        internal Owner(int ownerId)
        {
            var provider = GetProvider();
            PetOwnerDto dto = provider.GetOwnerById(ownerId);
            OwnerId = dto.OwnerId;
            OwnerName = dto.OwnerName;
            CountryId = dto.CountryId;
        }

        //create owner
        internal Owner(string countryCode, string ownerName)
        {
            var provider = GetProvider();
            var dto = new PetOwnerDto();
            dto = provider.RegisterOwner(countryCode, ownerName);

            OwnerId = dto.OwnerId;
            OwnerName = dto.OwnerName;
            CountryId = dto.CountryId;
        }

        private static IPetPolicyDataProvider GetProvider()
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
            return provider;
        }
    }
}
