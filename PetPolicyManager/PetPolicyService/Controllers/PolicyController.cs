using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetPolicyLibrary;
using PetPolicyObjectSchema;
using PetPolicyService.Models;

namespace PetPolicyService.Controllers
{
    public class PolicyController : ApiController
    {
        // GET: api/Policy
        public List<IPolicyAndOwnerSummary> Get()
        {
            return PetPolicyFactory.GetPolicyAndOwnerSummaryList();
        }

        // GET api/Policy/5
        public List<IPolicyAndOwnerSummary> Get(int id)
        {
            return PetPolicyFactory.GetPolicyAndOwnerSummaryListById(id);
        }

        [Route("api/policy/{policyId}/pets")]
        public List<IPolicyAndPetSummary> GetPetsByPolicy(int policyId)
        {
            return PetPolicyFactory.GetPolicyAndPetSummaryListByPolicyId(policyId);
        }

        [Route("api/policy/{policyId}/pets/{petId}")]
        public List<IPolicyAndPetSummary> GetPetByPolicy(int policyId, int petId)
        {
            return PetPolicyFactory.GetPolicyAndPetSummaryListByPolicyIdAndPetId(policyId, petId);
        }


        // POST: api/Policy
        public void Post([FromBody]string value)
        {
        }

        [Route("api/policy/{policyId}/pets")]
        public IHttpActionResult Put([FromUri]int policyId, [FromBody]PetModel pet)
        {
            int speciesId;
            switch (pet.Species)
            {
                case "cat":
                    speciesId = 1;
                    break;
                case "dog":
                    speciesId = 2;
                    break;
                default:
                    return BadRequest("Must select cat or dog.");

            }

            //add the pet to the datastore and then add the pet to the policy
            //in a production system you would have more error checking and transactional behavior around this
            //  so that the system didn't end up in an inconsistent state

            var petId = PetPolicyFactory.AddPet(pet.OwnerId, pet.PetName, speciesId, pet.BreedName, pet.PetDateOfBirth);
            PetPolicyFactory.AddPetToPolicy(petId, policyId);
            string location = Request.RequestUri.ToString();
            return Created(location, new { petId = petId });

        }

        // DELETE: api/Policy/5
        public void Delete(int id)
        {
        }
    }
}
