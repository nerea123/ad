using System;
using Gtk;

namespace Serpis.Ad
{
	public partial class Informacion : Gtk.Window
	{
		public Informacion () : 
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.ShowAll ();


		}

		public TreeView getTreviewTelevisor(){
			return treeview3;
		}

		public TreeView getTreviewComprador(){
			return treeview4;
		}
	}
}

