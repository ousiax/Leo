using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HoneyLovely
{
    public partial class FindForm : Form
    {
        public FindForm()
        {
            InitializeComponent();
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("name", "姓名"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("card", "卡号"));
            this.combSearchField.Items.Add(new KeyValuePair<string, string>("phone", "手机"));
            this.combSearchField.SelectedIndex = 0;
        }
    }
}
