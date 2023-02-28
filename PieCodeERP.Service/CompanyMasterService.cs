using AutoMapper;
using PiecodeERP.Data;
using PieCodeErp.Models;
using PieCodeERP.Repo.Interface;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;
namespace PieCodeERP.Service
{
    public class CompanyMasterService : ICompanyMasterService
    {
        private IRepository<CompanyMaster> _companyRepository;
        private IMapper _mapper;

        public CompanyMasterService(IRepository<CompanyMaster> companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public ERPResponseModel AddOrUpdateCompany(AddCompanyModel data)
        {
            try
            {
                var Companyresponse = _companyRepository.GetAllAsQuerable().WhereIf(data.Id > 0, x => x.Id != data.Id).FirstOrDefault(x => x.IsActive && (x.CompanyName.ToLower() == data.CompanyName.ToLower() || x.CompanyCode == data.CompanyCode));
                var response = new ERPResponseModel();

                if (Companyresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Companyresponse.CompanyName.ToLower() == data.CompanyName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "CompanyName", ErrorDescription = "Company already exist" });
                    }

                    if (Companyresponse.CompanyCode.ToLower() == data.CompanyCode.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "CompanyCode", ErrorDescription = "Company Code already exist" });
                    }
                    return response;
                }

                if (data.Id == 0)
                {

                    CompanyMaster tblCompany = _mapper.Map<AddCompanyModel, CompanyMaster>(data);
                    tblCompany.CreationDate = DateTime.Now;
                    tblCompany.IsActive = true;
                    _companyRepository.Insert(tblCompany);
                }
                else
                {
                    CompanyMaster chk = _companyRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    chk = _mapper.Map<AddCompanyModel, CompanyMaster>(data);
                    _companyRepository.Update(chk);
;

                }
                return new ERPResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "Company") : string.Format(Constants.UpdatedSuccessfully, "Company") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }
        public ERPResponseModel DeleteCompany(int data)
        {
            try
            {
                CompanyMaster chk = _companyRepository.GetByPredicate(x => x.Id == data);
                if (chk != null)
                {
                    chk.IsActive = false;
                    _companyRepository.Update(chk);
                    _companyRepository.SaveChanges();
                }

                return new ERPResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Company") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        public DataTableFilterModel GetCompanyList(DataTableFilterModel filter)
        {
            try
            {


                var data = _companyRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListCompanyModel()
                                     { CompanyId = y.Id,CompanyCode = y.CompanyCode, CompanyName = y.CompanyName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).Distinct().OrderByDescending(x => x.CompanyId).AsEnumerable();




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
                    data = data.Where(p => p.CompanyName.ToLower().Contains(searchText) || p.CompanyCode.ToLower().Contains(searchText));
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

        public AddCompanyModel GetCompany(int id)
        {
            try
            {
                var Company = _companyRepository.GetListByPredicate(x => x.IsActive == true && x.Id == id
                                     )
                                     .Select(y => new ListCompanyModel()
                                     { CompanyId = y.Id, CompanyCode = y.CompanyCode, CompanyName = y.CompanyName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).FirstOrDefault();

                if (Company != null)
                {
                    AddCompanyModel obj = new AddCompanyModel();
                    obj.Id = Company.CompanyId;
                    obj.CompanyCode = Company.CompanyCode;
                    obj.CompanyName = Company.CompanyName;
                    return obj;
                }
                return new AddCompanyModel();
            }
            catch (Exception ex)
            {
                return new AddCompanyModel();
            }

        }


    }
}
