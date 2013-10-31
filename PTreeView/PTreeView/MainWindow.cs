using System;
using Gtk;
using MySql.Data.MySqlClient;
using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	static String connectionString="Server=localhost;Database=dbprueba;" +
				"User Id=root;Password=sistemas";
			MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
			
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		mySqlConnection.Open();
			

		String selectSql="select * from articulo";
		TreeViewHelper treeViewHelper=new TreeViewHelper(treeView,mySqlConnection,selectSql);
		ListStore listStore=treeViewHelper.ListStore;
		editAction.Sensitive =false;
		removeAction.Sensitive=false;
			
		TreeIter iter;

		treeView.Selection.Changed += delegate{
		
 			bool isSelected =treeView.Selection.GetSelected(out iter);//true si hay algo seleccionado
			//Console.WriteLine(listStore.GetPath(iter)); //devuelve la fila que selecciono
			if(isSelected){
				editAction.Sensitive =true;
				removeAction.Sensitive=true;
				
			}
			else{
				editAction.Sensitive =false;
				removeAction.Sensitive=false;
			}
};
		editAction.Activated +=delegate{
					if(treeView.Selection.CountSelectedRows() == 0)//igual a 0 si no hay nada seleccionado
						return;
					string op=listStore.GetValue(iter,1).ToString();//tambien podriamos haberlo hecho con object
					MessageDialog md = new MessageDialog
						(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close,op); 
					md.Run();
					md.Destroy();
				//Console.WriteLine(listStore.GetValue(iter,1));
				};
			removeAction.Activated +=delegate{
			string nombre=listStore.GetValue(iter,1).ToString();
			MessageDialog md2 = new MessageDialog
						(this, DialogFlags.Modal, MessageType.Warning, ButtonsType.YesNo,"¿Seguro que quieres borrarlo? \n Borrar: "+nombre); 
			
			ResponseType result = (ResponseType)md2.Run ();
			string op=listStore.GetValue(iter,0).ToString();
			
			if (result == ResponseType.Yes){
				MySqlCommand delete=mySqlConnection.CreateCommand();
				delete.CommandText= "Delete from articulo where id="+op+"";
				delete.ExecuteNonQuery();
				md2.Destroy();
				int fieldcount=treeViewHelper.getFieldCount();
				for (int i=0;i<fieldcount;i++){//elimina columnas
					treeView.RemoveColumn(treeView.GetColumn(0));
					
				}
			listStore.Clear();//vacia el modelo
				//volvemos a mostrar treview actualizado
			treeViewHelper.actualizar(treeViewHelper.IDbCommand,listStore);
		
			}
			else{
				md2.Destroy();	
				
			}
		};
	}

	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		mySqlConnection.Close();
	}
}
	
