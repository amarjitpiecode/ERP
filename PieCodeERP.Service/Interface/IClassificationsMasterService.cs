using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeERP.Service.Interface
{
    public interface IClassificationsMasterService
    {
        public DataTableFilterModel GetClassificationsList(DataTableFilterModel filter);
        public ERPResponseModel AddOrUpdateClassifications(AddClassificationsModel data);
        public ERPResponseModel DeleteClassifications(int ClassificationsId);
        public AddClassificationsModel GetClassifications(int ClassificationsId);

    }
}