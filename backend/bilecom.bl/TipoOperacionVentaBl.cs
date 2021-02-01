﻿using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class TipoOperacionVentaBl : Conexion
    {
        TipoOperacionVentaDa tipoOperacionVentaDa = new TipoOperacionVentaDa();

        public List<TipoOperacionVentaBe> ListarTipoOperacionVentaPorEmpresa(int empresaId)
        {
            List<TipoOperacionVentaBe> lista = null;

            try
            {
                cn.Open();
                lista = tipoOperacionVentaDa.ListarPorEmpresa(empresaId, cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}