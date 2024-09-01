namespace Car_Traders
{
    partial class CarControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelCarName = new System.Windows.Forms.Label();
            this.LabelBrand = new System.Windows.Forms.Label();
            this.LabelPrice = new System.Windows.Forms.Label();
            this.BtnBuyNow = new System.Windows.Forms.Button();
            this.PictureBoxCarImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCarImage)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelCarName
            // 
            this.LabelCarName.AutoSize = true;
            this.LabelCarName.Font = new System.Drawing.Font("Palatino Linotype", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCarName.Location = new System.Drawing.Point(336, 322);
            this.LabelCarName.Name = "LabelCarName";
            this.LabelCarName.Size = new System.Drawing.Size(94, 38);
            this.LabelCarName.TabIndex = 1;
            this.LabelCarName.Text = "label1";
            // 
            // LabelBrand
            // 
            this.LabelBrand.AutoSize = true;
            this.LabelBrand.Font = new System.Drawing.Font("Palatino Linotype", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBrand.Location = new System.Drawing.Point(258, 321);
            this.LabelBrand.Name = "LabelBrand";
            this.LabelBrand.Size = new System.Drawing.Size(94, 38);
            this.LabelBrand.TabIndex = 2;
            this.LabelBrand.Text = "label1";
            // 
            // LabelPrice
            // 
            this.LabelPrice.AutoSize = true;
            this.LabelPrice.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPrice.ForeColor = System.Drawing.Color.Red;
            this.LabelPrice.Location = new System.Drawing.Point(279, 359);
            this.LabelPrice.Name = "LabelPrice";
            this.LabelPrice.Size = new System.Drawing.Size(73, 27);
            this.LabelPrice.TabIndex = 3;
            this.LabelPrice.Text = "label1";
            // 
            // BtnBuyNow
            // 
            this.BtnBuyNow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BtnBuyNow.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnBuyNow.Location = new System.Drawing.Point(250, 389);
            this.BtnBuyNow.Name = "BtnBuyNow";
            this.BtnBuyNow.Size = new System.Drawing.Size(168, 48);
            this.BtnBuyNow.TabIndex = 5;
            this.BtnBuyNow.Text = "Buy Now";
            this.BtnBuyNow.UseVisualStyleBackColor = false;
            this.BtnBuyNow.Click += new System.EventHandler(this.BtnBuyNow_Click_1);
            // 
            // PictureBoxCarImage
            // 
            this.PictureBoxCarImage.Location = new System.Drawing.Point(106, 24);
            this.PictureBoxCarImage.Name = "PictureBoxCarImage";
            this.PictureBoxCarImage.Size = new System.Drawing.Size(508, 295);
            this.PictureBoxCarImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxCarImage.TabIndex = 0;
            this.PictureBoxCarImage.TabStop = false;
            // 
            // CarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnBuyNow);
            this.Controls.Add(this.LabelPrice);
            this.Controls.Add(this.LabelBrand);
            this.Controls.Add(this.LabelCarName);
            this.Controls.Add(this.PictureBoxCarImage);
            this.Name = "CarControl";
            this.Size = new System.Drawing.Size(770, 537);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxCarImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxCarImage;
        private System.Windows.Forms.Label LabelCarName;
        private System.Windows.Forms.Label LabelBrand;
        private System.Windows.Forms.Label LabelPrice;
        private System.Windows.Forms.Button BtnBuyNow;
    }
}
