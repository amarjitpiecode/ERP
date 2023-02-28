using PieCodeErp.Models;
using PieCodeERP.Repo.Interface;

namespace PieCodeERP.Repo
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(ERPContext Context) : base(Context)
        {
        }
    }
}
