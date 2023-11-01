using Leo.App.ViewModels;

namespace Leo.App.Forms
{
    public partial class NewForm : Form
    {
        private readonly MemberViewModel _member;

        public NewForm(MemberViewModel member)
        {
            this._member = member;
            InitializeComponent();
            this.Load += (s, a) => InitializeDataBindings();
        }

        private void InitializeDataBindings()
        {
            this.txtName.DataBindings.Add(new Binding("Text", _member, "Name"));
            this.txtCardNo.DataBindings.Add(new Binding("Text", _member, "CardNo"));
            this.txtPhone.DataBindings.Add(new Binding("Text", _member, "Phone"));
            this.dtpBirthday.DataBindings.Add(new Binding("Value", _member, "Birthday"));

            for (int i = 0; i < this.combGender.Items.Count; i++)
            {
                if (string.Equals(_member.Gender, ((KeyValuePair<string, string>)this.combGender.Items[i]).Key))
                {
                    this.combGender.SelectedIndex = i;
                }
            }
            _member.PropertyChanged += (s, a) =>
            {
                if (string.Equals("Gender", a.PropertyName))
                {
                    for (int i = 0; i < this.combGender.Items.Count; i++)
                    {
                        if (string.Equals(_member.Gender, ((KeyValuePair<string, string>)this.combGender.Items[i]).Key))
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
                    this._member.Gender = ((KeyValuePair<string, string>)combGender.SelectedItem).Key;
                }
                this.DialogResult = DialogResult.OK;
            };
        }
    }
}
