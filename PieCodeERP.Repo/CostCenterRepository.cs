using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;

namespace PieCodeERP.Repo
{
    public class CostCenterRepository:Repository<CostCenter>, ICostCenterRepository
    {
        public CostCenterRepository(ERPContext context):base(context) { }
    }
}
