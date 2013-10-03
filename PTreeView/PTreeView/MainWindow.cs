using System;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
	static String connectionString="Server=localhost;Database=dbprueba;" +
				"User Id=root;Password=sistemas";
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		mySqlConnection.Open();
			
			MySqlCommand mysqlCommand=mySqlConnection.CreateCommand();
			mysqlCommand.CommandText= "select * from articulo";
			MySqlDataReader mysqlDataReader= mysqlCommand.ExecuteReader();
		
		int fieldcount=mysqlDataReader.FieldCount;
		string namecol;
		
		
		//añadir columna treewiew
		/*treeView.AppendColumn("id",new CellRendererText(),"text",0);
		treeView.AppendColumn("nombre",new CellRendererText(),"text",1);
		treeView.AppendColumn("categoria",new CellRendererText(),"text",2);
		treeView.AppendColumn("precio",new CellRendererText(),"text",3);*/
		for (int i=0;i<fieldcount;i++){
			namecol=mysqlDataReader.GetName(i);
			treeView.AppendColumn(namecol,new CellRendererText(),"text",i);
		}
		
		Type[] types=new Type[fieldcount];
		for (int i=0;i<fieldcount;i++)
			types[i]=typeof(string);
		
		//ListStore listStore=new ListStore(typeof(string),typeof(string));
		ListStore listStore=new ListStore(types);
		
		object[] values=new object[fieldcount];
		
		while(mysqlDataReader.Read()){
		for (int i=0;i<fieldcount;i++){
			values[i]=mysqlDataReader.GetValue(i).ToString();
			
			}
			Console.WriteLine(values[0]);
			listStore.AppendValues(values);
		}
		
		//listStore.AppendValues("1","uno");
		
		treeView.Model=listStore;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		mySqlConnection.Close();
	}
}
