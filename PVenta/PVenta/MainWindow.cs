using System;
using Gtk;
using MySql.Data.MySqlClient;
using Serpis.Ad;
using System.Data;

public partial class MainWindow: Gtk.Window
{	
	static string conexion="Server=localhost;Database=dbprueba;" +
				"User Id=root;Password=sistemas";
	
	static MySqlConnection mySqlConnection = new MySqlConnection(conexion);
	TreeViewHelper tree1;
	TreeViewHelper tree2;
	Window win;
	HBox hbox;
	VBox vbox;
	VBox vbox2;
	ListStore listStore;
	TreeIter iter;
	Entry text;Entry precio;
	ComboBoxHelper combot;
	bool enBlanco;
	MessageDialog md;
	Table tabla;
	int fielCountCategoria=0;int fieldCountArticulo=0;
	private IDbCommand dbCommand=mySqlConnection.CreateCommand();
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		InfoAction.Sensitive=false;
		addAction.Sensitive=true;
		deleteAction.Sensitive=false;
		refreshAction.Sensitive=true;
		mySqlConnection.Open();
		string sql="Select * from categoria";
		string sql2="Select * from articulo";
		if(notebook1.GetTabLabelText(notebook1.GetNthPage(0)).Equals("Categoria")){
			
			tree1=new TreeViewHelper(treeview2,mySqlConnection,sql);
			
		}
		if(notebook1.GetTabLabelText(notebook1.GetNthPage(1)).Equals("Articulo")){
			
			tree2=new TreeViewHelper(treeview1,mySqlConnection,sql2);
			
		}
		
		InfoAction.Activated +=delegate{

				listStore=creaListStore();
				string op=listStore.GetValue(iter,1).ToString();
				
					MessageDialog md = new MessageDialog
						(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Close,op); 
					md.Run();
					md.Destroy();
		};
		
		addAction.Activated +=delegate{
		
				win = new Window ("Test");
            	win.SetDefaultSize (300, 100);
				win.SetPosition(WindowPosition.Center);
				hbox=new HBox(false,0);
				vbox=new VBox(false,0);
				vbox2=new VBox(false,0);
				
				win.Add(hbox);
				hbox.PackStart(vbox,false,false,0);
				
				//hbox.PackStart(vbox2,false,false,0);
			if(esCategoria()){
			
				creaVentanaCategoria();
	
			}
			else{
				win.SetDefaultSize (300, 200);
				creaVentanaArticulo();
			}
		};
		
