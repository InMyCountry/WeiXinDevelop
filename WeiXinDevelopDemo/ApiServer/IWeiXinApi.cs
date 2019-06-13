using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXinDevelopDemo.ApiServer
{
   public interface IWeiXinApi
    {
        /// <summary>
        /// 验证服务器
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        string CheckServeUrl(string signature, string timestamp, string nonce, string echostr);
        /// <summary>
        /// 获取授权码
        /// </summary>
        /// <param name="grant_type">client_credential</param>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        string access_token(string grant_type, string appid, string secret);
        /// <summary>
        /// 获取微信服务器Ip
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        string getcallbackip(string accessToken);
        string authorize(string appid,string redirect_uri,string response_type,string scope,string state );
    }
}
