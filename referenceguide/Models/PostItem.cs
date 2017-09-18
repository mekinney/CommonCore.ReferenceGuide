using System;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class PostItem
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

        [EncryptedProperty]
        public string Password { get; set; }

		public int Age { get; set; }
	}
}
