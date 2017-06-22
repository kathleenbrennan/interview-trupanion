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


        // POST: api/Policy
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Policy/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Policy/5
        public void Delete(int id)
        {
        }
    }
}
