using System;
using System.Collections.Generic;
using System.Web.Http;
using PetPolicyLibrary;
using PetPolicyObjectSchema;
using PetPolicyService.Models;

namespace PetPolicyService.Controllers
{
    public class OwnerController : ApiController
    {

        // GET: api/Owner/5
        [Route("api/owner/{ownerId}")]
        public IOwner Get(int ownerId)
        {
            return OwnerFactory.GetOwner(ownerId);
        }

        // GET: api/Owner/5
        [Route("api/owner/{ownerId}/policy")]
        public List<IPolicyAndOwnerSummary> GetPoliciesByOwner(int ownerId)
        {
            return PetPolicyFactory.GetPolicyAndOwnerSummaryListByOwner(ownerId);
        }

        [Route("api/owner/{ownerId}/pets")]
        public List<IPolicyAndPetSummary> GetPoliciesAndPetsByOwner(int ownerId)
        {
            return PetPolicyFactory.GetPolicyAndPetSummaryListByOwnerId(ownerId);
        }

        //PUT: api/Owner/
        public IHttpActionResult Put([FromBody] OwnerModel owner)
        {
            string ownerName = owner.OwnerName;
            string countryCode = owner.CountryCode;

            try
            {
                var ownerDto = OwnerFactory.RegisterOwner(countryCode, ownerName);
                int ownerId = ownerDto.OwnerId;
                owner.OwnerId = ownerId;
                var location = Request.RequestUri + "/" + ownerId;
                return Created(location, owner);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/owner/{ownerId}/policy")]
        public IHttpActionResult Post([FromUri] int ownerId, [FromBody] OwnerModel ownerModel)
        {
            try
            {
                var policy = PetPolicyFactory.Enroll(ownerModel.CountryCode, ownerId);

                string location = Request.RequestUri.AbsoluteUri;
                return Created(location,
                    new PolicyModel {PolicyId = policy.PolicyId, PolicyNumber = policy.PolicyNumber});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/owner/{ownerId}/pets/moveToOwner={newOwnerId}")]
        public IHttpActionResult Post([FromUri] int ownerId, [FromUri] int newOwnerId)
        {
            var owner = OwnerFactory.GetOwner(ownerId);
            try
            {
                owner.MovePetsToNewOwner(newOwnerId);
                string requestUriAbsoluteUri = Request.RequestUri.AbsoluteUri;
                var indexOf = requestUriAbsoluteUri.LastIndexOf("/", StringComparison.Ordinal);
                string location = requestUriAbsoluteUri.Substring(0, indexOf); //remove query
                var updatedLocation = location.Replace(ownerId.ToString(), newOwnerId.ToString());
                //return URI /api/owner/{newOwnerId}/pets
                return Created<object>(updatedLocation,null);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
