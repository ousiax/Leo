namespace Leo.App
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dgvMemberDetails = new DataGridView();
            colDate = new DataGridViewTextBoxColumn();
            colItem = new DataGridViewTextBoxColumn();
            colCount = new DataGridViewTextBoxColumn();
            colHeight = new DataGridViewTextBoxColumn();
            colWeight = new DataGridViewTextBoxColumn();
            bdsMemberDetails = new BindingSource(components);
            bdsMembers = new BindingSource(components);
            splitContainer1 = new SplitContainer();
            txtAge = new TextBox();
            lblAge = new Label();
            combGender = new ComboBox();
            lblGender = new Label();
            dtpBirthday = new DateTimePicker();
            txtPhone = new TextBox();
            lblPhone = new Label();
            txtCardNo = new TextBox();
            lblCardNo = new Label();
            lblBirthday = new Label();
            txtName = new TextBox();
            lblName = new Label();
            menuStrip1 = new MenuStrip();
            menuNew = new ToolStripMenuItem();
            menuModify = new ToolStripMenuItem();
            menuFind = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvMemberDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bdsMemberDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bdsMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvMemberDetails
            // 
            dgvMemberDetails.AllowUserToAddRows = false;
            dgvMemberDetails.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.SelectionBackColor = Color.Silver;
            dgvMemberDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvMemberDetails.AutoGenerateColumns = false;
            dgvMemberDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMemberDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMemberDetails.Columns.AddRange(new DataGridViewColumn[] { colDate, colItem, colCount, colHeight, colWeight });
            dgvMemberDetails.DataSource = bdsMemberDetails;
            dgvMemberDetails.Dock = DockStyle.Fill;
            dgvMemberDetails.Location = new Point(0, 0);
            dgvMemberDetails.Margin = new Padding(5);
            dgvMemberDetails.Name = "dgvMemberDetails";
            dgvMemberDetails.ReadOnly = true;
            dgvMemberDetails.RowHeadersWidth = 62;
            dgvMemberDetails.Size = new Size(1022, 541);
            dgvMemberDetails.TabIndex = 0;
            // 
            // colDate
            // 
            colDate.DataPropertyName = "Date";
            dataGridViewCellStyle2.Format = "yyyy年MM月dd日 HH时mm分ss秒";
            colDate.DefaultCellStyle = dataGridViewCellStyle2;
            colDate.HeaderText = "日期";
            colDate.MinimumWidth = 8;
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            // 
            // colItem
            // 
            colItem.DataPropertyName = "Item";
            colItem.HeaderText = "项目";
            colItem.MinimumWidth = 8;
            colItem.Name = "colItem";
            colItem.ReadOnly = true;
            // 
            // colCount
            // 
            colCount.DataPropertyName = "Count";
            colCount.HeaderText = "次数";
            colCount.MinimumWidth = 8;
            colCount.Name = "colCount";
            colCount.ReadOnly = true;
            // 
            // colHeight
            // 
            colHeight.DataPropertyName = "Height";
            dataGridViewCellStyle3.Format = "##.##";
            colHeight.DefaultCellStyle = dataGridViewCellStyle3;
            colHeight.HeaderText = "身高 (CM)";
            colHeight.MinimumWidth = 8;
            colHeight.Name = "colHeight";
            colHeight.ReadOnly = true;
            // 
            // colWeight
            // 
            colWeight.DataPropertyName = "Weight";
            dataGridViewCellStyle4.Format = "##.##";
            colWeight.DefaultCellStyle = dataGridViewCellStyle4;
            colWeight.HeaderText = "体重 (KG)";
            colWeight.MinimumWidth = 8;
            colWeight.Name = "colWeight";
            colWeight.ReadOnly = true;
            // 
            // _bdsMemberDetails
            // 
            bdsMemberDetails.DataMember = "Details";
            bdsMemberDetails.DataSource = bdsMembers;
            // 
            // _bdsMembers
            // 
            bdsMembers.DataSource = typeof(Models.Member);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 35);
            splitContainer1.Margin = new Padding(5);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(txtAge);
            splitContainer1.Panel1.Controls.Add(lblAge);
            splitContainer1.Panel1.Controls.Add(combGender);
            splitContainer1.Panel1.Controls.Add(lblGender);
            splitContainer1.Panel1.Controls.Add(dtpBirthday);
            splitContainer1.Panel1.Controls.Add(txtPhone);
            splitContainer1.Panel1.Controls.Add(lblPhone);
            splitContainer1.Panel1.Controls.Add(txtCardNo);
            splitContainer1.Panel1.Controls.Add(lblCardNo);
            splitContainer1.Panel1.Controls.Add(lblBirthday);
            splitContainer1.Panel1.Controls.Add(txtName);
            splitContainer1.Panel1.Controls.Add(lblName);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvMemberDetails);
            splitContainer1.Size = new Size(1022, 637);
            splitContainer1.SplitterDistance = 90;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 1;
            // 
            // txtAge
            // 
            txtAge.DataBindings.Add(new Binding("Text", bdsMembers, "Age", true));
            txtAge.Enabled = false;
            txtAge.Location = new Point(467, 51);
            txtAge.Margin = new Padding(5);
            txtAge.Name = "txtAge";
            txtAge.ReadOnly = true;
            txtAge.Size = new Size(187, 39);
            txtAge.TabIndex = 40;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Location = new Point(359, 54);
            lblAge.Margin = new Padding(5, 0, 5, 0);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(128, 31);
            lblAge.TabIndex = 39;
            lblAge.Text = "年      龄：";
            // 
            // combGender
            // 
            combGender.DataBindings.Add(new Binding("Text", bdsMembers, "Gender", true));
            combGender.DisplayMember = "Value";
            combGender.DropDownStyle = ComboBoxStyle.DropDownList;
            combGender.Enabled = false;
            combGender.FormattingEnabled = true;
            combGender.Location = new Point(803, 14);
            combGender.Name = "combGender";
            combGender.Size = new Size(55, 39);
            combGender.TabIndex = 38;
            combGender.ValueMember = "Key";
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(695, 17);
            lblGender.Margin = new Padding(5, 0, 5, 0);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(128, 31);
            lblGender.TabIndex = 37;
            lblGender.Text = "性      别：";
            // 
            // dtpBirthday
            // 
            dtpBirthday.CustomFormat = "yyyy 年 MM 月 dd 日";
            dtpBirthday.DataBindings.Add(new Binding("Value", bdsMembers, "Birthday", true));
            dtpBirthday.Enabled = false;
            dtpBirthday.Format = DateTimePickerFormat.Custom;
            dtpBirthday.Location = new Point(114, 51);
            dtpBirthday.Margin = new Padding(5);
            dtpBirthday.Name = "dtpBirthday";
            dtpBirthday.Size = new Size(196, 39);
            dtpBirthday.TabIndex = 19;
            // 
            // txtPhone
            // 
            txtPhone.DataBindings.Add(new Binding("Text", bdsMembers, "Phone", true));
            txtPhone.Enabled = false;
            txtPhone.Location = new Point(467, 14);
            txtPhone.Margin = new Padding(5);
            txtPhone.Name = "txtPhone";
            txtPhone.ReadOnly = true;
            txtPhone.Size = new Size(187, 39);
            txtPhone.TabIndex = 18;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(359, 17);
            lblPhone.Margin = new Padding(5, 0, 5, 0);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(142, 31);
            lblPhone.TabIndex = 17;
            lblPhone.Text = "手        机：";
            // 
            // txtCardNo
            // 
            txtCardNo.DataBindings.Add(new Binding("Text", bdsMembers, "CardNo", true));
            txtCardNo.Enabled = false;
            txtCardNo.Location = new Point(803, 51);
            txtCardNo.Margin = new Padding(5);
            txtCardNo.Name = "txtCardNo";
            txtCardNo.ReadOnly = true;
            txtCardNo.Size = new Size(196, 39);
            txtCardNo.TabIndex = 16;
            // 
            // lblCardNo
            // 
            lblCardNo.AutoSize = true;
            lblCardNo.Location = new Point(703, 54);
            lblCardNo.Margin = new Padding(5, 0, 5, 0);
            lblCardNo.Name = "lblCardNo";
            lblCardNo.Size = new Size(134, 31);
            lblCardNo.TabIndex = 15;
            lblCardNo.Text = "会员卡号：";
            // 
            // lblBirthday
            // 
            lblBirthday.AutoSize = true;
            lblBirthday.Location = new Point(14, 57);
            lblBirthday.Margin = new Padding(5, 0, 5, 0);
            lblBirthday.Name = "lblBirthday";
            lblBirthday.Size = new Size(134, 31);
            lblBirthday.TabIndex = 13;
            lblBirthday.Text = "出生日期：";
            // 
            // txtName
            // 
            txtName.DataBindings.Add(new Binding("Text", bdsMembers, "Name", true));
            txtName.Enabled = false;
            txtName.Location = new Point(114, 14);
            txtName.Margin = new Padding(5);
            txtName.Name = "txtName";
            txtName.ReadOnly = true;
            txtName.Size = new Size(196, 39);
            txtName.TabIndex = 12;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(14, 14);
            lblName.Margin = new Padding(5, 0, 5, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(134, 31);
            lblName.TabIndex = 11;
            lblName.Text = "宝宝姓名：";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuNew, menuModify, menuFind });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(10, 3, 0, 3);
            menuStrip1.Size = new Size(1022, 35);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuNew
            // 
            menuNew.Name = "menuNew";
            menuNew.Size = new Size(64, 29);
            menuNew.Text = "新增";
            // 
            // menuModify
            // 
            menuModify.Name = "menuModify";
            menuModify.Size = new Size(64, 29);
            menuModify.Text = "修改";
            // 
            // menuFind
            // 
            menuFind.Name = "menuFind";
            menuFind.Size = new Size(65, 29);
            menuFind.Text = "查找";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1022, 672);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Font = new Font("Microsoft YaHei", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(5);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "会员管理系统";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMemberDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)bdsMemberDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)bdsMembers).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private SplitContainer splitContainer1;
        private TextBox txtPhone;
        private Label lblPhone;
        private TextBox txtCardNo;
        private Label lblCardNo;
        private Label lblBirthday;
        private TextBox txtName;
        private Label lblName;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuNew;
        private ToolStripMenuItem menuModify;
        private ToolStripMenuItem menuFind;
        private DateTimePicker dtpBirthday;
        private ComboBox combGender;
        private Label lblGender;
        private TextBox txtAge;
        private Label lblAge;
        private DataGridView dgvMemberDetails;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colItem;
        private DataGridViewTextBoxColumn colCount;
        private DataGridViewTextBoxColumn colHeight;
        private DataGridViewTextBoxColumn colWeight;
        private BindingSource bdsMembers;
        private BindingSource bdsMemberDetails;
    }
}

