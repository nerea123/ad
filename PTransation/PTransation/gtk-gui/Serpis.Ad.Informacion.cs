
// This file has been generated by the GUI designer. Do not modify.
namespace Serpis.Ad
{
	public partial class Informacion
	{
		private global::Gtk.VBox vbox3;
		private global::Gtk.Label label;
		private global::Gtk.HBox hbox1;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TreeView treeview3;
		private global::Gtk.Label label2;
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		private global::Gtk.TreeView treeview4;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Serpis.Ad.Informacion
			this.Name = "Serpis.Ad.Informacion";
			this.Title = global::Mono.Unix.Catalog.GetString ("Informacion");
			this.WindowPosition = ((global::Gtk.WindowPosition)(3));
			this.Gravity = ((global::Gdk.Gravity)(5));
			// Container child Serpis.Ad.Informacion.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.label = new global::Gtk.Label ();
			this.label.Name = "label";
			this.label.LabelProp = global::Mono.Unix.Catalog.GetString ("Televisores");
			this.vbox3.Add (this.label);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.label]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeview3 = new global::Gtk.TreeView ();
			this.treeview3.CanFocus = true;
			this.treeview3.Name = "treeview3";
			this.GtkScrolledWindow.Add (this.treeview3);
			this.hbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.GtkScrolledWindow]));
			w3.Position = 0;
			this.vbox3.Add (this.hbox1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox1]));
			w4.Position = 1;
			// Container child vbox3.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Comprador");
			this.vbox3.Add (this.label2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.label2]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeview4 = new global::Gtk.TreeView ();
			this.treeview4.CanFocus = true;
			this.treeview4.Name = "treeview4";
			this.GtkScrolledWindow1.Add (this.treeview4);
			this.vbox3.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.GtkScrolledWindow1]));
			w7.Position = 3;
			this.Add (this.vbox3);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 395;
			this.Show ();
		}
	}
}
