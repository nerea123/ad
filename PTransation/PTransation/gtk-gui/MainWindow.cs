
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox2;
	private global::Gtk.Label label2;
	private global::Gtk.HBox hbox3;
	private global::Gtk.ComboBox miCombo;
	private global::Gtk.Label label;
	private global::Gtk.ComboBox combo2;
	private global::Gtk.HBox hbox4;
	private global::Gtk.Label label5;
	private global::Gtk.Label LABEL3;
	private global::Gtk.Button button;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 30;
		// Container child vbox2.Gtk.Box+BoxChild
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("COMPRA TELEVISORES");
		this.vbox2.Add (this.label2);
		global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.label2]));
		w1.Position = 0;
		w1.Expand = false;
		w1.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox3 = new global::Gtk.HBox ();
		this.hbox3.Name = "hbox3";
		this.hbox3.Spacing = 30;
		// Container child hbox3.Gtk.Box+BoxChild
		this.miCombo = global::Gtk.ComboBox.NewText ();
		this.miCombo.Name = "miCombo";
		this.hbox3.Add (this.miCombo);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.miCombo]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.label = new global::Gtk.Label ();
		this.label.Name = "label";
		this.label.LabelProp = global::Mono.Unix.Catalog.GetString ("Cantidad");
		this.hbox3.Add (this.label);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.combo2 = global::Gtk.ComboBox.NewText ();
		this.combo2.AppendText (global::Mono.Unix.Catalog.GetString ("1"));
		this.combo2.AppendText (global::Mono.Unix.Catalog.GetString ("2"));
		this.combo2.AppendText (global::Mono.Unix.Catalog.GetString ("3"));
		this.combo2.AppendText (global::Mono.Unix.Catalog.GetString ("4"));
		this.combo2.AppendText (global::Mono.Unix.Catalog.GetString ("5"));
		this.combo2.Name = "combo2";
		this.combo2.Active = 0;
		this.hbox3.Add (this.combo2);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.combo2]));
		w4.Position = 2;
		w4.Expand = false;
		w4.Fill = false;
		this.vbox2.Add (this.hbox3);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox3]));
		w5.Position = 1;
		w5.Expand = false;
		w5.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox4 = new global::Gtk.HBox ();
		this.hbox4.Name = "hbox4";
		this.hbox4.Spacing = 6;
		// Container child hbox4.Gtk.Box+BoxChild
		this.label5 = new global::Gtk.Label ();
		this.label5.Name = "label5";
		this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Precio: 0,00€");
		this.hbox4.Add (this.label5);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.label5]));
		w6.Position = 0;
		w6.Expand = false;
		w6.Fill = false;
		// Container child hbox4.Gtk.Box+BoxChild
		this.LABEL3 = new global::Gtk.Label ();
		this.LABEL3.Name = "LABEL3";
		this.LABEL3.LabelProp = global::Mono.Unix.Catalog.GetString ("¡¡GRANDES OFERTAS!!");
		this.hbox4.Add (this.LABEL3);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.LABEL3]));
		w7.Position = 1;
		w7.Fill = false;
		// Container child hbox4.Gtk.Box+BoxChild
		this.button = new global::Gtk.Button ();
		this.button.CanFocus = true;
		this.button.Name = "button";
		this.button.UseUnderline = true;
		this.button.Label = global::Mono.Unix.Catalog.GetString ("Comprar");
		this.hbox4.Add (this.button);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.button]));
		w8.Position = 2;
		w8.Expand = false;
		w8.Fill = false;
		w8.Padding = ((uint)(9));
		this.vbox2.Add (this.hbox4);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox4]));
		w9.Position = 2;
		w9.Expand = false;
		w9.Fill = false;
		this.Add (this.vbox2);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 400;
		this.DefaultHeight = 165;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
