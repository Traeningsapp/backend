using System.Data;

namespace Persistence
{
    public interface IBaseRepository : IDisposable
    {
        IDbConnection? ÅbenConnection();
        IDbConnection DbConnection();
        IEnumerable<T> ExecuteStoredProcedure<T>(IDbConnection connection, string procedureNavn, object? parameters = null);
    }
}
