using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Services.SincroService
{
    public class ForeingKeyIdGeo : System.Attribute
    {
        private Type _srcTable;
        private string _matchingField;
        private string _valueField;

        public Type srcTable { get { return _srcTable; } }
        public string matchingField { get { return _matchingField; } }
        public string valueField { get { return _valueField; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="srcTable">Table from wich we will extract data</param>
        /// <param name="matchingField">Field belonging to srcTable that will be compared to the field this attribute is applied</param>
        /// <param name="valueField"> if the field from scrTable == matchingField the field this attribute is applied will be assigned this valueField value</param>
        public ForeingKeyIdGeo(Type srcTable, string matchingField, string valueField)
        {
            _srcTable = srcTable;
            _matchingField = matchingField;
            _valueField = valueField;
        }

        public ForeingKeyIdGeo(Type srcTable)
            : this(srcTable, "idGeo", "id")
        {
        }
    }
}
