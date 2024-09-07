namespace Car_Traders.Forms
{
    partial class UserCarForm
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
            this.flowLayoutPanelCars = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanelCars
            // 
            this.flowLayoutPanelCars.AutoScroll = true;
            this.flowLayoutPanelCars.Location = new System.Drawing.Point(41, 79);
            this.flowLayoutPanelCars.Name = "flowLayoutPanelCars";
            this.flowLayoutPanelCars.Size = new System.Drawing.Size(823, 445);
            this.flowLayoutPanelCars.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Silver;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = global::Car_Traders.Properties.Resources.pngtree_vector_search_icon_png_image_924820;
            this.btnSearch.Location = new System.Drawing.Point(543, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 32);
            this.btnSearch.TabIndex = 31;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(261, 21);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(276, 39);
            this.txtSearch.TabIndex = 30;
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnClearFilter.FlatAppearance.BorderSize = 0;
            this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFilter.ForeColor = System.Drawing.Color.White;
            this.btnClearFilter.Location = new System.Drawing.Point(605, 24);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(104, 36);
            this.btnClearFilter.TabIndex = 32;
            this.btnClearFilter.Text = "Clear Filters";
            this.btnClearFilter.UseVisualStyleBackColor = false;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(25, 21);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(230, 32);
            this.lblSearch.TabIndex = 29;
            this.lblSearch.Text = "Search by Name ";
            // 
            // UserCarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 524);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.flowLayoutPanelCars);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserCarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserCarsForm";
            this.Load += new System.EventHandler(this.UserCarForm_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCars;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Label lblSearch;
    }
}