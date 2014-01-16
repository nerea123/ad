using System;
using Gtk;
using Serpis.Ad;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		UiManagerHelper uiManagerHelper = new UiManagerHelper (UIManager);

		CategoriaListview categoriaListView = new CategoriaListview ();

		uiManagerHelper.SetActionGroup (categoriaListView.ActionGroup);

		notebook.AppendPage (categoriaListView,new Label("Categoria"));
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
