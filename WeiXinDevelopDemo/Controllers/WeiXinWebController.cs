using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeiXinDevelopDemo.ApiModel;
using WeiXinDevelopDemo.ApiServer;
using WeiXinDevelopDemo.Models;
using WeiXinDevelopDemo.Options;

namespace WeiXinDevelopDemo.Controllers
{
    public class WeiXinWebController : Controller
    {
        private ILogger<WeiXinWebController> _logger;
        private IWeiXinApi _IweiXinApi ;
        private readonly WeiXinDevelopOption _option;
        public WeiXinWebController(ILogger<WeiXinWebController> logger, IWeiXinApi IweiXinApi, IOptionsSnapshot<WeiXinDevelopOption> option)
        {
            _logger = logger;
            _IweiXinApi = IweiXinApi;
            _option = option.Value;
        }
        /// <summary>
        /// 微信官方文档;https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421135319
        /// 验证服务器地址
        /// </summary>
        /// <param name="signature">  微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <returns></returns>
        [HttpGet]
        public string CheckServeUrl(string signature, string timestamp, string nonce, string echostr)
        {
            return _IweiXinApi.CheckServeUrl(signature, timestamp, nonce, echostr);
        }
        /// <summary>
        /// 获取授权码并存储到数据库中
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetAccessToken()
        {
            _logger.LogError("开始");
           string accessToken= HttpContext.Session.GetString("accessToken");
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                _logger.LogError("获取授权码！");
                string appId = "wx272fd0eccf8fb594";
                string secretKey = "5794fed4d5db64f78b0be997d1bb6b99";
                string grant_type = "client_credential";
                var resultStr = _IweiXinApi.GetPlatformAccessToken(grant_type, appId, secretKey);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultStr);
                string token = obj.access_token;
                HttpContext.Session.SetString("accessToken", token);
                return token;
            }
            else
            {
                _logger.LogError("已经获取过授权码");
                return accessToken;
            }

      
        }
        /// <summary>
        /// 获取微信服务器Ip
        /// </summary>
        /// <returns></returns>
        public string GetWxServerIp()
        {
            string accessToken = GetAccessToken();
           string ips= _IweiXinApi.GetCallBackIp(accessToken);
            return ips;
        }
        public void GetCode()
        {
            string appId = "wx272fd0eccf8fb594";
            string redirectUri = "http://4fb9k4.natappfree.cc/WeiXinWeb/RedirectPage";
            string responseType = "code";
            string scope = "snsapi_userinfo";
            string state = "1001";
            string ips = _IweiXinApi.GetCodeAuthorize(appId,redirectUri,responseType,scope,state);
       
        }
        public ActionResult RedirectPage(string code = "",string state="")
        {
            string appId = "wx272fd0eccf8fb594";
            string secretKey = "5794fed4d5db64f78b0be997d1bb6b99";
            //第一步获取Code
          var info=  _IweiXinApi.GetAccessTokenByCode(appId, secretKey, code, "");
            string webToken = HttpContext.Session.GetString("webToken");
            string Openid = HttpContext.Session.GetString("Openid");
            if (string.IsNullOrWhiteSpace(webToken)||string.IsNullOrWhiteSpace(Openid))
            {
                AccessTokenWeb accessTokenWeb = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessTokenWeb>(info);
                if (!string.IsNullOrWhiteSpace(accessTokenWeb.AccessToken))
                {
                    HttpContext.Session.SetString("webToken", accessTokenWeb.AccessToken);

                }
                if (!string.IsNullOrWhiteSpace(accessTokenWeb.Openid))
                {
                    HttpContext.Session.SetString("Openid", accessTokenWeb.Openid);
                }
            }
        return     Redirect("../Home/Index");
        }
        public string GetCustomMenu()
        {
            string token = GetAccessToken();
            var info = _IweiXinApi.GetCustomMenu(token);
            return info;
        }
        public string CreateCustomMenu(string menuInfo)
        {
           
            string token = GetAccessToken();
            var info = _IweiXinApi.CreateCustomMenu(token, menuInfo);
       
            return info;
        }

        public string GetUserInfo()
        {
            string webToken = HttpContext.Session.GetString("webToken");
             webToken = "22_MJBax-CeFWThD9JlSnaZeMaSwv6wGdeSjr6BrR0Q0mfLObMohgSTV2gAU2kvNq4CzUC5C4BNTqvC2AIzXCRekL08jZgFIR5ADXhaMULAPH8";
            string openid = HttpContext.Session.GetString("Openid");
             openid = "oQ9UN6POP4lSv9rDgaEgmggYdHd8";
            var info = _IweiXinApi.GetUserInfo(webToken, openid,"");

            return info;
        }

    }
}
