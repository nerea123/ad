using System;
using Gtk;
using System.Collections.Generic;
using System.Data;

namespace Serpis.Ad
{
	public class TreeViewHelper
	{
		private int fieldcount;
		private String selectSql;
		private ListStore listStore;
		private TreeView treeView;
		private IDbCommand dbCommand;
		//private TreeIter iter;
		private string id;
		private string nombre;
		
		public TreeViewHelper (TreeView treeView,String selectSql) : 
			this(treeView,App.Instance.DbConnection,selectSql)
		{
			
		}
		
		public TreeViewHelper (TreeView treeView,IDbConnection dbConnection,String selectSql)
		{
			Console.WriteLine("Ctor Treeviewhelper");
			this.treeView=treeView;
			this.selectSql=selectSql;
			dbCommand=dbConnection.CreateCommand();
			dbCommand.CommandText= selectSql;
			IDataReader dataReader= dbCommand.ExecuteReader();
		
			fieldcount=dataReader.FieldCount;
			
			creaColumnas(fieldcount,dataReader);

			listStore=new ListStore(creaTipos(fieldcount));
			
			rellenar(fieldcount,listStore,dataReader);
			
			dataReader.Close();
			
			
			treeView.Model=listStore;
			
		}
		
		private int idColumIndex=0;
		public int IdColumIndex{
			get{return idColumIndex;}
			set{idColumIndex=value;}
		}
		
		  public string Id{
			get {
				TreeIter iter;
				treeView.Selection.GetSelected(out iter);
				id=listStore.GetValue(iter,0).ToString();
				if(id!=null)
					return id;
				//else 
					return string.Empty;
			}
		}
		 
		
		  public string Nombre{
			get {
				TreeIter iter;
				treeView.Selection.GetSelected(out iter);
				nombre=listStore.GetValue(iter,1).ToString();
				if(id!=null)
					return nombre;
				//else 
					return string.Empty;
			}
		}
		
		
		public int getFieldCount(){
			return fieldcount;
		}

		public ListStore ListStore{
			get {return listStore;}
		}
		
		public IDbCommand IDbCommand{
			get {return dbCommand;}
		}
		
		public void creaColumnas(int fieldcount,IDataReader dataReader){
		
		
		string namecol;
		for (int i=0;i<fieldcount;i++){
			namecol=dataReader.GetName(i);
			treeView.AppendColumn(namecol,new CellRendererText(),"text",i);
		}
	}
	
		public Type[] creaTipos(int fieldcount){
	
			
			Type[] types=new Type[fieldcount];
			for (int i=0;i<fieldcount;i++)
				types[i]=typeof(string);
			return types;
	}
	
		public void rellenar(int fieldcount,ListStore listStore,IDataReader dataReader){
			object[] values=new object[fieldcount];
			
			while(dataReader.Read()){
				for (int i=0;i<fieldcount;i++){
					values[i]=dataReader.GetValue(i).ToString();
					
				}
				
				listStore.AppendValues(values);
			}	
		}
		
		public void actualizar(IDbCommand dbCommand, ListStore listStore){
			
				dbCommand.CommandText= selectSql;
				IDataReader dataReader= dbCommand.ExecuteReader();
				int fieldcount2=dataReader.FieldCount;
				creaColumnas(fieldcount2,dataReader);
				rellenar(fieldcount2,listStore,dataReader);
		
				dataReader.Close();
	
	}

		public void refresh(){

			for (int i=0;i<getFieldCount();i++){//elimina columnas
				treeView.RemoveColumn(treeView.GetColumn(0));
			}
			listStore.Clear();

			this.actualizar(IDbCommand,listStore);
		}
		
		
		
	
	}
}

