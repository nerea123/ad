using System;
using Gtk;

namespace Serpis.Ad
{
	public interface IEntityListVIew
	{
		void New();
		void Refresh();
		bool HasSelected {get;}
		event EventHandler SelectedChanged;
		
		
	}
}

