using Gtk;
using System;
using System.Data;

namespace Serpis.Ad
{
	public class ArticuloListView : MyWidget
	{
		ComboBoxHelper combot;
		Entry precio;
		Entry text;
		bool enBlanco;
		int fielCountCategoria;
		TreeViewHelper helper;
		
		public ArticuloListView (IDbConnection dbConnection) : base (dbConnection)
		{
			App.Instance.SQL="select * from articulo";
			
			helper=new TreeViewHelper(TreeView,dbConnection,App.Instance.SQL);
		
		}
		
		
		public override void New ()
		{
			Console.WriteLine("ArticuloListView.New()");
			App.Instance.Wind=new Window("Añade articulo");
			App.Instance.Wind.SetDefaultSize(300,200);
			App.Instance.Wind.SetPosition(WindowPosition.Center);
			App.Instance.hb=new HBox(false,0);
			App.Instance.vb=new VBox(false,0);
			App.Instance.Wind.Add(App.Instance.hb);
			App.Instance.hb.PackStart(App.Instance.vb,false,false,0);

			creaVentanaArticulo();
		}
		
		
		
			public override void Refresh(){
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
		
		
		
		
		public void creaVentanaArticulo(){
			App.Instance.Tabla=new Table(4,2,false);
			separaFilas(App.Instance.Tabla,10);
			App.Instance.vb.PackStart(App.Instance.Tabla,false,false,0);
			Label cat=new Label("Introduce el nombre del nuevo articulo: ");
			text= new Entry();
			ComboBox cb=new ComboBox();
			Label cat2=new Label("Selecciona la categoria: ");
			combot= new ComboBoxHelper(dbConnection,cb,"nombre","id",0,"categoria");
			
			App.Instance.Tabla.Attach(cat,0,1,0,1);
			App.Instance.Tabla.Attach(text,1,2,0,1);
			App.Instance.Tabla.Attach(cat2,0,1,1,2);
			App.Instance.Tabla.Attach(cb,1,2,1,2);
			Label pre=new Label("Introduce el precio del nuevo articulo: ");
			precio=new Entry();
			
			App.Instance.Tabla.Attach(pre,0,1,2,3);
			App.Instance.Tabla.Attach(precio,1,2,2,3);
			Button button=new Button("Añadir");
					button.Clicked +=delegate{
					añadirArticulo(dbConnection);
					if(!enBlanco)
						App.Instance.Wind.Destroy();
			};
			
			App.Instance.Tabla.Attach(button,1,2,3,4);
			App.Instance.Wind.ShowAll();
	}
		
		public void añadirArticulo(IDbConnection dbConnection){
		
			string selectFormat="insert into articulo (nombre,categoria,precio) values('{0}',{1},{2})";
			string nombre=text.Text;
			int id=combot.ID;
			string precio2=precio.Text;
			if(nombre.Equals("") || nombre.Equals(" ")){
				string alert="El nombre no puede estar vacio";
				 App.Instance.crearAlerta(alert);
				enBlanco=true;
			}else if(id==0){
				string alert="Elige una categoria";
				 App.Instance.crearAlerta(alert);
				enBlanco=true;
			}
			else if(precio2.Equals("") || precio2.Equals(" ")){
				string alert="El precio no puede estar vacio";
				 App.Instance.crearAlerta(alert);
				enBlanco=true;
			}
			else{
				double precio3=double.Parse(precio2);
				IDbCommand agregar=dbConnection.CreateCommand();
				agregar.CommandText=string.Format(selectFormat,nombre,id,precio3);
				agregar.ExecuteNonQuery();
				enBlanco=false;
			}
		
	}
		
	}
}

