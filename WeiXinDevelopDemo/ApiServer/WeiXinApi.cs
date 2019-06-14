using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WeiXinDevelopDemo.ApiServer
{
    public class WeiXinApi : IWeiXinApi
    {
        public string CheckServeUrl(string signature, string timestamp, string nonce, string echostr)
        {
            return echostr;
        }
        public string GetPlatformAccessToken(string grant_type, string appid, string secret)
        {
            string Url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}";
            HttpClient httpClient = new HttpClient();
            string sss = httpClient.GetStringAsync(Url).Result;
            return sss;
        }

        public string GetCallBackIp(string accessToken)
        {
            string Url = $"https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={accessToken}";
            HttpClient httpClient = new HttpClient();
            string sss = httpClient.GetStringAsync(Url).Result;
            return sss;
        }

        public string CreateCustomMenu(string platformAccessToken, string body)
        {

          var info=  HttpClinetHelp.PostResponse($"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={platformAccessToken}", body);
            return info;
        }

        public string GetCustomMenu(string platformAccessToken)
        {
            HttpClient httpClient = new HttpClient();
          return   httpClient.GetStringAsync($"https://api.weixin.qq.com/cgi-bin/menu/get?access_token={platformAccessToken}").Result;
        }

        public string GetCodeAuthorize(string appid, string redirect_uri, string response_type, string scope, string state)
        {
            string Url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appid}&redirect_uri={redirect_uri}&response_type={response_type}&scope={scope}&state=state{state}#wechat_redirect";
            HttpClient httpClient = new HttpClient();
            string sss = httpClient.GetStringAsync(Url).Result;
            return sss;
        }

        public string GetAccessTokenByCode(string appId, string secret, string code, string grantType)
        {
            string url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={appId}&secret={secret}&code={code}&grant_type=authorization_code";
            HttpClient httpClient = new HttpClient();
            string info = httpClient.GetStringAsync(url).Result;
            return info;
        }

        public string GetUserInfo(string accessToken, string Openid, string lang= "zh_CN")
        {
            string url = $"https://api.weixin.qq.com/sns/userinfo?access_token={accessToken}&openid={Openid}&lang=zh_CN";
           string str= HttpClinetHelp.GetResponse(url);
            return str;
        }
    }
}
