namespace Laboratory_Information_System
{
    partial class DataTransfer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvTransferHistory = new Guna.UI2.WinForms.Guna2DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRecordsSentTotal = new System.Windows.Forms.Label();
            this.lblLastTransfer = new System.Windows.Forms.Label();
            this.lblNextTransfer = new System.Windows.Forms.Label();
            this.btnSendNow = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.lblLastErrorTime = new System.Windows.Forms.Label();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.lblFailedCount = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnTestConnection = new Guna.UI2.WinForms.Guna2Button();
            this.txtApiUrl = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtApiKey = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferHistory)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Transfer Data to web application via Api";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Transfer Data";
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel4.Controls.Add(this.dgvTransferHistory);
            this.guna2Panel4.Controls.Add(this.label8);
            this.guna2Panel4.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel4.CustomBorderThickness = new System.Windows.Forms.Padding(1);
            this.guna2Panel4.Location = new System.Drawing.Point(25, 423);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(1441, 364);
            this.guna2Panel4.TabIndex = 12;
            // 
            // dgvTransferHistory
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvTransferHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransferHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransferHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTransferHistory.ColumnHeadersHeight = 4;
            this.dgvTransferHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransferHistory.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTransferHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTransferHistory.Location = new System.Drawing.Point(17, 44);
            this.dgvTransferHistory.Name = "dgvTransferHistory";
            this.dgvTransferHistory.RowHeadersVisible = false;
            this.dgvTransferHistory.Size = new System.Drawing.Size(1395, 306);
            this.dgvTransferHistory.TabIndex = 21;
            this.dgvTransferHistory.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTransferHistory.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvTransferHistory.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvTransferHistory.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvTransferHistory.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvTransferHistory.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvTransferHistory.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTransferHistory.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvTransferHistory.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTransferHistory.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTransferHistory.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTransferHistory.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvTransferHistory.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvTransferHistory.ThemeStyle.ReadOnly = false;
            this.dgvTransferHistory.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTransferHistory.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTransferHistory.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTransferHistory.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvTransferHistory.ThemeStyle.RowsStyle.Height = 22;
            this.dgvTransferHistory.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTransferHistory.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvTransferHistory.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTransferHistory_CellMouseDoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 24);
            this.label8.TabIndex = 20;
            this.label8.Text = "Transfer History:";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.btnSendNow);
            this.guna2Panel1.Controls.Add(this.lblNextTransfer);
            this.guna2Panel1.Controls.Add(this.lblLastTransfer);
            this.guna2Panel1.Controls.Add(this.lblRecordsSentTotal);
            this.guna2Panel1.Controls.Add(this.lblStatus);
            this.guna2Panel1.Controls.Add(this.label7);
            this.guna2Panel1.Controls.Add(this.label6);
            this.guna2Panel1.Controls.Add(this.label5);
            this.guna2Panel1.Controls.Add(this.label4);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel1.CustomBorderThickness = new System.Windows.Forms.Padding(1);
            this.guna2Panel1.Location = new System.Drawing.Point(23, 20);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(385, 305);
            this.guna2Panel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Transfer Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Last Transfer:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Next Transfer:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Status:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "Records Sent:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(208, 157);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(49, 17);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Status:";
            // 
            // lblRecordsSentTotal
            // 
            this.lblRecordsSentTotal.AutoSize = true;
            this.lblRecordsSentTotal.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsSentTotal.Location = new System.Drawing.Point(208, 199);
            this.lblRecordsSentTotal.Name = "lblRecordsSentTotal";
            this.lblRecordsSentTotal.Size = new System.Drawing.Size(49, 17);
            this.lblRecordsSentTotal.TabIndex = 17;
            this.lblRecordsSentTotal.Text = "Status:";
            // 
            // lblLastTransfer
            // 
            this.lblLastTransfer.AutoSize = true;
            this.lblLastTransfer.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastTransfer.Location = new System.Drawing.Point(208, 73);
            this.lblLastTransfer.Name = "lblLastTransfer";
            this.lblLastTransfer.Size = new System.Drawing.Size(49, 17);
            this.lblLastTransfer.TabIndex = 18;
            this.lblLastTransfer.Text = "Status:";
            // 
            // lblNextTransfer
            // 
            this.lblNextTransfer.AutoSize = true;
            this.lblNextTransfer.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextTransfer.Location = new System.Drawing.Point(208, 115);
            this.lblNextTransfer.Name = "lblNextTransfer";
            this.lblNextTransfer.Size = new System.Drawing.Size(49, 17);
            this.lblNextTransfer.TabIndex = 19;
            this.lblNextTransfer.Text = "Status:";
            // 
            // btnSendNow
            // 
            this.btnSendNow.AutoRoundedCorners = true;
            this.btnSendNow.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSendNow.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSendNow.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSendNow.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSendNow.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSendNow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSendNow.ForeColor = System.Drawing.Color.White;
            this.btnSendNow.Location = new System.Drawing.Point(17, 244);
            this.btnSendNow.Name = "btnSendNow";
            this.btnSendNow.Size = new System.Drawing.Size(180, 37);
            this.btnSendNow.TabIndex = 12;
            this.btnSendNow.Text = "Send Now";
            this.btnSendNow.Click += new System.EventHandler(this.btnSendNow_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.lblFailedCount);
            this.guna2Panel2.Controls.Add(this.guna2Button2);
            this.guna2Panel2.Controls.Add(this.lblLastErrorTime);
            this.guna2Panel2.Controls.Add(this.lblPendingCount);
            this.guna2Panel2.Controls.Add(this.lblErrorMessage);
            this.guna2Panel2.Controls.Add(this.label9);
            this.guna2Panel2.Controls.Add(this.label11);
            this.guna2Panel2.Controls.Add(this.label12);
            this.guna2Panel2.Controls.Add(this.label13);
            this.guna2Panel2.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel2.CustomBorderThickness = new System.Windows.Forms.Padding(1);
            this.guna2Panel2.Location = new System.Drawing.Point(430, 20);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(385, 305);
            this.guna2Panel2.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(18, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 17);
            this.label13.TabIndex = 20;
            this.label13.Text = "Pedding Records:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(18, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 17);
            this.label12.TabIndex = 21;
            this.label12.Text = "Failed Records:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(18, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 17);
            this.label11.TabIndex = 22;
            this.label11.Text = "Last Error:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 24);
            this.label9.TabIndex = 20;
            this.label9.Text = "Transfer Queue:";
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.Location = new System.Drawing.Point(18, 199);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(89, 17);
            this.lblErrorMessage.TabIndex = 23;
            this.lblErrorMessage.Text = "Records Sent:";
            // 
            // lblPendingCount
            // 
            this.lblPendingCount.AutoSize = true;
            this.lblPendingCount.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingCount.Location = new System.Drawing.Point(206, 73);
            this.lblPendingCount.Name = "lblPendingCount";
            this.lblPendingCount.Size = new System.Drawing.Size(112, 17);
            this.lblPendingCount.TabIndex = 24;
            this.lblPendingCount.Text = "Pedding Records:";
            // 
            // lblLastErrorTime
            // 
            this.lblLastErrorTime.AutoSize = true;
            this.lblLastErrorTime.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastErrorTime.Location = new System.Drawing.Point(206, 158);
            this.lblLastErrorTime.Name = "lblLastErrorTime";
            this.lblLastErrorTime.Size = new System.Drawing.Size(112, 17);
            this.lblLastErrorTime.TabIndex = 26;
            this.lblLastErrorTime.Text = "Pedding Records:";
            // 
            // guna2Button2
            // 
            this.guna2Button2.AutoRoundedCorners = true;
            this.guna2Button2.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.guna2Button2.BorderThickness = 1;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.SystemColors.ButtonHighlight;
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.guna2Button2.Location = new System.Drawing.Point(21, 244);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(180, 37);
            this.guna2Button2.TabIndex = 20;
            this.guna2Button2.Text = "View failed records";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // lblFailedCount
            // 
            this.lblFailedCount.AutoSize = true;
            this.lblFailedCount.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFailedCount.Location = new System.Drawing.Point(206, 115);
            this.lblFailedCount.Name = "lblFailedCount";
            this.lblFailedCount.Size = new System.Drawing.Size(112, 17);
            this.lblFailedCount.TabIndex = 27;
            this.lblFailedCount.Text = "Pedding Records:";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.Controls.Add(this.txtApiKey);
            this.guna2Panel3.Controls.Add(this.txtApiUrl);
            this.guna2Panel3.Controls.Add(this.btnTestConnection);
            this.guna2Panel3.Controls.Add(this.label15);
            this.guna2Panel3.Controls.Add(this.label14);
            this.guna2Panel3.Controls.Add(this.label10);
            this.guna2Panel3.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.guna2Panel3.CustomBorderThickness = new System.Windows.Forms.Padding(1);
            this.guna2Panel3.Location = new System.Drawing.Point(840, 20);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(385, 305);
            this.guna2Panel3.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Modern No. 20", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(28, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(192, 24);
            this.label10.TabIndex = 28;
            this.label10.Text = "API Configuration:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(17, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 17);
            this.label14.TabIndex = 28;
            this.label14.Text = "API URL:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(17, 157);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 17);
            this.label15.TabIndex = 29;
            this.label15.Text = "API Key:";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.AutoRoundedCorners = true;
            this.btnTestConnection.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.btnTestConnection.BorderThickness = 1;
            this.btnTestConnection.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTestConnection.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTestConnection.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTestConnection.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTestConnection.FillColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTestConnection.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTestConnection.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnTestConnection.Location = new System.Drawing.Point(180, 244);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(180, 37);
            this.btnTestConnection.TabIndex = 28;
            this.btnTestConnection.Text = "Test Api connection";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click_1);
            // 
            // txtApiUrl
            // 
            this.txtApiUrl.AutoRoundedCorners = true;
            this.txtApiUrl.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtApiUrl.DefaultText = "";
            this.txtApiUrl.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtApiUrl.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtApiUrl.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtApiUrl.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtApiUrl.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtApiUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtApiUrl.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtApiUrl.Location = new System.Drawing.Point(147, 65);
            this.txtApiUrl.Name = "txtApiUrl";
            this.txtApiUrl.PlaceholderText = "";
            this.txtApiUrl.SelectedText = "";
            this.txtApiUrl.Size = new System.Drawing.Size(222, 36);
            this.txtApiUrl.TabIndex = 30;
            // 
            // txtApiKey
            // 
            this.txtApiKey.AutoRoundedCorners = true;
            this.txtApiKey.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtApiKey.DefaultText = "";
            this.txtApiKey.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtApiKey.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtApiKey.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtApiKey.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtApiKey.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtApiKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtApiKey.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtApiKey.Location = new System.Drawing.Point(147, 139);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.PlaceholderText = "";
            this.txtApiKey.SelectedText = "";
            this.txtApiKey.Size = new System.Drawing.Size(222, 36);
            this.txtApiKey.TabIndex = 31;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel5.Controls.Add(this.guna2Panel3);
            this.guna2Panel5.Controls.Add(this.guna2Panel2);
            this.guna2Panel5.Controls.Add(this.guna2Panel1);
            this.guna2Panel5.Location = new System.Drawing.Point(2, 71);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(1485, 346);
            this.guna2Panel5.TabIndex = 13;
            // 
            // DataTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1499, 799);
            this.Controls.Add(this.guna2Panel4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DataTransfer";
            this.RightToLeftLayout = true;
            this.Text = "DataTransfer";
            this.Load += new System.EventHandler(this.DataTransfer_Load_1);
            this.guna2Panel4.ResumeLayout(false);
            this.guna2Panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransferHistory)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            this.guna2Panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2DataGridView dgvTransferHistory;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnSendNow;
        private System.Windows.Forms.Label lblNextTransfer;
        private System.Windows.Forms.Label lblLastTransfer;
        private System.Windows.Forms.Label lblRecordsSentTotal;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label lblFailedCount;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private System.Windows.Forms.Label lblLastErrorTime;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2TextBox txtApiKey;
        private Guna.UI2.WinForms.Guna2TextBox txtApiUrl;
        private Guna.UI2.WinForms.Guna2Button btnTestConnection;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
    }
}