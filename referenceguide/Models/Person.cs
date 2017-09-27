using System;
namespace referenceguide
{
	public class Person
	{
		public string FName { get; set; }
		public string LName { get; set; }

		public string FullName
		{
			get { return $"{FName} {LName}"; }
		}
	}
}
