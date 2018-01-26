using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EPostgres;

namespace ADPostgres
{
    public class TemaAD
    {
        public List<Tema> obtener()
        {
            ConexionPosgreSQL.Conectar();
            DataSet datos = ConexionPosgreSQL.Seleccionar("tem_idtema,tem_descripcion,tem_uri", "tema", "tem_descripcion");
            List<Tema> lsItems = new List<Tema>();
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                Tema item = new Tema();
                item.tem_idtema = Int32.Parse(row[0].ToString());
                item.tem_descripcion = row[1].ToString();
                item.tem_uri = row[2].ToString();
                lsItems.Add(item);
            }
            ConexionPosgreSQL.Desconectar();
            return lsItems;
        }
    }
}
