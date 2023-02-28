using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;


namespace PieCodeERP.Service.Interface
{
    public interface ICostCenterMasterService
    {
        public DataTableFilterModel GetCostCenterList(DataTableFilterModel filter);
        public ERPResponseModel AddOrUpdateCostCenter(AddCostCenterModel data);
        public ERPResponseModel DeleteCostCenter(int CostCenterId);
        public AddCostCenterModel GetCostCenter(int CostCenterId);

    }
}
