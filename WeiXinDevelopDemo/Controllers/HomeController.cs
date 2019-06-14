using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeiXinDevelopDemo.Models;
using WeiXinDevelopDemo.Options;

namespace WeiXinDevelopDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeiXinDevelopOption _options;
        public HomeController(IOptionsSnapshot<WeiXinDevelopOption> options)
        {
            _options = options.Value;
        }
        public IActionResult Index()
        {
            ViewData["redirectUrl"] = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx272fd0eccf8fb594&redirect_uri={_options.RedirectUri}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
            return View();
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
