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
        PersonalDa personalDa = new PersonalDa();

        public List<PersonalBe> BuscarPersonal(int empresaId, string nroDocumentoIdentidad, string nombresCompletos, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<PersonalBe> lista = null;
            try
            {
                cn.Open();
                lista = personalDa.Buscar(empresaId, nroDocumentoIdentidad, nombresCompletos, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception){ lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public PersonalBe ObtenerPersonal(int EmpresaId, int PersonalId)
        {
            PersonalBe item = null;
            try
            {
                cn.Open();
                item = personalDa.Obtener(EmpresaId, PersonalId,cn);
                cn.Close();
            }
            catch(Exception ex){item = null;}
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }

        public bool GuardarPersonal(PersonalBe registro)
        {
            bool seGuardo = false;
                try
                {
                    cn.Open();
                    seGuardo = new PersonalDa().Guardar(registro, cn);
                    cn.Close();
                }
                catch (Exception ex){seGuardo = false;}
                finally {if (cn.State == ConnectionState.Open) cn.Close();}
            return seGuardo;
        }

        public bool EliminarCliente(int empresaId, int personalId, string Usuario)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = personalDa.Eliminar(empresaId, personalId, Usuario, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }

    }
}
