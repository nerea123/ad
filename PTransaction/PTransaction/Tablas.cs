using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace Serpis.Ad
{
	public class Tablas
	{
		public Tablas ()
		{
			IDbCommand dbCommand;
			string conexion="Server=localhost;Database=dbprueba;" +
			                 "User Id=root;Password=sistemas";

			string tabla = "CREATE TABLE IF NOT EXISTS televisor (" +
			                "articulo varchar(50)," +
			               "precio decimal(10,2)," +
			               "cantidad int(5));" +
			                "CREATE TABLE IF NOT EXISTS comprador (" +
			                "saldo decimal(10,2))";


			MySqlConnection mySqlConnection = new MySqlConnection(conexion);
			App.Instance.DbConnection = mySqlConnection;

			dbCommand=mySqlConnection.CreateCommand();
			dbCommand.CommandText= tabla;
			dbCommand.ExecuteNonQuery();

		}
	}

}

