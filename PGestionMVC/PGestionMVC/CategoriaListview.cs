using System;
using Gtk;

namespace Serpis.Ad
{
	public class CategoriaListview  : EntityListView
	{
		public CategoriaListview ()
		{
			TreeViewHelper treeViewHelper = new TreeViewHelper (treeView, App.Instance.DbConnection, "select id,nombre from categoria");

			Gtk.Action actionRefresh = new Gtk.Action("actionRefresh",null,null,Stock.Refresh);
			actionRefresh.Activated += delegate {
			
				treeViewHelper.refresh();
			};




			ActionGroup.Add (actionRefresh);

			Gtk.Action actionEdit = new Gtk.Action("actionEdit",null,null,Stock.Edit);
			actionEdit.Activated += delegate {
			
				Categoria categoria= Categoria.Load(treeViewHelper.Id);

				Console.WriteLine ("id=[{0}, nombre={1} ",categoria.Id,categoria.Nombre);

			};

			ActionGroup.Add (actionEdit);
		}

	}
}

