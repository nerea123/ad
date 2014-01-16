using System;

namespace PGestionMVC
{
	public partial class CategoriaView : Gtk.Window
	{
		public CategoriaView () : 
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

