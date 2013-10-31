using System;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow: Gtk.Window
{	
		static string conexion="Server=localhost;Database=dboctubre;" +
				"User Id=root;Password=sistemas";
	MySqlConnection mySqlConnection = new MySqlConnection(conexion);
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		mySqlConnection.Open();
		MySqlCommand mySqlCommand=mySqlConnection.CreateCommand();
		mySqlCommand.CommandText= "select * from categoria";
		MySqlDataReader mysqlDataReader= mySqlCommand.ExecuteReader();
		int fieldcount=mysqlDataReader.FieldCount;
		
		creaColumnas(fieldcount,mysqlDataReader);
		ListStore listStore=new ListStore(creaTipos(fieldcount));
		rellenar(fieldcount,listStore,mysqlDataReader);
		mysqlDataReader.Close();
		
		removeAction.Sensitive=false;
		treeView.Model=listStore;
		TreeIter iter;
		
		treeView.Selection.Changed+=delegate{
			bool isSelected=treeView.Selection.GetSelected(out iter);
			if(isSelected)
				removeAction.Sensitive=true;
			else
				removeAction.Sensitive=false;
	};
		
		removeAction.Activated +=delegate{
			string nombre=listStore.GetValue(iter,1).ToString();
			MessageDialog md2 = new MessageDialog
						(this, DialogFlags.Modal, MessageType.Warning, ButtonsType.YesNo,"¿Seguro que quieres borrarlo? \n Borrar: "+nombre); 
			
			ResponseType result = (ResponseType)md2.Run ();
			string op=listStore.GetValue(iter,0).ToString();
			
			if (result == ResponseType.Yes){
				MySqlCommand delete=mySqlConnection.CreateCommand();
				delete.CommandText= "Delete from categoria where id="+op+"";
				delete.ExecuteNonQuery();
				md2.Destroy();
				
				for (int i=0;i<fieldcount;i++){//elimina columnas
					treeView.RemoveColumn(treeView.GetColumn(0));
				}
				listStore.Clear();//vacia el modelo
				//volvemos a mostrar treview actualizado
				actualizar(mySqlCommand,listStore);
		
			}
			else{
				md2.Destroy();	
				
			}
		};
	}
	
	public void creaColumnas(int fieldcount,MySqlDataReader mysqlDataReader){
		
		string namecol;
		for (int i=0;i<fieldcount;i++){
			namecol=mysqlDataReader.GetName(i);
			treeView.AppendColumn(namecol,new CellRendererText(),"text",i);
		}
	}
	
	public Type[] creaTipos(int fieldcount){
	
		Type[] types=new Type[fieldcount];
		for (int i=0;i<fieldcount;i++)
			types[i]=typeof(string);
		return types;
	}
	
	public void rellenar(int fieldcount,ListStore listStore,MySqlDataReader mysqlDataReader){
		object[] values=new object[fieldcount];
		
		while(mysqlDataReader.Read()){
			for (int i=0;i<fieldcount;i++){
				values[i]=mysqlDataReader.GetValue(i).ToString();
				
			}
			
			listStore.AppendValues(values);
		}	
	}
	
	public void actualizar(MySqlCommand mysqlCommand, ListStore listStore){
				mysqlCommand.CommandText= "select * from categoria";
				MySqlDataReader mysqlDataReader= mysqlCommand.ExecuteReader();
				int fieldcount2=mysqlDataReader.FieldCount;
				creaColumnas(fieldcount2,mysqlDataReader);
				rellenar(fieldcount2,listStore,mysqlDataReader);
		
				mysqlDataReader.Close();
	
	}
	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		mySqlConnection.Close();
	}
}
