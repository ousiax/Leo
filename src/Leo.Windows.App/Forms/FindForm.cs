// MIT License

using System.Diagnostics.CodeAnalysis;
using Leo.Windows.ViewModels;

namespace Leo.Windows.Forms
{
    public partial class FindForm : Form
    {
        private readonly IReadOnlyList<CustomerViewModel> _customers;

        public CustomerViewModel? Index { get; private set; }

        public FindForm([DisallowNull] IReadOnlyList<CustomerViewModel> customers)
        {
            _customers = customers ?? throw new ArgumentNullException(nameof(customers));
            InitializeComponent();
            InitializeDataBindings();
            bdsCustomers.DataSource = _customers;
        }

        private void InitializeDataBindings()
        {
            combSearchField.Items.Add(new KeyValuePair<string, string>("phone", "手机"));
            combSearchField.Items.Add(new KeyValuePair<string, string>("name", "姓名"));
            combSearchField.Items.Add(new KeyValuePair<string, string>("card", "卡号"));
            combSearchField.SelectedIndex = 0;

            colGender.Items.Add(new KeyValuePair<string, string>("Male", "男"));
            colGender.Items.Add(new KeyValuePair<string, string>("Female", "女"));
            colGender.ValueMember = "Key";
            colGender.DisplayMember = "Value";

            dgvCustomers.RowTemplate.Height += 10;
        }


        private void SearchForm_Load(object sender, EventArgs e)
        {
            dgvCustomers.MouseDoubleClick += (s, a) =>
            {
                if (a.Button == MouseButtons.Left && dgvCustomers.HitTest(a.X, a.Y).RowIndex >= 0)
                {
                    if (dgvCustomers.CurrentRow.DataBoundItem is CustomerViewModel mem)
                    {
                        Index = mem;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            };

            btnSearch.Click += (s, a) =>
            {
                string selectedValue = ((KeyValuePair<string, string>)combSearchField.SelectedItem!).Key;

                switch (selectedValue)
                {
                    case "name":
                        bdsCustomers.DataSource = _customers.Where(m => m.Name?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                    case "card":
                        bdsCustomers.DataSource = _customers.Where(m => m.CardNo?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                    case "phone":
                        bdsCustomers.DataSource = _customers.Where(m => m.Phone?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                }
            };
        }
    }
}
