using System;
using System.Reflection;
using System.Collections.Generic;

namespace Serpis.Ad
{
	public class ModelInfo
	{
		public string TableName { get {return tableName;} }
		private Type type;
		public ModelInfo (Type type)
		{
			this.type = type;
			tableName = type.Name.ToLower ();
			fieldPropertyInfos=new List<PropertyInfo>();
			fieldNames=new List<string>();
			fieldNamesParamUpdate=new List<string>();
			fieldNamesParamSelect=new List<string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					keyPropertyInfo = propertyInfo;
					keyName = propertyInfo.Name.ToLower ();
				} else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)) {
					fieldPropertyInfos.Add (propertyInfo);
					fieldNames.Add (propertyInfo.Name.ToLower());
					fieldNamesParamUpdate.Add (formatparameter(propertyInfo.Name.ToLower()));
					fieldNamesParamSelect.Add (formatparameterSelect(propertyInfo.Name.ToLower()));

				}
			}
			select=String.Format("insert into {0} ({1}) values ( {2} ) ",tableName,String.Join(", ",fieldNames),String.Join(", ",fieldNamesParamSelect));
			update=string.Format("update {0} set {1} where {2}",tableName,String.Join(", ",fieldNamesParamUpdate), formatparameter (keyName));
		}

		private string tableName;
		private List<PropertyInfo> fieldPropertyInfos;
		private List<string> fieldNames;
		private List<string> fieldNamesParamUpdate;
		private List<string> fieldNamesParamSelect;
		private string keyName;
		private PropertyInfo keyPropertyInfo;
		public PropertyInfo KeyPropertyInfo { get { return keyPropertyInfo; } }
		public string KeyName { get {return keyName;}}
		public PropertyInfo[] FieldPropertyInfos {get {return fieldPropertyInfos.ToArray();}}
		public string[] FieldNames {get {return fieldNames.ToArray();}}

		private string select;
		public string SelectText{ get { return select; } }
		private string update;
		public string UpdateText{ get { return update; } }

		private static string formatparameter (string fiel){
			return string.Format("{0}=@{0}",fiel);
		}

		private static string formatparameterSelect (string fiel){
			return string.Format("@{0}",fiel);
		}
	}
}

