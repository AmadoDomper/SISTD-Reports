using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EPostgres;
namespace ADPostgres
{
    public class TipoAD
    {
        public List<Tipo> obtener()
        {
            ConexionPosgreSQL.Conectar();
            DataSet datos = ConexionPosgreSQL.Seleccionar("tip_idtipo,tip_descripcion,tip_uri", "tipo", "tip_descripcion");
            List<Tipo> lsItems = new List<Tipo>();
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                Tipo item = new Tipo();
                item.tip_idtipo = Int32.Parse(row[0].ToString());
                item.tip_descripcion = row[1].ToString();
                item.tip_uri = row[2].ToString();
                lsItems.Add(item);
            }
            ConexionPosgreSQL.Desconectar();
            return lsItems;
        }
    }
}
