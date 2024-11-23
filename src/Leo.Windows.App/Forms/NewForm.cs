// MIT License

using Leo.Windows.ViewModels;

namespace Leo.Windows.Forms
{
    public partial class NewForm : Form
    {
        private readonly CustomerViewModel _customer;

        public NewForm(CustomerViewModel customer)
        {
            _customer = customer;
            InitializeComponent();
            Load += (s, a) => InitializeDataBindings();
        }

        private void InitializeDataBindings()
        {
            txtName.DataBindings.Add(new Binding("Text", _customer, "Name"));
            txtCardNo.DataBindings.Add(new Binding("Text", _customer, "CardNo"));
            txtPhone.DataBindings.Add(new Binding("Text", _customer, "Phone"));
            dtpBirthday.DataBindings.Add(new Binding("Value", _customer, "Birthday"));

            for (int i = 0; i < combGender.Items.Count; i++)
            {
                if (string.Equals(
                    _customer.Gender,
                    ((KeyValuePair<string, string>)combGender.Items[i]!).Key,
                    StringComparison.OrdinalIgnoreCase))
                {
                    combGender.SelectedIndex = i;
                }
            }
            _customer.PropertyChanged += (s, a) =>
            {
                if (string.Equals("Gender", a.PropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    for (int i = 0; i < combGender.Items.Count; i++)
                    {
                        if (string.Equals(
                            _customer.Gender,
                            ((KeyValuePair<string, string>)combGender.Items[i]!).Key,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            combGender.SelectedIndex = i;
                        }
                    }
                }
            };
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            combGender.Items.Add(new KeyValuePair<string, string>("Male", "男"));
            combGender.Items.Add(new KeyValuePair<string, string>("Female", "女"));
            btnCancel.Click += (s, a) =>
            {
                Close();
                DialogResult = DialogResult.Cancel;
            };
            btnConfirm.Click += (s, a) =>
            {
                if (combGender.SelectedItem != null)
                {
                    _customer.Gender = ((KeyValuePair<string, string>)combGender.SelectedItem).Key;
                }
                DialogResult = DialogResult.OK;
            };
        }
    }
}
