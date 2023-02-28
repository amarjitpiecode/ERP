using AutoMapper;
using PiecodeERP.Data;
using PieCodeERP.Repo.Interface;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;


namespace PieCodeERP.Service
{
    public class EmployeeService : IEmployeeMasterService
    {
        private IRepository<Employee> _EmployeeRepository;
        private IMapper _mapper;
        public EmployeeService (IRepository<Employee> EmployeeRepository, IMapper mapper)
        {
            _EmployeeRepository = EmployeeRepository;
            _mapper = mapper;
        }

        public ERPResponseModel AddOrUpdateEmployee(AddEmployeeModel data)
        {
            try
            {
                var Employeeresponse = _EmployeeRepository.GetAllAsQuerable().WhereIf(data.EmployeeId > 0, x => x.Id != data.EmployeeId).FirstOrDefault(x => x.IsActive && (x.EmployeeName.ToLower() == data.EmployeeName.ToLower() || x.EmployeeAddress == data.EmployeeAddress.ToLower()||x.EmployeePhone==data.EmployeePhone));
                var response = new ERPResponseModel();

                if (Employeeresponse != null)
                {
                    response.IsSuccess = false;
                    response.Status = 400;
                    response.Errors = new List<ErrorDet>();
                    if (Employeeresponse.EmployeeName.ToLower() == data.EmployeeName.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "EmployeeName", ErrorDescription = "Employee already exist" });
                    }

                    if (Employeeresponse.EmployeeAddress.ToLower() == data.EmployeeAddress.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "EmployeeAddress", ErrorDescription = "Employee Code already exist" });
                    }
                    if (Employeeresponse.EmployeePhone.ToLower() == data.EmployeePhone.ToLower())
                    {
                        response.Errors.Add(new ErrorDet() { ErrorField = "EmployeeAddress", ErrorDescription = "Employee Code already exist" });
                    }
                    return response;
                }

                if (data.EmployeeId == 0)
                {

                    Employee tblEmployee = _mapper.Map<AddEmployeeModel, Employee>(data);
                    tblEmployee.CreationDate = DateTime.Now;
                    tblEmployee.IsActive = true;
                    _EmployeeRepository.Insert(tblEmployee);
                }
                else
                {
                    Employee chk = _EmployeeRepository.GetByPredicate(x => x.Id == data.EmployeeId && x.IsActive);
                    chk = _mapper.Map<AddEmployeeModel, Employee>(data);
                    _EmployeeRepository.Update(chk);

                }
                return new ERPResponseModel() { IsSuccess = true, Message = data.EmployeeId == 0 ? string.Format(Constants.AddedSuccessfully, "Employee") : string.Format(Constants.UpdatedSuccessfully, "Employee") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel() { IsSuccess = false, Message = Constants.GetDetailError };
            }
        }
        public ERPResponseModel DeleteEmployee(int data)
        {
            try
            {
                Employee chk = _EmployeeRepository.GetByPredicate(x => x.Id == data);
                if (chk != null)
                {
                    chk.IsActive = false;
                    _EmployeeRepository.Update(chk);
                    _EmployeeRepository.SaveChanges();
                }

                return new ERPResponseModel { IsSuccess = true, Message = string.Format(Constants.DeletedSuccessfully, "Employee") };
            }
            catch (Exception ex)
            {
                return new ERPResponseModel { IsSuccess = false, Message = ex.Message };
            }
        }
        public DataTableFilterModel GetEmployeeList(DataTableFilterModel filter)
        {
            try
            {


                var data = _EmployeeRepository.GetListByPredicate(x => x.IsActive == true
                                     )
                                     .Select(y => new ListEmployeeModel()
                                     { EmployeeId = y.Id, EmployeeAddress = y.EmployeeAddress, EmployeeName = y.EmployeeName,EmployeePhone=y.EmployeePhone, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).Distinct().OrderByDescending(x => x.EmployeeId).AsEnumerable();




                var totalCount = data.Count();
                if (!string.IsNullOrWhiteSpace(filter.search.value))
                {
                    var searchText = filter.search.value.ToLower();
                    data = data.Where(p => p.EmployeeName.ToLower().Contains(searchText) || p.EmployeePhone.ToLower().Contains(searchText) || p.EmployeePhone.ToLower().Contains(searchText));
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

        public AddEmployeeModel GetEmployee(int Id)
        {
            try
            {
                var Employee = _EmployeeRepository.GetListByPredicate(x => x.IsActive == true && x.Id == Id
                                     )
                                     .Select(y => new ListEmployeeModel()
                                     { EmployeeName = y.EmployeeName, EmployeeAddress = y.EmployeeAddress, EmployeePhone=y.EmployeePhone, IsActive = y.IsActive, CreationDate = y.CreationDate }
                                     ).FirstOrDefault();

                if (Employee != null)
                {
                    AddEmployeeModel obj = new AddEmployeeModel();
                    obj.EmployeeId = Employee.EmployeeId;
                    obj.EmployeeAddress = Employee.EmployeeAddress;
                    obj.EmployeeName = Employee.EmployeeName;
                    obj.EmployeePhone = Employee.EmployeePhone;
                    return obj;
                }
                return new AddEmployeeModel();
            }
            catch (Exception ex)
            {
                return new AddEmployeeModel();
            }

        }


    }
}
