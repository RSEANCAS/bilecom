using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.bl
{
    public class EmpresaBl : Conexion
    {
        DistritoDa distritoDa = new DistritoDa();
        ProvinciaDa provinciaDa = new ProvinciaDa();
        DepartamentoDa departamentoDa = new DepartamentoDa();
        PaisDa paisDa = new PaisDa();
        EmpresaDa empresaDa = new EmpresaDa();
        EmpresaConfiguracionDa empresaConfiguracionDa = new EmpresaConfiguracionDa();
        EmpresaAmbienteSunatDa empresaAmbienteSunatDa = new EmpresaAmbienteSunatDa();
        AmbienteSunatDa ambienteSunatDa = new AmbienteSunatDa();
        EmpresaImagenDa empresaImagenDa = new EmpresaImagenDa();

        public EmpresaBe ObtenerEmpresa(int empresaId, bool withUbigeo = false, bool withConfiguracion = false, List<ColumnasEmpresaImagen> columnasEmpresaImagen = null)
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

                    if (withConfiguracion)
                    {
                        item.EmpresaConfiguracion = empresaConfiguracionDa.Obtener(empresaId, cn);
                        if (item.EmpresaConfiguracion != null)
                        {
                            item.EmpresaConfiguracion.EmpresaAmbienteSunat = empresaAmbienteSunatDa.Obtener(item.EmpresaId, item.EmpresaConfiguracion.AmbienteSunatId, cn);
                            if (item.EmpresaConfiguracion.EmpresaAmbienteSunat != null) item.EmpresaConfiguracion.EmpresaAmbienteSunat.AmbienteSunat = ambienteSunatDa.Obtener(item.EmpresaConfiguracion.AmbienteSunatId, cn);
                        }
                    }

                    if(columnasEmpresaImagen != null)
                    {
                        item.EmpresaImagen = empresaImagenDa.ObtenerDinamico(empresaId, columnasEmpresaImagen, cn);
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
