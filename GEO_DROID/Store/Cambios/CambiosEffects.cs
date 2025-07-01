using GeoDroid.Data.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Store.Cambios
{
    public class CambiosEffects
    {

        private readonly GeoDroidDatabase _database;

        public CambiosEffects(GeoDroidDatabase database)
        {
            _database = database;
        }

    }
}
