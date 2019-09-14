namespace App.Services
{
    using App.Models;
    using System;

	public interface ICustomerService
	{
		bool AddCustomer(
			string firstname,
			string surname,
			string email,
			DateTime dateOfBirth,
			int companyId);

		void calculateCustomerAge(Customer customer);
	}
}
