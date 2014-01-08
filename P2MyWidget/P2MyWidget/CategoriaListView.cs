using Gtk;
using System;
using System.Data;
using Serpis.Ad;
using MySql.Data.MySqlClient;

namespace Serpis.Ad
{
	public class CategoriaListView : MyWidget
	{
		bool enBlanco;
		Entry text;
		int fielCountCategoria;
		TreeViewHelper helper;
		
		public CategoriaListView (IDbConnection dbConnection) : base (dbConnection)
		{
			App.Instance.SQL="select * from categoria";
			
		    helper=new TreeViewHelper(TreeView,dbConnection,App.Instance.SQL);
			
			
		
		}
		
		public override void New ()
		{
			Console.WriteLine("Categoria.New()");
			App.Instance.Wind=new Window("Añade categoria");
			App.Instance.Wind.SetDefaultSize(300,100);
			App.Instance.Wind.SetPosition(WindowPosition.Center);
			App.Instance.hb=new HBox(false,0);
			App.Instance.vb=new VBox(false,0);
			App.Instance.Wind.Add(App.Instance.hb);
			App.Instance.hb.Add(App.Instance.vb);
			//App.Instance.hb.PackStart(App.Instance.vb,false,false,0);

			creaVentanaCategoria();
		}
		
		public override void Refresh(){
				Console.WriteLine("Categoria.New()");
				App.Instance.Liststore=helper.ListStore;
				fielCountCategoria=helper.getFieldCount();
				Console.Write(fielCountCategoria);
				for (int i=0;i<fielCountCategoria;i++){//elimina columnas
					TreeView.RemoveColumn(TreeView.GetColumn(0));
				}
				App.Instance.Liststore.Clear();
				App.Instance.Liststore=helper.ListStore;
				helper.actualizar(helper.IDbCommand,App.Instance.Liststore);
		}
		
		
	
	public void creaVentanaCategoria(){
				
				App.Instance.Tabla=new Table(2,2,false);
				separaFilas(App.Instance.Tabla,5);
				App.Instance.vb.PackStart(App.Instance.Tabla,false,false,0);
				Label cat=new Label("Introduce el nombre de la nueva categoria: ");
				
				text= new Entry();
				
		
				App.Instance.Tabla.Attach(cat,0, 1, 0, 1);
				App.Instance.Tabla.Attach(text,1, 2, 0, 1);
				
				
				
				Button button=new Button("Añadir");
				button.Clicked +=delegate{
				añadirCategoria(dbConnection);
				if(!enBlanco)
					App.Instance.Wind.Destroy();
		};
				App.Instance.Tabla.Attach(button, 1, 2, 1, 2);
				
				App.Instance.Wind.ShowAll ();
	}
	
	public void	añadirCategoria (IDbConnection dbConnection){
			String texto=text.Text;
			
			if(texto.Equals("") || texto.Equals(" ")){
				string alert="El nombre no puede estar vacio";
				App.Instance.crearAlerta(alert);
				enBlanco=true;
			}
			
			else{
			   
				IDbCommand agregar=App.Instance.DbConnection.CreateCommand();
				agregar.CommandText="insert into categoria (nombre) values('"+texto+"')";
				agregar.ExecuteNonQuery();
				enBlanco=false;
			}
		}
		
	
	
	}
}

