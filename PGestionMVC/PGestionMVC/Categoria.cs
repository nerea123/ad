using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace Serpis.Ad
{
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


	
	}
}

