using Microsoft.Extensions.DependencyInjection;

namespace Leo.Web.Data.Services
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMemberRepository MemberRepository => _serviceProvider.GetRequiredService<IMemberRepository>();

        public IMemberDetailRepository MemberDetailRepository => _serviceProvider.GetRequiredService<IMemberDetailRepository>();
    }
}
