using bilecom.be;
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
        public List<PersonalBe> fListar(SqlConnection cn, int empresaId, string nroDocumentoIdentidad, string nombresCompletos)
        {
            List<PersonalBe> lPersonal = new List<PersonalBe>();
            PersonalBe oPersonal = new PersonalBe();

            using (SqlCommand oCommand = new SqlCommand("dbo.usp_personal_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@empresaId", empresaId);
                oCommand.Parameters.AddWithValue("@nroDocumentoIdentidad", nroDocumentoIdentidad);
                oCommand.Parameters.AddWithValue("@nombresCompletos", @nombresCompletos);

                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if(oDr.HasRows)
                    {
                        if(oDr.Read())
                        {
                            oPersonal = new PersonalBe();
                            if (!DBNull.Value.Equals(oDr["PersonalId"])) oPersonal.PersonalId = (int)oDr["PersonalId"];
                            if (!DBNull.Value.Equals(oDr["EmpresaId"])) oPersonal.EmpresaId = (int)oDr["EmpresaId"];
                            if (!DBNull.Value.Equals(oDr["TipoDocumentoIdentidadId"])) oPersonal.TipoDocumentoIdentidadId = (int)oDr["TipoDocumentoIdentidadId"];
                            if (!DBNull.Value.Equals(oDr["NroDocumentoIdentidad"])) oPersonal.NroDocumentoIdentidad = (string)oDr["NroDocumentoIdentidad"];
                            if (!DBNull.Value.Equals(oDr["NombresCompletos"])) oPersonal.NombresCompletos = (string)oDr["NombresCompletos"];
                            if (!DBNull.Value.Equals(oDr["Direccion"])) oPersonal.Direccion = (string)oDr["Direccion"];
                            if (!DBNull.Value.Equals(oDr["Correo"])) oPersonal.Correo = (string)oDr["Correo"];
                            if (!DBNull.Value.Equals(oDr["FlagActivo"])) oPersonal.FlagActivo = (bool)oDr["FlagActivo"];
                            lPersonal.Add(oPersonal);

                        }
                    }
                }
            }

            return lPersonal;
        }
    }
}
