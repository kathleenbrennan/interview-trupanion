using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPolicyClientConsoleApp
{
    public class PolicyAndOwnerSummary
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime PolicyEnrollmentDate { get; set; }
        public DateTime? PolicyCancellationDate { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int CountryId { get; set; }
        public string CountryIso3LetterCode { get; set; }

    }
}
