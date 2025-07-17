using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Repositories;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductsRepository _productsRepository;
        private readonly UsersRepository _usersRepository;
        private readonly OrdersRepository _ordersRepository;

        public HomeController(ILogger<HomeController> logger, ProductsRepository productsRepository, UsersRepository usersRepository, OrdersRepository ordersRepository)
        {
            _logger = logger;
            _productsRepository = productsRepository;
            _usersRepository = usersRepository;
            _ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
