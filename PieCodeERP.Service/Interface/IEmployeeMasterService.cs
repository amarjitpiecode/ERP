using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;


namespace PieCodeERP.Service.Interface
{
    public interface IEmployeeMasterService
    {
        public DataTableFilterModel GetEmployeeList(DataTableFilterModel filter);
        public ERPResponseModel AddOrUpdateEmployee(AddEmployeeModel data);
        public ERPResponseModel DeleteEmployee(int EmployeeId);
        public AddEmployeeModel GetEmployee(int EmployeeId);
    }
}
