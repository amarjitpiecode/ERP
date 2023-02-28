using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;


namespace PieCodeERP.Repo
{
    public class CompanyRepository : Repository<CompanyMaster>, ICompanyRepository
    {
        public CompanyRepository(ERPContext Context) : base(Context) { }

    }
}
