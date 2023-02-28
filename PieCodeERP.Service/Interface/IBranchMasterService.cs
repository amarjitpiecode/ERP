using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeERP.Service.Interface
{
    public interface IBranchMasterService
    {
        public DataTableFilterModel GetBranchList(DataTableFilterModel filter);
        public ERPResponseModel AddOrUpdateBranch(AddBranchModel data);
        public ERPResponseModel DeleteBranch(int BranchId);
        public AddBranchModel GetBranch(int BranchId);
    }
}
