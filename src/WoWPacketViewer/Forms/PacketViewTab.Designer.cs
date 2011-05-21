
namespace WoWPacketViewer
{
    partial class PacketViewTab
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PacketView = new WoWPacketViewer.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.HexView = new System.Windows.Forms.RichTextBox();
            this.ParsedView = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PacketView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(947, 588);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 0;
            // 
            // PacketView
            // 
            this.PacketView.BackColor = System.Drawing.SystemColors.WindowText;
            this.PacketView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.PacketView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PacketView.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PacketView.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.PacketView.FullRowSelect = true;
            this.PacketView.GridLines = true;
            this.PacketView.HideSelection = false;
            this.PacketView.Location = new System.Drawing.Point(0, 0);
            this.PacketView.MultiSelect = false;
            this.PacketView.Name = "PacketView";
            this.PacketView.ShowGroups = false;
            this.PacketView.Size = new System.Drawing.Size(947, 376);
            this.PacketView.TabIndex = 0;
            this.PacketView.UseCompatibleStateImageBehavior = false;
            this.PacketView.View = System.Windows.Forms.View.Details;
            this.PacketView.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this._list_CacheVirtualItems);
            this.PacketView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this._list_RetrieveVirtualItem);
            this.PacketView.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this._list_SearchForVirtualItem);
            this.PacketView.SelectedIndexChanged += new System.EventHandler(this._list_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ticks";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Client Opcode";
            this.columnHeader3.Width = 320;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Server Opcode";
            this.columnHeader4.Width = 320;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Packet Size";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Parser";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.HexView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ParsedView);
            this.splitContainer2.Size = new System.Drawing.Size(947, 208);
            this.splitContainer2.SplitterDistance = 404;
            this.splitContainer2.TabIndex = 0;
            // 
            // HexView
            // 
            this.HexView.BackColor = System.Drawing.SystemColors.WindowText;
            this.HexView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HexView.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HexView.ForeColor = System.Drawing.SystemColors.Window;
            this.HexView.Location = new System.Drawing.Point(0, 0);
            this.HexView.Name = "HexView";
            this.HexView.ReadOnly = true;
            this.HexView.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.HexView.Size = new System.Drawing.Size(404, 208);
            this.HexView.TabIndex = 0;
            this.HexView.Text = "";
            // 
            // ParsedView
            // 
            this.ParsedView.BackColor = System.Drawing.SystemColors.WindowText;
            this.ParsedView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParsedView.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ParsedView.ForeColor = System.Drawing.SystemColors.Window;
            this.ParsedView.Location = new System.Drawing.Point(0, 0);
            this.ParsedView.Name = "ParsedView";
            this.ParsedView.ReadOnly = true;
            this.ParsedView.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ParsedView.Size = new System.Drawing.Size(539, 208);
            this.ParsedView.TabIndex = 0;
            this.ParsedView.Text = "";
            // 
            // PacketViewTab
            // 
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "PacketViewTab";
            this.Size = new System.Drawing.Size(947, 588);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ListViewEx PacketView;
        private System.Windows.Forms.RichTextBox HexView;
        private System.Windows.Forms.RichTextBox ParsedView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}
