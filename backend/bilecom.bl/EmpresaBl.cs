using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class EmpresaBl : Conexion
    {
        DistritoDa distritoDa = new DistritoDa();
        ProvinciaDa provinciaDa = new ProvinciaDa();
        DepartamentoDa departamentoDa = new DepartamentoDa();
        PaisDa paisDa = new PaisDa();
        EmpresaDa empresaDa = new EmpresaDa();

        public EmpresaBe ObtenerEmpresa(int empresaId, bool withUbigeo = false)
        {
            EmpresaBe item = null;

            try
            {
                cn.Open();
                item = empresaDa.Obtener(empresaId, cn);
                if(item != null)
                {
                    if (withUbigeo)
                    {
                        item.Distrito = distritoDa.Obtener(item.DistritoId, cn);
                        item.Distrito.Provincia = provinciaDa.Obtener(item.Distrito.ProvinciaId, cn);
                        item.Distrito.Provincia.Departamento = departamentoDa.Obtener(item.Distrito.Provincia.DepartamentoId, cn);
                        item.Distrito.Provincia.Departamento.Pais = paisDa.Obtener(item.Distrito.Provincia.Departamento.PaisId, cn);
                    }
                }
                cn.Close();
            }
            catch (Exception ex) { throw ex; }
            finally { cn.Close(); }

            return item;
        }

        public EmpresaBe ObtenerEmpresaPorRuc(string ruc)
        {
            EmpresaBe item = null;

            try
            {
                cn.Open();

                item = empresaDa.ObtenerPorRuc(ruc, cn);
            }
            catch (Exception ex) { throw ex; }
            finally { cn.Close(); }

            return item;
        }
    }
}
