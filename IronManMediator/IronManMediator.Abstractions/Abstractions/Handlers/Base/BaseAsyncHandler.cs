using IronManMediator.Abstractions.Abstractions.Requests;

namespace IronManMediator.Abstractions.Abstractions.Handlers.Base
{
    public abstract class BaseAsyncHandler
    {
        public abstract Task<bool> CanHandle(object request);

        public abstract object InternalHandleHelper(object request);
    }
}
