using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class FindForm : Form
    {
        private readonly IReadOnlyList<Member> _members;

        public Member Index { get; private set; }

        public FindForm(IReadOnlyList<Member> members)
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

            this.dgvMembers.AutoGenerateColumns = false;
            this.dgvMembers.DataSource = _members;

            this.dgvMembers.RowTemplate.Height += 10;
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            this.btnSearch.Click += (s, a) =>
            {
                var selectedValue = ((KeyValuePair<string, string>)combSearchField.SelectedItem).Key;

                switch (selectedValue)
                {
                    case "name":
                        this.dgvMembers.DataSource = _members.Where(o => o.Name.Contains(txtSearchText.Text)).ToList();
                        break;
                    case "card":
                        this.dgvMembers.DataSource = _members.Where(o => o.CardNo.Contains(txtSearchText.Text)).ToList();
                        break;
                    case "phone":
                        this.dgvMembers.DataSource = _members.Where(o => o.Phone.Contains(txtSearchText.Text)).ToList();
                        break;
                }
            };

            this.dgvMembers.MouseDoubleClick += (s, a) =>
            {
                if (a.Button == MouseButtons.Left && dgvMembers.HitTest(a.X, a.Y).RowIndex > 0)
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
        }
    }
}
