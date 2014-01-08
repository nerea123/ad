using Gtk;
using System;

namespace Serpis.Ad
{
	public class UiManagerHelper
	{
		private UIManager uIManager;
		public UiManagerHelper (UIManager uIManager)
		{
			this.uIManager = uIManager;
		}
		
		private uint mergeId;
		private ActionGroup actionGroup;
		public void SetActionGroup(ActionGroup actionGroup) {
			if (this.actionGroup != null) { //this.actionGroup es el anterior
				uIManager.RemoveUi(mergeId);
				uIManager.RemoveActionGroup(this.actionGroup);
			}
			this.actionGroup = actionGroup;
			if(actionGroup==null)
				return;
			uIManager.InsertActionGroup(actionGroup, 0);
			mergeId = uIManager.AddUiFromString (getUi(actionGroup));
		}
		private const string prefix = "<ui><toolbar name='toolbar'>";
		private const string uiItem = "<toolitem name='{0}' action='{0}'/>";
		private const string sufix  = "</toolbar></ui>";
			
		private string getUi(ActionGroup actionGroup) {
			string uiItems = "";
			foreach (Gtk.Action action in actionGroup.ListActions())
				uiItems = uiItems + String.Format (uiItem, action.Name);
			return prefix + uiItems + sufix;
		}
	
	}
}

