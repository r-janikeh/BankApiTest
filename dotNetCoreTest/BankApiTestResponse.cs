using System;
using Newtonsoft.Json;

namespace dotNetCoreTest
{
    public class BankApiTestResponse
    {
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
