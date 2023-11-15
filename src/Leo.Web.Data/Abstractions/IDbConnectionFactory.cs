using System.Data.Common;

namespace Leo.Web.Data
{
    public interface IDbConnectionFactory
    {
        Task<DbConnection> OpenAsync();

        DbConnection Open();
    }
}
