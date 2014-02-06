using NUnit.Framework;
using System;
using System.Reflection;

namespace Serpis.Ad
{
	[TestFixture ()]
	internal class ModelInfoFoo
	{

		public ModelInfoFoo(int id, string nombre){
			this.Id = id;
			this.Nombre = nombre;
		}

		[Key]
		public int Id {get;set;}

		[Field]
		public string Nombre {get;set;}
	}

	[TestFixture ()]
	internal class ModelInfoBar

	{
		public ModelInfoBar(int id, string nombre, decimal precio){
			this.Id = id;
			this.Nombre = nombre;
			this.Precio = precio;
		}

		[Key]
		public int Id {get;set;}

		[Field]
		public string Nombre {get;set;}

		[Field]
		public decimal Precio {get;set;}
	}

	[TestFixture ()]
	public class ModelInfoTest
	{
		[Test ()]
		public void TableName ()
		{
			ModelInfo modelInfo = new ModelInfo (typeof(ModelInfoFoo));
			Assert.AreEqual ("modelinfofoo", modelInfo.TableName);
		}

		[Test ()]
		public void KeyPropertyInfo(){
			ModelInfo modelInfo = new ModelInfo (typeof(ModelInfoFoo));
			Assert.IsNotNull (modelInfo.KeyPropertyInfo);
			Assert.AreEqual ("Id", modelInfo.KeyPropertyInfo.Name);
		}

		[Test()]
		public void KeyName(){
			ModelInfo modelInfo = new ModelInfo (typeof(ModelInfoFoo));
			Assert.AreEqual ("id", modelInfo.KeyName);
		}

		[Test()]
		public void FieldpropertyInfos(){
			ModelInfo modelInfo = new ModelInfo (typeof(ModelInfoFoo)); 
			PropertyInfo[] fieldPropertyInfo = modelInfo.FieldPropertyInfos;
			Assert.AreEqual (1, fieldPropertyInfo.Length);

			modelInfo = new ModelInfo (typeof(ModelInfoBar));
			PropertyInfo[] fieldPropertyInfo2 = modelInfo.FieldPropertyInfos;
			Assert.AreEqual (2, fieldPropertyInfo2.Length);
		}

		[Test()]
		public void FieldNames(){
			ModelInfo modelInfo = new ModelInfo (typeof(ModelInfoFoo)); 
			string[] fieldName = modelInfo.FieldNames;
			Assert.Contains ("nombre", fieldName);
			Assert.AreEqual (1, fieldName.Length);

		}
	}
}

