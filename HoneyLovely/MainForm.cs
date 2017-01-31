using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;
using Alyio.Extensions;
using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class MainForm : Form
    {
        private const string CREATE_MEMBER_SQL = "CREATE TABLE IF NOT EXISTS member (id text PRIMARY KEY, name text NOT NULL, phone text NOT NULL, gender text, birthday text, cardno text);";

        private const string CREATE_MEMBER_DETAIL_SQL = "CREATE TABLE IF NOT EXISTS member_detail (id text NOT NULL, date text NOT NULL, item text NOT NULL, count integer, height number, weight number);";

        private string _connectionString;

        private readonly IList<Member> _members = new List<Member>();
        private readonly Member _currentMember = new Member();

        public MainForm()
        {
            InitializeComponent();
            InitializeDataBinding();
            InitializeDatabase();
            var mem = _members.FirstOrDefault();
            if (mem != null)
            {
                _currentMember.Dump(mem);
            }
        }

        private void InitializeDataBinding()
        {
            this.combGender.Items.Add(new KeyValuePair<string, string>("boy", "男"));
            this.combGender.Items.Add(new KeyValuePair<string, string>("girl", "女"));

            this.dataGridView1.AutoGenerateColumns = false;
            this.txtName.DataBindings.Add(new Binding("Text", _currentMember, "Name"));
            this.txtAge.DataBindings.Add(new Binding("Text", _currentMember, "Age"));
            this.txtCardNo.DataBindings.Add(new Binding("Text", _currentMember, "CardNo"));
            this.txtPhone.DataBindings.Add(new Binding("Text", _currentMember, "Phone"));
            this.dtpBirthday.DataBindings.Add(new Binding("Value", _currentMember, "Birthday"));

            _currentMember.PropertyChanged += (s, a) =>
            {
                if (string.Equals("Gender", a.PropertyName))
                {
                    for (int i = 0; i < this.combGender.Items.Count; i++)
                    {
                        if (string.Equals(_currentMember.Gender, ((KeyValuePair<string, string>)this.combGender.Items[i]).Key))
                        {
                            this.combGender.SelectedIndex = i;
                        }
                    }
                }
            };

            this.dataGridView1.DataSource = new BindingList<MemberDetail>(_currentMember.Detail);
        }

        private void InitializeDatabase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = CREATE_MEMBER_SQL;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = CREATE_MEMBER_DETAIL_SQL;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT * FROM member";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _members.Add(new Member
                            {
                                Id = Guid.Parse(reader["id"].ToString()),
                                Name = reader["name"].ToString(),
                                Birthday = reader["birthday"].ToDateTime() ?? DateTime.Now,
                                CardNo = reader["cardno"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Phone = reader["phone"].ToString()
                            });
                        }
                    }

                    cmd.CommandText = "SELECT * FROM member_detail WHERE id = @id";
                    foreach (var mem in _members)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = mem.Id });
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mem.Detail.Add(new MemberDetail
                                {
                                    MemberId = Guid.Parse(reader["id"].ToString()),
                                    Date = reader["date"].ToDateTime().Value,
                                    Item = reader["item"].ToString(),
                                    Count = reader["count"].ToInt32() ?? 0,
                                    Height = reader["gender"].ToDouble() ?? 0.0d,
                                    Weight = reader["phone"].ToDouble() ?? 0.0d
                                });
                            }
                        }
                    }
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
                        var newMember = new Member().Dump(frm.CurrentMember);
                        using (var conn = new SQLiteConnection(_connectionString))
                        {
                            conn.Open();
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "INSERT INTO member (id, name , phone , gender , birthday , cardno) "
                                + "VALUES (@id, @name , @phone , @gender , @birthday , @cardno)";
                                cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = Guid.NewGuid() });
                                cmd.Parameters.Add(new SQLiteParameter("@name") { DbType = DbType.String, Value = newMember.Name });
                                cmd.Parameters.Add(new SQLiteParameter("@phone") { DbType = DbType.String, Value = newMember.Phone });
                                cmd.Parameters.Add(new SQLiteParameter("@gender") { DbType = DbType.String, Value = newMember.Gender });
                                cmd.Parameters.Add(new SQLiteParameter("@birthday") { DbType = DbType.String, Value = newMember.Birthday });
                                cmd.Parameters.Add(new SQLiteParameter("@cardno") { DbType = DbType.String, Value = newMember.CardNo });
                                cmd.ExecuteNonQuery();
                            }
                        }
                        _members.Add(newMember);
                        _currentMember.Dump(newMember);
                    }
                }
            };

            menuModify.Click += (s, a) =>
            {
                using (var frm = new NewForm())
                {
                    frm.Text = "修改会议信息";
                    frm.CurrentMember.Dump(_currentMember);
                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _currentMember.Dump(frm.CurrentMember);
                        _members.First(o => o.Id.Equals(_currentMember.Id)).Dump(_currentMember);
                        using (var conn = new SQLiteConnection(_connectionString))
                        {
                            conn.Open();
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "UPDATE member "
                                + "SET name=@name , phone=@phone , gender=@gender , birthday=@birthday , cardno=@cardno "
                                + "WHERE id=@id";
                                cmd.Parameters.Add(new SQLiteParameter("@id") { DbType = DbType.String, Value = Guid.NewGuid() });
                                cmd.Parameters.Add(new SQLiteParameter("@name") { DbType = DbType.String, Value = _currentMember.Name });
                                cmd.Parameters.Add(new SQLiteParameter("@phone") { DbType = DbType.String, Value = _currentMember.Phone });
                                cmd.Parameters.Add(new SQLiteParameter("@gender") { DbType = DbType.String, Value = _currentMember.Gender });
                                cmd.Parameters.Add(new SQLiteParameter("@birthday") { DbType = DbType.String, Value = _currentMember.Birthday });
                                cmd.Parameters.Add(new SQLiteParameter("@cardno") { DbType = DbType.String, Value = _currentMember.CardNo });
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            };

            menuFind.Click += (s, a) =>
            {
                using (var frm = new FindForm(_members))
                {
                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _currentMember.Dump(frm.Member);
                    }
                }
            };
        }
    }
}
