using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var products = ProductData.GetProducts().OrderBy(p => p.Order).Take(6).ToView();
            ViewBag.Products = products;

            //ControllerContext.HttpContext.Request.RouteValues

            //return Content("Данные из первого контроллера");
            return View();
        }

        public string ConfiguredAction(string id, string value)
        {
            return $"Hello World! {id} - {value}";
        }

        public void Throw(string msg) => throw new ApplicationException(msg);
    }
}
