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
			foreach (PropertyInfo propertyInfo in type.GetProperties()) {
				if (propertyInfo.IsDefined (typeof(KeyAttribute), true)) {
					keyPropertyInfo = propertyInfo;
					keyName = propertyInfo.Name.ToLower ();
				} else if (propertyInfo.IsDefined (typeof(FieldAttribute), true)) {
					fieldPropertyInfos.Add (propertyInfo);
					fieldNames.Add (propertyInfo.Name.ToLower());
				}
			}

		}

		private string tableName;
		private List<PropertyInfo> fieldPropertyInfos;
		private List<string> fieldNames;
		private string keyName;
		private PropertyInfo keyPropertyInfo;
		public PropertyInfo KeyPropertyInfo { get { return keyPropertyInfo; } }
		public string KeyName { get {return keyName;}}
		public PropertyInfo[] FieldPropertyInfos {get {return fieldPropertyInfos.ToArray();}}
		public string[] FieldNames {get {return fieldNames.ToArray();}}
	}
}

