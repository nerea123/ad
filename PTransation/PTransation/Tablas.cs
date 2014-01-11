using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace Serpis.Ad
{
	public class Tablas
	{
		private IDbCommand dbCommand;
		public Tablas ()
		{
			/*CREAMOS LAS TABLAS TELEVISOR Y COMPRADOR CON LOS CAMPOS QUE MODIFICAREMOS
			 * (PRECIO,CANTIDAD,SALDO) SIENDO UNSIGNED PARA COMPROBAR QUE CUANDO ESTOS 
			 * CAMPOS LLEGUEN A TENER NUMEROS NEGATIVOS SE LANCE UNA EXCEPCION Y LA TRANSACCION
			 * HAGA EL ROLLBACK
			 * 
			 * */
			string conexion="Server=localhost;Database=dbprueba;" +
			                 "User Id=root;Password=sistemas";

			string tabla = "CREATE TABLE IF NOT EXISTS televisor (" +
			               "id int(5) PRIMARY KEY AUTO_INCREMENT," +
			               "articulo varchar(50)," +
			               "precio decimal(10,2) unsigned," +
			               "cantidad int(5) unsigned);" +
			               "CREATE TABLE IF NOT EXISTS comprador (" +
			               "id int(5) PRIMARY KEY AUTO_INCREMENT," +
			               "saldo decimal(10,2) unsigned)";

			string insert = "INSERT INTO televisor (articulo,precio,cantidad) values('Philips',300,5)," +
			                "('MiniTv',100,5)," +
			                "('Samsung',400,5)," +
			                "('Toshiba',500,5);" +
			                "INSERT INTO comprador (saldo) values (1000)";


			MySqlConnection mySqlConnection = new MySqlConnection(conexion);
			App.Instance.DbConnection = mySqlConnection;

			dbCommand=mySqlConnection.CreateCommand();
			dbCommand.CommandText= tabla;
			dbCommand.ExecuteNonQuery();
			dbCommand.CommandText= insert;
			dbCommand.ExecuteNonQuery();

		}

		public void borrar(){
			dbCommand.CommandText = "Drop table televisor; Drop table comprador";
			dbCommand.ExecuteNonQuery();
		}

	}

}