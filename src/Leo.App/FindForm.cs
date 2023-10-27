using Leo.Data.Domain.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace Leo.App
{
    public partial class FindForm : Form
    {
        private readonly IReadOnlyList<MemberViewModel> _members;

        public MemberViewModel? Index { get; private set; }

        public FindForm([DisallowNull] IReadOnlyList<MemberViewModel> members)
        {
            _members = members ?? throw new ArgumentNullException(nameof(members));
            InitializeComponent();
            InitializeDataBindings();
            bdsMembers.DataSource = _members;
        }

        private void InitializeDataBindings()
        {
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("phone", "手机"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("name", "姓名"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("card", "卡号"));
            this.combSearchField.SelectedIndex = 0;

            this.colGender.Items.Add(new KeyValuePair<string, string>("Male", "男"));
            this.colGender.Items.Add(new KeyValuePair<string, string>("Female", "女"));
            this.colGender.ValueMember = "Key";
            this.colGender.DisplayMember = "Value";

            this.dgvMembers.RowTemplate.Height += 10;
        }


        private void SearchForm_Load(object sender, EventArgs e)
        {
            this.dgvMembers.MouseDoubleClick += (s, a) =>
            {
                if (a.Button == MouseButtons.Left && dgvMembers.HitTest(a.X, a.Y).RowIndex >= 0)
                {
                    var mem = dgvMembers.CurrentRow.DataBoundItem as MemberViewModel;
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
                        bdsMembers.DataSource = _members.Where(m => m.Name?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                    case "card":
                        this.bdsMembers.DataSource = _members.Where(m => m.CardNo?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                    case "phone":
                        this.bdsMembers.DataSource = _members.Where(m => m.Phone?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                }
            };
        }
    }
}
