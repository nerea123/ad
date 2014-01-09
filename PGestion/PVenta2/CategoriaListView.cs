using System;
using Gtk;
using System.Data;

namespace Serpis.Ad
{
	public class CategoriaListView : EntityListView
	{
		private Table tabla;
		private bool enBlanco=false;
		
		public CategoriaListView ()
		{
			helper=new TreeViewHelper(treeView,"select * from categoria");
			setAction();
			refresh();
		}
		
		private void setAction(){
			Gtk.Action editAction=new Gtk.Action("editAction",null,null, Stock.Edit);
			actionGroup.Add(editAction);
			Gtk.Action refreshAction=new Gtk.Action("refreshAction",null,null, Stock.Refresh);
			actionGroup.Add(refreshAction);
			
			editAction.Sensitive=false;
			
			treeView.Selection.Changed +=delegate{
				editAction.Sensitive=treeView.Selection.CountSelectedRows() >0;
				
			};
			
			refreshAction.Activated +=delegate{
				refresh();
			};
			
			editAction.Activated +=delegate{
				edit();
			};
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
			creaVentana();
			
			Label cat=new Label("Introduce el nuevo nombre de categoría: ");
			Entry text= new Entry();
			string nombre=string.Format("select nombre from categoria where id={0}",helper.Id);
			nombre=(string)DbCommand(nombre);
			text.Text=nombre;
			tabla.Attach(cat,0,1,0,1);
			tabla.Attach(text,1,2,0,1);
			
			Button button=new Button();
					button.Image= Image.LoadFromResource("PVenta2.g.png");
					button.Clicked +=delegate{
					string nombreNuevo=text.Text;
					
					if(nombreNuevo.Equals("") || nombreNuevo.Equals(" ")){
						string alert="El nombre no puede estar vacio";
						crearAlerta(alert);
						enBlanco=true;
					}
					else{
					if(!nombreNuevo.Equals(nombre)){
							executeNonQuery(string.Format("update categoria set nombre='{0}' where id={1}",nombreNuevo,helper.Id));
						}
					enBlanco=false;
				}
					if(!enBlanco){
						window.Destroy();
						refresh();
				}
			};
			
			tabla.Attach(button,1,2,1,2);
			window.ShowAll();
		}
		
		private void creaVentana(){
			window=new Window("Editar categoria");
			window.SetDefaultSize (300, 100);
			window.SetPosition(WindowPosition.Center);
			VBox vbox=new VBox();
			tabla=new Table(2,2,false);
			separaFilas(tabla,10);
			vbox.PackStart(tabla,false,false,0);
			window.Add(vbox);
		}
	
	}
}

