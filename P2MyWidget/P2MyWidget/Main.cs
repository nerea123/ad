using System;
using Gtk;
using MySql.Data.MySqlClient;
namespace Serpis.Ad
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			
			Application.Init ();
			
			string connectionString="Server=localhost;Database=dbprueba;" +
				"User Id=root;Password=sistemas";
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			App.Instance.DbConnection = mySqlConnection;//TODO asignar objeto conexion
			
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
