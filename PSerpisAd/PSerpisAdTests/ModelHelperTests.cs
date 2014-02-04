using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Serpis.Ad
{
	[TestFixture ()]
	internal class ModelHelperFoo
	{
		[Key]
		public int Id {get;set;}

		[Field]
		public string Nombre {get;set;}
	}

	[TestFixture ()]
	internal class ModelHelperBar

		
	{
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

		[Test ()]
		public void SaveTest(){
			string selectText;
			string expected;

			List<string> campos=new List<string>();
			campos.Add ("sara");
			string id = "3";

			selectText = ModelHelper.SaveTest ();
			expected = "update modelhelperfoo set nombre=sara where id=3";
			Assert.AreEqual (expected, selectText);

			campos.Add ("10");
			selectText = ModelHelper.SaveTest ();
			expected = "update modelhelperbar set nombre=sara, precio=10 where id=3";
			Assert.AreEqual (expected, selectText);
		}
	}
}
