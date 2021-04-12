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
using Utility;
using Newtonsoft.Json;
using System.Globalization;

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
                                var result=BaiduAI.Ocr(bytedata);
                                if (result.log_id>0 && result.words_result_num>0)
                                {

                                    return Json(result.words_result.Select(x=>x.words).Aggregate((a,b)=>a+"   "+b));
                                    var moneyIndex = 0;
                                    var timeIndex = 0;
                                    for (int i = 0; i < result.words_result.Length; i++)
                                    {
                                        #region 微信
                                        if (result.words_result[i].words.Contains("当前状态"))
                                        {
                                            moneyIndex = i - 1;

                                            ticket.TicketMoney = decimal.Parse(result.words_result[moneyIndex].words);
                                        }
                                        if (result.words_result[i].words.Contains("时间"))
                                        {
                                            timeIndex = i + 1;
                                            if (DateTime.TryParse(result.words_result[timeIndex].words,out var temp))
                                            {
                                                ticket.TicketDate = temp;
                                            }
                                            else
                                            {
                                                ticket.TicketDate = DateTime.ParseExact(result.words_result[timeIndex].words, "yyyy-MM-ddHH:mm:ss", CultureInfo.CurrentCulture);
                                            }
                                            
                                        }
                                        #endregion

                                        #region 支付宝
                                        if (result.words_result[i].words.Contains("交易成功"))
                                        {
                                            moneyIndex = i - 1;

                                            ticket.TicketMoney = decimal.Parse(result.words_result[moneyIndex].words);
                                        }
                                        if (result.words_result[i].words=="创建时间")
                                        {
                                            timeIndex = i + 1;
                                            if (DateTime.TryParse(result.words_result[timeIndex].words, out var temp))
                                            {
                                                ticket.TicketDate = temp;
                                            }
                                            else
                                            {
                                                ticket.TicketDate = DateTime.ParseExact(result.words_result[timeIndex].words, "yyyy-MM-ddHH:mm", CultureInfo.CurrentCulture);
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                //tesseract未测通
                                //ticket.ReadFromFile(bytedata);
                                //tList.Add(ticket);
                            }
                            
                        }
                    }
                }
            }
            
            return Json(tList);
        }
    }
}
