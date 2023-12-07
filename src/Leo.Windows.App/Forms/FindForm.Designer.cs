using Leo.Windows.ViewModels;
using System.Windows.Forms;

namespace Leo.Windows.Forms
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            btnCancel = new Button();
            btnSearch = new Button();
            txtSearchText = new TextBox();
            combSearchField = new ComboBox();
            dgvCustomers = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colGender = new DataGridViewComboBoxColumn();
            colBirthday = new DataGridViewTextBoxColumn();
            colAge = new DataGridViewTextBoxColumn();
            colCardNo = new DataGridViewTextBoxColumn();
            bdsCustomers = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bdsCustomers).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(709, 14);
            btnCancel.Margin = new Padding(5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(125, 37);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(563, 14);
            btnSearch.Margin = new Padding(5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(125, 37);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "查找";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearchText
            // 
            txtSearchText.Location = new Point(119, 15);
            txtSearchText.Margin = new Padding(5);
            txtSearchText.Name = "txtSearchText";
            txtSearchText.Size = new Size(292, 39);
            txtSearchText.TabIndex = 6;
            // 
            // combSearchField
            // 
            combSearchField.DisplayCustomer = "Value";
            combSearchField.DropDownStyle = ComboBoxStyle.DropDownList;
            combSearchField.FormattingEnabled = true;
            combSearchField.Location = new Point(12, 14);
            combSearchField.Margin = new Padding(5);
            combSearchField.Name = "combSearchField";
            combSearchField.Size = new Size(97, 39);
            combSearchField.TabIndex = 5;
            combSearchField.ValueCustomer = "Key";
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle1.SelectionBackColor = Color.Silver;
            dgvCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colPhone, colGender, colBirthday, colAge, colCardNo });
            dgvCustomers.DataSource = bdsCustomers;
            dgvCustomers.Location = new Point(12, 62);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 62;
            dgvCustomers.RowTemplate.Height = 33;
            dgvCustomers.Size = new Size(824, 281);
            dgvCustomers.TabIndex = 9;
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
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "姓名";
            colName.MinimumWidth = 8;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "Phone";
            colPhone.HeaderText = "手机";
            colPhone.MinimumWidth = 8;
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
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
            // colBirthday
            // 
            colBirthday.DataPropertyName = "Birthday";
            dataGridViewCellStyle2.Format = "yyyy/MM/dd";
            colBirthday.DefaultCellStyle = dataGridViewCellStyle2;
            colBirthday.HeaderText = "生日";
            colBirthday.MinimumWidth = 8;
            colBirthday.Name = "colBirthday";
            colBirthday.ReadOnly = true;
            // 
            // colAge
            // 
            colAge.DataPropertyName = "Age";
            colAge.HeaderText = "年龄";
            colAge.MinimumWidth = 8;
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            // 
            // colCardNo
            // 
            colCardNo.DataPropertyName = "CardNo";
            colCardNo.HeaderText = "卡号";
            colCardNo.MinimumWidth = 8;
            colCardNo.Name = "colCardNo";
            colCardNo.ReadOnly = true;
            // 
            // bdsCustomers
            // 
            bdsCustomers.DataSource = typeof(CustomerViewModel);
            // 
            // FindForm
            // 
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(848, 355);
            Controls.Add(dgvCustomers);
            Controls.Add(btnCancel);
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
            Load += SearchForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ((System.ComponentModel.ISupportInitialize)bdsCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnSearch;
        private TextBox txtSearchText;
        private ComboBox combSearchField;
        private DataGridView dgvCustomers;
        private BindingSource bdsCustomers;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewComboBoxColumn colGender;
        private DataGridViewTextBoxColumn colBirthday;
        private DataGridViewTextBoxColumn colAge;
        private DataGridViewTextBoxColumn colCardNo;
    }
}