//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18052
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stetic {
    
    
    internal class Gui {
        
        private static bool initialized;
        
        public static void Build(object cobj, System.Type type) {
            global::Stetic.Gui.Build(cobj, type.FullName);
        }
        
        public static void Build(object cobj, string id) {
        }
        
        internal static void Initialize(Gtk.Widget iconRenderer) {
            if ((Stetic.Gui.initialized == false)) {
                Stetic.Gui.initialized = true;
            }
        }
    }
    
    internal class ActionGroups {
        
        public static Gtk.ActionGroup GetActionGroup(System.Type type) {
            return Stetic.ActionGroups.GetActionGroup(type.FullName);
        }
        
        public static Gtk.ActionGroup GetActionGroup(string name) {
            return null;
        }
    }
}
