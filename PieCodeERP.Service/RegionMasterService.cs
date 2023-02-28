using AutoMapper;
using PiecodeERP.Data;
using PieCodeErp.Models;
using PieCodeERP.Repo.Interface;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeERP.Service
{
    public class RegionMasterService : IRegionMasterService
    {
        private IRepository<Region> _regionRepository;
        private IMapper _mapper;
        public RegionMasterService(IRepository<Region> regionRepository,IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public ERPResponseModel AddOrUpdateRegion(AddRegionModel data)
        {
            try
            {
                var regionresponse = _regionRepository.GetAllAsQuerable().WhereIf(data.Id>0,x=>x.Id!=data.Id).FirstOrDefault(x => x.IsActive && (x.RegionName.ToLower() == data.RegionName.ToLower() || x.RegionCode == data.RegionCode));
                var response = new ERPResponseModel();

                if (regionresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (regionresponse.RegionName.ToLower() == data.RegionName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "RegionName", ErrorDescription = "Region already exist" });
                    }
                
                    if (regionresponse.RegionCode.ToLower() == data.RegionCode.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "RegionCode", ErrorDescription = "Region Code already exist" });
                    }
                    return response;
                }

                if (data.Id == 0)
                {
                   
                    Region tblRegion = _mapper.Map<AddRegionModel, Region>(data);
                    tblRegion.CreationDate = DateTime.Now;
                    tblRegion.IsActive = true;
                    
                    _regionRepository.Insert(tblRegion);
                }
                else {
                    Region chk = _regionRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    chk = _mapper.Map<AddRegionModel, Region>(data);
                    _regionRepository.Update(chk);

                }
                return new ERPResponseModel() { IsSuccess = true, Message = data.Id==0? string.Format(Constants.AddedSuccessfully, "Region") : string.Format(Constants.UpdatedSuccessfully, "Region") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }
        public ERPResponseModel DeleteRegion(int data)
        {
            try
            {
                Region chk = _regionRepository.GetByPredicate(x => x.Id == data);
                if (chk != null)
                {
                    chk.IsActive = false;
                    _regionRepository.Update(chk);
                    _regionRepository.SaveChanges();
                }

                return new ERPResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Region") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        public DataTableFilterModel GetRegionList(DataTableFilterModel filter)
        {
            try
            {


                var data = _regionRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListRegionModel()
                                     { RegionId = y.Id, RegionCode = y.RegionCode, RegionName = y.RegionName, IsActive = y.IsActive, CreationDate = y.CreationDate}
                                     ).Distinct().OrderByDescending(x => x.RegionId).AsEnumerable();



           
                var totalCount = data.Count();


                var sortColumn = string.Empty;
                var sortColumnDirection = string.Empty;
                if (filter.order != null && filter.order.Count() > 0)
                {
                    if (filter.order.Count() == 1)
                    {
                        sortColumnDirection = filter.order[0].dir;
                        if (filter.columns.Count() >= filter.order[0].column)
                        {
                            sortColumn = filter.columns[filter.order[0].column].data;
                        }
                    }
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    {
                        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)) && data.Count() > 0)
                        {
                            if (sortColumn.Length > 0)
                            {
                                sortColumn = sortColumn.First().ToString().ToUpper() + sortColumn.Substring(1);
                                if (sortColumnDirection == "asc")
                                {
                                    data = data.OrderByDescending(p => p.GetType()
                                            .GetProperty(sortColumn)
                                            .GetValue(p, null)).ToList();
                                }
                                else
                                {
                                    data = data.OrderBy(p => p.GetType()
                                           .GetProperty(sortColumn)
                                           .GetValue(p, null)).ToList();
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(filter.search.value))
                {
                    var searchText = filter.search.value.ToLower();
                    data = data.Where(p => p.RegionName.ToLower().Contains(searchText) || p.RegionCode.ToLower().Contains(searchText));
                }
                var filteredCount = data.Count();
                filter.recordsTotal = totalCount;
                filter.recordsFiltered = filteredCount;
                data = data.ToList();
            
                filter.data = data.Skip(filter.start).Take(filter.length).ToList();

                return filter;
            }
            catch (Exception ex)
            {
                return filter;
            }

        }

        public AddRegionModel GetRegion(int regionId)
        {
            try
            {
                var regions = _regionRepository.GetListByPredicate(x => x.IsActive == true && x.Id ==regionId
                                     )
                                     .Select(y => new ListRegionModel()
                                     { RegionId = y.Id, RegionCode = y.RegionCode, RegionName = y.RegionName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).FirstOrDefault();

                if (regions != null)
                {
                    AddRegionModel obj = new AddRegionModel();
                    obj.Id = regions.RegionId;
                    obj.RegionCode = regions.RegionCode;
                    obj.RegionName = regions.RegionName;
                    return obj;
                }
                return new AddRegionModel();
            }
            catch (Exception ex)
            {
                return new AddRegionModel();
            }

        }


    }
}
