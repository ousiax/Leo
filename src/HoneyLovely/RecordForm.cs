using System;
using System.Windows.Forms;
using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class RecordForm : Form
    {
        private readonly MemberDetail _detail;

        public MemberDetail Detail { get { return _detail; } }

        public RecordForm(MemberDetail detail)
        {
            _detail = detail;
            InitializeComponent();
            InitializeDataBinding();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.btnConfirm.Click += (s, a) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.btnCancel.Click += (s, a) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
        }

        private void InitializeDataBinding()
        {
            this.dtpDate.DataBindings.Add(new Binding("Value", _detail, "Date"));
            this.txtItem.DataBindings.Add(new Binding("Text", _detail, "Item"));
            this.txtCount.DataBindings.Add(new Binding("Text", _detail, "Count"));
            this.txtHeight.DataBindings.Add(new Binding("Text", _detail, "Height"));
            this.txtWeight.DataBindings.Add(new Binding("Text", _detail, "Weight"));
        }

        private void RecordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
