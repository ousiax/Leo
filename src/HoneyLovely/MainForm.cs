using HoneyLovely.Models;

namespace HoneyLovely
{
    public partial class MainForm : Form
    {
        private readonly IMemberService _memberService;
        private readonly IMemberDetailService _memberDetailService;
        private readonly BindingSource _bindingSource = new() { DataSource = new List<Member>() };

        private List<Member> Members { get { return _bindingSource.DataSource as List<Member>; } }
        private Member Member { get { return _bindingSource.Current as Member; } }

        public MainForm(IMemberService memberService, IMemberDetailService memberDetailService)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;

            InitializeComponent();
            InitializeDataBinding();
            InitializeDatabase();
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
                                Id = Member.Id,
                                Date = DateTime.Now
                            }))
                            {
                                frm.Text = "新增";
                                var result = frm.ShowDialog();
                                if(result == DialogResult.OK)
                                {
                                    var newDetail = new MemberDetail().Dump(frm.Detail);
                                    _memberDetailService.CreateAsync(newDetail).ContinueWith(t=>{
                                        if(t.IsCompletedSuccessfully){Member.Details.Add(newDetail);
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

            this.txtName.DataBindings.Add(new Binding("Text", _bindingSource, "Name"));
            this.txtAge.DataBindings.Add(new Binding("Text", _bindingSource, "Age"));
            this.txtCardNo.DataBindings.Add(new Binding("Text", _bindingSource, "CardNo"));
            this.txtPhone.DataBindings.Add(new Binding("Text", _bindingSource, "Phone"));
            this.dtpBirthday.DataBindings.Add(new Binding("Value", _bindingSource, "Birthday"));

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

            this.dataGridView1.AutoGenerateColumns = false;
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
                    _bindingSource.ResetBindings(false);
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
                        Members.Add(newMember);
                        Member.Dump(newMember);
                    }
                }
            };

            menuModify.Click += (s, a) =>
            {
                using (var frm = new NewForm())
                {
                    frm.Text = "修改会议信息";
                    frm.CurrentMember.Dump(Member);
                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Member.Dump(frm.CurrentMember);
                        Members.First(o => o.Id.Equals(Member.Id)).Dump(Member);
                        _memberService.UpdateAsync(Member).ContinueWith(t => { });
                    }
                }
            };

            menuFind.Click += (s, a) =>
            {
                using (var frm = new FindForm(Members))
                {
                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Member.Dump(frm.Member);
                    }
                }
            };
        }
    }
}
