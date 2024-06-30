using IronManMediator.Abstractions.Abstractions.Requests;

namespace IronManMediator.Abstractions.Abstractions
{
    public interface IIronManMediator
    {
        TResult Send<TResult>(Request<TResult> request) where TResult : class;
        Task<TResult> SendAsync<TResult>(AsyncRequest<TResult> request) where TResult : class;
    }
}
