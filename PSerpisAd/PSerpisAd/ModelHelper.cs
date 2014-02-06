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


		private static string formatparameter (string fiel){
			return string.Format("{0}=@{0}",fiel);
		}

		public static string GetDelete(Type type){

			string keyParameter=null;
			string keyField=null;


			foreach (PropertyInfo propertyInfo in type.GetProperties()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					keyParameter="@"+propertyInfo.Name.ToLower ();
					keyField=propertyInfo.Name.ToLower ();
				}
			}
			string tableName = type.Name.ToLower();
			return string.Format("Delete from {0} where {1}={2} ",tableName,keyField,keyParameter);
		}

		public static void Delete(object obj){
			Type type = obj.GetType ();
			IDbCommand deleteDbCommand = App.Instance.DbConnection.CreateCommand ();
			deleteDbCommand.CommandText = GetDelete (obj.GetType ());
			foreach (PropertyInfo propertyInfo in type.GetProperties()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					object valueType= propertyInfo.GetValue(obj,null);
					DbCommandUtil.AddParameter(deleteDbCommand, propertyInfo.Name.ToLower(),valueType);
				}
			}
			deleteDbCommand.ExecuteNonQuery ();
		}



		public static string GetInsert(Type type){

			ModelInfo modelInfo = ModelInfoStore.Get (type);
			List<String> fieldParameters = new List<String> ();
			List<String> fields = new List<String> ();

			foreach (PropertyInfo propertyInfo in modelInfo.FieldPropertyInfos) {
					fieldParameters.Add("@"+propertyInfo.Name.ToLower ());
					fields.Add (propertyInfo.Name.ToLower ());

			}
			string tableName = type.Name.ToLower();
			return string.Format("insert into {0} ({1}) values ( {2} ) ",tableName,String.Join(", ",fields),String.Join(", ",fieldParameters));
		}

		public static void Insert(object obj){
			ModelInfo modelInfo = ModelInfoStore.Get (obj.GetType());
			Type type = obj.GetType ();
			IDbCommand insertDbCommand = App.Instance.DbConnection.CreateCommand ();
			insertDbCommand.CommandText = GetInsert (obj.GetType ());
			foreach (PropertyInfo propertyInfo in modelInfo.FieldPropertyInfos) {
					object valueType= propertyInfo.GetValue(obj,null);
					DbCommandUtil.AddParameter(insertDbCommand, propertyInfo.Name.ToLower(),valueType);
				
			}
			insertDbCommand.ExecuteNonQuery ();
		}

		public static string GetUpdate(Type type){
			string keyParameter=null;
			List<String> fieldParameters = new List<String> ();
			ModelInfo modelInfo = ModelInfoStore.Get (type);
			foreach (PropertyInfo propertyInfo in modelInfo.KeyPropertyInfo) 
					keyParameter = formatparameter (propertyInfo.Name.ToLower ());
				 
			foreach(PropertyInfo propertyInfo in modelInfo.FieldPropertyInfos) 
				fieldParameters.Add(formatparameter (propertyInfo.Name.ToLower ()));

				string tableName = type.Name.ToLower();
			return string.Format("update {0} set {1} where {2}",tableName,String.Join(", ",fieldParameters), keyParameter);
		}

		public static void Save(object obj){
			ModelInfo modelInfo = ModelInfoStore.Get (obj.GetType());
			Type type = obj.GetType ();
			IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand ();
			updateDbCommand.CommandText = GetUpdate (obj.GetType ());
			foreach (PropertyInfo propertyInfo in modelInfo.KeyPropertyInfo) {
					object valueType= propertyInfo.GetValue(obj,null);
					DbCommandUtil.AddParameter(updateDbCommand, propertyInfo.Name.ToLower(),valueType);

			}
			foreach (PropertyInfo propertyInfo in modelInfo.FieldPropertyInfos) {
				object valueType= propertyInfo.GetValue(obj,null);
				DbCommandUtil.AddParameter(updateDbCommand, propertyInfo.Name.ToLower(),valueType);

			}
			updateDbCommand.ExecuteNonQuery ();
		}

		public static void Save2(object obj) {
			Type type = obj.GetType ();
			string keyName = null;
			object keyValue = null;
			string tableName = type.Name.ToLower();
			IDbCommand updateDbCommand = App.Instance.DbConnection.CreateCommand ();
			foreach(PropertyInfo propertyInfo in type.GetProperties()){
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					keyName = propertyInfo.Name.ToLower ();
					keyValue = propertyInfo.GetValue (obj, null);
				}
				else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)) {
					DbCommandUtil.AddParameter (updateDbCommand, propertyInfo.Name.ToLower (), propertyInfo.GetValue (obj ,null));
					updateDbCommand.CommandText = String.Format("update {0} set {1}=@{2} where {3}={4}",tableName,propertyInfo.Name.ToLower (),propertyInfo.Name.ToLower (),keyName,keyValue);
					updateDbCommand.ExecuteNonQuery ();	
				}

			}

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

			return String.Format("update {0} set {1} where {2}={3}",tableName,String.Join(", ",aux),keyName,keyValue);	
		}


	}
}


