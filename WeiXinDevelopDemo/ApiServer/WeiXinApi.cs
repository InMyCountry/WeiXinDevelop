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

        public string CustomMenuCreate(string platformAccessToken, string body)
        {
            throw new NotImplementedException();
        }

        public string GetCustomMenu(string platformAccessToken)
        {
            throw new NotImplementedException();
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
    }
}
