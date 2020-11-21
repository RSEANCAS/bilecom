using bilecom.be;
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
    public class EmpresaDa
    {
        public EmpresaBe Obtener(int empresaId, SqlConnection cn)
        {
            EmpresaBe item = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresa_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            item = new EmpresaBe();
                            if (dr.Read())
                            {
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.Ruc = dr.GetData<string>("Ruc");
                                item.RazonSocial = dr.GetData<string>("RazonSocial");
                                item.NombreComercial = dr.GetData<string>("NombreComercial");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public EmpresaBe ObtenerPorRuc(string ruc, SqlConnection cn)
        {
            EmpresaBe item = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresa_obtener_x_ruc", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", ruc.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            item = new EmpresaBe();
                            if (dr.Read())
                            {
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.Ruc = dr.GetData<string>("Ruc");
                                item.RazonSocial = dr.GetData<string>("RazonSocial");
                                item.NombreComercial = dr.GetData<string>("NombreComercial");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public bool Guardar(EmpresaBe registro, SqlConnection cn, out int? empresaId)
        {
            empresaId = null;
            bool seGuardo = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresa_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@empresaId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.InputOutput, Value = registro.EmpresaId.GetNullable() });
                    cmd.Parameters.AddWithValue("@ruc", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@razonSocial", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@nombreComercial", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@creadoPor", registro.CreadoPor.GetNullable());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return seGuardo;
        }
    }
}
