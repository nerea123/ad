using System;
using Gtk;
using System.Data;
using MySql.Data.MySqlClient;

namespace Serpis.Ad
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string connectionString="Server=localhost;Database=dbprueba;" +
				"User Id=root;Password=sistemas";
			
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			
			App.Instance.DbConnection=mySqlConnection;
			
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
