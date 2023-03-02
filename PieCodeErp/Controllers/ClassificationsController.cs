using Microsoft.AspNetCore.Mvc;
using PieCodeERP.Service.Interface;
using PieCodeERP.ViewModel;
using PieCodeERP.ViewModel.Helpers;

namespace PieCodeErp.Controllers
{
    public class ClassificationsController : Controller
    {
        
            private readonly IClassificationsMasterService _ClassificationsService;
            public ClassificationsController(IClassificationsMasterService ClassificationsService)
            {
                _ClassificationsService = ClassificationsService;
            }

            public IActionResult Index()
            {
                return View();
            }
            public JsonResult GetAllClassifications(DataTableFilterModel filter)
            {
                var list = _ClassificationsService.GetClassificationsList(filter);
                return Json(list);
            }
            public ActionResult Edit(int id)
            {
                return View("ManageClassifications", _ClassificationsService.GetClassifications(id));
            }


            public ActionResult Create()
            {
                return View("ManageClassifications", new AddClassificationsModel());
            }

        [HttpPost]
            public JsonResult AddOrUpdateClassifications(AddClassificationsModel ClassificationsVM)
            {
                return Json(_ClassificationsService.AddOrUpdateClassifications(ClassificationsVM));
            }
            public IActionResult DeleteClassifications(int id)
            {
                var list = _ClassificationsService.DeleteClassifications(id);
                return Json(list);
            }

        }
    }
