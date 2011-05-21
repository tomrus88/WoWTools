using System;
using System.IO;
using System.Windows.Forms;
using WowTools.Core;

namespace WoWPacketViewer
{
    public partial class FrmMain : Form
    {
        private FrmSearch searchForm;

        private PacketViewTab SelectedTab
        {
            get
            {
                try
                {
                    return (PacketViewTab)tabControl1.SelectedTab.Controls[0];
                }
                catch
                {
                    return null;
                }
            }
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void OpenMenu_Click(object sender, EventArgs e)
        {
            if (_openDialog.ShowDialog() != DialogResult.OK)
                return;

            _statusLabel.Text = "Loading...";
            var file = _openDialog.FileName;

            CreateTab(file);

            _statusLabel.Text = String.Format("Done.");
        }

        private void CreateTab(string file)
        {
            var viewTab = new PacketViewTab(file);
            viewTab.Dock = DockStyle.Fill;

            var tabPage = new TabPage(viewTab.Text);
            tabPage.Controls.Add(viewTab);

            tabControl1.Controls.Add(tabPage);
            tabControl1.SelectedTab = tabPage;

            if (!tabControl1.Visible)
                tabControl1.Visible = true;
        }

        private void SaveMenu_Click(object sender, EventArgs e)
        {
            if (SelectedTab == null)
                return;

            if (!SelectedTab.Loaded)
            {
                MessageBox.Show("You should load something first!");
                return;
            }

            _saveDialog.FileName = Path.GetFileName(_openDialog.FileName).Replace("bin", "txt");

            if (_saveDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var stream = new StreamWriter(_saveDialog.OpenFile()))
            {
                foreach (var p in SelectedTab.Packets)
                {
                    stream.Write(p.HexLike());
                }
            }
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FindMenu_Click(object sender, EventArgs e)
        {
            if (SelectedTab == null)
                return;

            CreateSearchFormIfNeed();

            if (!searchForm.Visible)
                searchForm.Show(this);

            searchForm.Select();
        }

        private bool CreateSearchFormIfNeed()
        {
            if (searchForm == null || searchForm.IsDisposed)
            {
                searchForm = new FrmSearch();
                searchForm.CurrentTab = SelectedTab;
                searchForm.Show(this);
                return true;
            }
            return false;
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedTab == null)
                return;

            if (e.KeyCode != Keys.F3)
                return;

            if (!CreateSearchFormIfNeed())
                searchForm.FindNext();
        }

        private void saveAsParsedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTab == null)
                return;

            if (!SelectedTab.Loaded)
            {
                MessageBox.Show("You should load something first!");
                return;
            }

            if (_saveDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var stream = new StreamWriter(_saveDialog.OpenFile()))
            {
                foreach (var p in SelectedTab.Packets)
                {
                    string parsed = ParserFactory.CreateParser(p).ToString();
                    if (String.IsNullOrEmpty(parsed))
                        continue;
                    stream.Write(parsed);
                }
            }
        }

        private void saveWardenAsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTab == null)
                return;

            if (!SelectedTab.Loaded)
            {
                MessageBox.Show("You should load something first!");
                return;
            }

            _saveDialog.FileName = Path.GetFileName(_openDialog.FileName).Replace("bin", "txt");

            if (_saveDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var stream = new StreamWriter(_saveDialog.OpenFile()))
            {
                foreach (var p in SelectedTab.Packets)
                {
                    if (p.Code != OpCodes.CMSG_WARDEN_DATA && p.Code != OpCodes.SMSG_WARDEN_DATA)
                        continue;
                    //stream.Write(Utility.HexLike(p));

                    var parsed = ParserFactory.CreateParser(p).ToString();
                    if (String.IsNullOrEmpty(parsed))
                        continue;
                    stream.Write(parsed);
                }
            }
        }

        private void reloadDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParserFactory.ReInit();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ParserFactory.Init();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!tabControl1.HasChildren)
                tabControl1.Visible = false;

            if (searchForm != null)
                searchForm.CurrentTab = SelectedTab;
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return;

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                var r = tabControl1.GetTabRect(i);
                if (r.Contains(e.Location))
                {
                    closeTabToolStripMenuItem.Tag = tabControl1.TabPages[i];
                    closeAllButThisToolStripMenuItem.Tag = tabControl1.TabPages[i];
                    contextMenuStrip1.Show(tabControl1, e.Location);
                    break;
                }
            }
        }

        private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
                return;

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                var r = tabControl1.GetTabRect(i);
                if (r.Contains(e.Location))
                {
                    tabControl1.TabPages[i].Dispose();
                    break;
                }
            }
        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((TabPage)((ToolStripMenuItem)sender).Tag).Dispose();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (tabControl1.HasChildren)
                tabControl1.TabPages[0].Dispose();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var thisTab = ((ToolStripMenuItem)sender).Tag;

            int index = 0;
            while (tabControl1.TabPages.Count != 1)
            {
                if (tabControl1.TabPages[index] == thisTab)
                {
                    index++;
                    continue;
                }

                tabControl1.TabPages[index].Dispose();
            }
        }

        private void wardenDebugToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            WardenData.Enabled = wardenDebugToolStripMenuItem.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NYI!");
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTab == null)
                return;

            colorDialog1.Color = SelectedTab.PacketList.ForeColor;

            var result = colorDialog1.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            SelectedTab.PacketList.ForeColor = colorDialog1.Color;
        }

        private void backgroungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTab == null)
                return;

            colorDialog1.Color = SelectedTab.PacketList.BackColor;

            var result = colorDialog1.ShowDialog();

            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            SelectedTab.PacketList.BackColor = colorDialog1.Color;
        }
    }
}
