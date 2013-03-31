namespace MistClient
{
    partial class ShowBackpack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowBackpack));
            this.list_inventory = new BrightIdeasSoftware.ObjectListView();
            this.column_inventory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_value = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_defindex = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager();
            this.checkBox1 = new MetroFramework.Controls.MetroCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).BeginInit();
            this.SuspendLayout();
            // 
            // list_inventory
            // 
            this.list_inventory.AllColumns.Add(this.column_inventory);
            this.list_inventory.AllColumns.Add(this.column_value);
            this.list_inventory.AllColumns.Add(this.column_defindex);
            this.list_inventory.AllColumns.Add(this.column_url);
            this.list_inventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_inventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_inventory});
            this.list_inventory.LargeImageList = this.LargeImageList;
            this.list_inventory.Location = new System.Drawing.Point(23, 63);
            this.list_inventory.MultiSelect = false;
            this.list_inventory.Name = "list_inventory";
            this.list_inventory.Size = new System.Drawing.Size(313, 359);
            this.list_inventory.SmallImageList = this.LargeImageList;
            this.list_inventory.StateImageList = this.LargeImageList;
            this.list_inventory.TabIndex = 0;
            this.list_inventory.UseCompatibleStateImageBehavior = false;
            this.list_inventory.View = System.Windows.Forms.View.Details;
            // 
            // column_inventory
            // 
            this.column_inventory.AspectName = "ItemName";
            this.column_inventory.CellPadding = null;
            this.column_inventory.FillsFreeSpace = true;
            this.column_inventory.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.column_inventory.Text = "Item";
            this.column_inventory.UseInitialLetterForGroup = true;
            // 
            // column_value
            // 
            this.column_value.AspectName = "ItemPrice";
            this.column_value.CellPadding = null;
            this.column_value.DisplayIndex = 1;
            this.column_value.Groupable = false;
            this.column_value.IsVisible = false;
            this.column_value.Text = "Value";
            this.column_value.Width = 90;
            // 
            // column_defindex
            // 
            this.column_defindex.AspectName = "DefIndex";
            this.column_defindex.CellPadding = null;
            this.column_defindex.IsVisible = false;
            // 
            // column_url
            // 
            this.column_url.AspectName = "ItemURL";
            this.column_url.CellPadding = null;
            this.column_url.DisplayIndex = 1;
            this.column_url.IsVisible = false;
            // 
            // LargeImageList
            // 
            this.LargeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.LargeImageList.ImageSize = new System.Drawing.Size(64, 64);
            this.LargeImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.OwnerForm = this;
            this.metroStyleManager1.OwnerUserControl = null;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.CustomBackground = false;
            this.checkBox1.CustomForeColor = false;
            this.checkBox1.FontSize = MetroFramework.MetroLinkSize.Small;
            this.checkBox1.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.checkBox1.Location = new System.Drawing.Point(225, 428);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 15);
            this.checkBox1.Style = MetroFramework.MetroColorStyle.Blue;
            this.checkBox1.StyleManager = this.metroStyleManager1;
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "View item values";
            this.checkBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.checkBox1.UseStyleColors = false;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ShowBackpack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 466);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.list_inventory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ShowBackpack";
            this.ShadowType = MetroFramework.Forms.ShadowType.DropShadow;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Backpack Viewer";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowBackpack_FormClosing);
            this.Load += new System.EventHandler(this.ShowBackpack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView list_inventory;
        private System.Windows.Forms.ImageList LargeImageList;
        private BrightIdeasSoftware.OLVColumn column_inventory;
        private BrightIdeasSoftware.OLVColumn column_url;
        private BrightIdeasSoftware.OLVColumn column_defindex;
        private BrightIdeasSoftware.OLVColumn column_value;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroCheckBox checkBox1;
    }
}