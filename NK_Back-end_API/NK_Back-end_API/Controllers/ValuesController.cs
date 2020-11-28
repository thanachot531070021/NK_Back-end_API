using NK_Back_end_API.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NK_Back_end_API.Controllers
{
    public class ValuesController : ApiController
    {

        private DB_DevEntities _database = new DB_DevEntities();


           [Route("api/accoubt/login")]
            public IHttpActionResult PostLogin()
            {
                return  BadRequest("Login POST Page.");
            }

    // GET api/values
    public IEnumerable<Members> Get()
        //public IEnumerable<string> Get()
        {
            return this._database.Members.ToList();
            //return this._database.Members.Select( m=>m.email).ToList();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
