using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_UrlChecker.Infrastructure;

namespace App_UrlChecker.Controllers
{
    public class AnotherController:Controller
    {
        public IActionResult About()
        {
            int id = 5; //Expression tree will not consider this variable as constant

            return this.RedirectTo<HomeController>(c => c.Index(id,"Test asp.net"));
        }
    }
}
