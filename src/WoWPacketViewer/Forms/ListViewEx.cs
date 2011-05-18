using System.Windows.Forms;

namespace WoWPacketViewer
{
    class ListViewEx : ListView
    {
        public ListViewEx()
            : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
