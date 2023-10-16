using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class MainForm : Form
    {
        private readonly IMemberService _memberService;
        private readonly IMemberDetailService _memberDetailService;
        private readonly BindingSource _bdsMembers = new() { DataSource = new List<Member>() };

        public MainForm(IMemberService memberService, IMemberDetailService memberDetailService)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;

            InitializeComponent();
            InitializeDataBinding();
            InitializeDatabase();
            InitializeContextMenu();
        }

        private List<Member> Members { get { return _bdsMembers.DataSource as List<Member>; } }

        private Member CurrentMember { get { return _bdsMembers.Current as Member; } }

        private void InitializeContextMenu()
        {
            this.dgvMemberDetails.MouseClick += (_, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var menu = new ContextMenuStrip();
                    menu.Items.AddRange(new ToolStripMenuItem[]
                     {
                        new ToolStripMenuItem("新增", null, (s, a) => {
                            var newMemberDetail = new MemberDetail
                            {
                                Id = CurrentMember.Id,
                                Date = DateTime.Now
                            };

                            using var frm = new RecordForm(newMemberDetail);
                            frm.Text = "新增";
                            var result = frm.ShowDialog();
                            if(result == DialogResult.OK)
                            {
                                _memberDetailService.CreateAsync(newMemberDetail).ContinueWith(t => {
                                    if(t.IsCompletedSuccessfully){
                                        CurrentMember.Details.Add(newMemberDetail);
                                    }
                                });
                            }
                        }),
                     });

                    int currentMouseOverRow = dgvMemberDetails.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow >= 0)
                    {
                        menu.Items.Add(new ToolStripMenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                    }

                    menu.Show(this.dgvMemberDetails, new Point(e.X, e.Y), ToolStripDropDownDirection.Left);
                }
            };
        }

        private void InitializeDataBinding()
        {
            this.combGender.Items.Add(new KeyValuePair<string, string>("boy", "男"));
            this.combGender.Items.Add(new KeyValuePair<string, string>("girl", "女"));

            this.txtName.DataBindings.Add(new Binding("Text", _bdsMembers, "Name"));
            this.txtAge.DataBindings.Add(new Binding("Text", _bdsMembers, "Age"));
            this.txtCardNo.DataBindings.Add(new Binding("Text", _bdsMembers, "CardNo"));
            this.txtPhone.DataBindings.Add(new Binding("Text", _bdsMembers, "Phone"));
            this.dtpBirthday.DataBindings.Add(new Binding("Value", _bdsMembers, "Birthday"));

            //Member.PropertyChanged += (s, a) =>
            //{
            //    if (string.Equals("Gender", a.PropertyName))
            //    {
            //        for (int i = 0; i < this.combGender.Items.Count; i++)
            //        {
            //            if (string.Equals(_bindingSource.Gender, ((KeyValuePair<string, string>)this.combGender.Items[i]).Key))
            //            {
            //                this.combGender.SelectedIndex = i;
            //            }
            //        }
            //    }
            //};

            this.dgvMemberDetails.AutoGenerateColumns = false;
            //this.dataGridView1.DataSource = new BindingSource { DataSource = _bindingSource };
            //this.dataGridView1.DataMember = "Details";
        }

        private void InitializeDatabase()
        {
            _memberService.GetAsync().ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    Members.AddRange(t.Result);
                    _bdsMembers.ResetBindings(false);
                }
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            menuNew.Click += (s, a) =>
            {
                var newMember = new Member();
                using var frm = new NewForm(newMember);
                frm.Text = "新增会员信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _memberService.CreateAsync(newMember).ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            Members.Add(newMember);
                            _bdsMembers.ResetBindings(false);
                            _bdsMembers.Position = Members.IndexOf(newMember);
                        }
                    });
                }
            };

            menuModify.Click += (s, a) =>
            {
                using var frm = new NewForm(CurrentMember);
                frm.Text = "修改会议信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _memberService.UpdateAsync(CurrentMember).ContinueWith(t =>
                    {
                        _bdsMembers.ResetBindings(false);
                        _bdsMembers.Position = Members.IndexOf(CurrentMember);
                    });
                }
            };

            menuFind.Click += (s, a) =>
            {
                using var frm = new FindForm(Members);
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _bdsMembers.Position = Members.IndexOf(frm.Index);
                }
            };
        }
    }
}
