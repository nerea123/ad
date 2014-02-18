using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Serpis.Ad
{
//si es internal es publico para el ensamblado pero privado para los demas

	public class ModelInfo
	{
		public string TableName { get {return tableName;} }
		private Type type;
		internal ModelInfo (Type type)
		{
			this.type = type;
			tableName = type.Name.ToLower ();
			fieldPropertyInfos=new List<PropertyInfo>();
			fieldNames=new List<string>();
			fieldNamesParamUpdate=new List<string>();
			fieldNamesParamSelect=new List<string>();
			fieldNamesParamInsert=new List<string>();
			/*IfieldNames = from value in type.GetProperties()
			              select value;*/

			foreach (PropertyInfo propertyInfo in type.GetProperties()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					Console.WriteLine ( propertyInfo.Name);
					keyPropertyInfo = propertyInfo;
					keyName = propertyInfo.Name.ToLower ();
				} else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)) {
					fieldPropertyInfos.Add (propertyInfo);
					fieldNames.Add (propertyInfo.Name.ToLower());
					fieldNamesParamUpdate.Add (formatparameter(propertyInfo.Name.ToLower()));
					fieldNamesParamInsert.Add (formatparameterSelect(propertyInfo.Name.ToLower()));
					fieldNamesParamSelect.Add (propertyInfo.Name.ToLower());
				}
			}
			insert=String.Format("insert into {0} ({1}) values ( {2} ) ",tableName,String.Join(", ",fieldNames),String.Join(", ",fieldNamesParamInsert));
			select = String.Format ("select {0} from {1} where {2}",string.Join(", ",fieldNamesParamSelect),tableName,formatparameter (keyName));
			update=string.Format("update {0} set {1} where {2}",tableName,String.Join(", ",fieldNamesParamUpdate), formatparameter (keyName));
		}

		private string tableName;
		private List<PropertyInfo> fieldPropertyInfos;
		private List<string> fieldNames;
		//private IEnumerable<PropertyInfo> IfieldNames;
		private List<string> fieldNamesParamUpdate;
		private List<string> fieldNamesParamSelect;
		private List<string> fieldNamesParamInsert;
		private string keyName;
		private PropertyInfo keyPropertyInfo;
		public PropertyInfo KeyPropertyInfo { get { return keyPropertyInfo; } }
		public string KeyName { get {return keyName;}}
		public PropertyInfo[] FieldPropertyInfos {get {return fieldPropertyInfos.ToArray();}}
		public string[] FieldNames {get {return fieldNames.ToArray();}}

		private string select;
		public string InsertText{ get { return insert; } }
		private string update;
		public string UpdateText{ get { return update; } }
		private string insert;
		public string SelectText{ get { return select; } }

		private static string formatparameter (string fiel){
			return string.Format("{0}=@{0}",fiel);
		}

		private static string formatparameterSelect (string fiel){
			return string.Format("@{0}",fiel);
		}
	}
}

