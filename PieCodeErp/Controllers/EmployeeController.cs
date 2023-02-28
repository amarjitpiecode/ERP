using Microsoft.AspNetCore.Mvc;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeErp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeMasterService _EmployeeService;
        public EmployeeController(IEmployeeMasterService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEmployees(DataTableFilterModel filter)
        {
            var list = _EmployeeService.GetEmployeeList(filter);
            return Json(list);
        }
        public ActionResult Edit(int id)
        {
            return View("ManageEmployee", _EmployeeService.GetEmployee(id));
        }


        public ActionResult Create()
        {
            return View("ManageEmployee", new AddEmployeeModel());
        }

        public JsonResult AddOrUpdateEmployee(AddEmployeeModel EmployeeVM)
        {
            return Json(_EmployeeService.AddOrUpdateEmployee(EmployeeVM));
        }
        public IActionResult DeleteEmployee(int id)
        {
            var list = _EmployeeService.DeleteEmployee(id);
            return Json(list);
        }

    }
}
