namespace App.Repository
{
	using App.Models;
	using System.Configuration;
	using System.Data;
	using System.Data.SqlClient;

	public class CompanyRepository : ICompanyRepository
	{
		public Company GetById(int id)
		{
			Company company = null;
			var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

			var reader = SqlHelper.Execute<SqlDataReader>("uspGetCompanyById", new SqlParameter("@CompanyId", SqlDbType.Int) { Value = id });

			while (reader.Read())
			{
				company = new Company
				{
					Id = int.Parse(reader["CompanyId"].ToString()),
					Name = reader["Name"].ToString(),
					Classification = (Classification)int.Parse(reader["ClassificationId"].ToString())
				};
			}

			return company;
		}
	}
}
