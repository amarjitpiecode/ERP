using AutoMapper;
using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeERP.Service
{
    public class CostCenterService: ICostCenterMasterService
    {
        private IRepository<CostCenter> _CostCenterRepository;
        private IMapper _mapper;
        public CostCenterService (IRepository<CostCenter> CostCenterRepository, IMapper mapper)
        {
            _CostCenterRepository = CostCenterRepository;
            _mapper = mapper;
        }

        public ERPResponseModel AddOrUpdateCostCenter(AddCostCenterModel data)
        {
            try
            {
                var CostCenterresponse = _CostCenterRepository.GetAllAsQuerable().WhereIf(data.Id > 0, x => x.Id != data.Id).FirstOrDefault(x => x.IsActive && (x.CostCenterName.ToLower() == data.CostCenterName.ToLower() || x.CostCenterCode == data.CostCenterCode));
                var response = new ERPResponseModel();

                if (CostCenterresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                   
                    if (CostCenterresponse.CostCenterName.ToLower() == data.CostCenterName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "CostCenterName", ErrorDescription = "CostCenter already exist" });
                    }

                    if (CostCenterresponse.CostCenterCode.ToLower() == data.CostCenterCode.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "CostCenterCode", ErrorDescription = "CostCenter Code already exist" });
                    }
                    return response;
                }

                if (data.Id == 0)
                {
                    
                    CostCenter tblCostCenter = _mapper.Map<AddCostCenterModel, CostCenter>(data);
                    tblCostCenter.CreationDate = DateTime.Now;
                    tblCostCenter.IsActive = true;
                    _CostCenterRepository.Insert(tblCostCenter);
                }

                else
                    {
                       CostCenter chk = _CostCenterRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    
                chk = _mapper.Map<AddCostCenterModel, CostCenter>(data);
                    _CostCenterRepository.Update(chk);

            }
                return new ERPResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "Region") : string.Format(Constants.UpdatedSuccessfully, "Region") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }


        public ERPResponseModel DeleteCostCenter(int data)
        {
            try
            {
                CostCenter chk = _CostCenterRepository.GetByPredicate(x => x.Id == data);
                if (chk != null)
                {
                    chk.IsActive = false;
                    _CostCenterRepository.Update(chk);
                    _CostCenterRepository.SaveChanges();
                }

                return new ERPResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "CostCenter") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        public DataTableFilterModel GetCostCenterList(DataTableFilterModel filter)
        {
            try
            {


                var data = _CostCenterRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListCostCenterModel()
                                     { CostCenterId = y.Id, CCCode = y.CostCenterCode, CCName = y.CostCenterName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).Distinct().OrderByDescending(x => x.CostCenterId).AsEnumerable();




                var totalCount = data.Count();
                if (!string.IsNullOrWhiteSpace(filter.search.value))
                {
                    var searchText = filter.search.value.ToLower();
                    data = data.Where(p => p.CCName.ToLower().Contains(searchText) || p.CCCode.ToLower().Contains(searchText));
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

        public AddCostCenterModel GetCostCenter(int Id)
        {
            try
            {
                var CostCenter = _CostCenterRepository.GetListByPredicate(x => x.IsActive == true && x.Id == Id
                                     )
                                     .Select(y => new ListCostCenterModel()
                                     { CCName = y.CostCenterName, CCCode = y.CostCenterCode, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).FirstOrDefault();

                if (CostCenter != null)
                {
                    AddCostCenterModel obj = new AddCostCenterModel();
                    obj.Id = CostCenter.CostCenterId;
                    obj.CostCenterCode = CostCenter.CCCode;
                    obj.CostCenterName = CostCenter.CCName;
                    return obj;
                }
                return new AddCostCenterModel();
            }
            catch (Exception ex)
            {
                return new AddCostCenterModel();
            }

        }


    }
}
