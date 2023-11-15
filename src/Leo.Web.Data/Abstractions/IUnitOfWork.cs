namespace Leo.Web.Data
{
    public interface IUnitOfWork
    {
        IMemberRepository MemberRepository { get; }

        IMemberDetailRepository MemberDetailRepository { get; }
    }
}
