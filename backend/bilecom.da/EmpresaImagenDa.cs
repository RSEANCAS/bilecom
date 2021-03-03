using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.da
{
    public class EmpresaImagenDa
    {
        public EmpresaImagenBe ObtenerDinamico(int empresaId, List<ColumnasEmpresaImagen> columnas, SqlConnection cn)
        {
            EmpresaImagenBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_emisorimagen_obtener_dinamico", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@columnas", string.Join(", ", columnas.Select(x => x.ToString()).ToArray()).GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new EmpresaImagenBe();

                            if (dr.Read())
                            {
                                respuesta.EmpresaId = dr.GetData<int>("EmpresaId");
                                if(columnas.Contains(ColumnasEmpresaImagen.LogoTipoContenido)) respuesta.LogoTipoContenido = dr.GetData<string>("LogoTipoContenido");
                                if (columnas.Contains(ColumnasEmpresaImagen.Logo)) respuesta.Logo = dr.GetData<byte[]>("Logo");
                                if (columnas.Contains(ColumnasEmpresaImagen.LogoFormatoTipoContenido)) respuesta.LogoFormatoTipoContenido = dr.GetData<string>("LogoFormatoTipoContenido");
                                if (columnas.Contains(ColumnasEmpresaImagen.LogoFormato)) respuesta.LogoFormato = dr.GetData<byte[]>("LogoFormato");
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
