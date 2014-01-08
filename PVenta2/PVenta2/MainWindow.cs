using System;
using Gtk;
using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		UiManagerHelper uiHelper= new UiManagerHelper(UIManager);
		ArticuloListView articulo=new ArticuloListView();
		CategoriaListView categoria=new CategoriaListView();
		
		notebook.AppendPage(articulo,new Label("ArticuloListView"));
		notebook.AppendPage(categoria,new Label("CategoriaListView"));
		
		uiHelper.SetActionGroup(articulo.ActionGroup);
		
			
		notebook.SwitchPage+=delegate{
			
			IEntityListView entityListView = (IEntityListView)notebook.CurrentPageWidget;
			uiHelper.SetActionGroup(entityListView.ActionGroup);	
		};
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
