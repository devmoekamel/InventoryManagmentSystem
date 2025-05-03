using Microsoft.AspNetCore.Mvc;

namespace InventoryManagmentSystem.Controllers
{
    public class TestController : Controller
    {

        public TestController()
        {
            
        
        
        } 
        public IActionResult Index()
        {
            return View();
        }
    }
}
