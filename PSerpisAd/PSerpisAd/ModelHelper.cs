using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace Serpis.Ad
{
	public class ModelHelper
	{

		public static string GetSelect(Type type){
			string keyName=null;
			List<string> fieldNames= new List<string>();

			/*obtenemos las propiedades del objeto que pasemos por parametro marcadas como key y field*/
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
					keyName = propertyInfo.Name.ToLower();
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					fieldNames.Add (propertyInfo.Name.ToLower());

			}
			string tableName = type.Name.ToLower();
			return string.Format ("select {0} from {1} where {2}=",string.Join(", ",fieldNames),tableName,keyName);
			
		}

		public static object Load(Type type, string id){
			IDbCommand select = App.Instance.DbConnection.CreateCommand ();
			select.CommandText = GetSelect (type) + id;
			IDataReader reader = select.ExecuteReader ();
			reader.Read ();

			object obj=Activator.CreateInstance(type);
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true))
					propertyInfo.SetValue(obj,int.Parse(id),null);
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true))
					propertyInfo.SetValue(obj,reader[propertyInfo.Name.ToLower()],null);

			}
			reader.Close ();
			return obj;
		}
		
		public static void Save(object obj) {
			Type type = obj.GetType ();
			string keyName = null;
			object keyValue = null;
			List<object> fieldNames= new List<object>();
			List<object> campos=new List<object>();
			IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand ();
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					keyName = propertyInfo.Name.ToLower ();
					keyValue = propertyInfo.GetValue (obj, null);
				}
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)) {
					fieldNames.Add (propertyInfo.Name.ToLower ());
					campos.Add (propertyInfo.GetValue (obj ,null));
				}

			}
			string tableName = type.Name.ToLower();
			List<object> aux=new List<object>();
			for(int i=0;i<campos.Count;i++){
				aux.Add (fieldNames [i] + "=" + campos [i]);
			}
			updateDbCommand.CommandText = String.Format("update {0} set {1} where {2}={3}",tableName,String.Join(", ",aux),keyName,keyValue);
			updateDbCommand.ExecuteNonQuery ();	

			//return String.Format("update {0} set {1} where {2}={3}",tableName,String.Join(", ",aux),keyName,int.Parse(id));;		
		}

		public static string SaveTest(object obj) {
			Type type = obj.GetType ();
			string keyName = null;
			object keyValue = null;
			List<object> fieldNames= new List<object>();
			List<object> campos=new List<object>();
			//IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand ();
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					keyName = propertyInfo.Name.ToLower ();
					keyValue = propertyInfo.GetValue (obj, null);
				}
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)) {
					fieldNames.Add (propertyInfo.Name.ToLower ());
					campos.Add (propertyInfo.GetValue (obj ,null));
				}

			}
			string tableName = type.Name.ToLower();
			List<object> aux=new List<object>();
			for(int i=0;i<campos.Count;i++){
				aux.Add (fieldNames [i] + "=" + campos [i]);
			}
			//updateDbCommand.CommandText = String.Format("update {0} set {1} where {2}={3}",tableName,String.Join(", ",aux),keyName,keyValue);
			//updateDbCommand.ExecuteNonQuery ();	

			return String.Format("update {0} set {1} where {2}={3}",tableName,String.Join(", ",aux),keyName,keyValue);;		
		}
	}
}


