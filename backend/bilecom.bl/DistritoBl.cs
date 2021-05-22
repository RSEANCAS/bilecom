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
    public class DistritoBl : Conexion
    {
        DistritoDa distritoDa = new DistritoDa();
        ProvinciaDa provinciaDa = new ProvinciaDa();
        DepartamentoDa departamentoDa = new DepartamentoDa();
        PaisDa paisDa = new PaisDa();

        public List<DistritoBe> ListarDistrito()
        {
            List<DistritoBe> lista = null;
            try
            {
                cn.Open();
                lista = distritoDa.Listar(cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public DistritoBe ObtenerDistrito(int distritoId, bool withUbigeo = false)
        {
            DistritoBe item = null;
            try
            {
                cn.Open();
                item = distritoDa.Obtener(distritoId, cn);
                if (item != null && withUbigeo)
                {
                    item.Provincia = provinciaDa.Obtener(item.ProvinciaId, cn);
                    if (item.Provincia != null)
                    {
                        item.Provincia.Departamento = departamentoDa.Obtener(item.Provincia.DepartamentoId, cn);
                        if (item.Provincia.Departamento != null)
                        {
                            item.Provincia.Departamento.Pais = paisDa.Obtener(item.Provincia.Departamento.PaisId, cn);
                        }
                    }
                }
                cn.Close();
            }
            catch (Exception ex) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }
    }
}