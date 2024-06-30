using IronManMediator.Abstractions.Abstractions.Handlers.Base;
using IronManMediator.Abstractions.Abstractions.Requests;

namespace IronManMediator.Abstractions.Abstractions.Handlers
{
    public abstract class RequestHandler<TRequest, TResult> : BaseHandler
        where TResult : class
        where TRequest : Request<TResult>
    {
        protected virtual bool RequestCanBeHandled(TRequest request)
        {
            return true;
        }
        public override bool CanHandle(object request) => request is TRequest && RequestCanBeHandled((TRequest)request);
        public override object InternalHandleHelper(object request)
        {
            return Handle((TRequest)request);
        }
        public abstract TResult Handle(TRequest request);
    }
}
