﻿using bilecom.be;
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
        public List<ProductoBe> fListar(SqlConnection cn, int productoId, string nombre)
        {
            List<ProductoBe> lProducto = new List<ProductoBe>();
            ProductoBe oProducto;

            using (SqlCommand oCommand = new SqlCommand("dbo.usp_producto_listar", cn))
            {
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@productoId", productoId);
                oCommand.Parameters.AddWithValue("@nombre", nombre);
                using (SqlDataReader oDr = oCommand.ExecuteReader())
                {
                    if(oDr.HasRows)
                    {
                        if(oDr.Read())
                        {
                            oProducto = new ProductoBe();
                            if (!DBNull.Value.Equals(oDr["ProductoId"])) oProducto.ProductoId = (int)oDr["ProductoId"];
                            if (!DBNull.Value.Equals(oDr["CategoriaId"])) oProducto.CategoriaId = (int)oDr["CategoriaId"];
                            if (!DBNull.Value.Equals(oDr["Nombre"])) oProducto.Nombre = (string)oDr["Nombre"];
                            lProducto.Add(oProducto);
                        }
                    }
                }
            }
            return lProducto;
        }
    }
}
