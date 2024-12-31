using System.Net;

namespace Project.BLL.Helper
{
    public class Response<type1>
    {
        public HttpStatusCode Code { get; set; }

        public string Message { get; set; }

        public string Status { get; set; }

        public type1? Data { get; set; }
    }
}
