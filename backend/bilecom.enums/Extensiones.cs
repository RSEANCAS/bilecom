using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.enums
{
    public static class Extensiones
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }

    public class Enum<T> where T : struct, IConvertible
    {
        public static int Count
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new ArgumentException("T must be an enumerated type");

                return Enum.GetNames(typeof(T)).Length;
            }
        }

        public static ICollection<dynamic> GetCollection()
        {
            ICollection<dynamic> collection = new List<dynamic>();
            Type type = typeof(T);

            string[] names = Enum.GetNames(type);

            foreach (string name in names)
            {
                if (name != null)
                {
                    FieldInfo field = type.GetField(name);
                    if (field != null)
                    {
                        DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                        string descripcion = "";
                        if (attr != null) descripcion = attr.Description;
                        else field.ToString();
                        //else throw new Exception($"El valor {name} no tiene el atributo DescriptionAttribute en el tipo {type}");

                        object fieldValue = Enum.Parse(type, name);

                        dynamic item = new { Value = ((int)fieldValue).ToString(), Text = descripcion };
                        collection.Add(item);
                    }
                }
            }
            return collection;
        }

        public static bool Exists(string name)
        {
            bool exists = false;

            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            string[] nameArray = Enum.GetNames(typeof(T)).Select(x => x.ToUpper()).ToArray();

            exists = nameArray.Contains(name.ToUpper());

            return exists;
        }
    }
}
