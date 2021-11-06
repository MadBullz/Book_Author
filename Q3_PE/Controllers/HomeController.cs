using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Q3_PE.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Q3_PE.Controllers
{
    public class HomeController : Controller
    {
        PRN292_Sum2020_B5Context context = new PRN292_Sum2020_B5Context();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }




        public IActionResult Index()
        {
            return View(context.Books.ToList());
        }

        public IActionResult Delete(int id)
        {

            var book = context.Books.Where(b => b.Id == id).FirstOrDefault();
            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
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
