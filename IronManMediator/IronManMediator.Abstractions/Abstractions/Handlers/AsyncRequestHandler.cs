using IronManMediator.Abstractions.Abstractions.Handlers.Base;
using IronManMediator.Abstractions.Abstractions.Requests;

namespace IronManMediator.Abstractions.Abstractions.Handlers
{
    public abstract class AsyncRequestHandler<TRequest, TResult> : BaseAsyncHandler
        where TResult : class
        where TRequest : AsyncRequest<TResult>
    {
        protected virtual Task<bool> RequestCanBeHandled(TRequest request)
        {
            return Task.FromResult(true);
        }
        public async override Task<bool> CanHandle(object request) => request is TRequest && await RequestCanBeHandled((TRequest)request);
        public override object InternalHandleHelper(object request)
        {
            return Handle((TRequest)request);
        }
        public abstract Task<TResult> Handle(TRequest request);
    }
}
