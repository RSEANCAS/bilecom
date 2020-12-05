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
    public class ProveedorDa
    {
        public List<ProveedorBe> fListar(SqlConnection cn, int empresaId, string nroDocumentoIdentidad, string razonSocial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ProveedorBe> lProveedor = new List<ProveedorBe>();
            ProveedorBe oProveedor;
            using (SqlCommand oCommand = new SqlCommand("usp_proveedor_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                oCommand.Parameters.AddWithValue("@nroDocumentoIdentidad", nroDocumentoIdentidad.GetNullable());
                oCommand.Parameters.AddWithValue("@razonSocial", razonSocial.GetNullable());
                oCommand.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                oCommand.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                oCommand.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                oCommand.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if(oDr.HasRows)
                    {
                        while(oDr.Read())
                        {
                            oProveedor = new ProveedorBe();
                            oProveedor.ProveedorId = oDr.GetData<int>("Fila");
                            //if (!DBNull.Value.Equals(oDr["ProveedorId"])) oProveedor.ProveedorId = (int)oDr["ProveedorId"];
                            if (!DBNull.Value.Equals(oDr["TipoDocumento"])) oProveedor.TipoDocumentoIdentidad = (int)oDr["TipoDocumento"];
                            oProveedor.TDocumentoIdentidad = new TipoDocumentoIdentidadBe();
                            if (!DBNull.Value.Equals(oDr["DescripcionTipoDocumentoIdentidad"])) oProveedor.TDocumentoIdentidad.Descripcion = (string)oDr["DescripcionTipoDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["NroDocumentoIdentidad"])) oProveedor.NroDocumentoIdentidad = (string)oDr["NroDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["RazonSocial"])) oProveedor.RazonSocial = (string)oDr["RazonSocial"];
                            lProveedor.Add(oProveedor);
                            if (!DBNull.Value.Equals(oDr["Total"])) totalRegistros = (int)oDr["Total"];
                        }
                    }
                }
            }

            return lProveedor;
        }

        public bool ProveedorGuardar(ProveedorBe proveedorBe, SqlConnection cn)
        {
            bool respuesta = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_proveedor_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@EmpresaId", proveedorBe.EmpresaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@ProveedorId", proveedorBe.ProveedorId.GetNullable());
                    oCommand.Parameters.AddWithValue("@TipoDocumentoIdentidad", proveedorBe.TipoDocumentoIdentidad.GetNullable());
                    oCommand.Parameters.AddWithValue("@NroDocumentoIdentidad", proveedorBe.NroDocumentoIdentidad.GetNullable());
                    oCommand.Parameters.AddWithValue("@RazonSocial", proveedorBe.RazonSocial.GetNullable());
                    oCommand.Parameters.AddWithValue("@Usuario", proveedorBe.Usuario.GetNullable());


                    int result = oCommand.ExecuteNonQuery();
                    if (result > 0) respuesta = true;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}
