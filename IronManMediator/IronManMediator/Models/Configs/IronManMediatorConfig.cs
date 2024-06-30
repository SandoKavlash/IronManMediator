using IronManMediator.Abstractions.Abstractions.Handlers;
using IronManMediator.Abstractions.Abstractions.Handlers.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace IronManMediator.Models.Configs
{
    internal struct GuidTypeCouple
    {
        public Guid Identifier;
        public Type Type;
    }
    public class IronManMediatorConfig
    {
        internal readonly List<GuidTypeCouple> asyncHandlersGuidTypes;
        internal readonly List<GuidTypeCouple> handlersGuidTypes;
        private readonly Type _handlerType;
        private readonly Type _asyncHandlerType;
        private readonly IServiceCollection _services;
        public IronManMediatorConfig(IServiceCollection serviceCollection)
        {
            _handlerType = typeof(BaseHandler);
            _asyncHandlerType = typeof(BaseAsyncHandler);
            asyncHandlersGuidTypes = new List<GuidTypeCouple>();
            handlersGuidTypes = new List<GuidTypeCouple>();
            _services = serviceCollection;
        }
        public IronManMediatorConfig RegisterFromAssemblyContaining<T>()
        {
            Assembly assembly = typeof(T).Assembly;
            IEnumerable<Type> allTypes = assembly
                .GetTypes();

            IEnumerable<Type> handlers = allTypes.Where(t => t.IsSubclassOf(_handlerType));
            foreach (var handler in handlers)
            {
                Guid identifier = Guid.NewGuid();
                _services.AddKeyedSingleton(handler, identifier);
                handlersGuidTypes.Add(new GuidTypeCouple() { Identifier = identifier, Type = handler});
            }

            IEnumerable<Type> asyncHandlers = allTypes.Where(t => t.IsSubclassOf(_asyncHandlerType));
            foreach(var asyncHandler in asyncHandlers)
            {
                Guid identifier = Guid.NewGuid();
                _services.AddKeyedSingleton(asyncHandler, identifier);
                asyncHandlersGuidTypes.Add(new GuidTypeCouple() { Identifier = identifier, Type = asyncHandler});
            }

            return this;
        }
    }
}
