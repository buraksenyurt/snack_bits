using Azon.SDK.Components;
using Azon.SDK.Contracts;

namespace Azon.Persistence
{
    public class SqlPersistence
        : IDatabasePersistence
    {
        public string ConnectionString => "data source=Northwind;server=localhost:";

        public Control[] Load()
        {
            throw new NotImplementedException();
        }

        public bool Save(Control[] controls)
        {
            Console.WriteLine("Database saving");
            return true;
        }
    }
}