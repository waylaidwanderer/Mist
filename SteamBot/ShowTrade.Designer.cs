namespace MistClient
{
    partial class ShowTrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowTrade));
            this.list_inventory = new BrightIdeasSoftware.ObjectListView();
            this.column_inventory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAllItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.list_userofferings = new BrightIdeasSoftware.ObjectListView();
            this.column_userofferings = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_uo_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.text_input = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.button_accept = new System.Windows.Forms.Button();
            this.label_cancel = new System.Windows.Forms.Label();
            this.list_otherofferings = new BrightIdeasSoftware.ObjectListView();
            this.column_otherofferings = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_oo_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.check_userready = new System.Windows.Forms.CheckBox();
            this.check_otherready = new System.Windows.Forms.CheckBox();
            this.text_log = new System.Windows.Forms.RichTextBox();
            this.column_value = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_uo_value = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_oo_value = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_userofferings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_otherofferings)).BeginInit();
            this.SuspendLayout();
            // 
            // list_inventory
            // 
            this.list_inventory.AllColumns.Add(this.column_inventory);
            this.list_inventory.AllColumns.Add(this.column_value);
            this.list_inventory.AllColumns.Add(this.column_id);
            this.list_inventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_inventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_inventory,
            this.column_value});
            this.list_inventory.ContextMenuStrip = this.contextMenuStrip1;
            this.list_inventory.Location = new System.Drawing.Point(14, 15);
            this.list_inventory.Name = "list_inventory";
            this.list_inventory.Size = new System.Drawing.Size(349, 242);
            this.list_inventory.TabIndex = 0;
            this.list_inventory.UseCompatibleStateImageBehavior = false;
            this.list_inventory.View = System.Windows.Forms.View.Details;
            this.list_inventory.ItemActivate += new System.EventHandler(this.list_inventory_ItemActivate);
            // 
            // column_inventory
            // 
            this.column_inventory.AspectName = "ItemName";
            this.column_inventory.CellPadding = null;
            this.column_inventory.FillsFreeSpace = true;
            this.column_inventory.ImageAspectName = "ImageURL";
            this.column_inventory.Text = "Your Inventory";
            this.column_inventory.UseInitialLetterForGroup = true;
            this.column_inventory.Width = 91;
            // 
            // column_id
            // 
            this.column_id.AspectName = "ItemID";
            this.column_id.CellPadding = null;
            this.column_id.IsVisible = false;
            this.column_id.Text = "ItemID";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAllItemsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(146, 26);
            // 
            // addAllItemsToolStripMenuItem
            // 
            this.addAllItemsToolStripMenuItem.Name = "addAllItemsToolStripMenuItem";
            this.addAllItemsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addAllItemsToolStripMenuItem.Text = "Add All Items";
            this.addAllItemsToolStripMenuItem.Click += new System.EventHandler(this.addAllItemsToolStripMenuItem_Click);
            // 
            // list_userofferings
            // 
            this.list_userofferings.AllColumns.Add(this.column_userofferings);
            this.list_userofferings.AllColumns.Add(this.column_uo_id);
            this.list_userofferings.AllColumns.Add(this.column_uo_value);
            this.list_userofferings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.list_userofferings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_userofferings,
            this.column_uo_value});
            this.list_userofferings.Location = new System.Drawing.Point(369, 15);
            this.list_userofferings.Name = "list_userofferings";
            this.list_userofferings.Size = new System.Drawing.Size(248, 145);
            this.list_userofferings.TabIndex = 1;
            this.list_userofferings.UseCompatibleStateImageBehavior = false;
            this.list_userofferings.View = System.Windows.Forms.View.Details;
            this.list_userofferings.ItemActivate += new System.EventHandler(this.list_userofferings_ItemActivate);
            // 
            // column_userofferings
            // 
            this.column_userofferings.AspectName = "ItemName";
            this.column_userofferings.CellPadding = null;
            this.column_userofferings.FillsFreeSpace = true;
            this.column_userofferings.Text = "Your Offerings:";
            this.column_userofferings.UseInitialLetterForGroup = true;
            this.column_userofferings.Width = 100;
            // 
            // column_uo_id
            // 
            this.column_uo_id.AspectName = "ItemID";
            this.column_uo_id.CellPadding = null;
            this.column_uo_id.IsVisible = false;
            this.column_uo_id.Text = "ItemID";
            // 
            // text_input
            // 
            this.text_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_input.Location = new System.Drawing.Point(14, 425);
            this.text_input.MaxLength = 2048;
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(249, 57);
            this.text_input.TabIndex = 4;
            this.text_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_input_KeyPress);
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(269, 425);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(94, 57);
            this.button_send.TabIndex = 5;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // button_accept
            // 
            this.button_accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_accept.Enabled = false;
            this.button_accept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_accept.Location = new System.Drawing.Point(369, 362);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(248, 89);
            this.button_accept.TabIndex = 9;
            this.button_accept.Text = "Accept Trade";
            this.button_accept.UseVisualStyleBackColor = true;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // label_cancel
            // 
            this.label_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cancel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_cancel.Location = new System.Drawing.Point(369, 454);
            this.label_cancel.Name = "label_cancel";
            this.label_cancel.Size = new System.Drawing.Size(248, 28);
            this.label_cancel.TabIndex = 10;
            this.label_cancel.Text = "Cancel Trade";
            this.label_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_cancel.Click += new System.EventHandler(this.label_cancel_Click);
            this.label_cancel.MouseEnter += new System.EventHandler(this.label_cancel_MouseEnter);
            this.label_cancel.MouseLeave += new System.EventHandler(this.label_cancel_MouseLeave);
            // 
            // list_otherofferings
            // 
            this.list_otherofferings.AllColumns.Add(this.column_otherofferings);
            this.list_otherofferings.AllColumns.Add(this.column_oo_id);
            this.list_otherofferings.AllColumns.Add(this.column_oo_value);
            this.list_otherofferings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.list_otherofferings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_otherofferings,
            this.column_oo_value});
            this.list_otherofferings.Location = new System.Drawing.Point(369, 188);
            this.list_otherofferings.Name = "list_otherofferings";
            this.list_otherofferings.Size = new System.Drawing.Size(248, 145);
            this.list_otherofferings.TabIndex = 11;
            this.list_otherofferings.UseCompatibleStateImageBehavior = false;
            this.list_otherofferings.View = System.Windows.Forms.View.Details;
            // 
            // column_otherofferings
            // 
            this.column_otherofferings.AspectName = "ItemName";
            this.column_otherofferings.CellPadding = null;
            this.column_otherofferings.FillsFreeSpace = true;
            this.column_otherofferings.Text = "Other\'s Offerings:";
            this.column_otherofferings.UseInitialLetterForGroup = true;
            this.column_otherofferings.Width = 120;
            // 
            // column_oo_id
            // 
            this.column_oo_id.AspectName = "ItemID";
            this.column_oo_id.CellPadding = null;
            this.column_oo_id.IsVisible = false;
            this.column_oo_id.Text = "ItemID";
            // 
            // check_userready
            // 
            this.check_userready.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_userready.AutoSize = true;
            this.check_userready.Enabled = false;
            this.check_userready.Location = new System.Drawing.Point(369, 166);
            this.check_userready.Name = "check_userready";
            this.check_userready.Size = new System.Drawing.Size(102, 17);
            this.check_userready.TabIndex = 12;
            this.check_userready.Text = "Ready to trade?";
            this.check_userready.UseVisualStyleBackColor = true;
            this.check_userready.CheckedChanged += new System.EventHandler(this.check_userready_CheckedChanged);
            // 
            // check_otherready
            // 
            this.check_otherready.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_otherready.AutoSize = true;
            this.check_otherready.Enabled = false;
            this.check_otherready.Location = new System.Drawing.Point(369, 339);
            this.check_otherready.Name = "check_otherready";
            this.check_otherready.Size = new System.Drawing.Size(102, 17);
            this.check_otherready.TabIndex = 13;
            this.check_otherready.Text = "Ready to trade?";
            this.check_otherready.UseVisualStyleBackColor = true;
            // 
            // text_log
            // 
            this.text_log.Location = new System.Drawing.Point(14, 263);
            this.text_log.Name = "text_log";
            this.text_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.text_log.Size = new System.Drawing.Size(349, 156);
            this.text_log.TabIndex = 14;
            this.text_log.Text = "";
            // 
            // column_value
            // 
            this.column_value.AspectName = "ItemPrice";
            this.column_value.CellPadding = null;
            this.column_value.Text = "Value";
            this.column_value.Width = 90;
            // 
            // column_uo_value
            // 
            this.column_uo_value.AspectName = "ItemPrice";
            this.column_uo_value.CellPadding = null;
            this.column_uo_value.Text = "Value";
            // 
            // column_oo_value
            // 
            this.column_oo_value.AspectName = "ItemPrice";
            this.column_oo_value.CellPadding = null;
            this.column_oo_value.Text = "Value";
            // 
            // ShowTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(631, 497);
            this.Controls.Add(this.text_log);
            this.Controls.Add(this.check_otherready);
            this.Controls.Add(this.check_userready);
            this.Controls.Add(this.list_otherofferings);
            this.Controls.Add(this.label_cancel);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.text_input);
            this.Controls.Add(this.list_userofferings);
            this.Controls.Add(this.list_inventory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(587, 536);
            this.Name = "ShowTrade";
            this.Text = "ShowTrade";
            this.Activated += new System.EventHandler(this.ShowTrade_Activated);
            this.Deactivate += new System.EventHandler(this.ShowTrade_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowTrade_FormClosing);
            this.Load += new System.EventHandler(this.ShowTrade_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.list_userofferings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_otherofferings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_input;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Label label_cancel;
        private BrightIdeasSoftware.OLVColumn column_userofferings;
        private BrightIdeasSoftware.OLVColumn column_otherofferings;
        public BrightIdeasSoftware.ObjectListView list_inventory;
        public BrightIdeasSoftware.ObjectListView list_userofferings;
        public BrightIdeasSoftware.ObjectListView list_otherofferings;
        public System.Windows.Forms.CheckBox check_otherready;
        private BrightIdeasSoftware.OLVColumn column_uo_id;
        private BrightIdeasSoftware.OLVColumn column_oo_id;
        private BrightIdeasSoftware.OLVColumn column_id;
        private BrightIdeasSoftware.OLVColumn column_inventory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addAllItemsToolStripMenuItem;
        public System.Windows.Forms.CheckBox check_userready;
        public System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.RichTextBox text_log;
        private BrightIdeasSoftware.OLVColumn column_value;
        private BrightIdeasSoftware.OLVColumn column_uo_value;
        private BrightIdeasSoftware.OLVColumn column_oo_value;
    }
}