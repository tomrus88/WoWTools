using System.Windows.Forms;

namespace WoWPacketViewer
{
    class ListViewEx : ListView
    {
        public ListViewEx()
            : base()
        {
            DoubleBuffered = true;
        }
    }
}
