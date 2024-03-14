// MIT License

using Leo.Windows.ViewModels;

namespace Leo.Windows.Forms
{
    public partial class NewForm : Form
    {
        private readonly CustomerViewModel _customer;

        public NewForm(CustomerViewModel customer)
        {
            this._customer = customer;
            InitializeComponent();
            this.Load += (s, a) => InitializeDataBindings();
        }

        private void InitializeDataBindings()
        {
            this.txtName.DataBindings.Add(new Binding("Text", _customer, "Name"));
            this.txtCardNo.DataBindings.Add(new Binding("Text", _customer, "CardNo"));
            this.txtPhone.DataBindings.Add(new Binding("Text", _customer, "Phone"));
            this.dtpBirthday.DataBindings.Add(new Binding("Value", _customer, "Birthday"));

            for (int i = 0; i < this.combGender.Items.Count; i++)
            {
                if (string.Equals(
                    _customer.Gender,
                    ((KeyValuePair<string, string>)this.combGender.Items[i]!).Key,
                    StringComparison.OrdinalIgnoreCase))
                {
                    this.combGender.SelectedIndex = i;
                }
            }
            _customer.PropertyChanged += (s, a) =>
            {
                if (string.Equals("Gender", a.PropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    for (int i = 0; i < this.combGender.Items.Count; i++)
                    {
                        if (string.Equals(
                            _customer.Gender,
                            ((KeyValuePair<string, string>)this.combGender.Items[i]!).Key,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            this.combGender.SelectedIndex = i;
                        }
                    }
                }
            };
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            this.combGender.Items.Add(new KeyValuePair<string, string>("Male", "男"));
            this.combGender.Items.Add(new KeyValuePair<string, string>("Female", "女"));
            this.btnCancel.Click += (s, a) =>
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            };
            this.btnConfirm.Click += (s, a) =>
            {
                if (combGender.SelectedItem != null)
                {
                    this._customer.Gender = ((KeyValuePair<string, string>)combGender.SelectedItem).Key;
                }
                this.DialogResult = DialogResult.OK;
            };
        }
    }
}
