using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPolicyLibrary
{

    public static class PetPolicyFactory
    {

        public static PetPolicy Enroll(string countryCode)
        {
            if(String.IsNullOrWhiteSpace(countryCode) || countryCode.Length < 3 || countryCode.Length > 3 )
            {
                throw new UnableToCreatePolicyException("A three-letter country code is required to enroll a policy");
            }
            return new PetPolicy(countryCode);

        }

    }


    public class PetPolicy
    {
        private string _policyNumber = String.Empty;

        public string PolicyNumber { get => _policyNumber; set => _policyNumber = value; }

        protected PetPolicy() { }

        //todo: make so you have to call this from the factory method
        public PetPolicy(string countryCode)
        {
            _policyNumber = countryCode;
        }
    }
}
