using System;
using App.Common;
using App.Models;
using App.Repository;
using App.Services;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests
{
	[TestClass]
	public class CustomerTests
	{
		private readonly ICurrentDateTimeProvider currentDateTimeProvider;
		private readonly ICustomerService customerService;

		public CustomerTests()
		{
			this.currentDateTimeProvider = new MockCurrentDateTimeProvider(
				new DateTime(2019, 09, 14, 4, 44, 0));
			var companyRepository = new CompanyRepository();
			this.customerService = new CustomerService(companyRepository, this.currentDateTimeProvider);
		}

		[TestMethod]
		public void ShouldCalculateCorrectAgeForGivenDateOfBirth()
		{
			var customer = Builder<Customer>.CreateNew()
				.With(c => c.DateOfBirth = new DateTime(1990, 03, 27)).Build();

			customerService.calculateCustomerAge(customer);

			var expectedAge = 29;

			Assert.AreEqual(expectedAge, customer.Age);
		}

		[TestMethod]
		public void ShouldThrowExceptionForNullDateOfBirth()
		{
			var customer = Builder<Customer>.CreateNew()
				.With(c => c.DateOfBirth = null).Build();

			Assert.ThrowsException<Exception>(
				() => customerService.calculateCustomerAge(customer));
		}

	}
}
