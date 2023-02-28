using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieCodeERP.Repo
{
    public class DepartmentRepository :Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ERPContext Context) : base(Context) { }
    }
}
