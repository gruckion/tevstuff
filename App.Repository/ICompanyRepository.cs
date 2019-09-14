using App.Models;

namespace App.Repository
{
	public interface ICompanyRepository : IRepository<Company>
	{
		Company GetById(int id);
	}
}
