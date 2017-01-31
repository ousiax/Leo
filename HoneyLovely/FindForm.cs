using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class FindForm : Form
    {
        private readonly IList<Member> _members;

        public Member Member { get; private set; } = new Member();

        public FindForm(IList<Member> members)
        {
            _members = members;
            InitializeComponent();
            InitializeDataBindings();
        }

        private void InitializeDataBindings()
        {
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("phone", "手机"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("name", "姓名"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("card", "卡号"));
            this.combSearchField.SelectedIndex = 0;

            this.colGender.Items.Add(new KeyValuePair<string, string>("boy", "男"));
            this.colGender.Items.Add(new KeyValuePair<string, string>("girl", "女"));
            this.colGender.ValueMember = "Key";
            this.colGender.DisplayMember = "Value";

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = _members;

            this.dataGridView1.RowTemplate.Height += 10;
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            this.btnSearch.Click += (s, a) =>
            {
                var selectedValue = ((KeyValuePair<string, string>)combSearchField.SelectedItem).Key;

                switch (selectedValue)
                {
                    case "name":
                        this.dataGridView1.DataSource = _members.Where(o => o.Name.Contains(txtSearchText.Text)).ToList();
                        break;
                    case "card":
                        this.dataGridView1.DataSource = _members.Where(o => o.CardNo.Contains(txtSearchText.Text)).ToList();
                        break;
                    case "phone":
                        this.dataGridView1.DataSource = _members.Where(o => o.Phone.Contains(txtSearchText.Text)).ToList();
                        break;
                }
            };

            this.dataGridView1.MouseDoubleClick += (s, a) =>
            {
                if (a.Button == MouseButtons.Left && dataGridView1.HitTest(a.X, a.Y).RowIndex > 0)
                {
                    var mem = dataGridView1.CurrentRow.DataBoundItem as Member;
                    if (mem != null)
                    {
                        this.Member.Dump(mem);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            };
        }
    }
}
