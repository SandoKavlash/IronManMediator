using IronManMediator.Abstractions.Abstractions.Requests;
using IronManMediator.App.Responses;

namespace IronManMediator.App.Requests
{
    public class TestRequest : AsyncRequest<TestResponse>
    {
        public string Message { get; set; }
    }
}
