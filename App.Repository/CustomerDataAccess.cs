namespace App.Repository
{
	using App.Models;
    using System;
    using System.Configuration;
	using System.Data;
	using System.Data.SqlClient;

	public static class CustomerDataAccess
	{
		public static void AddCustomer(Customer customer)
		{
			var sqlParameters = new[]
			{
				new SqlParameter("@Firstname", SqlDbType.VarChar, 50) { Value = customer.Firstname },
				new SqlParameter("@Surname", SqlDbType.VarChar, 50) { Value = customer.Surname },
				new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = customer.DateOfBirth },
				new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50) { Value = customer.EmailAddress },
				new SqlParameter("@HasCreditLimit", SqlDbType.Bit) { Value = customer.HasCreditLimit },
				new SqlParameter("@CreditLimit", SqlDbType.Int) { Value = customer.CreditLimit },
				new SqlParameter("@CompanyId", SqlDbType.Int) { Value = customer.Company.Id }
			};

			SqlHelper.Execute<int>("uspAddCustomer", sqlParameters);
		}
	}

	public static class SqlHelper
	{
		public static T Execute<T>(string commandText, params SqlParameter[] sqlParameters)
		{
			var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

			using (var connection = new SqlConnection(connectionString))
			{
				var command = new SqlCommand
				{
					Connection = connection,
					CommandType = CommandType.StoredProcedure,
					CommandText = commandText
				};

				command.Parameters.AddRange(sqlParameters);

				connection.Open();

				if (typeof(T).GetType() == typeof(SqlDataReader))
				{
					return (T) Convert.ChangeType(command.ExecuteReader(CommandBehavior.CloseConnection), typeof(T));
				}
				else
				{
					return (T)Convert.ChangeType(command.ExecuteNonQuery(), typeof(T));
				}

			}
		}
	}
}
