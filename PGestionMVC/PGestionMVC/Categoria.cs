using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Collections.Generic;

namespace Serpis.Ad
{
	public class Categoria
	{
		//se puede escribir de la siguiente forma para hacerlo mas corto
		//public int Id {set;get;}


		private int id;

		[KeyAttribute]
		public int Id{
			get{ return id;}
			set{ id = value;}
		}


		private string nombre;

		[FieldAttribute]
		public string Nombre{
			get{ return nombre;}
			set{ nombre = value;}
		}



		/*public static Object Load(Type type,string id){
			object obj=Activator.CreateInstance (type);
			PropertyInfo propertyInfo;
			return obj;
		}*/

	
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

		public static void Save(Categoria categoria) {
			IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand ();
			updateDbCommand.CommandText = "update categoria set nombre=@nombre where id=" + categoria.Id;
			DbCommandUtil.AddParameter (updateDbCommand, "nombre", categoria.Nombre);
			updateDbCommand.ExecuteNonQuery ();			
		}


	
	}
}

