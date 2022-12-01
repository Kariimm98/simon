using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Simon
{
    public class ColorButton : Panel
    {
        public EventArgs Click { get; set; }
        public Color CurrentColor { get; set; }
        public Color UnselectColor { get; set; }
    

    }
}
