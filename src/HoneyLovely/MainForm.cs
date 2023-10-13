using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class MainForm : Form
    {
        private readonly List<Member> _members = new List<Member>();
        private readonly Member _currentMember = new Member();
        private readonly IMemberService _memberService;
        private readonly IMemberDetailService _memberDetailService;

        public MainForm(IMemberService memberService, IMemberDetailService memberDetailService)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;

            InitializeComponent();
            InitializeDataBinding();
            InitializeDatabase();
            var mem = _members.FirstOrDefault();
            if (mem != null)
            {
                _currentMember.Dump(mem);
            }
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            this.dataGridView1.MouseClick += (_, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var menu = new ContextMenuStrip();
                    menu.Items.AddRange(new ToolStripMenuItem[]
                     {
                        new ToolStripMenuItem("新增",null, (s, a)=> {
                            using(var frm = new RecordForm(new MemberDetail
                            {
                                Id = _currentMember.Id,
                                Date = DateTime.Now
                            }))
                            {
                                frm.Text = "新增";
                                var result = frm.ShowDialog();
                                if(result == DialogResult.OK)
                                {
                                    var newDetail = new MemberDetail().Dump(frm.Detail);
                                    _memberDetailService.CreateAsync(newDetail).ContinueWith(t=>{
                                        if(t.IsCompletedSuccessfully){_currentMember.Details.Add(newDetail);
                                        }
                                    });
                                }
                            }
                        }),
                     });

                    int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow >= 0)
                    {
                        menu.Items.Add(new ToolStripMenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                    }

                    menu.Show(this.dataGridView1, new Point(e.X, e.Y), ToolStripDropDownDirection.Left);
                }
            };
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

            this.dataGridView1.DataSource = new BindingSource { DataSource = _currentMember.Details };
        }

        private void InitializeDatabase()
        {
            _memberService.GetAsync().ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    _members.AddRange(t.Result);
                }
            });
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

                        _memberService.CreateAsync(newMember).ContinueWith(t =>
                        {
                        });
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
                        _memberService.UpdateAsync(_currentMember).ContinueWith(t => { });
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
