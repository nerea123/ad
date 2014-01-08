using System;
using Gtk;
using System.Data;
namespace Serpis.Ad
{
	public class ArticuloListView : EntityListView
	{
		
		public ArticuloListView ()
		{
			string sql="select * from articulo";
			 helper=new TreeViewHelper(treeView,sql);
			
			Gtk.Action editAction=new Gtk.Action("editAction",null,null, Stock.Edit);
			actionGroup.Add(editAction);
			
			Gtk.Action deleteAction=new Gtk.Action("deleteAction",null,null, Stock.Delete);
			actionGroup.Add(deleteAction);
			
			deleteAction.Activated+=delegate{
				executeNonQuery(string.Format("delete from articulo where id={0}",helper.Id));	
			};
			
			Gtk.Action newAction=new Gtk.Action("newAction",null,null, Stock.New);
			actionGroup.Add(newAction);
			
		newAction.Activated+=delegate{
				
				executeNonQuery(string.Format("insert into articulo (nombre) values ('{0}')",DateTime.Now));
			};
			
			treeView.Selection.Changed += delegate {
				editAction.Sensitive=treeView.Selection.CountSelectedRows() >0;
				string id=getId();
				Console.WriteLine(id);
			};
			
		
		}
		
			private static void executeNonQuery(string sql){
				executeNonQuery(App.Instance.DbConnection,sql);
			}
		
			private static void executeNonQuery(IDbConnection dbConnection, string sql){
				
				IDbCommand dbCommand= dbConnection.CreateCommand();
				dbCommand.CommandText=sql;
				dbCommand.ExecuteNonQuery();
			}
	
	}
}

