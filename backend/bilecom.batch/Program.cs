using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.batch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args == null ? "" : string.Join(" ", args));

            if (args == null)
            {
                Console.WriteLine("No se indicaron los argumentos");
                return;
            }

            if (args.Length != 3)
            {
                Console.WriteLine("Se deben especificar 3 argumentos");
                return;
            }

            bool errorConversionIsStatic = bool.TryParse(args[0], out bool isStatic);

            if (!errorConversionIsStatic)
            {
                Console.WriteLine($"No se puede convertir {args[0]} al tipo de dato bool");
                return;
            }

            string nombreCompletoClase = args[1];
            string nombreMetodo = args[2];

            Type procesosType = Type.GetType(nombreCompletoClase);
            if (isStatic)
            {
                MethodInfo magicMethod = procesosType.GetMethod(nombreMetodo);
                magicMethod.Invoke(null, new object[] { });
            }
            else
            {
                ConstructorInfo procesosConstructor = procesosType.GetConstructor(Type.EmptyTypes);
                object procesosClassObject = procesosConstructor.Invoke(new object[] { });

                MethodInfo magicMethod = procesosType.GetMethod(nombreMetodo);
                magicMethod.Invoke(procesosClassObject, new object[] { });
            }
        }
    }
}
