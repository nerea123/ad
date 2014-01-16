using System;
using Gtk;
using Serpis.Ad;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;

public partial class MainWindow: Gtk.Window
{
	ComboBoxHelper combohelper;
	Tablas tabla;
	IDbCommand comand;
	decimal result;
	TreeViewHelper helper;
	TreeViewHelper helper2;
	Informacion inf;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		//WinAPI.SiempreEncima(this.Handle.ToInt32());
		//WinAPI.NoSiempreEncima(inf.Handle.ToInt32());
		rellenaVentana ();
		comand = App.Instance.DbConnection.CreateCommand ();

		this.miCombo.Changed += delegate {
			if(combohelper.ID==0)
				label5.Text="Precio:0,00€";
			else{
				actualiza();
			}
		};
		

		this.button.Clicked += delegate {
			if(compruebaCombo()){

				transaccion();
				refresh(inf.getTreviewComprador(),helper2);
				refresh(inf.getTreviewTelevisor(),helper);
			}

		};
	}

	private void actualiza(){

		comand.CommandText="select precio from televisor where id="+this.combohelper.ID+"";
		result=(decimal)comand.ExecuteScalar ();
		label5.Text ="Precio:"+ result.ToString()+"€";
		
	}

	private void rellenaVentana(){
		tabla = new Tablas ();
		combohelper = new ComboBoxHelper (App.Instance.DbConnection,this.miCombo,"articulo","id",0,"televisor");
		inf = new Informacion ();
		helper = new TreeViewHelper (inf.getTreviewTelevisor(), "select * from televisor");
		helper2 = new TreeViewHelper (inf.getTreviewComprador(), "select saldo from comprador");
		Show ();
	}

	protected void crearAlerta(string alert, MessageType m){
		MessageDialog md = new MessageDialog(this, DialogFlags.Modal, m, ButtonsType.Ok,alert); 
		md.Run();
		md.Destroy();
	}

	private bool compruebaCombo(){
		if (combohelper.ID == 0) {
			crearAlerta ("Debes seleccionar un televisor",MessageType.Warning);
			return false;
		}
		return true;
	}

	private void transaccion(){

		bool correcto = true;
		//empezamos la transaccion
		IDbTransaction trans=App.Instance.DbConnection.BeginTransaction();
		comand.Transaction = trans;

		try{

			comand.CommandText="Update televisor set cantidad=cantidad-"+getCantidadAComprar()+" where id="+this.combohelper.ID+"";
			comand.ExecuteNonQuery();
			result*=getCantidadAComprar();

			//aunque en la tabla comprador el saldo es de tipo decimal
			//si le restamos result siendo este tambien de tipo decimal da error, 
			//si pasamos result de decimal a double no da error ¿?
			comand.CommandText=string.Format("Update comprador set saldo=saldo-{0} where id=1 ",(double)result);
			comand.ExecuteNonQuery();
			trans.Commit();
		}catch (Exception e){
			crearAlerta ("Imposible realizar la transaccion",MessageType.Error);
			correcto = false;
			actualiza ();
			Console.WriteLine (e.Message);
			try{
				trans.Rollback();
			}catch(Exception ex){

			}
		}
		if(correcto)
			crearAlerta ("Transaccion realizada con exito", MessageType.Info);
	}

	private int getCantidadAComprar(){
		TreeIter iter;
		this.combo2.GetActiveIter(out iter);
		//ListStore liststore;
		int id=int.Parse((string)this.combo2.Model.GetValue(iter,0));
		return id;
	}

	private void refresh(TreeView treeView,TreeViewHelper helper){
		ListStore listStore=helper.ListStore;
		int fieldCountArticulo=helper.getFieldCount();
		for (int i=0;i<fieldCountArticulo;i++){//elimina columnas
			treeView.RemoveColumn(treeView.GetColumn(0));
		}
		listStore.Clear();
		listStore=helper.ListStore;
		helper.actualizar(helper.IDbCommand,listStore);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		tabla.borrar ();
		Application.Quit ();
		a.RetVal = true;
	}
}

