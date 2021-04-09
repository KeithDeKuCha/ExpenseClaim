using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Enum;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseClaim.Controllers
{
    public class TicketController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ReadTicket(List<IFormFile> files, int type)
        {
            var tList = new List<TicketModel>();
            if (files!=null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file.Length>0)
                    {
                        var ticket = new TicketModel();
                        if (type == (int)TicketType.ScreenShot)
                        {
                            var bytedata = new byte[file.Length];
                            using(var fs = file.OpenReadStream())
                            {
                                fs.Read(bytedata, 0, bytedata.Length);
                                ticket.ReadFromFile(bytedata);
                                tList.Add(ticket);
                            }
                            
                        }
                    }
                }
            }
            
            return Json(tList);
        }
    }
}
