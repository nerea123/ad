using Gtk;
using System;

using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		UiManagerHelper uiManagerHelper = new UiManagerHelper(UIManager);
		
		CategoriaListView categoriaListView = new CategoriaListView();
		
		uiManagerHelper.SetActionGroup(categoriaListView.ActionGroup);
		
		notebook.AppendPage (categoriaListView, new Label("Categorías"));
		
		notebook.SwitchPage += delegate {
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