		deleteAction.Activated +=delegate{
			if(esCategoria()){
				listStore=creaListStore();
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
			}
			else{
				md2.Destroy();	
				
			}
			}
			else{
			
				listStore=creaListStore();
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
				}
				else{
				md2.Destroy();	
				
				}
			}
		};
		
		notebook1.SwitchPage+=delegate{
			if(esCategoria()){
					bool isSelected=treeview2.Selection.GetSelected(out iter);
					if(isSelected){
						InfoAction.Sensitive=true;
						addAction.Sensitive=true;
						deleteAction.Sensitive=true;
						refreshAction.Sensitive=true;
					}
					else{
						InfoAction.Sensitive=false;
						//addAction.Sensitive=false;
						deleteAction.Sensitive=false;
						//refreshAction.Sensitive=false;
				}
			}
			else{
				bool isSelected=treeview1.Selection.GetSelected(out iter);
				if(isSelected){
					InfoAction.Sensitive=true;
					addAction.Sensitive=true;
					deleteAction.Sensitive=true;
					refreshAction.Sensitive=true;
				}
				else{
					InfoAction.Sensitive=false;
					//addAction.Sensitive=false;
					deleteAction.Sensitive=false;
					//refreshAction.Sensitive=false;
				}
			}
		};
		treeview1.Selection.Changed +=delegate{
			
			if(!esCategoria()){
				bool isSelected=treeview1.Selection.GetSelected(out iter);
				if(isSelected){
					InfoAction.Sensitive=true;
					//addAction.Sensitive=true;
					deleteAction.Sensitive=true;
					refreshAction.Sensitive=true;
				}
				else{
					InfoAction.Sensitive=false;
					//addAction.Sensitive=false;
					deleteAction.Sensitive=false;
					//refreshAction.Sensitive=false;
				}
			}
		};
		
			treeview2.Selection.Changed +=delegate{
			
				if(esCategoria()){
					bool isSelected=treeview2.Selection.GetSelected(out iter);
					if(isSelected){
						InfoAction.Sensitive=true;
						//addAction.Sensitive=true;
						deleteAction.Sensitive=true;
						refreshAction.Sensitive=true;
					}
					else{
						InfoAction.Sensitive=false;
						//addAction.Sensitive=false;
						deleteAction.Sensitive=false;
						//refreshAction.Sensitive=false;
				}
			}
		};
		
		refreshAction.Activated +=delegate{
			if(esCategoria()){
				listStore=tree1.ListStore;
				fielCountCategoria=tree1.getFieldCount();
				for (int i=0;i<fielCountCategoria;i++){//elimina columnas
					treeview2.RemoveColumn(treeview2.GetColumn(0));
				}
				listStore.Clear();
				listStore=tree1.ListStore;
				tree1.actualizar(dbCommand,listStore);
				
			}
			else{
				listStore=tree2.ListStore;
				fieldCountArticulo=tree2.getFieldCount();
				for (int i=0;i<fieldCountArticulo;i++){//elimina columnas
					treeview1.RemoveColumn(treeview1.GetColumn(0));
				}
				listStore.Clear();
				listStore=tree2.ListStore;
				tree2.actualizar(dbCommand,listStore);
			}
		};
	}
	
	public ListStore creaListStore(){
	
		if(esCategoria()){
			bool isSelected =treeview2.Selection.GetSelected(out iter);
			if(isSelected){
				return listStore=tree1.ListStore;
			}
			else{
				return listStore;
			}
		}
		else{
			bool isSelected =treeview1.Selection.GetSelected(out iter);
			if(isSelected)
				return listStore=tree2.ListStore;
			else
				return listStore;
		}
	}
	
	public void separaFilas(Table tabla,int espacio){
	
				int row=(int)tabla.NRows;
				for(int i=0;i<row;i++){
				tabla.SetRowSpacing((uint)i,(uint)espacio);
		}
	}
	public void creaVentanaCategoria(){
				
				tabla=new Table(2,2,false);
				separaFilas(tabla,5);
				vbox.PackStart(tabla,false,false,0);
				Label cat=new Label("Introduce el nombre de la nueva categoria: ");
				
				text= new Entry();
				//vbox.PackStart (cat, false, false, 10);
		
				tabla.Attach(cat,0, 1, 0, 1);
				tabla.Attach(text,1, 2, 0, 1);
				//tabla.Add(text);
				vbox2.PackStart (text, false, false, 10);
				
				Button button=new Button("Añadir");
				button.Clicked +=delegate{
				añadirCategoria(mySqlConnection);
				if(!enBlanco)
					win.Destroy();
		};
				tabla.Attach(button, 1, 2, 1, 2);
				//vbox.PackStart (button, false, false, 0);
				win.ShowAll ();
	}
	
	public void creaVentanaArticulo(){
		tabla=new Table(4,2,false);
		separaFilas(tabla,10);
		vbox.PackStart(tabla,false,false,0);
		Label cat=new Label("Introduce el nombre del nuevo articulo: ");
		text= new Entry();
		//vbox.PackStart (cat, false, false, 2);
		//vbox.PackStart (text, false, false, 0);
		ComboBox cb=new ComboBox();
		Label cat2=new Label("Selecciona la categoria: ");
		combot= new ComboBoxHelper(mySqlConnection,cb,"nombre","id",0,"categoria");
		//vbox.PackStart (cat2, false, false, 10);
		//vbox.PackStart (cb, false, false, 10);
		tabla.Attach(cat,0,1,0,1);
		tabla.Attach(text,1,2,0,1);
		tabla.Attach(cat2,0,1,1,2);
		tabla.Attach(cb,1,2,1,2);
		Label pre=new Label("Introduce el precio del nuevo articulo: ");
		precio=new Entry();
		//vbox.PackStart (pre, false, false, 10);
		//vbox.PackStart (precio, false, false, 10);
		tabla.Attach(pre,0,1,2,3);
		tabla.Attach(precio,1,2,2,3);
		Button button=new Button("Añadir");
				button.Clicked +=delegate{
				añadirArticulo(mySqlConnection);
				if(!enBlanco)
					win.Destroy();
		};
		//vbox.PackStart(button,false,false,0);
		tabla.Attach(button,1,2,3,4);
		win.ShowAll();
	}
	
	public void añadirArticulo(MySqlConnection mySqlConnection){
		if(!esCategoria()){
			string selectFormat="insert into articulo (nombre,categoria,precio) values('{0}',{1},{2})";
			string nombre=text.Text;
			int id=combot.ID;
			string precio2=precio.Text;
			if(nombre.Equals("") || nombre.Equals(" ")){
				string alert="El nombre no puede estar vacio";
				 crearAlerta(alert);
				enBlanco=true;
			}else if(id==0){
				string alert="Elige una categoria";
				 crearAlerta(alert);
				enBlanco=true;
			}
			else if(precio2.Equals("") || precio2.Equals(" ")){
				string alert="El precio no puede estar vacio";
				 crearAlerta(alert);
				enBlanco=true;
			}
			else{
				double precio3=double.Parse(precio2);
				MySqlCommand agregar=mySqlConnection.CreateCommand();
				agregar.CommandText=string.Format(selectFormat,nombre,id,precio3);
				agregar.ExecuteNonQuery();
				enBlanco=false;
			}
		}
	}
	
	public void crearAlerta(string alert){
		md = new MessageDialog
						(this, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok,alert); 
					md.Run();
					md.Destroy();
	}
	public void añadirCategoria(MySqlConnection mySqlConnection){
		if(esCategoria()){
			String texto=text.Text;
			
			if(texto.Equals("") || texto.Equals(" ")){
				string alert="El nombre no puede estar vacio";
				crearAlerta(alert);
				enBlanco=true;
			}
			
			else{
			   
				MySqlCommand agregar=mySqlConnection.CreateCommand();
				agregar.CommandText="insert into categoria (nombre) values('"+texto+"')";
				agregar.ExecuteNonQuery();
				enBlanco=false;
			}
		}
		
	}
	
	public void eliminarCategoria(){
		
	}
	
	public bool esCategoria(){
		return notebook1.CurrentPageWidget==notebook1.GetNthPage(0);
	}
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
		mySqlConnection.Close();
		
	}
}
