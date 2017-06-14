using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPolicyLibrary
{
    public class UnableToCreatePolicyException : ApplicationException
    {
        public UnableToCreatePolicyException(string message) : base(message)
        {
        }
    }
}
