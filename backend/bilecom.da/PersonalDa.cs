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
    public class PersonalDa
    {
        public List<PersonalBe> fListar(SqlConnection cn, int empresaId, string nroDocumentoIdentidad, string nombresCompletos, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<PersonalBe> lPersonal = new List<PersonalBe>();
            PersonalBe oPersonal = new PersonalBe();

            using (SqlCommand oCommand = new SqlCommand("dbo.usp_personal_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                oCommand.Parameters.AddWithValue("@nroDocumentoIdentidad", nroDocumentoIdentidad.GetNullable());
                oCommand.Parameters.AddWithValue("@nombresCompletos", nombresCompletos.GetNullable());
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
                            oPersonal = new PersonalBe();
                            if (!DBNull.Value.Equals(oDr["PersonalId"])) oPersonal.PersonalId = (int)oDr["PersonalId"];
                            if (!DBNull.Value.Equals(oDr["EmpresaId"])) oPersonal.EmpresaId = (int)oDr["EmpresaId"];
                            if (!DBNull.Value.Equals(oDr["TipoDocumentoIdentidadId"])) oPersonal.TipoDocumentoIdentidadId = (int)oDr["TipoDocumentoIdentidadId"];
                            oPersonal.TipoDocumentoIdentidad = new TipoDocumentoIdentidadBe();
                            if (!DBNull.Value.Equals(oDr["DescripcionTipoDocumentoIdentidad"])) oPersonal.TipoDocumentoIdentidad.Descripcion = (string)oDr["DescripcionTipoDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["NroDocumentoIdentidad"])) oPersonal.NroDocumentoIdentidad = (string)oDr["NroDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["NombresCompletos"])) oPersonal.NombresCompletos = (string)oDr["NombresCompletos"];
                            if (!DBNull.Value.Equals(oDr["Direccion"])) oPersonal.Direccion = (string)oDr["Direccion"];
                            if (!DBNull.Value.Equals(oDr["Correo"])) oPersonal.Correo = (string)oDr["Correo"];
                            if (!DBNull.Value.Equals(oDr["FlagActivo"])) oPersonal.FlagActivo = (bool)oDr["FlagActivo"];
                            lPersonal.Add(oPersonal);

                            if (!DBNull.Value.Equals(oDr["Total"])) totalRegistros = (int)oDr["Total"];
                        }
                    }
                }
            }
            return lPersonal;
        }

        public PersonalBe PersonalObtener(int EmpresaId,int PersonalId,SqlConnection cn)
        {
            PersonalBe respuesta = null;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_personal_obtener", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@EmpresaId", EmpresaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@PersonalId", PersonalId.GetNullable());
                    
                    using (SqlDataReader dr = oCommand.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new PersonalBe();
                                respuesta.PersonalId = dr.GetData<int>("PersonalId");
                                respuesta.EmpresaId = dr.GetData<int>("EmpresaId");
                                respuesta.TipoDocumentoIdentidadId = dr.GetData<int>("TipoDocumentoIdentidadId");
                                respuesta.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidad");
                                respuesta.NombresCompletos = dr.GetData<string>("NombresCompletos");
                                respuesta.Direccion = dr.GetData<string>("Direccion");
                                respuesta.Correo = dr.GetData<string>("Correo");
                                respuesta.FlagActivo = dr.GetData<bool>("FlagActivo");
                                respuesta.DistritoId = dr.GetData<int>("DistritoId");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }
        public bool PersonalGuardar(PersonalBe personalBe,SqlConnection cn)
        {
            bool respuesta = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.[usp_personal_guardar]", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@PersonalId", personalBe.PersonalId.GetNullable());
                    oCommand.Parameters.AddWithValue("@EmpresaId", personalBe.EmpresaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@TipoDocumentoIdentidadId", personalBe.TipoDocumentoIdentidadId.GetNullable());
                    oCommand.Parameters.AddWithValue("@NroDocumentoIdentidad", personalBe.NroDocumentoIdentidad.GetNullable());
                    oCommand.Parameters.AddWithValue("@NombresCompletos", personalBe.NombresCompletos.GetNullable());
                    oCommand.Parameters.AddWithValue("@DistritoId", personalBe.DistritoId.GetNullable());
                    oCommand.Parameters.AddWithValue("@Direccion", personalBe.Direccion.GetNullable());
                    oCommand.Parameters.AddWithValue("@Correo", personalBe.Correo.GetNullable());
                    oCommand.Parameters.AddWithValue("@FlagActivo", personalBe.FlagActivo.GetNullable());
                    oCommand.Parameters.AddWithValue("@Usuario", personalBe.Usuario.GetNullable());
                    
                    //oCommand.ExecuteNonQuery();
                    //respuesta = true;
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
