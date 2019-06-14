using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXinDevelopDemo.ApiModel
{
    public class AccessTokenWeb
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
        [JsonProperty(PropertyName = "errcode")]
        public string ErrCode { get; set; }
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrMsg { get; set; }
  
    }
}
