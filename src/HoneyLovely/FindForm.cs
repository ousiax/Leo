using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class FindForm : Form
    {
        private readonly IReadOnlyList<Member> _members;

        public Member Index { get; private set; }

        public FindForm(IReadOnlyList<Member> members)
        {
            InitializeComponent();
            InitializeDataBindings();
            _members = members;
            bdsMembers.DataSource = _members;
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

            this.dgvMembers.RowTemplate.Height += 10;
        }


        private void SearchForm_Load(object sender, EventArgs e)
        {
            this.dgvMembers.MouseDoubleClick += (s, a) =>
            {
                var dgvMembers = s as DataGridView;
                if (a.Button == MouseButtons.Left && dgvMembers.HitTest(a.X, a.Y).RowIndex >= 0)
                {
                    var mem = dgvMembers.CurrentRow.DataBoundItem as Member;
                    if (mem != null)
                    {
                        this.Index = mem;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            };

            this.btnSearch.Click += (s, a) =>
            {
                var selectedValue = ((KeyValuePair<string, string>)combSearchField.SelectedItem).Key;

                switch (selectedValue)
                {
                    case "name":
                        this.bdsMembers.DataSource = _members.Where(o => o.Name.Contains(txtSearchText.Text)).ToList();
                        break;
                    case "card":
                        this.bdsMembers.DataSource = _members.Where(o => o.CardNo.Contains(txtSearchText.Text)).ToList();
                        break;
                    case "phone":
                        this.bdsMembers.DataSource = _members.Where(o => o.Phone.Contains(txtSearchText.Text)).ToList();
                        break;
                }
            };
        }
    }
}
