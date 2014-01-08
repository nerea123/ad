using System;
using MySql.Data.MySqlClient;
using System.Data;
using Gtk;

namespace Serpis.Ad
{
	public class ComboBoxHelper
	{
		ComboBox comboBox;
		
		ListStore liststore;
		int id;
		TreeIter iter;
		private const string selectFormat="select {0}, {1} from {2}";
		public ComboBoxHelper (IDbConnection dbConnection,ComboBox comboBox,string nombre, string id,int elementoInicial,string tabla)
		{
			this.comboBox=comboBox;
			
			IDbCommand dbCommand= dbConnection.CreateCommand();
			dbCommand.CommandText = string.Format(selectFormat,id,nombre,tabla);
			
			IDataReader dbDataReader= dbCommand.ExecuteReader();
			
//			CellRendererText cell1=new CellRendererText();
//			comboBox.PackStart(cell1,false);
//			comboBox.AddAttribute(cell1,"text",0);
			
			CellRendererText cell2=new CellRendererText();
			comboBox.PackStart(cell2,false);
			comboBox.AddAttribute(cell2,"text",1);//añadimos columnas
			liststore=new ListStore(typeof(int),typeof(string));
			
			TreeIter initialIter= liststore.AppendValues(0,"<sin asignar>");//si el elemento inicial no existe se selecciona esta opcion
			while(dbDataReader.Read()){
				int id2=(int)dbDataReader[id];
				string nombre2=(string)dbDataReader[nombre];
				TreeIter iter=liststore.AppendValues(id2,nombre2);	
				if(elementoInicial==id2)
					initialIter=iter;
			}
			dbDataReader.Close();
			comboBox.Model=liststore;
			comboBox.SetActiveIter(initialIter);
		}
		
		public ListStore ListStore{
			get {return liststore;}
		}
		
		public int ID{
			get {
				comboBox.GetActiveIter(out iter);
				int id=(int)liststore.GetValue(iter,0);
				return id;}
		}
			
	}
}

