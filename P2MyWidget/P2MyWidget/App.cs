using System;
using System.Data;
using Gtk;
namespace Serpis.Ad
{
	public class App
	{
		private static App instance = new App();
		
		public static App Instance {
			get {return instance;}	
		}
		
		private IDbConnection dbConnection;
		
		private string sql;
		
		private Window wind;
		
		private HBox hbox;
		
		private VBox vbox;
		
		private Table tabla;
		
		private ListStore liststore;
		
		public ListStore Liststore{
			get {return liststore;}
			set 
			{
				liststore= value;
				
		}
	}
		
		public void crearAlerta(string alert){
		MessageDialog md = new MessageDialog(wind, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok,alert); 
					md.Run();
					md.Destroy();
	}
		
			public Table Tabla{
			get {return tabla;}
			set 
			{
				tabla= value;
				
		}
	}
		
		public HBox hb{
			get {return hbox;}
			set 
			{
				hbox= value;
				
		}
	}
		
			public VBox vb{
			get {return vbox;}
			set 
			{
				vbox= value;
				
		}
	}
		
		public Window Wind{
			get {return wind;}
			set 
			{
				wind= value;
				
		}
	}
	
		
		public string SQL{
			get {return sql;}
			set 
			{
				sql = value;
				
		}
	}
		
		public IDbConnection DbConnection{
			get {return dbConnection;}
			set 
			{
				dbConnection = value;
				if(dbConnection.State == ConnectionState.Closed){
					dbConnection.Open();	
				}
			}
		}
		
	}
}

