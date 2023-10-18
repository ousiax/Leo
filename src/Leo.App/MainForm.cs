using Leo.UI;
using Leo.UI.Models;

namespace Leo.App
{
    public partial class MainForm : Form
    {
        private readonly IMemberService _memberService;
        private readonly IMemberDetailService _memberDetailService;

        public MainForm(IMemberService memberService, IMemberDetailService memberDetailService)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;

            InitializeComponent();
            InitializeContextMenu();
            LoadMembersAsync();
        }

        private List<Member> Members { get { return bdsMembers.List as List<Member>; } }

        private Member CurrentMember { get { return bdsMembers.Current as Member; } }

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
                                        this.Invoke(() => bdsMemberDetails.ResetBindings(false));
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

        private Task LoadMembersAsync()
        {
            return _memberService.GetAsync().ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully)
                {
                    //var restore = _bdsMembers.RaiseListChangedEvents;
                    //_bdsMembers.RaiseListChangedEvents = false;
                    //foreach (var member in t.Result)
                    //{
                    //    _bdsMembers.Add(member);
                    //}
                    //if (restore)
                    //{
                    //    _bdsMembers.ResetBindings(true);
                    //}
                    //_bdsMembers.RaiseListChangedEvents = restore;

                    var members = new List<Member>();
                    members.AddRange(t.Result);
                    this.Invoke(() => bdsMembers.DataSource = members);
                    //_bdsMembers.ResetBindings(false);
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
                            this.Invoke(() =>
                            {
                                bdsMembers.ResetBindings(false);
                                bdsMembers.Position = Members.IndexOf(newMember);
                            });
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
                        bdsMembers.ResetBindings(false);
                        bdsMembers.Position = Members.IndexOf(CurrentMember);
                    });
                }
            };

            menuFind.Click += (s, a) =>
            {
                using var frm = new FindForm(Members);
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    bdsMembers.Position = Members.IndexOf(frm.Index);
                }
            };
        }
    }
}
