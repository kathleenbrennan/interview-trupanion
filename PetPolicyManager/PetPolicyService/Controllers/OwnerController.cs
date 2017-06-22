using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetPolicyLibrary;
using PetPolicyObjectSchema;

namespace PetPolicyService.Controllers
{
    public class OwnerController : ApiController
    {
        // GET: api/Owner
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Owner/5
        [Route("api/owner/{id}")]
        public string Get(int id)
        {
            return "owner by id";
        }

        // GET: api/Owner/5
        [Route("api/owner/{ownerId}/policy")]
        public List<IPolicyAndOwnerSummary> GetPoliciesByOwner(int ownerId)
        {
            return PetPolicyFactory.GetPolicySummaryListByOwner(ownerId);
        }

        // POST: api/Owner
        [Route("api/owner/{ownerId}/policy/countryCode={countryCode}")]
        public IHttpActionResult Post([FromUri]int ownerId, [FromUri]string countryCode)
        {
            try
            {
                var policy = PetPolicyFactory.Enroll(countryCode, ownerId);
                string location = Request.RequestUri + "/" + ownerId.ToString() + "/policy";
                return Created(location, new {policyNumber = policy.PolicyNumber});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //// PUT: api/Owner/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Owner/5
        //public void Delete(int id)
        //{
        //}
    }
}
