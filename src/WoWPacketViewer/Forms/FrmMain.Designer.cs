namespace WoWPacketViewer
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWardenAsTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsParsedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wardenDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._openDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.colorChooser = new System.Windows.Forms.ColorDialog();
            this.hexViewTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexViewBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parsedTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parsedBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(931, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsTextToolStripMenuItem,
            this.saveWardenAsTextToolStripMenuItem,
            this.saveAsParsedTextToolStripMenuItem,
            this.reloadDefinitionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenMenu_Click);
            // 
            // saveAsTextToolStripMenuItem
            // 
            this.saveAsTextToolStripMenuItem.Name = "saveAsTextToolStripMenuItem";
            this.saveAsTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAsTextToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveAsTextToolStripMenuItem.Text = "&Save As Text...";
            this.saveAsTextToolStripMenuItem.Click += new System.EventHandler(this.SaveMenu_Click);
            // 
            // saveWardenAsTextToolStripMenuItem
            // 
            this.saveWardenAsTextToolStripMenuItem.Name = "saveWardenAsTextToolStripMenuItem";
            this.saveWardenAsTextToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveWardenAsTextToolStripMenuItem.Text = "Save Warden As Text...";
            this.saveWardenAsTextToolStripMenuItem.Click += new System.EventHandler(this.saveWardenAsTextToolStripMenuItem_Click);
            // 
            // saveAsParsedTextToolStripMenuItem
            // 
            this.saveAsParsedTextToolStripMenuItem.Name = "saveAsParsedTextToolStripMenuItem";
            this.saveAsParsedTextToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveAsParsedTextToolStripMenuItem.Text = "Save As Parsed Text...";
            this.saveAsParsedTextToolStripMenuItem.Click += new System.EventHandler(this.saveAsParsedTextToolStripMenuItem_Click);
            // 
            // reloadDefinitionsToolStripMenuItem
            // 
            this.reloadDefinitionsToolStripMenuItem.Name = "reloadDefinitionsToolStripMenuItem";
            this.reloadDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.reloadDefinitionsToolStripMenuItem.Text = "Reload definitions";
            this.reloadDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.reloadDefinitionsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.wardenDebugToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.findToolStripMenuItem.Text = "Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.FindMenu_Click);
            // 
            // wardenDebugToolStripMenuItem
            // 
            this.wardenDebugToolStripMenuItem.CheckOnClick = true;
            this.wardenDebugToolStripMenuItem.Name = "wardenDebugToolStripMenuItem";
            this.wardenDebugToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.wardenDebugToolStripMenuItem.Text = "Warden Debug";
            this.wardenDebugToolStripMenuItem.CheckedChanged += new System.EventHandler(this.wardenDebugToolStripMenuItem_CheckedChanged);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listViewTextToolStripMenuItem,
            this.listViewBackgroundToolStripMenuItem,
            this.hexViewTextToolStripMenuItem,
            this.hexViewBackgroundToolStripMenuItem,
            this.parsedTextToolStripMenuItem,
            this.parsedBackgroundToolStripMenuItem});
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.colorsToolStripMenuItem.Text = "Colors";
            // 
            // listViewTextToolStripMenuItem
            // 
            this.listViewTextToolStripMenuItem.Name = "listViewTextToolStripMenuItem";
            this.listViewTextToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.listViewTextToolStripMenuItem.Tag = "PacketView";
            this.listViewTextToolStripMenuItem.Text = "ListView Text";
            this.listViewTextToolStripMenuItem.Click += new System.EventHandler(this.foreColorStripMenuItem_Click);
            // 
            // listViewBackgroundToolStripMenuItem
            // 
            this.listViewBackgroundToolStripMenuItem.Name = "listViewBackgroundToolStripMenuItem";
            this.listViewBackgroundToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.listViewBackgroundToolStripMenuItem.Tag = "PacketView";
            this.listViewBackgroundToolStripMenuItem.Text = "ListView Background";
            this.listViewBackgroundToolStripMenuItem.Click += new System.EventHandler(this.backColorStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.aboutToolStripMenuItem.Text = "About Packet Viewer";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 498);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(931, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _statusLabel
            // 
            this._statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(39, 17);
            this._statusLabel.Text = "Ready";
            // 
            // _openDialog
            // 
            this._openDialog.Filter = "WoW Binary Files|*.bin;*.pkt|SQLite Files|*.sqlite|Sniffitzt XML Files|*.xml";
            // 
            // _saveDialog
            // 
            this._saveDialog.Filter = "Text Files|*.txt";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 70);
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeTabToolStripMenuItem.Text = "Close";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // closeAllButThisToolStripMenuItem
            // 
            this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
            this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeAllButThisToolStripMenuItem.Text = "Close All But This";
            this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(931, 474);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.Visible = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDoubleClick);
            // 
            // colorChooser
            // 
            this.colorChooser.FullOpen = true;
            // 
            // hexViewTextToolStripMenuItem
            // 
            this.hexViewTextToolStripMenuItem.Name = "hexViewTextToolStripMenuItem";
            this.hexViewTextToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.hexViewTextToolStripMenuItem.Tag = "HexView";
            this.hexViewTextToolStripMenuItem.Text = "HexView Text";
            this.hexViewTextToolStripMenuItem.Click += new System.EventHandler(this.foreColorStripMenuItem_Click);
            // 
            // hexViewBackgroundToolStripMenuItem
            // 
            this.hexViewBackgroundToolStripMenuItem.Name = "hexViewBackgroundToolStripMenuItem";
            this.hexViewBackgroundToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.hexViewBackgroundToolStripMenuItem.Tag = "HexView";
            this.hexViewBackgroundToolStripMenuItem.Text = "HexView Background";
            this.hexViewBackgroundToolStripMenuItem.Click += new System.EventHandler(this.backColorStripMenuItem_Click);
            // 
            // parsedTextToolStripMenuItem
            // 
            this.parsedTextToolStripMenuItem.Name = "parsedTextToolStripMenuItem";
            this.parsedTextToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.parsedTextToolStripMenuItem.Tag = "ParsedView";
            this.parsedTextToolStripMenuItem.Text = "Parsed Text";
            this.parsedTextToolStripMenuItem.Click += new System.EventHandler(this.foreColorStripMenuItem_Click);
            // 
            // parsedBackgroundToolStripMenuItem
            // 
            this.parsedBackgroundToolStripMenuItem.Name = "parsedBackgroundToolStripMenuItem";
            this.parsedBackgroundToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.parsedBackgroundToolStripMenuItem.Tag = "ParsedView";
            this.parsedBackgroundToolStripMenuItem.Text = "Parsed Background";
            this.parsedBackgroundToolStripMenuItem.Click += new System.EventHandler(this.backColorStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 520);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Packet Viewer";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsParsedTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWardenAsTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsTextToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog _openDialog;
        private System.Windows.Forms.SaveFileDialog _saveDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem wardenDebugToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorChooser;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listViewTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listViewBackgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexViewTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexViewBackgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parsedTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parsedBackgroundToolStripMenuItem;
    }
}
