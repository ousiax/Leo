using System.Data.Common;

namespace Leo.Web.Data
{
    public interface IDbConnectionManager
    {
        Task<DbConnection> OpenAsync();

        DbConnection Open();
    }
}
