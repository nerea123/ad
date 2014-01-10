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

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		rellenaVentana ();
		comand = App.Instance.DbConnection.CreateCommand ();

		this.miCombo.Changed += delegate {
			comand.CommandText="select precio from televisor where id="+this.combohelper.ID+"";
			decimal result=(decimal)comand.ExecuteScalar ();
			label5.Text ="Precio:"+ result.ToString()+"€";
		};
		this.button.Activated += delegate {
			if(compruebaCombo()){


			}

		};
	}



	private void rellenaVentana(){
		tabla = new Tablas ();
		combohelper = new ComboBoxHelper (App.Instance.DbConnection,this.miCombo,"articulo","id",0,"televisor");
		
	}

	protected void crearAlerta(string alert){
		MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok,alert); 
		md.Run();
		md.Destroy();
	}

	private bool compruebaCombo(){
		if (combohelper.ID == 0) {
			crearAlerta ("Debes seleccionar un televisor");
			return false;
		}
		return true;
	}

	private void transaccion(){

		//empezamos la transaccion
		IDbTransaction trans=App.Instance.DbConnection.BeginTransaction();
		comand.Transaction = trans;

		try{

			comand.CommandText="";
			trans.Commit();
		}catch (Exception e){

			try{
				trans.Rollback();
			}catch(Exception ex){

			}
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		tabla.borrar ();
		Application.Quit ();
		a.RetVal = true;
	}
}
