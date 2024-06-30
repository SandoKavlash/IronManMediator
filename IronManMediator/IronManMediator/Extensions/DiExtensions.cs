using IronManMediator.Abstractions.Abstractions;
using IronManMediator.Abstractions.Abstractions.Handlers.Base;
using IronManMediator.Models.Configs;
using Microsoft.Extensions.DependencyInjection;

namespace IronManMediator.Extensions
{
    public static class DiExtensions
    {
        private static object handlersListLock = new object();
        private static List<BaseHandler> handlers;

        private static object asyncHandlersListLock = new object();
        private static List<BaseAsyncHandler> asyncHandlers;
        public static IServiceCollection AddIronManMediator(this IServiceCollection services, Action<IronManMediatorConfig> configAction)
        {
            services.AddSingleton<IIronManMediator, Implementations.IronManMediator>();
            IronManMediatorConfig configuration = new IronManMediatorConfig(services);
            configAction(configuration);
            services.AddSingleton<IronManMediatorConfig>(configuration);
            return services;
        }
        internal static List<BaseHandler> GetAllHandlers(this IServiceProvider serviceProvider, IronManMediatorConfig config)
        {
            lock(handlersListLock)
            {
                if(handlers == null)
                {
                    handlers = new List<BaseHandler>();
                    foreach(var handlerGuidType in config.handlersGuidTypes)
                    {
                        handlers.Add((BaseHandler)serviceProvider.GetRequiredKeyedService(handlerGuidType.Type,handlerGuidType.Identifier));
                    }
                }
                return handlers;
            }
        }

        internal static List<BaseAsyncHandler> GetAllAsyncHandlers(this IServiceProvider serviceProvider, IronManMediatorConfig config)
        {
            lock(asyncHandlersListLock)
            {
                if(asyncHandlers == null)
                {
                    asyncHandlers = new List<BaseAsyncHandler>();
                    foreach(var asyncHandlerGuidType in config.asyncHandlersGuidTypes)
                    {
                        asyncHandlers.Add((BaseAsyncHandler)serviceProvider.GetRequiredKeyedService(asyncHandlerGuidType.Type, asyncHandlerGuidType.Identifier));
                    }
                }
                return asyncHandlers;
            }
        }
    }
}
