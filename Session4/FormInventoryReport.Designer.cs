namespace Session4
{
    partial class FormInventoryReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbOut = new System.Windows.Forms.RadioButton();
            this.rdbReceived = new System.Windows.Forms.RadioButton();
            this.rdbCurrent = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Warehouse:";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(16, 77);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(388, 28);
            this.cmbWarehouse.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbOut);
            this.groupBox1.Controls.Add(this.rdbReceived);
            this.groupBox1.Controls.Add(this.rdbCurrent);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(450, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(697, 117);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inventory Type";
            // 
            // rdbOut
            // 
            this.rdbOut.AutoSize = true;
            this.rdbOut.Location = new System.Drawing.Point(494, 59);
            this.rdbOut.Name = "rdbOut";
            this.rdbOut.Size = new System.Drawing.Size(123, 24);
            this.rdbOut.TabIndex = 2;
            this.rdbOut.TabStop = true;
            this.rdbOut.Text = "Out of Stock";
            this.rdbOut.UseVisualStyleBackColor = true;
            this.rdbOut.CheckedChanged += new System.EventHandler(this.rdbOut_CheckedChanged);
            // 
            // rdbReceived
            // 
            this.rdbReceived.AutoSize = true;
            this.rdbReceived.Location = new System.Drawing.Point(259, 59);
            this.rdbReceived.Name = "rdbReceived";
            this.rdbReceived.Size = new System.Drawing.Size(146, 24);
            this.rdbReceived.TabIndex = 1;
            this.rdbReceived.TabStop = true;
            this.rdbReceived.Text = "Received Stock";
            this.rdbReceived.UseVisualStyleBackColor = true;
            this.rdbReceived.CheckedChanged += new System.EventHandler(this.rdbReceived_CheckedChanged);
            // 
            // rdbCurrent
            // 
            this.rdbCurrent.AutoSize = true;
            this.rdbCurrent.Location = new System.Drawing.Point(43, 59);
            this.rdbCurrent.Name = "rdbCurrent";
            this.rdbCurrent.Size = new System.Drawing.Size(133, 24);
            this.rdbCurrent.TabIndex = 0;
            this.rdbCurrent.TabStop = true;
            this.rdbCurrent.Text = "Current Stock";
            this.rdbCurrent.UseVisualStyleBackColor = true;
            this.rdbCurrent.CheckedChanged += new System.EventHandler(this.rdbCurrent_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Result:";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(12, 181);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersWidth = 51;
            this.dgvResult.RowTemplate.Height = 24;
            this.dgvResult.Size = new System.Drawing.Size(1238, 480);
            this.dgvResult.TabIndex = 4;
            // 
            // FormInventoryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbWarehouse);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInventoryReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormInventoryReport_Closed);
            this.Load += new System.EventHandler(this.FormInventoryReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.RadioButton rdbOut;
        private System.Windows.Forms.RadioButton rdbReceived;
        private System.Windows.Forms.RadioButton rdbCurrent;
    }
}