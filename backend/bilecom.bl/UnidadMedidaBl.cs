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
    public class UnidadMedidaBl : Conexion
    {
        UnidadMedidaDa unidadMedidaDa = new UnidadMedidaDa();

        public List<UnidadMedidaBe> Listar ()
        {
            List<UnidadMedidaBe> respuesta = null;
            try
            {
                cn.Open();
                respuesta = unidadMedidaDa.Listar(cn);
                cn.Close();
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) cn.Close();
            }
            return respuesta;
        }
    }
}
