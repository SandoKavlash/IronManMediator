using IronManMediator.Abstractions.Abstractions.Requests;

namespace IronManMediator.Abstractions.Abstractions.Handlers.Base
{
    public abstract class BaseHandler
    {
        public abstract bool CanHandle(object request);
        public abstract object InternalHandleHelper(object request);
    }
}
