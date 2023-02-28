using AutoMapper;
using PiecodeERP.Data;
using PieCodeErp.Models;
using PieCodeERP.Repo.Interface;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeERP.Service
{
    public class Departmentmasterservice : IDepartmentMasterService
    {
        private IRepository<Department> _departmentRepository;
        private IMapper _mapper;
        public Departmentmasterservice(IRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository=departmentRepository;
            _mapper = mapper;
        }

        public ERPResponseModel AddOrUpdateDepartment(AddDepartmentModel data)
        {
            try
            {
                var Departmentresponse = _departmentRepository.GetAllAsQuerable().WhereIf(data.Id > 0, x => x.Id != data.Id).FirstOrDefault(x => x.IsActive && (x.DepartmentName.ToLower() == data.DepartmentName.ToLower() || x.DepartmentCode == data.DepartmentCode));
                var response = new ERPResponseModel();

                if (Departmentresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Departmentresponse.DepartmentName.ToLower() == data.DepartmentName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "DepartmentName", ErrorDescription = "Department already exist" });
                    }

                    if (Departmentresponse.DepartmentCode.ToLower() == data.DepartmentCode.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "DepartmentCode", ErrorDescription = "Department Code already exist" });
                    }
                    return response;
                }

                if (data.Id == 0)
                {
                   
                     Department tblDepartment = _mapper.Map<AddDepartmentModel, Department>(data);
                    tblDepartment.CreationDate = DateTime.Now;
                    tblDepartment.IsActive = true;
                    _departmentRepository.Insert(tblDepartment);
                }
                else
                {
                    Department chk = _departmentRepository.GetByPredicate(x => x.Id == data.Id && x.IsActive);
                    chk = _mapper.Map<AddDepartmentModel, Department>(data);
                    _departmentRepository.Update(chk);

                }
                return new ERPResponseModel() { IsSuccess = true, Message = data.Id == 0 ? string.Format(Constants.AddedSuccessfully, "Region") : string.Format(Constants.UpdatedSuccessfully, "Region") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }
        public ERPResponseModel DeleteDepartment(int data)
        {
            try
            {
                Department chk = _departmentRepository.GetByPredicate(x => x.Id == data);
                if (chk != null)
                {
                    chk.IsActive = false;
                    _departmentRepository.Update(chk);
                    _departmentRepository.SaveChanges();
                }

                return new ERPResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Department") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        public DataTableFilterModel GetDepartmentList(DataTableFilterModel filter)
        {
            try
            {


                var data = _departmentRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListDepartmentModel()
                                     { DepartmentId = y.Id, DepartmentCode = y.DepartmentCode, DepartmentName = y.DepartmentName, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).Distinct().OrderByDescending(x => x.DepartmentId).AsEnumerable();




                var totalCount = data.Count();
                if (!string.IsNullOrWhiteSpace(filter.search.value))
                {
                    var searchText = filter.search.value.ToLower();
                    data = data.Where(p => p.DepartmentName.ToLower().Contains(searchText) || p.DepartmentCode.ToLower().Contains(searchText));
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

        public AddDepartmentModel GetDepartment(int Id)
        {
            try
            {
                var Department = _departmentRepository.GetListByPredicate(x => x.IsActive == true && x.Id == Id
                                     )
                                     .Select(y => new ListDepartmentModel()
                                     { DepartmentName = y.DepartmentName, DepartmentCode = y.DepartmentCode, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).FirstOrDefault();

                if (Department != null)
                {
                    AddDepartmentModel obj = new AddDepartmentModel();
                    obj.Id = Department.DepartmentId;
                    obj.DepartmentCode = Department.DepartmentCode;
                    obj.DepartmentName = Department.DepartmentName;
                    return obj;
                }
                return new AddDepartmentModel();
            }
            catch (Exception ex)
            {
                return new AddDepartmentModel();
            }

        }


    }
}
