// MIT License

using Leo.Windows.ViewModels;

namespace Leo.Windows.Forms
{
    public partial class RecordForm : Form
    {
        private readonly CustomerDetailViewModel _detail;

        public RecordForm(CustomerDetailViewModel detail)
        {
            _detail = detail;
            InitializeComponent();
            InitializeDataBinding();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            btnConfirm.Click += (s, a) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
            btnCancel.Click += (s, a) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        private void InitializeDataBinding()
        {
            dtpDate.DataBindings.Add(new Binding("Value", _detail, "Date"));
            txtItem.DataBindings.Add(new Binding("Text", _detail, "Item"));
            txtCount.DataBindings.Add(new Binding("Text", _detail, "Count"));
            txtHeight.DataBindings.Add(new Binding("Text", _detail, "Height"));
            txtWeight.DataBindings.Add(new Binding("Text", _detail, "Weight"));
        }

        private void RecordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
