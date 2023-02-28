
using AutoMapper;
using PiecodeERP.Data;
using PieCodeERP.ViewModel;

namespace PieCodeERP.Service
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AddBranchModel, BranchMaster>().ReverseMap();
            CreateMap<AddCompanyModel, CompanyMaster>().ReverseMap();
            CreateMap<AddCostCenterModel, CostCenter>().ReverseMap();
            CreateMap<AddDepartmentModel, Department>().ReverseMap();
            CreateMap<AddRegionModel, RegionMaster>().ReverseMap();
        }
    }
}
