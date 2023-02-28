using AutoMapper;
using PiecodeERP.Data;
using PieCodeErp.Models;
using PieCodeERP.Repo.Interface;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;
namespace PieCodeERP.Service
{
    public class BranchMasterService : IBranchMasterService
    {
        private IRepository<BranchMaster> _BranchRepository;
        private IMapper _mapper;

        public BranchMasterService(IRepository<BranchMaster> BranchRepository, IMapper mapper)
        {
            _BranchRepository = BranchRepository;
            _mapper = mapper;
        }

        public ERPResponseModel AddOrUpdateBranch(AddBranchModel data)
        {
            try
            {
                var Branchresponse = _BranchRepository.GetAllAsQuerable().WhereIf(data.Id > 0, x => x.Id != data.Id).FirstOrDefault(x => x.IsActive && (x.BranchName.ToLower() == data.BranchName.ToLower() || x.BranchCode == data.BranchCode));
                var response = new ERPResponseModel();

                if (Branchresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Branchresponse.BranchName.ToLower() == data.BranchName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "BranchName", ErrorDescription = "Branch already exist" });
                    }

                    if (Branchresponse.BranchCode.ToLower() == data.BranchCode.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "BranchCode", ErrorDescription = "Branch Code already exist" });
                    }
                    return response;
                }

                if (data.Id == 0)
                {
                   BranchMaster tblBranch = _mapper.Map<AddBranchModel, BranchMaster>(data);
                    tblBranch.CreationDate = DateTime.Now;
                    tblBranch.IsActive = true;
                    _BranchRepository.Insert(tblBranch);
                }
                else
                {
                    BranchMaster chk = _BranchRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    chk = _mapper.Map<AddBranchModel, BranchMaster>(data);
                    _BranchRepository.Update(chk);

                }
                return new ERPResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "Branch") : string.Format(Constants.UpdatedSuccessfully, "Branch") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }
        public ERPResponseModel DeleteBranch(int data)
        {
            try
            {
                BranchMaster chk = _BranchRepository.GetByPredicate(x => x.Id == data);
                if (chk != null)
                {
                    chk.IsActive = false;
                    _BranchRepository.Update(chk);
                    _BranchRepository.SaveChanges();
                }

                return new ERPResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Branch") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        public DataTableFilterModel GetBranchList(DataTableFilterModel filter)
        {
            try
            {


                var data = _BranchRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListBranchModel()
                                     { BranchId = y.Id, BranchCode = y.BranchCode, BranchName = y.BranchName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).Distinct().OrderByDescending(x => x.BranchId).AsEnumerable();




                var totalCount = data.Count();
                if (!string.IsNullOrWhiteSpace(filter.search.value))
                {
                    var searchText = filter.search.value.ToLower();
                    data = data.Where(p => p.BranchName.ToLower().Contains(searchText) || p.BranchCode.ToLower().Contains(searchText));
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

        public AddBranchModel GetBranch(int BranchId)
        {
            try
            {
                var Branchs = _BranchRepository.GetListByPredicate(x => x.IsActive == true && x.Id == BranchId
                                     )
                                     .Select(y => new ListBranchModel()
                                     { BranchId = y.Id, BranchCode = y.BranchCode, BranchName = y.BranchName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).FirstOrDefault();

                if (Branchs != null)
                {
                    AddBranchModel obj = new AddBranchModel();
                    obj.Id = Branchs.BranchId;
                    obj.BranchCode = Branchs.BranchCode;
                    obj.BranchName = Branchs.BranchName;
                    return obj;
                }
                return new AddBranchModel();
            }
            catch (Exception ex)
            {
                return new AddBranchModel();
            }

        }


    }
}
