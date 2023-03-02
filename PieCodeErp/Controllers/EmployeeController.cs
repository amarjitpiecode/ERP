using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;
using System.Collections;

namespace PieCodeErp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeMasterService _EmployeeService;
        private readonly ICompanyMasterService _CompanyService;

        public EmployeeController(IEmployeeMasterService EmployeeService, ICompanyMasterService companyService)
        {
            _EmployeeService = EmployeeService;
            _CompanyService = companyService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEmployee(DataTableFilterModel filter)
        {
            var list = _EmployeeService.GetEmployeeList(filter);
            return Json(list);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.CompanyList = new SelectList((IList)_CompanyService.GetAllCopmanies().Data, "Id", "CompanyName");
            return View("ManageEmployee", _EmployeeService.GetEmployee(id));
        }


        public ActionResult Create()
        {
            ViewBag.CompanyList = new SelectList((IList)_CompanyService.GetAllCopmanies().Data, "Id", "CompanyName");
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
