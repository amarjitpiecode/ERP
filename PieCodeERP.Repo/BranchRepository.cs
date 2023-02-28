using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;

namespace PieCodeERP.Repo
{

    public class BranchRepository : Repository<BranchMaster>, IBranchRepository
    {
        public BranchRepository(ERPContext Context) : base(Context)
        {



        }
    }
}
