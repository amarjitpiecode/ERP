using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeERP.Service.Interface
{

    public interface IRegionMasterService
    {
        public DataTableFilterModel GetRegionList(DataTableFilterModel filter);
        public ERPResponseModel AddOrUpdateRegion(AddRegionModel data);
        public ERPResponseModel DeleteRegion(int RegionId);
        public AddRegionModel GetRegion(int regionId);
    }
}
