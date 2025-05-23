using SQLite;


namespace GeoDroid.Data.SQL
{
    public class GeoDroidDatabase
    {
        SQLiteAsyncConnection database;

        public GeoDroidDatabase()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }


    }
}
