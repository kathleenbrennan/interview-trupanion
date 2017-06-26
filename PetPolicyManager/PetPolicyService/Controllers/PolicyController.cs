using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Adds a pet to the policy
        /// </summary>
        /// <param name="policyId">Unique identifier of the policy.</param>
        /// <param name="petModel">JSON object with the properties of the pet.</param>
        /// <returns>Http result</returns>
        [Route("api/policy/{policyId}/pets")]
        public IHttpActionResult Post([FromUri]int policyId, [FromBody]PetModel petModel)
        {
            int speciesId;
            switch (petModel.Species)
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

            try
            {
                //add the pet to the datastore and then add the pet to the policy
                //in a production system you would have more error checking and transactional behavior around this
                //  so that the system didn't end up in an inconsistent state

                var petId = PetPolicyFactory.AddPet(petModel.OwnerId, petModel.PetName, speciesId, petModel.BreedName,
                    petModel.PetDateOfBirth);
                petModel.PetId = petId;
                PetPolicyFactory.AddPetToPolicy(petId, policyId);
                string location = Request.RequestUri.ToString();
                return Created(location, petModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Remove a pet from the policy
        /// </summary>
        /// <param name="policyId">Unique identifier of the policy.</param>
        /// <param name="petId">JSON object with the properties of the pet.</param>
        /// <returns>Http result</returns>
        [Route("api/policy/{policyId}/pets/{petId}")]
        public IHttpActionResult Delete([FromUri]int policyId, [FromUri]int petId)
        {
            try
            {
                //internally we are setting the pet's status on the policy as removed
                //  but we are not actually deleting the record
                //  but for the user's point of view we tell them the pet was removed
                PetPolicyFactory.RemovePetFromPolicy(petId, policyId);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
