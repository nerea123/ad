using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Serpis.Ad
{
	[TestFixture ()]
	internal class ModelHelperFoo
	{

		public ModelHelperFoo(int id, string nombre){
			this.Id = id;
			this.Nombre = nombre;
		}

		[Key]
		public int Id {get;set;}

		[Field]
		public string Nombre {get;set;}
	}

	[TestFixture ()]
	internal class ModelHelperBar

	{
		public ModelHelperBar(int id, string nombre, decimal precio){
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


	public class ModelHelperTest
	{

		public void GetSelect ()
		{
			string selectText;
			string expected;

			selectText = ModelHelper.GetSelect (typeof(ModelHelperFoo));
			expected = "select nombre from modelhelperfoo where id=";
			Assert.AreEqual (expected, selectText);

			selectText = ModelHelper.GetSelect (typeof(ModelHelperBar));
			expected = "select nombre, precio from modelhelperbar where id=";
			Assert.AreEqual (expected, selectText);


		}

		//[Test ()]
		public void SaveTest(){
			string selectText;
			string expected;


			ModelHelperFoo foo = new ModelHelperFoo (3, "cat1");
			selectText = ModelHelper.SaveTest (foo);
			expected = "update modelhelperfoo set nombre=cat1 where id=3";
			Assert.AreEqual (expected, selectText);

			ModelHelperBar bar= new ModelHelperBar(3, "cat1",10);
//			campos.Add ("10");
			selectText = ModelHelper.SaveTest (bar);
			expected = "update modelhelperbar set nombre=cat1, precio=10 where id=3";
			Assert.AreEqual (expected, selectText);
		}

		[Test ()]
		public void GetUpdate(){

			string selectText;
			string expected;

			selectText = ModelHelper.GetUpdate(typeof(ModelHelperFoo));
			expected = "update modelhelperfoo set nombre=@nombre where id=@id";
			Assert.AreEqual (expected, selectText);

			selectText = ModelHelper.GetUpdate(typeof(ModelHelperBar));
			expected = "update modelhelperbar set nombre=@nombre, precio=@precio where id=@id";
			Assert.AreEqual (expected, selectText);
		}

		[Test ()]
		public void GetInsert(){

			string selectText;
			string expected;

			selectText = ModelHelper.GetInsert(typeof(ModelHelperFoo));
			expected = "insert into modelhelperfoo (nombre) values ( @nombre ) ";
			Assert.AreEqual (expected, selectText);

			selectText = ModelHelper.GetInsert(typeof(ModelHelperBar));
			expected = "insert into modelhelperbar (nombre, precio) values ( @nombre, @precio ) ";
			Assert.AreEqual (expected, selectText);
		}
	}
}

