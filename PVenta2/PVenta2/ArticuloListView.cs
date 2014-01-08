using System;
using Gtk;
using System.Data;

namespace Serpis.Ad
{
	public class ArticuloListView : EntityListView
	{
		private Table tabla;
		private bool enBlanco;
		private Entry text;
		private Entry precio;
		private ComboBoxHelper combot;
		private VBox vbox;
		private string titulo;
		
		public ArticuloListView ()
		{
			helper=new TreeViewHelper(treeView,"select * from articulo");
			setActions();		
	
		}
		
		private void setActions(){
			
			Gtk.Action editAction=new Gtk.Action("editAction",null,null, Stock.Edit);
			actionGroup.Add(editAction);
			
			Gtk.Action addAction=new Gtk.Action("addAction",null,null, Stock.Add);
			actionGroup.Add(addAction);
			
			Gtk.Action refreshAction=new Gtk.Action("refreshAction",null,null, Stock.Refresh);
			actionGroup.Add(refreshAction);
			
			Gtk.Action removeAction=new Gtk.Action("removetAction",null,null, Stock.Remove);
			actionGroup.Add(removeAction);
			
			removeAction.Sensitive=false;
			editAction.Sensitive=false;
			
			treeView.Selection.Changed +=delegate{
				
				removeAction.Sensitive= treeView.Selection.CountSelectedRows() >0;
				editAction.Sensitive=treeView.Selection.CountSelectedRows() >0;
				
			};
			
			addAction.Activated+=delegate{
				creaVentanaArticulo();
			};
			
			removeAction.Activated +=delegate{
				
				delete();
			};
			
			refreshAction.Activated += delegate {
				refresh();
			};
			
			editAction.Activated+= delegate {
				edit();
			};
		}
		
		private void ventana(string titulo){
			window = new Window (titulo);
            window.SetDefaultSize (300, 200);
			window.SetPosition(WindowPosition.Center);
			vbox=new VBox();
			tabla=new Table(4,2,false);
			separaFilas(tabla,10);
			vbox.PackStart(tabla,false,false,0);
		}
		
		public void creaVentanaArticulo(){
			titulo="Añadir articulo";
			ventana(titulo);
			Label cat=new Label("Introduce el nombre del nuevo articulo: ");
			text= new Entry();
			ComboBox cb=new ComboBox();
			Label cat2=new Label("Selecciona la categoria: ");
			combot= new ComboBoxHelper(App.Instance.DbConnection,cb,"nombre","id",0,"categoria");
			tabla.Attach(cat,0,1,0,1);
			tabla.Attach(text,1,2,0,1);
			tabla.Attach(cat2,0,1,1,2);
			tabla.Attach(cb,1,2,1,2);
			Label pre=new Label("Introduce el precio del nuevo articulo: ");
			precio=new Entry();
			tabla.Attach(pre,0,1,2,3);
			tabla.Attach(precio,1,2,2,3);
			Button button=new Button("Añadir");
					button.Clicked +=delegate{
					añadirArticulo(App.Instance.DbConnection);
					if(!enBlanco){
						window.Destroy();
						refresh();
				}
			};
	
			tabla.Attach(button,1,2,3,4);
			window.Add(vbox);
			window.ShowAll();
		}
		
		public void añadirArticulo(IDbConnection mySqlConnection){
		
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
				executeNonQuery(string.Format(selectFormat,nombre,id,precio3));
				enBlanco=false;
				}
			}
		
		private void delete(){
		
				string id=helper.Id;
				string nombre=helper.Nombre;
				MessageDialog md2 = new MessageDialog
						(window, DialogFlags.Modal, MessageType.Warning, ButtonsType.YesNo,"¿Seguro que quieres borrarlo? \n Borrar: "+nombre); 
			
				ResponseType result = (ResponseType)md2.Run ();
				
			
				if (result == ResponseType.Yes){
					executeNonQuery(string.Format("Delete from articulo where id={0}",id));
					md2.Destroy();
					refresh();
				}
				else{
				md2.Destroy();	
				
				}
		}
		
		private void refresh(){
				ListStore listStore=helper.ListStore;
				int fieldCountArticulo=helper.getFieldCount();
				for (int i=0;i<fieldCountArticulo;i++){//elimina columnas
					treeView.RemoveColumn(treeView.GetColumn(0));
				}
				listStore.Clear();
				listStore=helper.ListStore;
				helper.actualizar(helper.IDbCommand,listStore);
		}
		
		private void edit(){
			titulo="Editar articulo";
			ventana(titulo);

			Label cat=new Label("Introduce el nuevo nombre del articulo: ");
			text= new Entry();
			
			string nombre="";
			int categoria=0;
			//double sprecio=0;
			decimal sprecio=0;
		
			IDbCommand dbCommand=App.Instance.DbConnection.CreateCommand();
			dbCommand.CommandText=string.Format ("select * from articulo where id={0}",helper.Id);
			
			IDataReader dataReader= dbCommand.ExecuteReader();
			while(dataReader.Read()){
				nombre=(string)dataReader.GetValue(1);
				categoria=(int)dataReader.GetValue(2);
				//sprecio=(double)dataReader.GetValue(3);
				sprecio=(decimal)dataReader.GetValue(3);
			}
			dataReader.Close();
			
			text.Text=nombre;
			
			ComboBox cb=new ComboBox();
			Label cat2=new Label("Selecciona la categoria: ");
			int id= categoria;
			combot= new ComboBoxHelper(App.Instance.DbConnection,cb,"nombre","id",id,"categoria");
			
			tabla.Attach(cat,0,1,0,1);
			tabla.Attach(text,1,2,0,1);
			tabla.Attach(cat2,0,1,1,2);
			tabla.Attach(cb,1,2,1,2);
			
			Label pre=new Label("Introduce el nuevo precio del articulo: ");
			precio=new Entry();
			precio.Text=sprecio.ToString();
			
			tabla.Attach(pre,0,1,2,3);
			tabla.Attach(precio,1,2,2,3);
	
			Button button=new Button();
			
					button.Image= Image.LoadFromResource("PVenta2.g.png");

					button.Clicked +=delegate{
				
					string precioNuevo=precio.Text;
					string nombreNuevo=text.Text;
					int categoriaNueva=combot.ID;
				
					if(nombreNuevo.Equals("") ||nombreNuevo.Equals(" ")){
						string alert="El nombre no puede estar vacio";
						 crearAlerta(alert);
						enBlanco=true;
					}else if(categoriaNueva==0){
						string alert="Elige una categoria";
						 crearAlerta(alert);
						enBlanco=true;
					}
					else if(precioNuevo.Equals("") || precioNuevo.Equals(" ")){
						string alert="El precio no puede estar vacio";
						 crearAlerta(alert);
						enBlanco=true;
					}
					else{
						if(!precioNuevo.Equals(sprecio)){
							double precio2=double.Parse(precio.Text);
							executeNonQuery(string.Format("update articulo set precio={0} where id={1}",precio2,helper.Id));
						}
						if(!nombreNuevo.Equals(nombre)){
							executeNonQuery(string.Format("update articulo set nombre='{0}' where id={1}",nombreNuevo,helper.Id));
						}
						if(categoriaNueva!=id){
							executeNonQuery(string.Format("update articulo set categoria={0} where id={1}",categoriaNueva,helper.Id));
						}
						enBlanco=false;
						}
					
					if(!enBlanco){
						window.Destroy();
						refresh();
				}
			};
	
			tabla.Attach(button,1,2,3,4);
			
			window.Add(vbox);
			window.ShowAll();
		}
		
		
	}
	
	
}

