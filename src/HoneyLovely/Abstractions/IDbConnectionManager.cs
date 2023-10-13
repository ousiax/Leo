using System.Data.Common;

namespace HoneyLovely
{
    public interface IDbConnectionManager
    {
        Task<DbConnection> OpenAsync();

        DbConnection Open();
    }
}
