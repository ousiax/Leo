using System.Data.Common;

namespace HoneyLovely.Web
{
    public interface IDbConnectionManager
    {
        Task<DbConnection> OpenAsync();

        DbConnection Open();
    }
}
