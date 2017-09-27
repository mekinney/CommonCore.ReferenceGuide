using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System.Collections.ObjectModel;

namespace referenceguide
{
	public class Name
	{
		public string title { get; set; }
		public string first { get; set; }
		public string last { get; set; }
	}

	public class Location
	{
		public string street { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string postcode { get; set; }
	}

	public class Login
	{
		public string username { get; set; }
		public string password { get; set; }
		public string salt { get; set; }
		public string md5 { get; set; }
		public string sha1 { get; set; }
		public string sha256 { get; set; }
	}

	public class Id
	{
		public string name { get; set; }
		public string value { get; set; }
	}

	public class Picture
	{
		public string large { get; set; }
		public string medium { get; set; }
		public string thumbnail { get; set; }
	}


	public class Result
	{
		public string gender { get; set; }
		public Name name { get; set; }
		public Location location { get; set; }
		public string email { get; set; }
		public Login login { get; set; }
		public string dob { get; set; }
		public string registered { get; set; }
		public string phone { get; set; }
		public string cell { get; set; }
		public Id id { get; set; }
		public Picture picture { get; set; }
		public string nat { get; set; }
	}

	public class Info
	{
		public string seed { get; set; }
		public int results { get; set; }
		public int page { get; set; }
		public string version { get; set; }
	}

	public class RootObject
	{
		public List<Result> results { get; set; }
		public Info info { get; set; }
	}

	public class RandomUser : ObservableObject
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ImageUrl { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Phone { get; set; }

		public string FullName
		{
            get { return $"{FirstName} {LastName}".ToTitleCase(); }
		}

		public FormattedString FullAddress
		{

			get
			{
				var fs = new FormattedString();
				var addr = new Span() { Text = $"{Address} \n" };
				var cityStateZip = new Span() { Text = $"{City}, {State}   {Zip} \n" };
				var ph = new Span() { Text = $"{Phone}" };
				fs.Spans.Add(addr);
				fs.Spans.Add(cityStateZip);
				fs.Spans.Add(ph);
				return fs;
			}
		}
	}

	public static class RootObjectExtension
	{
		public static ObservableCollection<RandomUser> ToRandomUserObservableCollection(this List<Result> list)
		{
			var collection = new ObservableCollection<RandomUser>();
			foreach (var item in list)
			{

				collection.Add(new RandomUser()
				{
					FirstName = item.name.first,
					LastName = item.name.last,
					ImageUrl = item.picture.medium,
					Address = item.location.street,
					City = item.location.city,
					State = item.location.state,
					Zip = item.location.postcode,
					Phone = item.cell
				});
			}
			return collection;
		}

		public static List<RandomUser> ToRandomUserList(this List<Result> list)
		{
			var collection = new List<RandomUser>();
			foreach (var item in list)
			{

				collection.Add(new RandomUser()
				{
					FirstName = item.name.first,
					LastName = item.name.last,
					ImageUrl = item.picture.medium,
					Address = item.location.street,
					City = item.location.city,
					State = item.location.state,
					Zip = item.location.postcode,
					Phone = item.cell
				});
			}
			return collection;
		}
	}
}
