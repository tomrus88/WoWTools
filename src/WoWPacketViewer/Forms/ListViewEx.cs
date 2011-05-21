using System.Windows.Forms;

namespace WoWPacketViewer
{
    public class ListViewEx : ListView
    {
        public ListViewEx()
            : base()
        {
            DoubleBuffered = true;
        }
    }
}
