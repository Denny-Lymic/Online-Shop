using System.Diagnostics;
using BackEnd.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Repositories;

namespace BackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductsRepository _productsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IOrdersRepository _ordersRepository;

        public HomeController(ILogger<HomeController> logger, IProductsRepository productsRepository, IUsersRepository usersRepository, IOrdersRepository ordersRepository)
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
