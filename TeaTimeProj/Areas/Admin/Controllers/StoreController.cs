using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeaTimeProj.DataAccess.Data;
using TeaTimeProj.DataAccess.Repository.IRepository;
using TeaTimeProj.Models;
using TeaTimeProj.Models.ViewModels;
using TeaTimeProj.Utility;

namespace TeaTimeProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class StoreController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhostEnvironment;

        public StoreController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webhostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList();

            return View(objStoreList);
        }

        //Create and update combined into upsert

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //Create 
                return View(new Store());
            }
            else
            {
                //Update 
                Store storeObj = _unitOfWork.Store.Get(u => u.Id == id);

                return View(storeObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Store storeObj)
        {
            if (ModelState.IsValid)
            {
                if (storeObj.Id == 0)
                {
                    _unitOfWork.Store.Add(storeObj);
                    TempData["success"] = "商店資料新增成功!";
                }
                else
                {
                    _unitOfWork.Store.Update(storeObj);
                    TempData["success"] = "商店資料更新成功!";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(storeObj);
            }
        }



        #region API CALLS  

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList();
            return Json(new { data = objStoreList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var storeToBeDeleted = _unitOfWork.Store.Get(u => u.Id == id);

            if (storeToBeDeleted == null)
            {
                return Json(new { success = false, message = "刪除失敗" });
            }

            _unitOfWork.Store.Remove(storeToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }


        #endregion







    }
}
