using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Serpis.Ad
{
	class MainClass
	{
		private static IEnumerable<string> getcolumNames2(MySqlDataReader mysqlDataReader){
			int numCol=mysqlDataReader.FieldCount;
			List<string> columNames= new List<string>();
			for(int i=0;i<numCol;i++)
				columNames.Add(mysqlDataReader.GetName(i));
			return columNames;
		}
		
		private static IEnumerable<string> getcolumNames(MySqlDataReader mysqlDataReader){
			string[] names=new string [mysqlDataReader.FieldCount];
			int numCol=mysqlDataReader.FieldCount;
			for(int i=0;i<numCol;i++){
				names[i]=mysqlDataReader.GetName(i);	
			}
			return names;
		}
		
		private static string getline(MySqlDataReader mysqlDataReader){
			int col=mysqlDataReader.FieldCount;
			string linea=" ";
			for(int i=0;i<col;i++){
				if (mysqlDataReader.GetValue(i) is DBNull)
							linea+="null    ";
					else
			 				linea+=mysqlDataReader.GetValue(i)+"    ";
			}
				
			return linea;
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
			Console.WriteLine("*********************************");
			
			//obtener datos
			while(mysqlDataReader.Read()){
				/*for(int i=0;i<numCol;i++){
					
						if (mysqlDataReader.GetValue(i) is DBNull)
							Console.Write("null"+"    ");
					else
			 				Console.Write(mysqlDataReader.GetValue(i)+"    ");
				//
				}*/
				Console.WriteLine (getline( mysqlDataReader));
				Console.WriteLine();
			}
			    
			Console.WriteLine();
			//obtener cabeceras mediante metodo
			string[] names=(string[])getcolumNames(mysqlDataReader);
			
			for(int i=0;i<names.Length;i++)
				Console.Write(names[i]+"   ");
			
			Console.WriteLine();
			Console.WriteLine(string.Join("  ",getcolumNames(mysqlDataReader)));
			
			
			mysqlDataReader.Close();
			mySqlConnection.Close();
			Console.WriteLine ("ok");
		}	
	}
}
