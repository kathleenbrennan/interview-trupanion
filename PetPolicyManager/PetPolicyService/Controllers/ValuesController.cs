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
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<IPetPolicySummary> Get()
        {
            return PetPolicyFactory.GetPolicySummaryList();
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public List<IPetPolicySummary> Get(int id)
        {
            return PetPolicyFactory.GetPolicySummaryListById(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
