using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IBaseRepository : IDisposable
    {
        IDbConnection? ÅbenConnection();
        IDbConnection DbConnection();
        IEnumerable<T> ExecuteStoredProcedure<T>(IDbConnection connection, string procedureNavn, object? parameters = null);
    }
}
