using Leo.Windows.ViewModels;
using System.Diagnostics.CodeAnalysis;

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
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("phone", "手机"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("name", "姓名"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("card", "卡号"));
            this.combSearchField.SelectedIndex = 0;

            this.colGender.Items.Add(new KeyValuePair<string, string>("Male", "男"));
            this.colGender.Items.Add(new KeyValuePair<string, string>("Female", "女"));
            this.colGender.ValueCustomer = "Key";
            this.colGender.DisplayCustomer = "Value";

            this.dgvCustomers.RowTemplate.Height += 10;
        }


        private void SearchForm_Load(object sender, EventArgs e)
        {
            this.dgvCustomers.MouseDoubleClick += (s, a) =>
            {
                if (a.Button == MouseButtons.Left && dgvCustomers.HitTest(a.X, a.Y).RowIndex >= 0)
                {
                    var mem = dgvCustomers.CurrentRow.DataBoundItem as CustomerViewModel;
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
                        bdsCustomers.DataSource = _customers.Where(m => m.Name?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                    case "card":
                        this.bdsCustomers.DataSource = _customers.Where(m => m.CardNo?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                    case "phone":
                        this.bdsCustomers.DataSource = _customers.Where(m => m.Phone?.Contains(txtSearchText.Text) ?? false).ToList();
                        break;
                }
            };
        }
    }
}
