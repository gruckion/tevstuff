namespace App.Models
{
	using System;

	public class Customer
    {
		// This is needed for the unit tests and the fluent builder
		public Customer()
		{

		}

		public Customer(
			string firstname,
			string surname,
			DateTime dateOfBirth,
			string emailAddress,
			Company company)
		{
			Id = (new Random()).Next(1, 10000); // TODO(Tevin): We should index users in the database
			this.Firstname = firstname;
			this.Surname = surname;
			this.DateOfBirth = dateOfBirth;
			this.EmailAddress = emailAddress;
			this.Company = company;
		}

        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Company Company { get; set; }

		public int Age { get; set; }

	}
}