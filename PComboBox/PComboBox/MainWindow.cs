using System;
using Gtk;
using MySql.Data.MySqlClient;
using System.Data;
using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	//ListStore listStore;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		IDbConnection dbConnection= new MySqlConnection("Server=localhost;Database=dbprueba;" +
				"User Id=root;Password=sistemas");
		dbConnection.Open();

		
		ComboBoxHelper combo= new ComboBoxHelper(dbConnection,Combobox,"nombre","id",1,"categoria");
		//listStore=combo.ListStore;

//		TreeIter iter;
//		liststore.GetIterFirst(out iter);
//		do{
//			if((int)liststore.GetValue(iter,0)==elementoInicial){
//				break;
//			}
//		}
//		while(liststore.IterNext(ref iter));
		
		
		Combobox.Changed +=delegate{
			Console.WriteLine("El id es {0}",combo.ID);
		};
	}
	
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
