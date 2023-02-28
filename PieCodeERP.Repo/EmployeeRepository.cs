using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;


namespace PieCodeERP.Repo
{
    
    
        public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
        {
            public EmployeeRepository(ERPContext Context) : base(Context) { }
        }
    
}

