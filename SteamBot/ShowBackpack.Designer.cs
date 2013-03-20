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
            this.column_defindex = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.column_url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).BeginInit();
            this.SuspendLayout();
            // 
            // list_inventory
            // 
            this.list_inventory.AllColumns.Add(this.column_inventory);
            this.list_inventory.AllColumns.Add(this.column_defindex);
            this.list_inventory.AllColumns.Add(this.column_url);
            this.list_inventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_inventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_inventory});
            this.list_inventory.LargeImageList = this.LargeImageList;
            this.list_inventory.Location = new System.Drawing.Point(12, 12);
            this.list_inventory.MultiSelect = false;
            this.list_inventory.Name = "list_inventory";
            this.list_inventory.Size = new System.Drawing.Size(335, 406);
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
            this.column_inventory.Text = "Inventory";
            this.column_inventory.UseInitialLetterForGroup = true;
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
            // ShowBackpack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 430);
            this.Controls.Add(this.list_inventory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowBackpack";
            this.Text = "ShowBackpack";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowBackpack_FormClosing);
            this.Load += new System.EventHandler(this.ShowBackpack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView list_inventory;
        private System.Windows.Forms.ImageList LargeImageList;
        private BrightIdeasSoftware.OLVColumn column_inventory;
        private BrightIdeasSoftware.OLVColumn column_url;
        private BrightIdeasSoftware.OLVColumn column_defindex;
    }
}