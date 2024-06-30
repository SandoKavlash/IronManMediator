using IronManMediator.Abstractions.Abstractions;
using IronManMediator.Abstractions.Abstractions.Handlers.Base;
using IronManMediator.Abstractions.Abstractions.Requests;
using IronManMediator.Extensions;
using IronManMediator.Models.Configs;

namespace IronManMediator.Implementations
{
    internal class IronManMediator : IIronManMediator
    {
        private readonly List<BaseHandler> _handlers;
        private readonly List<BaseAsyncHandler> _asyncHandlers;
        public IronManMediator(IServiceProvider serviceProvider, IronManMediatorConfig configuration)
        {
            _handlers = serviceProvider.GetAllHandlers(configuration);
            _asyncHandlers = serviceProvider.GetAllAsyncHandlers(configuration);
        }

        public TResult Send<TResult>(Request<TResult> request)
            where TResult : class
        {
            BaseHandler thisRequestHandler = null;
            foreach(BaseHandler handler in _handlers)
            {
                if(handler.CanHandle(request))
                {
                    thisRequestHandler = handler;
                    break;
                }
            }
            if (thisRequestHandler == null) throw new ArgumentException($"there is not handler for type of request you provided");
            return (TResult)thisRequestHandler.InternalHandleHelper(request);
        }

        public async Task<TResult> SendAsync<TResult>(AsyncRequest<TResult> request)
            where TResult : class
        {
            BaseAsyncHandler thisRequestAsyncHandler = null;
            foreach(BaseAsyncHandler handler in _asyncHandlers)
            {
                if(await handler.CanHandle(request))
                {
                    thisRequestAsyncHandler = handler;
                    break;
                }
            }
            if(thisRequestAsyncHandler == null) throw new ArgumentException($"there is not async handler for type of request you provided");

            return await ((Task<TResult>)thisRequestAsyncHandler.InternalHandleHelper(request));
        }
    }
}
