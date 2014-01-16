using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Data;

namespace Serpis.Ad
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string connectionString="Server=localhost;Database=dbprueba;" +
			                         "User Id=root;Password=sistemas";

			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

			Categoria cat = new Categoria ();
			cat.Nombre = DateTime.Now.ToString ();

			Categoria.Save (cat);


			App.Instance.DbConnection=mySqlConnection;
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();

		}
	}

	public class Categoria
	{
		//se puede escribir de la siguiente forma para hacerlo mas corto
		//public int Id {set;get;}
		private int id;
		public int Id{
			get{ return id;}
			set{ id = value;}
		}
		private string nombre;
		public string Nombre{
			get{ return nombre;}
			set{ nombre = value;}
		}

		public static Categoria Load(string id){

			IDbCommand select = App.Instance.DbConnection.CreateCommand ();
			select.CommandText = string.Format("select nombre from categoria where id={0}",id);
			IDataReader reader = select.ExecuteReader ();
			reader.Read ();

			Categoria categoria = new Categoria ();
			categoria.Id = int.Parse (id);
			categoria.Nombre = reader ["nombre"].ToString ();
			reader.Close ();

			return categoria;

		}

		public static void Save(Categoria cat) {
			IDbCommand update = App.Instance.DbConnection.CreateCommand ();
			update.CommandText = "update categoria set nombre=@nombre where id="+cat.Id;
			DbCommandUtil.AddParameter (update,"nombre",cat.Nombre);
			update.ExecuteNonQuery ();
		}

	}
}
