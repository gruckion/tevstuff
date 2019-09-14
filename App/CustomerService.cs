namespace App.Services
{
	using App.Models;
	using App.Repository;
    using App.Services.Validator;
    using System;

	public class CustomerService
	{
		private readonly ICompanyRepository CompanyRepository;

		public CustomerService()
		{
			// TODO(Tevin): Setup DI container and parse the instance into customer service
			this.CompanyRepository = new CompanyRepository();
		}

		public bool AddCustomer(
			string firstname,
			string surname,
			string email,
			DateTime dateOfBirth,
			int companyId)
		{
			var company = CompanyRepository.GetById(companyId);
			var customer = new Customer(firstname, surname, dateOfBirth, email, company);

			if (!CustomerValidator.ValidateCustomer(customer))
			{
				return false;
			}

			if (company.Name == "VeryImportantClient")
			{
				// Skip credit check
				customer.HasCreditLimit = false;
			}
			else if (company.Name == "ImportantClient")
			{
				// Do credit check and double credit limit
				customer.HasCreditLimit = true;
				using (var customerCreditService = new CustomerCreditServiceClient())
				{
					var creditLimit = customerCreditService
						.GetCreditLimit(customer.Id, customer.DateOfBirth);
					creditLimit = creditLimit * 2;
					customer.CreditLimit = creditLimit;
				}
			}
			else
			{
				// Do credit check
				customer.HasCreditLimit = true;
				using (var customerCreditService = new CustomerCreditServiceClient())
				{
					var creditLimit = customerCreditService
						.GetCreditLimit(customer.Id, customer.DateOfBirth);
					customer.CreditLimit = creditLimit;
				}
			}

			if (customer.HasCreditLimit && customer.CreditLimit < 500)
			{
				return false;
			}

			CustomerDataAccess.AddCustomer(customer);

			return true;
		}

		private int GetCustomerAge()
		{
			return 1;
		}


	}
}
