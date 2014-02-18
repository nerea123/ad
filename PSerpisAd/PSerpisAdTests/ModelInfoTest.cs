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

		public ModelInfoFoo(){
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

		public ModelInfoBar(){
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
			ModelInfo modelInfo = ModelInfoStore.Get (typeof(ModelInfoFoo));
			Assert.AreEqual ("modelinfofoo", modelInfo.TableName);
		}

		[Test ()]
		public void KeyPropertyInfo(){
			ModelInfo modelInfo =  ModelInfoStore.Get (typeof(ModelInfoFoo));
			Assert.IsNotNull (modelInfo.KeyPropertyInfo);
			Assert.AreEqual ("Id", modelInfo.KeyPropertyInfo.Name);
		}

		[Test()]
		public void KeyName(){
			ModelInfo modelInfo =  ModelInfoStore.Get (typeof(ModelInfoFoo));
			Assert.AreEqual ("id", modelInfo.KeyName);
		}

		[Test()]
		public void FieldpropertyInfos(){
			ModelInfo modelInfo =  ModelInfoStore.Get (typeof(ModelInfoFoo)); 
			PropertyInfo[] fieldPropertyInfo = modelInfo.FieldPropertyInfos;
			Assert.AreEqual (1, fieldPropertyInfo.Length);

			modelInfo =  ModelInfoStore.Get (typeof(ModelInfoBar));
			PropertyInfo[] fieldPropertyInfo2 = modelInfo.FieldPropertyInfos;
			Assert.AreEqual (2, fieldPropertyInfo2.Length);
		}

		[Test()]
		public void FieldNames(){
			ModelInfo modelInfo = ModelInfoStore.Get (typeof(ModelInfoFoo)); 
			string[] fieldName = modelInfo.FieldNames;
			Assert.Contains ("nombre", fieldName);
			Assert.AreEqual (1, fieldName.Length);

		}

		[Test()]
		public void InsertText(){
			ModelInfo modelInfo =  ModelInfoStore.Get (typeof(ModelInfoFoo)); 
			string select = modelInfo.InsertText;
			Assert.AreEqual ("insert into modelinfofoo (nombre) values ( @nombre ) ",select);
			ModelInfo modelInfo2 =  ModelInfoStore.Get (typeof(ModelInfoBar)); 
			string select2 = modelInfo2.InsertText;
			Assert.AreEqual ("insert into modelinfobar (nombre, precio) values ( @nombre, @precio ) ",select2);

		}

		[Test()]
		public void UpdateText(){
			ModelInfo modelInfo =  ModelInfoStore.Get (typeof(ModelInfoFoo)); 
			string update= modelInfo.UpdateText;
			Assert.AreEqual ("update modelinfofoo set nombre=@nombre where id=@id",update);
			ModelInfo modelInfo2 =  ModelInfoStore.Get (typeof(ModelInfoBar)); 
			string update2 = modelInfo2.UpdateText;
			Assert.AreEqual ("update modelinfobar set nombre=@nombre, precio=@precio where id=@id",update2);

		}

		[Test()]
		public void SelectText(){
			ModelInfo modelInfo =  ModelInfoStore.Get (typeof(ModelInfoFoo)); 
			string select= modelInfo.SelectText;
			Assert.AreEqual ("select nombre from modelinfofoo where id=@id",select);
			ModelInfo modelInfo2 =  ModelInfoStore.Get (typeof(ModelInfoBar)); 
			string select2 = modelInfo2.SelectText;
			Assert.AreEqual ("select nombre, precio from modelinfobar where id=@id",select2);

		}

		[Test()]
		public void SelectText2(){
			string[] fields = new string[]{"nombre"};
			ModelInfo modelInfo =   ModelInfoStore.Get(typeof(ModelInfoFoo),"id",fields); 
			string select= modelInfo.SelectText;
			Assert.AreEqual ("select nombre from modelinfofoo where id=@id",select);
			string[] fields2 = new string[]{"nombre","precio"};
			ModelInfo modelInfo2 =  ModelInfoStore.Get(typeof(ModelInfoBar),"id",fields2);  ; 
			string select2 = modelInfo2.SelectText;
			Assert.AreEqual ("select nombre, precio from modelinfobar where id=@id",select2);

		}
	}
}

