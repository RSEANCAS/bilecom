using bilecom.be;
using bilecom.da;
using bilecom.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.bl
{
    public class TokenBl : Conexion
    {
        TokenDa tokenDa = new TokenDa();
        public bool GuardarToken(TokenBe token, out string codigoToken)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                codigoToken = Guid.NewGuid().ToString();
                bool esValido = tokenDa.Validar(token.UsuarioId, token.EmpresaId, codigoToken, token.TipoTokenId, cn);
                while (esValido)
                {
                    codigoToken = Guid.NewGuid().ToString();
                    esValido = tokenDa.Validar(token.UsuarioId, token.EmpresaId, codigoToken, token.TipoTokenId, cn);
                }
                token.CodigoToken = codigoToken;
                seGuardo = tokenDa.Guardar(cn, token);
            }
            catch (Exception)
            {

                throw;
            }
            finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
        public bool ValidarToken(int usuarioId, int empresaId, string codigoToken, int tipoTokenId)
        {
            bool esValido = false;
            try
            {
                cn.Open();
                esValido = tokenDa.Validar(usuarioId, empresaId, codigoToken, tipoTokenId, cn);
            }
            catch (Exception)
            {

                throw;
            }
            finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }
            return esValido;
        }
        public TokenBe ObtenerToken(int usuarioId, int empresaId, string codigoToken, int tipoTokenId)
        {
            TokenBe token = null;
            try
            {
                cn.Open();
                token = tokenDa.Obtener(usuarioId, empresaId, codigoToken, tipoTokenId, cn);
                cn.Close();
            }
            catch (Exception ex)
            {
                token = null;
            }
            finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }
            return token;
        }
    }
}
