using GeoDroid.Data.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Store.Recaudacion
{
    public class RecaudacionEffect
    {

        private readonly GeoDroidDatabase _database;

        public RecaudacionEffect(GeoDroidDatabase database)
        {
            _database = database;
        }
    }
}
