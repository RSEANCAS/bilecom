﻿using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class TipoOperacionVentaDa
    {
        public List<TipoOperacionVentaBe> ListarPorEmpresaTipoComprobante(int empresaId, int tipoComprobanteId, SqlConnection cn)
        {
            List<TipoOperacionVentaBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("dbo.usp_tipooperacionventa_listar_x_empresa_tipocomprobante", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<TipoOperacionVentaBe>();

                        while (dr.Read())
                        {
                            TipoOperacionVentaBe item = new TipoOperacionVentaBe();
                            item.TipoOperacionVentaId = dr.GetData<int>("TipoOperacionVentaId");
                            item.Nombre = dr.GetData<string>("Nombre");
                            item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                            lista.Add(item);

                        }
                    }
                }
            }
            return lista;
        }

        public TipoOperacionVentaBe Obtener(int tipoOperacionVentaId, SqlConnection cn)
        {
            TipoOperacionVentaBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipooperacionventa_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoOperacionVentaId", tipoOperacionVentaId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new TipoOperacionVentaBe();
                                respuesta.TipoOperacionVentaId = dr.GetData<int>("TipoOperacionVentaId");
                                respuesta.CodigoSunat = dr.GetData<string>("CodigoSunat");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }
    }
}
