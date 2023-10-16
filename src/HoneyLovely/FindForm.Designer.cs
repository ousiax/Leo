namespace HoneyLovely
{
    partial class FindForm
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            combSearchField = new ComboBox();
            txtSearchText = new TextBox();
            btnSearch = new Button();
            dgvMembers = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colBirthday = new DataGridViewTextBoxColumn();
            colGender = new DataGridViewComboBoxColumn();
            colAge = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colCardNo = new DataGridViewTextBoxColumn();
            colId = new DataGridViewTextBoxColumn();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            SuspendLayout();
            // 
            // combSearchField
            // 
            combSearchField.DisplayMember = "Value";
            combSearchField.DropDownStyle = ComboBoxStyle.DropDownList;
            combSearchField.FormattingEnabled = true;
            combSearchField.Location = new Point(20, 19);
            combSearchField.Margin = new Padding(5);
            combSearchField.Name = "combSearchField";
            combSearchField.Size = new Size(97, 39);
            combSearchField.TabIndex = 0;
            combSearchField.ValueMember = "Key";
            // 
            // txtSearchText
            // 
            txtSearchText.Location = new Point(130, 19);
            txtSearchText.Margin = new Padding(5);
            txtSearchText.Name = "txtSearchText";
            txtSearchText.Size = new Size(292, 39);
            txtSearchText.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(574, 14);
            btnSearch.Margin = new Padding(5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(125, 37);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "查找";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // dgvMembers
            // 
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = Color.Silver;
            dgvMembers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMembers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMembers.Columns.AddRange(new DataGridViewColumn[] { colName, colBirthday, colGender, colAge, colPhone, colCardNo, colId });
            dgvMembers.Location = new Point(20, 63);
            dgvMembers.Margin = new Padding(5);
            dgvMembers.MultiSelect = false;
            dgvMembers.Name = "dgvMembers";
            dgvMembers.ReadOnly = true;
            dgvMembers.RowHeadersWidth = 62;
            dgvMembers.Size = new Size(814, 273);
            dgvMembers.TabIndex = 3;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "姓名";
            colName.MinimumWidth = 8;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colBirthday
            // 
            colBirthday.DataPropertyName = "Birthday";
            dataGridViewCellStyle4.Format = "yyyy/MM/dd";
            colBirthday.DefaultCellStyle = dataGridViewCellStyle4;
            colBirthday.HeaderText = "出生日期";
            colBirthday.MinimumWidth = 8;
            colBirthday.Name = "colBirthday";
            colBirthday.ReadOnly = true;
            // 
            // colGender
            // 
            colGender.DataPropertyName = "Gender";
            colGender.HeaderText = "性别";
            colGender.MinimumWidth = 8;
            colGender.Name = "colGender";
            colGender.ReadOnly = true;
            colGender.Resizable = DataGridViewTriState.True;
            colGender.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // colAge
            // 
            colAge.DataPropertyName = "Age";
            colAge.HeaderText = "年龄";
            colAge.MinimumWidth = 8;
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "Phone";
            colPhone.HeaderText = "手机";
            colPhone.MinimumWidth = 8;
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            // 
            // colCardNo
            // 
            colCardNo.DataPropertyName = "CardNo";
            colCardNo.HeaderText = "卡号";
            colCardNo.MinimumWidth = 8;
            colCardNo.Name = "colCardNo";
            colCardNo.ReadOnly = true;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Id";
            colId.MinimumWidth = 8;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(709, 14);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(125, 37);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // FindForm
            // 
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(848, 355);
            Controls.Add(btnCancel);
            Controls.Add(dgvMembers);
            Controls.Add(btnSearch);
            Controls.Add(txtSearchText);
            Controls.Add(combSearchField);
            Font = new Font("Microsoft YaHei", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(5);
            MaximizeBox = false;
            Name = "FindForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "查找会员";
            Load += FindForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox combSearchField;
        private TextBox txtSearchText;
        private Button btnSearch;
        private DataGridView dgvMembers;
        private Button btnCancel;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colBirthday;
        private DataGridViewComboBoxColumn colGender;
        private DataGridViewTextBoxColumn colAge;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colCardNo;
        private DataGridViewTextBoxColumn colId;
    }
}