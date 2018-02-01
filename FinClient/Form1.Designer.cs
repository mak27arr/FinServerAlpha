namespace FinClient
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ip_LABLE = new System.Windows.Forms.Label();
            this.ip_tB = new System.Windows.Forms.TextBox();
            this.port_tB = new System.Windows.Forms.TextBox();
            this.port_lb = new System.Windows.Forms.Label();
            this.conect_but = new System.Windows.Forms.Button();
            this.trans_dGV = new System.Windows.Forms.DataGridView();
            this.log_tB = new System.Windows.Forms.TextBox();
            this.FinTool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.trans_dGV)).BeginInit();
            this.SuspendLayout();
            // 
            // ip_LABLE
            // 
            this.ip_LABLE.AutoSize = true;
            this.ip_LABLE.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ip_LABLE.Location = new System.Drawing.Point(13, 13);
            this.ip_LABLE.Name = "ip_LABLE";
            this.ip_LABLE.Size = new System.Drawing.Size(24, 20);
            this.ip_LABLE.TabIndex = 0;
            this.ip_LABLE.Text = "IP";
            // 
            // ip_tB
            // 
            this.ip_tB.Location = new System.Drawing.Point(44, 12);
            this.ip_tB.Name = "ip_tB";
            this.ip_tB.Size = new System.Drawing.Size(100, 20);
            this.ip_tB.TabIndex = 1;
            this.ip_tB.Text = "127.0.0.1";
            this.ip_tB.TextChanged += new System.EventHandler(this.ip_tB_TextChanged);
            // 
            // port_tB
            // 
            this.port_tB.Location = new System.Drawing.Point(213, 12);
            this.port_tB.Name = "port_tB";
            this.port_tB.Size = new System.Drawing.Size(36, 20);
            this.port_tB.TabIndex = 3;
            this.port_tB.Text = "8080";
            // 
            // port_lb
            // 
            this.port_lb.AutoSize = true;
            this.port_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.port_lb.Location = new System.Drawing.Point(155, 13);
            this.port_lb.Name = "port_lb";
            this.port_lb.Size = new System.Drawing.Size(52, 20);
            this.port_lb.TabIndex = 2;
            this.port_lb.Text = "PORT";
            // 
            // conect_but
            // 
            this.conect_but.Location = new System.Drawing.Point(255, 9);
            this.conect_but.Name = "conect_but";
            this.conect_but.Size = new System.Drawing.Size(71, 23);
            this.conect_but.TabIndex = 4;
            this.conect_but.Text = "Conect";
            this.conect_but.UseVisualStyleBackColor = true;
            this.conect_but.Click += new System.EventHandler(this.conect_but_Click);
            // 
            // trans_dGV
            // 
            this.trans_dGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trans_dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trans_dGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FinTool,
            this.FinTime,
            this.Price,
            this.Volume});
            this.trans_dGV.Location = new System.Drawing.Point(12, 38);
            this.trans_dGV.Name = "trans_dGV";
            this.trans_dGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.trans_dGV.Size = new System.Drawing.Size(443, 313);
            this.trans_dGV.TabIndex = 5;
            // 
            // log_tB
            // 
            this.log_tB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log_tB.Location = new System.Drawing.Point(12, 357);
            this.log_tB.MaxLength = 2147483647;
            this.log_tB.Multiline = true;
            this.log_tB.Name = "log_tB";
            this.log_tB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log_tB.Size = new System.Drawing.Size(443, 61);
            this.log_tB.TabIndex = 6;
            // 
            // FinTool
            // 
            this.FinTool.Frozen = true;
            this.FinTool.HeaderText = "FinTool";
            this.FinTool.Name = "FinTool";
            this.FinTool.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FinTime
            // 
            this.FinTime.Frozen = true;
            this.FinTime.HeaderText = "FinTime";
            this.FinTime.Name = "FinTime";
            this.FinTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Volume
            // 
            this.Volume.HeaderText = "Volume";
            this.Volume.Name = "Volume";
            this.Volume.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 430);
            this.Controls.Add(this.log_tB);
            this.Controls.Add(this.trans_dGV);
            this.Controls.Add(this.conect_but);
            this.Controls.Add(this.port_tB);
            this.Controls.Add(this.port_lb);
            this.Controls.Add(this.ip_tB);
            this.Controls.Add(this.ip_LABLE);
            this.Name = "Form1";
            this.Text = "FinClient";
            ((System.ComponentModel.ISupportInitialize)(this.trans_dGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ip_LABLE;
        private System.Windows.Forms.TextBox ip_tB;
        private System.Windows.Forms.TextBox port_tB;
        private System.Windows.Forms.Label port_lb;
        private System.Windows.Forms.Button conect_but;
        private System.Windows.Forms.DataGridView trans_dGV;
        private System.Windows.Forms.TextBox log_tB;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinTool;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volume;
    }
}

