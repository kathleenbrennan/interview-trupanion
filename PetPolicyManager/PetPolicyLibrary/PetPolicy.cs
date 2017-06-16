using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public interface IPetPolicy
    {
        string PolicyNumber { get; set; }
    }

    public class PetPolicy : IPetPolicy
    {
        public string PolicyNumber { get; set; }

        private PetPolicy() { }

        //enhancement: make so you have to call this from the factory method
        public PetPolicy(string countryCode)
        {
            PolicyNumber = countryCode;
        }
    }
}
