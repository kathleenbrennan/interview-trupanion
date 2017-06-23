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

        public IHttpActionResult Post([FromBody]OwnerModel owner)
        {
            string ownerName = owner.OwnerName;
            string countryCode = owner.CountryIso3LetterCode;

            try
            {
                var ownerDto = OwnerFactory.RegisterOwner(countryCode, ownerName);
                int ownerId = ownerDto.OwnerId;
                owner.OwnerId = ownerId;
                string location = Request.RequestUri + "/" + ownerId.ToString();
                return Created(location, owner);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/owner/{ownerId}/policy/countryCode={countryCode}")]
        public IHttpActionResult Put([FromUri]int ownerId, [FromUri]string countryCode)
        {
            try
            {
                var policy = PetPolicyFactory.Enroll(countryCode, ownerId);
                string location = Request.RequestUri + "/" + ownerId.ToString() + "/policy";
                return Created(location, new {policyId = policy.PolicyId, policyNumber = policy.PolicyNumber});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //// DELETE: api/Owner/5
        //public void Delete(int id)
        //{
        //}
    }
}
