using System;
using System.Web.Http;

namespace referenceguide.web.Controllers
{
    public class PostItem
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class SimplePostController : ApiController
    {
		[System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("api/SimplePost/TestPersonPost")]
		public PostItem TestPersonPost(PostItem person)
		{
            person.Id = Guid.NewGuid();
            return person;
		}
    }
}
