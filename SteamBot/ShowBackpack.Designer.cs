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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowBackpack));
            this.list_inventory = new BrightIdeasSoftware.ObjectListView();
            this.column_inventory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.list_inventory)).BeginInit();
            this.SuspendLayout();
            // 
            // list_inventory
            // 
            this.list_inventory.AllColumns.Add(this.column_inventory);
            this.list_inventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_inventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_inventory});
            this.list_inventory.Location = new System.Drawing.Point(12, 12);
            this.list_inventory.Name = "list_inventory";
            this.list_inventory.Size = new System.Drawing.Size(399, 406);
            this.list_inventory.TabIndex = 0;
            this.list_inventory.UseCompatibleStateImageBehavior = false;
            this.list_inventory.View = System.Windows.Forms.View.Details;
            // 
            // column_inventory
            // 
            this.column_inventory.AspectName = "ItemName";
            this.column_inventory.CellPadding = null;
            this.column_inventory.FillsFreeSpace = true;
            this.column_inventory.Text = "Inventory";
            this.column_inventory.UseInitialLetterForGroup = true;
            // 
            // ShowBackpack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 430);
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
        private BrightIdeasSoftware.OLVColumn column_inventory;
    }
}