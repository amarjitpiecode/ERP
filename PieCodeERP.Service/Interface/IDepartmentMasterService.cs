using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;


namespace PieCodeERP.Service.Interface
{
    public interface IDepartmentMasterService
    {

        public DataTableFilterModel GetDepartmentList(DataTableFilterModel filter);
        public ERPResponseModel AddOrUpdateDepartment(AddDepartmentModel data);
        public ERPResponseModel DeleteDepartment(int DepartmentId);
        public AddDepartmentModel GetDepartment(int departmentId);

    }
}
