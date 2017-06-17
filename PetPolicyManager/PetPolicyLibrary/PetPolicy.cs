using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPolicyObjectSchema;
using PetPolicyDataProvider;
using System.Configuration;

namespace PetPolicyLibrary
{

    public static class PetPolicyFactory
    {

        public static IPetPolicy Enroll(string countryCode)
        {
            if(string.IsNullOrWhiteSpace(countryCode) || countryCode.Length < 3 || countryCode.Length > 3 )
            {
                throw new UnableToCreatePolicyException("A three-letter country code is required to enroll a policy");
            }
            return new PetPolicy(countryCode);

        }
    }

    public class PetPolicy : IPetPolicy
    {

        public string PolicyNumber { get; set; }

        private PetPolicy()
        {
            //hide constructor so unable to create without initialization 
        }

        //todo: use dependency injection to determine 
        //  whether or not to use database
        //protected readonly IPetPolicyDataProvider _provider;

        //enhancement: make so you have to call this from the factory method
        public PetPolicy(string countryCode)
        {
            var useDatabaseConfigSetting =
                System.Configuration.ConfigurationManager.AppSettings["useDatabase"];

            //todo: tryParse
            bool useDatabase = Boolean.Parse(useDatabaseConfigSetting);

            IPetPolicyDataProvider provider =
                PetPolicyDataProviderFactory.GetProvider(useDatabase: useDatabase);
            var dto = new PetPolicyDto();
            try
            {
                dto.PolicyNumber = provider.GeneratePolicyNumber(countryCode);
            }
            catch (Exception ex)
            {
                //todo: Exception handling
                throw (ex);
            }

            PolicyNumber = dto.PolicyNumber;
        }
    }
}
