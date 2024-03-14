// MIT License

using Dapper;
using System.Data;

namespace Leo.Web.Data.SQLite.TypeHandlers
{
    public sealed class GuidToStringHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            return value == null ? Guid.Empty : Guid.Parse(value.ToString()!);
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString("D");
        }
    }
}