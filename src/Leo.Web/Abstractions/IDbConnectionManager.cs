using System.Data.Common;

namespace Leo.Web
{
    public interface IDbConnectionManager
    {
        Task<DbConnection> OpenAsync();

        DbConnection Open();
    }
}
