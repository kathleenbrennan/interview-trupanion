using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetPolicyService.Models
{
    public class PetModel
    {
        public int PetId { get; set; }
        public int OwnerId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public string BreedName { get; set; }
        public DateTime PetDateOfBirth { get; set; }
    }
}