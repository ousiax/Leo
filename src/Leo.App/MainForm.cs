using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.ViewModels;
using Leo.UI;
using Microsoft.Extensions.Logging;

namespace Leo.App
{
    public partial class MainForm : Form
    {
        private readonly IMemberService _memberService;
        private readonly IMemberDetailService _memberDetailService;
        private readonly IMapper _mapper;
        private readonly ILogger<MainForm> _logger;

        public MainForm(
            IMemberService memberService,
            IMemberDetailService memberDetailService,
            IMapper mapper,
            ILogger<MainForm> logger)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;
            _mapper = mapper;
            _logger = logger;

            InitializeComponent();
            InitializeContextMenu();
            LoadMembersAsync();
        }

        private List<MemberViewModel> Members { get { return (List<MemberViewModel>)bdsMembers.List; } }

        private MemberViewModel CurrentMember { get { return (MemberViewModel)bdsMembers.Current; } }

        private void InitializeContextMenu()
        {
            this.dgvMemberDetails.MouseClick += (_, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var menu = new ContextMenuStrip();
                    menu.Items.AddRange(new ToolStripMenuItem[]
                     {
                        new ToolStripMenuItem("新增", null, async (s, a) => {
                            var newMemberDetailViewModel = new MemberDetailViewModel
                            {
                                Id = CurrentMember.Id,
                                Date = DateTime.Now
                            };

                            using var frm = new RecordForm(newMemberDetailViewModel);
                            frm.Text = "新增";
                            var result = frm.ShowDialog();
                            if(result == DialogResult.OK)
                            {
                                var newMemberDetailDto = _mapper.Map<MemberDetailDto>(newMemberDetailViewModel);
                                newMemberDetailDto.MemberId = CurrentMember.Id;
                                await _memberDetailService.CreateAsync(newMemberDetailDto).ContinueWith(async t => {
                                    if(t.IsCompletedSuccessfully){
                                        var detailDto = await _memberDetailService.GetAsync(Guid.Parse(t.Result!));
                                        var detailViewModel = _mapper.Map<MemberDetailViewModel>(detailDto);
                                        CurrentMember.Details.Add(detailViewModel);
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

                    var members = new List<MemberViewModel>();
                    members.AddRange(_mapper.Map<List<MemberViewModel>>(t.Result));
                    this.Invoke(() => bdsMembers.DataSource = members);
                    //_bdsMembers.ResetBindings(false);
                }
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            menuNew.Click += async (s, a) =>
            {
                var newMemberViewModel = new MemberViewModel { Birthday = DateTime.Now };
                using var frm = new NewForm(newMemberViewModel);
                frm.Text = "新增会员信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var newMemberDto = _mapper.Map<MemberDto>(newMemberViewModel);
                    var id = await _memberService.CreateAsync(newMemberDto).ConfigureAwait(false);
                    if (id != null)
                    {
                        var memberDto = await _memberService.GetAsync(Guid.Parse(id));
                        var memeberViewMode = _mapper.Map<MemberViewModel>(memberDto);
                        Members.Add(memeberViewMode);
                        this.Invoke(() =>
                        {
                            bdsMembers.ResetBindings(false);
                            bdsMembers.Position = Members.IndexOf(memeberViewMode);
                        });
                    }
                }
            };

            menuModify.Click += (s, a) =>
            {
                using var frm = new NewForm(CurrentMember);
                frm.Text = "修改会议信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var memberDto = _mapper.Map<MemberDto>(CurrentMember);
                    _memberService.UpdateAsync(memberDto).ContinueWith(t =>
                    {
                        bdsMembers.ResetBindings(false);
                        //bdsMembers.Position = Members.IndexOf(CurrentMember);
                    });
                }
            };

            menuFind.Click += (s, a) =>
            {
                using var frm = new FindForm(Members);
                var result = frm.ShowDialog();
                if (result == DialogResult.OK && frm.Index != null)
                {
                    bdsMembers.Position = Members.IndexOf(frm.Index);
                }
            };
        }

        // Fix multiple current changed event triggers problem when the form first loads.
        private MemberViewModel _previousMemberViewModel;

        private async void bdsMembers_CurrentChanged(object sender, EventArgs e)
        {
            var member = bdsMembers.Current as MemberViewModel;
            if (member != null && _previousMemberViewModel != member)
            {
                _previousMemberViewModel = member;

                var detailDtos = await _memberDetailService.GetByMemberIdAsync(member.Id).ConfigureAwait(false);
                var detailViewModels = _mapper.Map<IEnumerable<MemberDetailViewModel>>(detailDtos);
                member.Details.Clear();
                member.Details.AddRange(detailViewModels);
                this.Invoke(() =>
                {
                    bdsMemberDetails.DataSource = null;
                    bdsMemberDetails.DataSource = member.Details;
                });
            }
        }
    }
}
