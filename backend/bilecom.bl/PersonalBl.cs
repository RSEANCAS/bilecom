using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class PersonalBl : Conexion
    {
        public List<PersonalBe> Listar(int empresaId, string nroDocumentoIdentidad, string nombresCompletos, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<PersonalBe> lPersonal = new List<PersonalBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lPersonal = new PersonalDa().fListar(cn, empresaId, nroDocumentoIdentidad, nombresCompletos, pagina, cantidadRegistros, columnaOrden, ordenMax, out totalRegistros);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lPersonal;
        }

        public PersonalBe PersonalObtener(int EmpresaId, int PersonalId)
        {
            PersonalBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = new PersonalDa().PersonalObtener(EmpresaId, PersonalId,cn);
                cn.Close();
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }
        public bool PersonalGuardar(PersonalBe personalBe)
        {
            bool respuesta = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    respuesta = new PersonalDa().PersonalGuardar(personalBe, cn);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return respuesta;
        }
    }
}
