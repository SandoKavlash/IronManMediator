using IronManMediator.Abstractions.Abstractions.Handlers;
using IronManMediator.App.Requests;
using IronManMediator.App.Responses;

namespace IronManMediator.App.RequestHandlers
{
    public class TestRequestHandler : AsyncRequestHandler<TestRequest, TestResponse>
    {
        public async override Task<TestResponse> Handle(TestRequest request)
        {
            return new TestResponse() { LastCharacter = request.Message[request.Message.Length - 1] };
        }
    }
}
