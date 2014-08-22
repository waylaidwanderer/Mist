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
            this.LargeImageList = new System.Windows.Forms.ImageList(this.components);
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.webControl1 = new Awesomium.Windows.Forms.WebControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // LargeImageList
            // 
            this.LargeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.LargeImageList.ImageSize = new System.Drawing.Size(64, 64);
            this.LargeImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = this;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // webControl1
            // 
            this.webControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webControl1.Location = new System.Drawing.Point(23, 63);
            this.webControl1.Size = new System.Drawing.Size(507, 380);
            this.webControl1.TabIndex = 2;
            // 
            // ShowBackpack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 466);
            this.Controls.Add(this.webControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowBackpack";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Backpack Viewer";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowBackpack_FormClosing);
            this.Load += new System.EventHandler(this.ShowBackpack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList LargeImageList;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private Awesomium.Windows.Forms.WebControl webControl1;
    }
}