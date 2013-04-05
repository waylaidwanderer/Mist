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
            this.column_value = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAllItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableGroupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.list_userofferings = new BrightIdeasSoftware.ObjectListView();
            this.column_userofferings = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_uo_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_uo_value = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.text_input = new System.Windows.Forms.TextBox();
            this.label_cancel = new System.Windows.Forms.Label();
            this.list_otherofferings = new BrightIdeasSoftware.ObjectListView();
            this.column_otherofferings = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_oo_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_oo_value = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.text_log = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableItemGroupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSuggestedItemPricesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.text_search = new System.Windows.Forms.TextBox();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.button_accept = new MetroFramework.Controls.MetroButton();
            this.button_send = new MetroFramework.Controls.MetroButton();
            this.label_yourvalue = new MetroFramework.Controls.MetroLabel();
            this.label_othervalue = new MetroFramework.Controls.MetroLabel();
            this.check_userready = new MetroFramework.Controls.MetroCheckBox();
            this.check_otherready = new MetroFramework.Controls.MetroCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_userofferings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_otherofferings)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
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
            this.column_inventory});
            this.list_inventory.ContextMenuStrip = this.contextMenuStrip1;
            this.list_inventory.Location = new System.Drawing.Point(23, 87);
            this.list_inventory.Name = "list_inventory";
            this.list_inventory.Size = new System.Drawing.Size(387, 230);
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
            // column_value
            // 
            this.column_value.AspectName = "ItemPrice";
            this.column_value.CellPadding = null;
            this.column_value.DisplayIndex = 1;
            this.column_value.IsVisible = false;
            this.column_value.Text = "Value";
            this.column_value.Width = 90;
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
            this.addAllItemsToolStripMenuItem,
            this.disableGroupingToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(193, 48);
            // 
            // addAllItemsToolStripMenuItem
            // 
            this.addAllItemsToolStripMenuItem.Name = "addAllItemsToolStripMenuItem";
            this.addAllItemsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.addAllItemsToolStripMenuItem.Text = "Add All Items";
            this.addAllItemsToolStripMenuItem.Click += new System.EventHandler(this.addAllItemsToolStripMenuItem_Click);
            // 
            // disableGroupingToolStripMenuItem
            // 
            this.disableGroupingToolStripMenuItem.CheckOnClick = true;
            this.disableGroupingToolStripMenuItem.Name = "disableGroupingToolStripMenuItem";
            this.disableGroupingToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.disableGroupingToolStripMenuItem.Text = "Disable Item Grouping";
            this.disableGroupingToolStripMenuItem.Click += new System.EventHandler(this.disableGroupingToolStripMenuItem_Click);
            // 
            // list_userofferings
            // 
            this.list_userofferings.AllColumns.Add(this.column_userofferings);
            this.list_userofferings.AllColumns.Add(this.column_uo_id);
            this.list_userofferings.AllColumns.Add(this.column_uo_value);
            this.list_userofferings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.list_userofferings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_userofferings});
            this.list_userofferings.Location = new System.Drawing.Point(416, 87);
            this.list_userofferings.Name = "list_userofferings";
            this.list_userofferings.Size = new System.Drawing.Size(287, 145);
            this.list_userofferings.TabIndex = 7;
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
            // column_uo_value
            // 
            this.column_uo_value.AspectName = "ItemPrice";
            this.column_uo_value.CellPadding = null;
            this.column_uo_value.DisplayIndex = 1;
            this.column_uo_value.IsVisible = false;
            this.column_uo_value.Text = "Value";
            // 
            // text_input
            // 
            this.text_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_input.Location = new System.Drawing.Point(23, 510);
            this.text_input.MaxLength = 2048;
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(287, 57);
            this.text_input.TabIndex = 0;
            this.text_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_input_KeyPress);
            // 
            // label_cancel
            // 
            this.label_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_cancel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label_cancel.Location = new System.Drawing.Point(416, 542);
            this.label_cancel.Name = "label_cancel";
            this.label_cancel.Size = new System.Drawing.Size(287, 28);
            this.label_cancel.TabIndex = 6;
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
            this.column_otherofferings});
            this.list_otherofferings.Location = new System.Drawing.Point(416, 261);
            this.list_otherofferings.Name = "list_otherofferings";
            this.list_otherofferings.Size = new System.Drawing.Size(287, 146);
            this.list_otherofferings.TabIndex = 8;
            this.list_otherofferings.UseCompatibleStateImageBehavior = false;
            this.list_otherofferings.View = System.Windows.Forms.View.Details;
            // 
            // column_otherofferings
            // 
            this.column_otherofferings.AspectName = "ItemName";
            this.column_otherofferings.CellPadding = null;
            this.column_otherofferings.FillsFreeSpace = true;
            this.column_otherofferings.IsVisible = false;
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
            // column_oo_value
            // 
            this.column_oo_value.AspectName = "ItemPrice";
            this.column_oo_value.CellPadding = null;
            this.column_oo_value.DisplayIndex = 1;
            this.column_oo_value.IsVisible = false;
            this.column_oo_value.Text = "Value";
            // 
            // text_log
            // 
            this.text_log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_log.Location = new System.Drawing.Point(23, 349);
            this.text_log.Name = "text_log";
            this.text_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.text_log.Size = new System.Drawing.Size(387, 155);
            this.text_log.TabIndex = 14;
            this.text_log.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableItemGroupingToolStripMenuItem,
            this.viewSuggestedItemPricesToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.Gray;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // disableItemGroupingToolStripMenuItem
            // 
            this.disableItemGroupingToolStripMenuItem.CheckOnClick = true;
            this.disableItemGroupingToolStripMenuItem.Name = "disableItemGroupingToolStripMenuItem";
            this.disableItemGroupingToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.disableItemGroupingToolStripMenuItem.Text = "Disable Item Grouping";
            this.disableItemGroupingToolStripMenuItem.Click += new System.EventHandler(this.disableItemGroupingToolStripMenuItem_Click);
            // 
            // viewSuggestedItemPricesToolStripMenuItem
            // 
            this.viewSuggestedItemPricesToolStripMenuItem.CheckOnClick = true;
            this.viewSuggestedItemPricesToolStripMenuItem.Name = "viewSuggestedItemPricesToolStripMenuItem";
            this.viewSuggestedItemPricesToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.viewSuggestedItemPricesToolStripMenuItem.Text = "View Suggested Item Prices";
            this.viewSuggestedItemPricesToolStripMenuItem.Click += new System.EventHandler(this.viewSuggestedItemPricesToolStripMenuItem_Click);
            // 
            // text_search
            // 
            this.text_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_search.ForeColor = System.Drawing.Color.Gray;
            this.text_search.Location = new System.Drawing.Point(23, 323);
            this.text_search.Name = "text_search";
            this.text_search.Size = new System.Drawing.Size(387, 20);
            this.text_search.TabIndex = 18;
            this.text_search.Text = "Search for an item in your inventory...";
            this.text_search.TextChanged += new System.EventHandler(this.text_search_TextChanged);
            this.text_search.Enter += new System.EventHandler(this.text_search_Enter);
            this.text_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_search_KeyPress);
            this.text_search.Leave += new System.EventHandler(this.text_search_Leave);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // button_accept
            // 
            this.button_accept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_accept.Enabled = false;
            this.button_accept.Location = new System.Drawing.Point(416, 436);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(287, 103);
            this.button_accept.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_accept.StyleManager = this.metroStyleManager1;
            this.button_accept.TabIndex = 5;
            this.button_accept.Text = "Accept Trade";
            this.button_accept.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_accept.Click += new System.EventHandler(this.button_accept_Click);
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(316, 510);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(94, 57);
            this.button_send.Style = MetroFramework.MetroColorStyle.Blue;
            this.button_send.StyleManager = this.metroStyleManager1;
            this.button_send.TabIndex = 2;
            this.button_send.Text = "Send";
            this.button_send.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // label_yourvalue
            // 
            this.label_yourvalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_yourvalue.BackColor = System.Drawing.Color.Transparent;
            this.label_yourvalue.Location = new System.Drawing.Point(472, 235);
            this.label_yourvalue.Name = "label_yourvalue";
            this.label_yourvalue.Size = new System.Drawing.Size(234, 73);
            this.label_yourvalue.Style = MetroFramework.MetroColorStyle.Blue;
            this.label_yourvalue.StyleManager = this.metroStyleManager1;
            this.label_yourvalue.TabIndex = 21;
            this.label_yourvalue.Text = "Total Value: 0.00";
            this.label_yourvalue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_yourvalue.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // label_othervalue
            // 
            this.label_othervalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_othervalue.BackColor = System.Drawing.Color.Transparent;
            this.label_othervalue.Location = new System.Drawing.Point(472, 410);
            this.label_othervalue.Name = "label_othervalue";
            this.label_othervalue.Size = new System.Drawing.Size(234, 73);
            this.label_othervalue.Style = MetroFramework.MetroColorStyle.Blue;
            this.label_othervalue.StyleManager = this.metroStyleManager1;
            this.label_othervalue.TabIndex = 22;
            this.label_othervalue.Text = "Total Value: 0.00";
            this.label_othervalue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label_othervalue.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // check_userready
            // 
            this.check_userready.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_userready.AutoSize = true;
            this.check_userready.Enabled = false;
            this.check_userready.Location = new System.Drawing.Point(416, 238);
            this.check_userready.Name = "check_userready";
            this.check_userready.Size = new System.Drawing.Size(104, 15);
            this.check_userready.Style = MetroFramework.MetroColorStyle.Blue;
            this.check_userready.StyleManager = this.metroStyleManager1;
            this.check_userready.TabIndex = 3;
            this.check_userready.Text = "Ready to trade?";
            this.check_userready.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.check_userready.UseVisualStyleBackColor = true;
            this.check_userready.CheckedChanged += new System.EventHandler(this.check_userready_CheckedChanged);
            // 
            // check_otherready
            // 
            this.check_otherready.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_otherready.AutoSize = true;
            this.check_otherready.Enabled = false;
            this.check_otherready.Location = new System.Drawing.Point(416, 413);
            this.check_otherready.Name = "check_otherready";
            this.check_otherready.Size = new System.Drawing.Size(104, 15);
            this.check_otherready.Style = MetroFramework.MetroColorStyle.Blue;
            this.check_otherready.StyleManager = this.metroStyleManager1;
            this.check_otherready.TabIndex = 4;
            this.check_otherready.Text = "Ready to trade?";
            this.check_otherready.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.check_otherready.UseVisualStyleBackColor = true;
            // 
            // ShowTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.None;
            this.ClientSize = new System.Drawing.Size(726, 590);
            this.Controls.Add(this.check_otherready);
            this.Controls.Add(this.check_userready);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.text_search);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label_cancel);
            this.Controls.Add(this.text_log);
            this.Controls.Add(this.list_otherofferings);
            this.Controls.Add(this.text_input);
            this.Controls.Add(this.list_userofferings);
            this.Controls.Add(this.list_inventory);
            this.Controls.Add(this.label_yourvalue);
            this.Controls.Add(this.label_othervalue);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(587, 536);
            this.Name = "ShowTrade";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Trade Session";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Activated += new System.EventHandler(this.ShowTrade_Activated);
            this.Deactivate += new System.EventHandler(this.ShowTrade_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShowTrade_FormClosed);
            this.Load += new System.EventHandler(this.ShowTrade_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.list_userofferings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_otherofferings)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_input;
        private System.Windows.Forms.Label label_cancel;
        private BrightIdeasSoftware.OLVColumn column_userofferings;
        private BrightIdeasSoftware.OLVColumn column_otherofferings;
        public BrightIdeasSoftware.ObjectListView list_inventory;
        public BrightIdeasSoftware.ObjectListView list_userofferings;
        public BrightIdeasSoftware.ObjectListView list_otherofferings;
        private BrightIdeasSoftware.OLVColumn column_uo_id;
        private BrightIdeasSoftware.OLVColumn column_oo_id;
        private BrightIdeasSoftware.OLVColumn column_id;
        private BrightIdeasSoftware.OLVColumn column_inventory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addAllItemsToolStripMenuItem;
        private System.Windows.Forms.RichTextBox text_log;
        private BrightIdeasSoftware.OLVColumn column_value;
        private BrightIdeasSoftware.OLVColumn column_uo_value;
        private BrightIdeasSoftware.OLVColumn column_oo_value;
        private System.Windows.Forms.ToolStripMenuItem disableGroupingToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableItemGroupingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewSuggestedItemPricesToolStripMenuItem;
        private System.Windows.Forms.TextBox text_search;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroButton button_send;
        private MetroFramework.Controls.MetroLabel label_yourvalue;
        private MetroFramework.Controls.MetroLabel label_othervalue;
        public MetroFramework.Controls.MetroCheckBox check_otherready;
        public MetroFramework.Controls.MetroCheckBox check_userready;
        public MetroFramework.Controls.MetroButton button_accept;
    }
}