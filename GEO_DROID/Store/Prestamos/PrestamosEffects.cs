using GeoDroid.Data.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Store.Prestamos
{
    public class PrestamosEffects
    {
        private readonly GeoDroidDatabase _database;

        public PrestamosEffects(GeoDroidDatabase database)
        {
            _database = database;

        }
    }
}
