using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		ActionGroup actionGroup1 = new ActionGroup("pageActionGroup");
		
		Gtk.Action newAction = new Gtk.Action("newAction", null, null, Stock.New);
		actionGroup1.Add (newAction);
		Gtk.Action editAction = new Gtk.Action("editAction", null, null, Stock.Edit);
		actionGroup1.Add (editAction);
		UIManager.InsertActionGroup(actionGroup1, 0);
		
//		uint mergeId = UIManager.AddUiFromString(
//			"<ui>" +
//			"<toolbar name='toolbar'>" +
//			"<toolitem name='newAction' action='newAction'/>" +
//			"<toolitem name='editAction' action='editAction'/>" +
//			"</toolbar>" +
//			"</ui>");

		uint mergeId = UIManager.AddUiFromString( getUi(actionGroup1) );
		
		Console.WriteLine ("mergeId={0}", mergeId);
		
		
		
		ActionGroup actionGroup2 = new ActionGroup("pageActionGroup");
		
		Gtk.Action deleteAction = new Gtk.Action("deleteAction", null, null, Stock.Delete);
		actionGroup2.Add (deleteAction);
		
		
		executeAction.Activated += delegate {
			Console.WriteLine("executeAction.Activated");
			
			//Console.WriteLine("UIManager.Ui='{0}'", UIManager.Ui);
			UIManager.RemoveUi (mergeId);
			UIManager.RemoveActionGroup(actionGroup1);
			
			
			UIManager.InsertActionGroup(actionGroup2, 0);
			UIManager.AddUiFromString( getUi(actionGroup2) );
			
		};
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
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}