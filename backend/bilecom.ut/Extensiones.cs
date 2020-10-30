using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.ut
{
    public static class Extensiones
    {
        public static T GetData<T>(this IDataReader dr, string field)
        {
            T value = default(T);

            if (!DBNull.Value.Equals(dr[field])) value = (T)dr[field];

            return value;
        }

        public static object GetNullable<T>(this T obj)
        {
            object value = obj;

            if (obj == null) value = DBNull.Value;

            return value;
        }
    }
}
