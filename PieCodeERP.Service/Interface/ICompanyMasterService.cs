using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;
using System.Collections;

namespace PieCodeERP.Service.Interface
{
    public interface ICompanyMasterService
    {

        public ERPResponseModel AddOrUpdateCompany(AddCompanyModel data);
        public ERPResponseModel DeleteCompany(int CompanyId);
        public AddCompanyModel GetCompany(int id);
        public DataTableFilterModel GetCompanyList(DataTableFilterModel filter);



    }

}