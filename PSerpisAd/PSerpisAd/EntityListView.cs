using System;
using Gtk;
using System.Data;

namespace Serpis.Ad
	
{
	public class EntityListView : Gtk.Bin,IEntityListView
	{
		protected TreeView treeView;
		protected ActionGroup actionGroup;
		protected TreeViewHelper helper;
		protected Window window;
		
		public EntityListView ()
		{
			SizeRequested += delegate ( object o, SizeRequestedArgs args){
				if(Child != null)
					args.Requisition=Child.SizeRequest();
			};
				
			SizeAllocated += delegate ( object o, SizeAllocatedArgs args){
				if(Child != null)
					Child.Allocation = args.Allocation;
			};
			
			VBox vbox=new VBox();
			ScrolledWindow scroll= new ScrolledWindow();
			scroll.ShadowType=ShadowType.EtchedIn;
			treeView=new TreeView();
			
			scroll.Add(treeView);
			vbox.Add(scroll);
			Add(vbox);
			
			ShowAll();
			
			actionGroup= new ActionGroup("entityListView");

		}
		
		public ActionGroup ActionGroup{
				
				get{return actionGroup;}
			}
		
		
		protected static void executeNonQuery(string sql){
				executeNonQuery(App.Instance.DbConnection,sql);
			}
		
		protected static void executeNonQuery(IDbConnection dbConnection, string sql){
				
				IDbCommand dbCommand= dbConnection.CreateCommand();
				dbCommand.CommandText=sql;
				dbCommand.ExecuteNonQuery();
			}
		
		protected void separaFilas(Table tabla,int espacio){
	
				int row=(int)tabla.NRows;
				for(int i=0;i<row;i++){
				tabla.SetRowSpacing((uint)i,(uint)espacio);
			}			
		}
	
		protected void crearAlerta(string alert){
				MessageDialog md = new MessageDialog(window, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok,alert); 
				md.Run();
				md.Destroy();
	}
		
		protected object DbCommand(string selectSql){
			IDbCommand dbCommand=App.Instance.DbConnection.CreateCommand();
			dbCommand.CommandText= selectSql;
			object result= dbCommand.ExecuteScalar();
			return result;
		}
		
	}
}

