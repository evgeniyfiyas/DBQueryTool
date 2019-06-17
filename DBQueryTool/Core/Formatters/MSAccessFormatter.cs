using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace DBQueryTool.Core.Formatters
{
    class MSAccessFormatter : IFormatter<DataTable>
    {
        public List<object> Format(DataTable formattable)
        {
            // Transforming datatable to key => value associative array of objects.
            var result = new List<object>();

            foreach (var row in formattable.AsEnumerable())
            {
                var item = new Hashtable();

                for (var i = 0; i < formattable.Columns.Count; i++)
                {
                    item[formattable.Columns[i].ColumnName] = row[i].ToString();
                }

                result.Add(item);
            }

            return result;
        }
    }
}
