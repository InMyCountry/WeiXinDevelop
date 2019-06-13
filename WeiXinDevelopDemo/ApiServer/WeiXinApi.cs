using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeiXinDevelopDemo.ApiServer
{
    public class WeiXinApi : IWeiXinApi
    {
        public string CheckServeUrl(string signature, string timestamp, string nonce, string echostr)
        {
            return echostr;
        }
        public string access_token(string grant_type, string appid, string secret)
        {
            string Url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}";
            HttpClient httpClient = new HttpClient();
            string sss = httpClient.GetStringAsync(Url).Result;
            return sss;
        }

        public string getcallbackip(string accessToken)
        {
            string Url = $"https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={accessToken}";
            HttpClient httpClient = new HttpClient();
            string sss = httpClient.GetStringAsync(Url).Result;
            return sss;
        }

        public string authorize(string appid, string redirect_uri, string response_type, string scope, string state)
        {
            string Url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appid}&redirect_uri={redirect_uri}&response_type={response_type}&scope={scope}&state=state{state}#wechat_redirect";
            HttpClient httpClient = new HttpClient();
            string sss = httpClient.GetStringAsync(Url).Result;
            return sss;
        }
    }
}
