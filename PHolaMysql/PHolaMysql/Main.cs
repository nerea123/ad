using System;
using MySql.Data.MySqlClient;

namespace Serpis.Ad
{
	class MainClass
	{
		private static string[] getcolumNames(MySqlDataReader mysqlDataReader){
			string[] names=new string [mysqlDataReader.FieldCount];
			int numCol=mysqlDataReader.FieldCount;
			for(int i=0;i<numCol;i++){
				names[i]=mysqlDataReader.GetName(i);	
			}
			return names;
		}
		
		public static void Main (string[] args)
		{
			string connectionString="Server=localhost;Database=dbprueba;" +
				"User Id=root;Password=sistemas";
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			mySqlConnection.Open();
			
			MySqlCommand mysqlCommand=mySqlConnection.CreateCommand();
			mysqlCommand.CommandText= "select * from articulo";
			MySqlDataReader mysqlDataReader= mysqlCommand.ExecuteReader();
			
			//obtener cabeceras
			int numCol=mysqlDataReader.FieldCount;
			for(int i=0;i<numCol;i++){
				Console.Write(mysqlDataReader.GetName(i));
				Console.Write("   ");
			}
			Console.WriteLine();
			Console.WriteLine("****************");
			
			//obtener datos
			while(mysqlDataReader.Read()){
				for(int i=0;i<numCol;i++)
			 		Console.Write(mysqlDataReader.GetValue(i)+"    ");
				Console.WriteLine();
			}
			    
			Console.WriteLine();
			//obtener cabeceras mediante metodo
			string[] names=getcolumNames(mysqlDataReader);
			
			for(int i=0;i<names.Length;i++)
				Console.Write(names[i]+"   ");
			
			mysqlDataReader.Close();
			mySqlConnection.Close();
			Console.WriteLine ("ok");
		}
	}
}
