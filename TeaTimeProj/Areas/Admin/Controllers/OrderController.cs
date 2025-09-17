using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Claims;
using TeaTimeProj.DataAccess.Repository.IRepository;
using TeaTimeProj.Models;
using TeaTimeProj.Models.ViewModels;
using TeaTimeProj.Utility;

namespace TeaTimeProj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM = new OrderVM
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };
            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.Address = OrderVM.OrderHeader.Address;

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["success"] = "訂購人資訊更新成功";
            
            return RedirectToAction(nameof(Details),new { orderId = orderHeaderFromDb.Id });

        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult StartProcessing()
        {
            
            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusInProcess);
            _unitOfWork.Save();

            TempData["success"] = "訂單狀態更新成功";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult OrderReady()
        {

            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusReady);
            _unitOfWork.Save();

            TempData["success"] = "訂單狀態更新成功";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult OrderCompleted()
        {

            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusCompleted);
            _unitOfWork.Save();

            TempData["success"] = "訂單狀態更新成功";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Manager)]
        public IActionResult OrderCanceled()
        {

            _unitOfWork.OrderHeader.UpdateStatus(OrderVM.OrderHeader.Id, SD.StatusCanceled);
            _unitOfWork.Save();

            TempData["success"] = "訂單取消成功";

            return RedirectToAction(nameof(Details), new { orderId = OrderVM.OrderHeader.Id });
        }





        #region API Calls

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            //Get all order infos, including to ApplicationUser who placed order

            IEnumerable<OrderHeader> objOrderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee)||User.IsInRole(SD.Role_Manager))
            {
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                //Get the userId of currently logged in user
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser");
            }


            switch (status)
            {
                case "Pending":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusPending);
                    break;
                case "Processing":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;
                case "Ready":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusReady);
                    break;
                case "Completed":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusCompleted);
                    break;
                case "Canceled":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusCanceled);
                    break;
                default:
                    break;
            }

            return Json(new { data = objOrderHeaders });


        }


        #endregion


    }
}
