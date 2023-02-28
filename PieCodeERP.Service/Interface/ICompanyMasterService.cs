using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;


namespace PieCodeERP.Service.Interface
{
    public interface ICompanyMasterService
    {

        public DataTableFilterModel GetCompanyList(DataTableFilterModel filter);
        public ERPResponseModel AddOrUpdateCompany(AddCompanyModel data);
        public ERPResponseModel DeleteCompany(int CompanyId);
        public AddCompanyModel GetCompany(int id);
    }
}
