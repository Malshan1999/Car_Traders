namespace Car_Traders.Forms
{
    partial class ConfirmedOrdersForm
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
            this.ConfirmedOrdersDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmedOrdersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ConfirmedOrdersDataGridView
            // 
            this.ConfirmedOrdersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConfirmedOrdersDataGridView.Location = new System.Drawing.Point(51, 38);
            this.ConfirmedOrdersDataGridView.Name = "ConfirmedOrdersDataGridView";
            this.ConfirmedOrdersDataGridView.RowHeadersWidth = 62;
            this.ConfirmedOrdersDataGridView.RowTemplate.Height = 28;
            this.ConfirmedOrdersDataGridView.Size = new System.Drawing.Size(715, 378);
            this.ConfirmedOrdersDataGridView.TabIndex = 0;
            // 
            // ConfirmedOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConfirmedOrdersDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfirmedOrdersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfirmedOrdersForm";
            this.Load += new System.EventHandler(this.ConfirmedOrdersForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmedOrdersDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ConfirmedOrdersDataGridView;
    }
}