using System;
using Gtk;

using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		UiManagerHelper uiManagerHelper = new UiManagerHelper(UIManager);
		
		ArticuloListView articuloListView = new ArticuloListView();
		CategoriaListView categoriaListView = new CategoriaListView();
	

		notebook.AppendPage ( articuloListView, new Label("Articulos"));
		notebook.AppendPage ( categoriaListView, new Label("Categorias"));
		
		uiManagerHelper.SetActionGroup(articuloListView.ActionGroup);
		
		notebook.SwitchPage+=delegate{
			
			IEntityListView entityListView = (IEntityListView)notebook.CurrentPageWidget;
			uiManagerHelper.SetActionGroup(entityListView.ActionGroup);	
		};
		
	
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
