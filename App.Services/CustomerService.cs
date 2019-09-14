namespace App.Services
{
    using App.Common;
    using App.Models;
	using App.Repository;
    using App.Services.Validator;
    using System;

	public class CustomerService : ICustomerService
	{
		private readonly ICompanyRepository companyRepository;

		private readonly ICurrentDateTimeProvider currentDateTimeProvider;

		public CustomerService(
			CompanyRepository companyRepository,
			ICurrentDateTimeProvider currentDateTimeProvider)
		{
			// TODO(Tevin): Setup DI container and parse the instance into customer service
			this.companyRepository = companyRepository ?? new CompanyRepository();
			this.currentDateTimeProvider = currentDateTimeProvider ?? new CurrentDateTimeProvider();

		}

		public bool AddCustomer(
			string firstname,
			string surname,
			string email,
			DateTime dateOfBirth,
			int companyId)
		{
			var company = companyRepository.GetById(companyId);
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
						.GetCreditLimit(customer.Id, customer.DateOfBirth.Value);
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
						.GetCreditLimit(customer.Id, customer.DateOfBirth.Value);
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

		// TODO(Tevin): Consider placing this logic somewhere else
		public void calculateCustomerAge(Customer customer)
		{
			if (customer.DateOfBirth == null)
			{
				throw new InvalidOperationException("Customer must have a date of birth");
			}

			var dateOfBirth = customer.DateOfBirth.Value;

			var now = this.currentDateTimeProvider.Now();
			int age = now.Year - dateOfBirth.Year;

			if (now.Month < dateOfBirth.Month
				|| (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
				age--;

			customer.Age = age;
		}
	}
}
