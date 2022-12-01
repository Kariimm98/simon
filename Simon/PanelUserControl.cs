using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Simon
{
    public class PanelUserControl : UserControl
    {
        public PanelUserControl()
        {
           
        }

        public event EventHandler ButtonClick;//ok

        private void TestingClicks(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(sender, e);
        }
        public void OnClick(object sender, EventArgs e)
        {
            TestingClicks(sender, e);
        }
    }
}
