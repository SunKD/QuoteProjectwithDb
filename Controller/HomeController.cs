using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using DbConnection;


namespace quoting.Controllers
{
    public class HomeController : Controller
    {
        private DbConnector quoteDB;

        public HomeController(){
            quoteDB = new DbConnector();
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("process")]
        public IActionResult Add(string name, string quote){
            System.Console.WriteLine(name);
            System.Console.WriteLine(quote);
            string query = $"INSERT INTO quote (Name, Quote) values('{name}', '{quote}')";
            DbConnector.Execute(query);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("board")]
        public IActionResult Board()
        {
            // List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM quote");
            var allQuotes =  DbConnector.Query("SELECT * FROM quote Order by Created_at desc;");
            ViewBag.allQuote = allQuotes;
            return View("Quotes");
        }
    }
}