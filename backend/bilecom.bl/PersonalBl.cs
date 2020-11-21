using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class PersonalBl : Conexion
    {
        public List<PersonalBe> Listar(int empresaId, string nroDocumentoIdentidad, string nombresCompletos)
        {
            List<PersonalBe> lPersonal = new List<PersonalBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lPersonal = new PersonalDa().fListar(cn, empresaId, nroDocumentoIdentidad, nombresCompletos);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lPersonal;
        }
    }
}
