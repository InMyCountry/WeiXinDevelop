using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXinDevelopDemo.ApiServer
{
   public interface IWeiXinApi
    {
        #region 微信公众平台
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
        /// 获取微信公众平台授权码
        /// 用于管理公众号内容
        /// </summary>
        /// <param name="grant_type">client_credential</param>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        string GetPlatformAccessToken(string grant_type, string appid, string secret);
        /// <summary>
        /// 获取微信服务器Ip
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        string GetCallBackIp(string accessToken);
        #endregion

        #region 自定义菜单
        /// <summary>
        /// post
        /// 官方API：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141013
        /// 创建自定义菜单
        /// </summary>
        /// <param name="platformAccessToken">平台授权码</param>
        /// <param name="body">json菜单信息</param>
        /// <returns></returns>
        string CustomMenuCreate(string platformAccessToken,string body);

        /// <summary>
        /// get
        ///  https://api.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN
        ///  获取自定义菜单信息
        /// </summary>
        /// <param name="platformAccessToken"></param>
        /// <returns></returns>
        string GetCustomMenu(string platformAccessToken);
        #endregion
        #region 网页授权
        /// <summary>
        /// 官方API：
        ///  第一步获取code
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="redirect_uri"></param>
        /// <param name="response_type"></param>
        /// <param name="scope"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        string GetCodeAuthorize(string appid, string redirect_uri, string response_type, string scope, string state);
        /// <summary>
        /// 第二部通过code获取网页授权码
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="code"></param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        string GetAccessTokenByCode(string appId, string secret, string code, string grantType);
        #endregion

    }
}
