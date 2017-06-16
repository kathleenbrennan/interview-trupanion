using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetPolicyObjectSchema;
using PetPolicyDataProvider;

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

        private PetPolicy() {
            //hide constructor so unable to create without initialization 
        }

        private readonly IPetPolicyDataProvider _provider = PetPolicyDataProviderFactory.GetProvider(useDatabase: false);

        //enhancement: make so you have to call this from the factory method
        public PetPolicy(string countryCode)
        {

            var dto = new PetPolicyDto();
            try
            {
                dto.PolicyNumberAlphaPortion = countryCode;
                dto.PolicyNumberNumericPortion = _provider.GeneratePolicyNumberIncrement();
            }
            catch (Exception ex)
            {
                //todo: Exception handling
                throw (ex);
            }

            PolicyNumber = string.Concat(dto.PolicyNumberAlphaPortion, dto.PolicyNumberNumericPortion);

        }
    }
}
