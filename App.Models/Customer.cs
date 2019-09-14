namespace App.Models
{
	using System;

	public class Customer
    {
		public Customer(
			string firstname,
			string surname,
			DateTime dateOfBirth,
			string emailAddress,
			Company company)
		{
			Id = (new Random()).Next(1, 10000); // TODO(Tevin): Change this to actually index users
			this.Firstname = "";
			this.Surname = "";
			this.DateOfBirth = DateTime.Now;
			this.EmailAddress = "";
			this.Company = Company;
		}

        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Company Company { get; set; }

		public int Age
		{
			get
			{
				var now = DateTime.Now;
				int age = now.Year - DateOfBirth.Year;

				if (now.Month < DateOfBirth.Month || (now.Month == DateOfBirth.Month && now.Day < DateOfBirth.Day))
					age--;

				return age;
			}
		}

	}
}