namespace Leo.Web.Data.Services
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IMemberRepository memberRepository,
            IMemberDetailRepository memberDetailRepository)
        {
            MemberRepository = memberRepository;
            MemberDetailRepository = memberDetailRepository;
        }

        public IMemberRepository MemberRepository { get; }

        public IMemberDetailRepository MemberDetailRepository { get; }
    }
}
