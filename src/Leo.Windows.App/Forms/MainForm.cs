using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.UI;
using Leo.UI.Services;
using Leo.Windows.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.Versioning;

namespace Leo.Windows.Forms
{
    public partial class MainForm : Form
    {
        private readonly IMemberService _memberService;
        private readonly IMemberDetailService _memberDetailService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MainForm> _logger;

        [SupportedOSPlatform("windows10.0.18362")]
        public MainForm(
            IMemberService memberService,
            IMemberDetailService memberDetailService,
            IAuthenticationService authenticationService,
            IMapper mapper,
            IServiceProvider serviceProvider,
            ILogger<MainForm> logger)
        {
            _memberService = memberService;
            _memberDetailService = memberDetailService;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _logger = logger;

            InitializeComponent();
            InitializeContextMenu();
            this.Load += async (s, e) => await LoadMembersAsync();
            this.Load += async (s, a) =>
            {
                var result = await _authenticationService.ExecuteAsync();
                this.Text = $"{this.Text} ({result.Account.Username})";
            };
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
                                var id = await _memberDetailService.CreateAsync(newMemberDetailDto);
                                var detailDto = await _memberDetailService.GetAsync(Guid.Parse(id !));
                                var detailViewModel = _mapper.Map<MemberDetailViewModel>(detailDto);
                                CurrentMember.Details.Add(detailViewModel);
                                bdsMemberDetails.ResetBindings(false);
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

        private async Task LoadMembersAsync()
        {
            var memeberDtos = await _memberService.GetAsync();
            var members = new List<MemberViewModel>();
            members.AddRange(_mapper.Map<List<MemberViewModel>>(memeberDtos));
            bdsMembers.DataSource = members;
            //_bdsMembers.ResetBindings(false);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.combGender.Items.Add(new KeyValuePair<string, string>("Male", "男"));
            //this.combGender.Items.Add(new KeyValuePair<string, string>("Female", "女"));
            menuNew.Click += async (s, a) =>
            {
                var newMemberViewModel = new MemberViewModel { Birthday = DateTime.Now };
                using var frm = new NewForm(newMemberViewModel);
                frm.Text = "新增会员信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var newMemberDto = _mapper.Map<MemberDto>(newMemberViewModel);
                    var id = await _memberService.CreateAsync(newMemberDto);
                    if (id != null)
                    {
                        var memberDto = await _memberService.GetAsync(Guid.Parse(id));
                        var memeberViewMode = _mapper.Map<MemberViewModel>(memberDto);
                        Members.Add(memeberViewMode);
                        bdsMembers.ResetBindings(false);
                        bdsMembers.Position = Members.IndexOf(memeberViewMode);
                    }
                }
            };

            menuModify.Click += async (s, a) =>
            {
                using var frm = new NewForm(CurrentMember);
                frm.Text = "修改会员信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var memberDto = _mapper.Map<MemberDto>(CurrentMember);
                    await _memberService.UpdateAsync(memberDto);
                    bdsMembers.ResetBindings(false);
                    //bdsMembers.Position = Members.IndexOf(CurrentMember);
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

            menuEcho.Click += (s, a) =>
            {
                using var scope = _serviceProvider.CreateScope();
                scope.ServiceProvider.GetRequiredService<EchoForm>().ShowDialog();
            };
        }

        // Fix multiple current changed event triggers problem when the form first loads.
        private MemberViewModel? _previousMemberViewModel;

        private async void bdsMembers_CurrentChanged(object sender, EventArgs e)
        {
            if (bdsMembers.Current is MemberViewModel member && _previousMemberViewModel != member)
            {
                _previousMemberViewModel = member;

                var detailDtos = await _memberDetailService.GetByMemberIdAsync(member.Id);
                var detailViewModels = _mapper.Map<IEnumerable<MemberDetailViewModel>>(detailDtos);
                member.Details.Clear();
                member.Details.AddRange(detailViewModels);
                bdsMemberDetails.DataSource = null;
                bdsMemberDetails.DataSource = member.Details;
            }
        }
    }
}
