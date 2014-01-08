using System;
using Gtk;
namespace Serpis.Ad
{
	public class CategoriaListView : EntityListView
	{
		public CategoriaListView ()
		{
			string sql="select * from categoria";
			helper=new TreeViewHelper(treeView,sql);
			
		}

		
	}
}

