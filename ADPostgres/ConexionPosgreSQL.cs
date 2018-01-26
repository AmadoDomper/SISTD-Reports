using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using System.Data;
namespace ADPostgres
{
    public class ConexionPosgreSQL
    {
        //Declaramos un objeto conexión, adaptador, comando, tabla, cadena de conexión y un bindingsource.
        static NpgsqlConnection Conex = new NpgsqlConnection();
        static NpgsqlDataAdapter Adaptador = new NpgsqlDataAdapter();
        static NpgsqlCommand Comando = new NpgsqlCommand();
        static string CadenaDeConexion;

        public static void Conectar()
        {
            //Le damos los parámetros necesarios para la cadena de conexión.       
            CadenaDeConexion = "Server=10.10.10.14; Port=5432; User Id=postgres; Password=123456; Database=articulos;";
            //Pasamos la cadena de conexión al objeto conexión.
            Conex.ConnectionString = CadenaDeConexion;
            //Abrimos la conexión.
            Conex.Open();
        }

        public static void Desconectar()
        {
            //Cerramos la conexión.
            Conex.Close();
        }

        public static DataSet Seleccionar(string campos, string tabla, string orden)
        {
            //Declaramos una variable para almacenar la consulta.
            string Consulta = "select " + campos + " from " + tabla + " order by " + orden + ";";
            //Creamos nuestro adaptador y le pasamos la consulta y la conexión.
            Adaptador = new NpgsqlDataAdapter(Consulta, Conex);
            //Creamos un comando constructor y le pasamos el adaptador.
            NpgsqlCommandBuilder ComandoConstructor = new NpgsqlCommandBuilder(Adaptador);
            //Llenamos nuestra tabla con los datos de nuestro adaptador.
            DataSet Tabla = new DataSet();
            Adaptador.Fill(Tabla);
            return Tabla;
        }
        public static DataSet ejecutar(string sql)
        {
            Adaptador = new NpgsqlDataAdapter(sql, Conex);
            NpgsqlCommandBuilder ComandoConstructor = new NpgsqlCommandBuilder(Adaptador);
            DataSet Tabla = new DataSet();
            Adaptador.Fill(Tabla);
            return Tabla;
        }
    }
}
