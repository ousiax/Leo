using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HoneyLovely
{
    public partial class NewForm : Form
    {
        public NewForm()
        {
            InitializeComponent();
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            this.combGender.Items.Add(new KeyValuePair<string, string>("boy", "男宝宝"));
            this.combGender.Items.Add(new KeyValuePair<string, string>("girl", "女宝宝"));
            this.btnCancel.Click += (s, a) =>
            {
                this.Close();
            };
        }
    }
}
