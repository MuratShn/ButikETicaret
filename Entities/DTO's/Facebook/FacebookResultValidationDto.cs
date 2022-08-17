using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class FacebookResultValidationDto : IDto
    {
        [JsonPropertyName("data")]
        public FacebookResultDataDto Data { get; set; }
    }

    public class FacebookResultDataDto : IDto
    {

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("app_id")]
        public string App_id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("application")]
        public string Application { get; set; }
        //[JsonPropertyName("data_access_expires_at")]
        //public string Data_access_expires_at { get; set; } //saniye formatında yazılmıştı galiba

        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }

    }

}


//"{\"data\":
//    {\
//"app_id\":\"517652853462350\",
//\"type\":\"USER\",
//\"application\":\"ButikETicaret\",
//\"data_access_expires_at\":1668517192,
//\"expires_at\":1660744800,
//\"is_valid\":true,
//\"scopes\":[\"email\",\"public_profile\"],
//\"user_id\":\"4849181511849889\"}}"
