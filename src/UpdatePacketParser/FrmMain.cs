using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WowTools.Core;

namespace UpdatePacketParser
{
    public partial class FrmMain : Form
    {
        Parser m_parser;    // active parser
        FilterForm m_filterForm;

        public FrmMain()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhanledExceptionHandler);
        }

        private static void UnhanledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            MessageBox.Show(args.ExceptionObject.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            richTextBox1.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var slvic = listView1.SelectedItems;
            for (var i = 0; i < slvic.Count; i++)
            {
                var lvsic = slvic[i].SubItems;
                if (lvsic.Count != 2)
                    return;
                sb.AppendLine(lvsic[0].Text + '\t' + lvsic[1].Text);
            }

            var temp = sb.ToString();
            if (String.IsNullOrEmpty(temp))
                return;
            Clipboard.SetText(temp);
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            var slvic = listView2.SelectedItems;
            for (var i = 0; i < slvic.Count; i++)
            {
                var lvsic = slvic[i].SubItems;
                if (lvsic.Count != 2)
                    return;
                sb.AppendLine(lvsic[0].Text + '\t' + lvsic[1].Text);
            }

            var temp = sb.ToString();
            if (String.IsNullOrEmpty(temp))
                return;
            Clipboard.SetText(temp);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
                LoadFile(openFileDialog1.FileName);
        }

        private void LoadFile(string filename)
        {
            if (m_parser != null)
            {
                m_parser.Close();
                m_parser = null;
            }
            listBox1.Items.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            richTextBox1.Clear();
            switch (Path.GetExtension(filename))
            {
                case ".pkt":
                case ".bin":
                    m_parser = new Parser(new WowCorePacketReader(filename));
                    break;
                case ".sqlite":
                    m_parser = new Parser(new SqLitePacketReader(filename));
                    break;
                case ".xml":
                    m_parser = new Parser(new SniffitztPacketReader(filename));
                    break;
                default:
                    break;
            }
            //UpdateFieldsLoader.LoadUpdateFields(12025);
            //var br = new BinaryReader(new FileStream("upd400.bin", FileMode.Open));
            //m_parser = new Parser(br, WowTools.Core.OpCodes.SMSG_UPDATE_OBJECT);
            //br.Close();

            m_parser.PrintObjects(listBox1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var str = listBox1.Items[listBox1.SelectedIndex].ToString().Split(' ');
            var guid = ulong.Parse(str[0], System.Globalization.NumberStyles.AllowHexSpecifier);

            listView1.Items.Clear();
            m_parser.PrintObjectInfo(guid, listView1);

            // process updates
            listView2.Items.Clear();
            m_parser.PrintObjectUpdatesInfo(guid, listView2);

            // process movement info
            richTextBox1.Clear();
            m_parser.PrintObjectMovementInfo(guid, richTextBox1);
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_filterForm == null || m_filterForm.IsDisposed)
                m_filterForm = new FilterForm();

            if (m_filterForm.Visible)
                return;

            m_filterForm.Show(this);
        }

        public void PrintObjectType(ObjectTypeMask mask, CustomFilterMask customMask)
        {
            m_parser.PrintObjectsType(listBox1, mask, customMask);
        }
    }
}
