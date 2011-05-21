using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WowTools.Core;

namespace WoWPacketViewer
{
    public partial class PacketViewTab : UserControl, ISupportFind
    {
        private IPacketReader packetViewer;
        private List<Packet> packets;
        private Dictionary<int, ListViewItem> _listCache = new Dictionary<int, ListViewItem>();
        private bool _searchUp;
        private bool _ignoreCase;
        private string file;

        public PacketViewTab(string file)
        {
            InitializeComponent();

            Text = Path.GetFileName(file);

            packetViewer = PacketReaderFactory.Create(Path.GetExtension(file));

            packets = packetViewer.ReadPackets(file).ToList();

            PacketView.VirtualMode = true;
            PacketView.VirtualListSize = packets.Count;
            PacketView.EnsureVisible(0);
        }

        public void SetColors(Color listFore, Color listBack, Color hexFore, Color hexBack, Color parsedFore, Color parsedBack)
        {
            PacketView.ForeColor = listFore;
            PacketView.BackColor = listBack;
            HexView.ForeColor = hexFore;
            HexView.BackColor = hexBack;
            ParsedView.ForeColor = parsedFore;
            ParsedView.BackColor = parsedBack;
        }

        private int SelectedIndex
        {
            get
            {
                var sic = PacketView.SelectedIndices;
                return sic.Count > 0 ? sic[0] : 0;
            }
        }

        public bool Loaded
        {
            get { return packetViewer != null; }
        }

        public List<Packet> Packets { get { return packets; } }

        public string File
        {
            get { return file; }
            set { file = value; }
        }

        public ListViewEx PacketList
        {
            get { return PacketView; }
        }

        public uint Build
        {
            get { return packetViewer.Build; }
        }

        #region ISupportFind Members

        public void Search(string text, bool searchUp, bool ignoreCase)
        {
            if (!Loaded)
                return;

            _searchUp = searchUp;
            _ignoreCase = ignoreCase;

            var item = PacketView.FindItemWithText(text, true, SelectedIndex, true);
            if (item != null)
            {
                PacketView.BeginUpdate();
                PacketView.GridLines = false;
                item.Selected = true;
                item.EnsureVisible();
                PacketView.GridLines = true;
                PacketView.EndUpdate();
                return;
            }

            MessageBox.Show(string.Format("Can't find:'{0}'", text), "Packet Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        private void _list_SelectedIndexChanged(object sender, EventArgs e)
        {
            var packet = packets[SelectedIndex];

            HexView.Text = packet.HexLike();
            ParsedView.Text = ParserFactory.CreateParser(packet).ToString();
        }

        private void _list_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            // check to see if the requested item is currently in the cache
            if (_listCache.ContainsKey(e.ItemIndex))
            {
                // A cache hit, so get the ListViewItem from the cache instead of making a new one.
                e.Item = _listCache[e.ItemIndex];
            }
            else
            {
                // A cache miss, so create a new ListViewItem and pass it back.
                var item = CreateListViewItemByIndex(e.ItemIndex);
                e.Item = item;
                _listCache[e.ItemIndex] = item;
            }
        }

        private void _list_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            var comparisonType = _ignoreCase
                                    ? StringComparison.InvariantCultureIgnoreCase
                                    : StringComparison.InvariantCulture;

            if (_searchUp)
            {
                for (var i = SelectedIndex - 1; i >= 0; --i)
                    if (SearchMatches(e, comparisonType, i))
                        break;
            }
            else
            {
                for (int i = SelectedIndex + 1; i < PacketView.Items.Count; ++i)
                    if (SearchMatches(e, comparisonType, i))
                        break;
            }
        }

        private bool SearchMatches(SearchForVirtualItemEventArgs e, StringComparison comparisonType, int i)
        {
            var op = packets[i].Code.ToString();
            if (op.IndexOf(e.Text, comparisonType) != -1)
            {
                e.Index = i;
                return true;
            }
            return false;
        }

        private void _list_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            // We've gotten a request to refresh the cache. First check if it's really necessary.
            if (_listCache.ContainsKey(e.StartIndex) && _listCache.ContainsKey(e.EndIndex))
            {
                // If the newly requested cache is a subset of the old cache, no need to rebuild everything, so do nothing.
                return;
            }

            // Now we need to rebuild the cache.
            int length = e.EndIndex - e.StartIndex + 1; // indexes are inclusive

            // Fill the cache with the appropriate ListViewItems.
            for (int i = 0; i < length; ++i)
            {
                // Skip already existing ListViewItemsItems
                if (_listCache.ContainsKey(e.StartIndex + i))
                    continue;

                // Add new ListViewItemsItem to the cache
                _listCache.Add(e.StartIndex + i, CreateListViewItemByIndex(e.StartIndex + i));
            }
        }

        private ListViewItem CreateListViewItemByIndex(int index)
        {
            var p = packets[index];

            return p.Direction == Direction.Client
                ? new ListViewItem(new[]
                    {
                        p.UnixTime.ToString("X8"), 
                        p.TicksCount.ToString("X8"), 
                        p.Code.ToString(),
                        String.Empty,
                        p.Data.Length.ToString(),
                        ParserFactory.HasParser(p.Code).ToString()
                    })
                : new ListViewItem(new[]
                    {
                        p.UnixTime.ToString("X8"), 
                        p.TicksCount.ToString("X8"), 
                        String.Empty,
                        p.Code.ToString(), 
                        p.Data.Length.ToString(),
                        ParserFactory.HasParser(p.Code).ToString()
                    });
        }

        private Control ControlsLoop(string name, Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Name == name)
                    return c;
                else if (c.Controls.Count != 0)
                {
                    var res = ControlsLoop(name, c);
                    if (res != null)
                        return res;
                }
            }
            return null;
        }

        public Control GetControlByName(string name)
        {
            return ControlsLoop(name, this);
        }
    }
}
