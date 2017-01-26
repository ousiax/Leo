using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class MainForm : Form
    {
        private const string CREATE_MEMBER_SQL = "CREATE TABLE IF NOT EXISTS member (id integer NOT NULL, name text NOT NULL, phone text NOT NULL, gender text, birthday datetime, cardno text);";

        private const string CREATE_MEMBER_DETAIL_SQL = "CREATE TABLE IF NOT EXISTS member_detail (id integer NOT NULL, date text NOT NULL, item text NOT NULL, count integer, height number, weight number);";

        private readonly IList<Member> _member = new List<Member>();

        public MainForm()
        {
            InitializeDatabase();
            InitializeComponent();
            InitializeDataBinding();
        }

        private void InitializeDataBinding()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.combGender.Items.Add(new KeyValuePair<string, string>("boy", "男"));
            this.combGender.Items.Add(new KeyValuePair<string, string>("girl", "女"));

            _member.Add(new Member { Name = "张三", Gender = "girl", Birthday = DateTime.Now.AddDays(-180) });
            _member.Add(new Member
            {
                Name = "李四",
                Gender = "boy",
                Birthday = DateTime.Now.AddDays(-540),
                Phone = "18651625872",
                CardNo = "18651625872",
                Detail = new List<MemberDetail> { new MemberDetail { Date = DateTime.Now.AddDays(-30) }, new MemberDetail { Date = DateTime.Now.AddDays(-30) }, new MemberDetail { Date = DateTime.Now.AddDays(-30) }, new MemberDetail { Date = DateTime.Now.AddDays(-30) }, new MemberDetail { Date = DateTime.Now.AddDays(-30) } }
            });

            this.memberBindingSource.DataSource = _member;
            this.dataGridView1.DataSource = this.memberBindingSource;
            this.dataGridView1.DataMember = "Detail";

            this.memberBindingSource.Position = 1;

            var current = (Member)this.memberBindingSource.Current;
            for (int i = 0; i < this.combGender.Items.Count; i++)
            {
                if (string.Equals(current.Gender, ((KeyValuePair<string, string>)this.combGender.Items[i]).Key))
                {
                    this.combGender.SelectedIndex = i;
                }
            }
        }

        private void InitializeDatabase()
        {
            var connStr = ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = CREATE_MEMBER_SQL;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = CREATE_MEMBER_DETAIL_SQL;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT * FROM member";
                    var dataSet = new DataSet();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dataSet);

                    dataSet = new DataSet();
                    cmd.CommandText = "SELECT * FROM member_detail";
                    SQLiteDataAdapter adapter2 = new SQLiteDataAdapter(cmd);
                    adapter.Fill(dataSet);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            menuNew.Click += (s, a) =>
            {
                using (var frm = new NewForm())
                {
                    frm.Text = "新增会员信息";
                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _member.Add(new Member
                        {
                            Name = frm.txtName.Text,
                            Birthday = frm.dtpBirthday.Value,
                            Gender = (string)frm.combGender.SelectedValue,
                            Phone = frm.txtPhone.Text,
                            CardNo = frm.txtCardNo.Text
                        });
                        memberBindingSource.Position = _member.Count - 1;
                    }
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
