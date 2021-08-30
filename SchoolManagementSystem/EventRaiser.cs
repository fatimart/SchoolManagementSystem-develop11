using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public static class EventRaiser
    {
        public static void Raise ( this EventHandler handler, object sender )
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

       
        public static void Raise<T> ( this EventHandler<T> handler, object sender, T value ) where T : EventArgs
        {
            handler?.Invoke(sender, value);
        }
    }
}
