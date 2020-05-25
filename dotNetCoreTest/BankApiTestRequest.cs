using System;
namespace dotNetCoreTest
{
    public class BankApiTestRequest
    {
        public string XAuthHeader { get; set; }
        public string ContentTypeHeader { get; set; }
        public string BankAccount { get; set; }
    }
}
