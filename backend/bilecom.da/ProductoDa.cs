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
    public class ProductoDa
    {
        public List<ProductoBe> fListar(SqlConnection cn, string categoriaNombre, string nombre, int empresaId)
        {
            List<ProductoBe> lProducto = null;
            ProductoBe oProducto;

            using (SqlCommand oCommand = new SqlCommand("dbo.usp_producto_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@categoriaNombre", categoriaNombre);
                oCommand.Parameters.AddWithValue("@nombre", nombre);
                oCommand.Parameters.AddWithValue("@EmpresaId", empresaId);
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if(oDr.HasRows)
                    {
                        lProducto = new List<ProductoBe>();
                        if (oDr.Read())
                        {
                            oProducto = new ProductoBe();

                            oProducto.Nombre = oDr.GetData<string>("NombreProducto");
                            oProducto.categoriaProducto = new CategoriaProductoBe();
                            oProducto.categoriaProducto.Nombre = oDr.GetData<string>("NombreCategoria");
                            lProducto.Add(oProducto);
                        }
                    }
                }
            }
            return lProducto;
        }
        public bool ProductoGuardar(ProductoBe productoBe, SqlConnection cn)
        {
            bool respuesta = false;
            try
            { 
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_producto_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@EmpresaId", productoBe.EmpresaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@ProductoId", productoBe.ProductoId.GetNullable());
                    oCommand.Parameters.AddWithValue("@CategoriaId", productoBe.CategoriaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@Nombre", productoBe.Nombre.GetNullable());
                    oCommand.Parameters.AddWithValue("@Usuario", productoBe.Usuario.GetNullable());

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
        public bool ProductoActualizar(ProductoBe productoBe, SqlConnection cn)
        {
            bool respuesta = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_producto_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@ProductoId", productoBe.ProductoId.GetNullable());
                    oCommand.Parameters.AddWithValue("@CategoriaId", productoBe.CategoriaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@Nombre", productoBe.Nombre.GetNullable());
                    oCommand.Parameters.AddWithValue("@Usuario", productoBe.Usuario.GetNullable().GetNullable());

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