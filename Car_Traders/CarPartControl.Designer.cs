namespace Car_Traders
{
    partial class CarPartControl
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
            this.PictureBoxPartImage = new System.Windows.Forms.PictureBox();
            this.LabelPartName = new System.Windows.Forms.Label();
            this.LabelBrand = new System.Windows.Forms.Label();
            this.LabelPrice = new System.Windows.Forms.Label();
            this.BtnBuyNow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPartImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxPartImage
            // 
            this.PictureBoxPartImage.Location = new System.Drawing.Point(155, 16);
            this.PictureBoxPartImage.Name = "PictureBoxPartImage";
            this.PictureBoxPartImage.Size = new System.Drawing.Size(508, 295);
            this.PictureBoxPartImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxPartImage.TabIndex = 0;
            this.PictureBoxPartImage.TabStop = false;
            // 
            // LabelPartName
            // 
            this.LabelPartName.AutoSize = true;
            this.LabelPartName.Font = new System.Drawing.Font("Palatino Linotype", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPartName.Location = new System.Drawing.Point(358, 314);
            this.LabelPartName.Name = "LabelPartName";
            this.LabelPartName.Size = new System.Drawing.Size(150, 38);
            this.LabelPartName.TabIndex = 1;
            this.LabelPartName.Text = "Part Name";
            // 
            // LabelBrand
            // 
            this.LabelBrand.AutoSize = true;
            this.LabelBrand.Font = new System.Drawing.Font("Palatino Linotype", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBrand.Location = new System.Drawing.Point(272, 314);
            this.LabelBrand.Name = "LabelBrand";
            this.LabelBrand.Size = new System.Drawing.Size(95, 38);
            this.LabelBrand.TabIndex = 2;
            this.LabelBrand.Text = "Brand";
            // 
            // LabelPrice
            // 
            this.LabelPrice.AutoSize = true;
            this.LabelPrice.Font = new System.Drawing.Font("Microsoft Tai Le", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPrice.Location = new System.Drawing.Point(344, 352);
            this.LabelPrice.Name = "LabelPrice";
            this.LabelPrice.Size = new System.Drawing.Size(54, 25);
            this.LabelPrice.TabIndex = 3;
            this.LabelPrice.Text = "Price";
            // 
            // BtnBuyNow
            // 
            this.BtnBuyNow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.BtnBuyNow.Location = new System.Drawing.Point(291, 390);
            this.BtnBuyNow.Name = "BtnBuyNow";
            this.BtnBuyNow.Size = new System.Drawing.Size(168, 48);
            this.BtnBuyNow.TabIndex = 4;
            this.BtnBuyNow.Text = "Buy Now";
            this.BtnBuyNow.UseVisualStyleBackColor = false;
            this.BtnBuyNow.Click += new System.EventHandler(this.btnBuyNow_Click);
            // 
            // CarPartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnBuyNow);
            this.Controls.Add(this.LabelPrice);
            this.Controls.Add(this.LabelBrand);
            this.Controls.Add(this.LabelPartName);
            this.Controls.Add(this.PictureBoxPartImage);
            this.Name = "CarPartControl";
            this.Size = new System.Drawing.Size(770, 537);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPartImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxPartImage;
        private System.Windows.Forms.Label LabelPartName;
        private System.Windows.Forms.Label LabelBrand;
        private System.Windows.Forms.Label LabelPrice;
        private System.Windows.Forms.Button BtnBuyNow;
    }
}
