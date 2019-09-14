namespace App.Services.Validator
{
	using App.Models;

	public static class CustomerValidator
	{
		public static bool ValidateCustomer(Customer customer)
		{
			return CustomerValidator.ValidateName(customer.Firstname, customer.Surname)
				&& CustomerValidator.ValidateEmail(customer.EmailAddress)
				&& CustomerValidator.ValidateAdaultAge(customer.Age);
		}

		public static bool ValidateName(string firstname, string surname) =>
			!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(surname);

		public static bool ValidateEmail(string email) =>
			email.Contains("@") && email.Contains(".");

		public static bool ValidateAdaultAge(int age) => age > 21;
	}
}
