using System.Collections.Generic;
using System.Windows.Forms;

namespace HoneyLovely
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            combGender.Items.Add(new KeyValuePair<string, string>("boy", "男宝宝"));
            combGender.Items.Add(new KeyValuePair<string, string>("girl", "女宝宝"));
            combGender.SelectedIndex = 0;

            menuNew.Click += (s, a) =>
            {
                using (var frm = new NewForm())
                {
                    frm.Text = "新增会员信息";
                    frm.ShowDialog();
                }
            };

            menuModify.Click += (s, a) =>
            {
                using (var frm = new NewForm())
                {
                    frm.Text = "修改会议信息";
                    frm.ShowDialog();
                }
            };

            menuFind.Click += (s, a) =>
            {
                using (var frm = new FindForm())
                {
                    frm.ShowDialog();
                }
            };
        }
    }
}
