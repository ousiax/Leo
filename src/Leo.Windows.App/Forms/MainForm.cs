// MIT License

using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.UI;
using Leo.UI.Services;
using Leo.Windows.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Runtime.Versioning;

namespace Leo.Windows.Forms
{
    public partial class MainForm : Form
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerDetailService _customerDetailService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MainForm> _logger;

        [SupportedOSPlatform("windows10.0.18362")]
        public MainForm(
            ICustomerService customerService,
            ICustomerDetailService customerDetailService,
            IAuthenticationService authenticationService,
            IMapper mapper,
            IServiceProvider serviceProvider,
            ILogger<MainForm> logger)
        {
            _customerService = customerService;
            _customerDetailService = customerDetailService;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _logger = logger;

            InitializeComponent();
            InitializeContextMenu();
            this.Load += async (s, e) => await LoadCustomersAsync();
            this.Load += async (s, a) =>
            {
                var result = await _authenticationService.ExecuteAsync();
                this.Text = $"{this.Text} ({result.Account.Username})";
            };
        }

        private List<CustomerViewModel> Customers { get { return (List<CustomerViewModel>)bdsCustomers.List; } }

        private CustomerViewModel CurrentCustomer { get { return (CustomerViewModel)bdsCustomers.Current; } }

        private void InitializeContextMenu()
        {
            this.dgvCustomerDetails.MouseClick += (_, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var menu = new ContextMenuStrip();
                    menu.Items.AddRange(new ToolStripMenuItem[]
                     {
                        new ToolStripMenuItem("新增", null, async (s, a) => {
                            var newCustomerDetailViewModel = new CustomerDetailViewModel
                            {
                                Id = CurrentCustomer.Id,
                                Date = DateTime.Now
                            };

                            using var frm = new RecordForm(newCustomerDetailViewModel);
                            frm.Text = "新增";
                            var result = frm.ShowDialog();
                            if(result == DialogResult.OK)
                            {
                                var newCustomerDetailDto = _mapper.Map<CustomerDetailDto>(newCustomerDetailViewModel);
                                newCustomerDetailDto.CustomerId = CurrentCustomer.Id;
                                var id = await _customerDetailService.CreateAsync(newCustomerDetailDto);
                                var detailDto = await _customerDetailService.GetAsync(id!);
                                var detailViewModel = _mapper.Map<CustomerDetailViewModel>(detailDto);
                                CurrentCustomer.Details.Add(detailViewModel);
                                bdsCustomerDetails.ResetBindings(false);
                            }
                        }),
                     });

                    int currentMouseOverRow = dgvCustomerDetails.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow >= 0)
                    {
                        menu.Items.Add(new ToolStripMenuItem(string.Format(CultureInfo.InvariantCulture, "Do something to row {0}", currentMouseOverRow)));
                    }

                    menu.Show(this.dgvCustomerDetails, new Point(e.X, e.Y), ToolStripDropDownDirection.Left);
                }
            };
        }

        private async Task LoadCustomersAsync()
        {
            var memeberDtos = await _customerService.GetAsync();
            var customers = new List<CustomerViewModel>();
            customers.AddRange(_mapper.Map<List<CustomerViewModel>>(memeberDtos));
            bdsCustomers.DataSource = customers;
            //_bdsCustomers.ResetBindings(false);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.combGender.Items.Add(new KeyValuePair<string, string>("Male", "男"));
            //this.combGender.Items.Add(new KeyValuePair<string, string>("Female", "女"));
            menuNew.Click += async (s, a) =>
            {
                var newCustomerViewModel = new CustomerViewModel { Birthday = DateTime.Now };
                using var frm = new NewForm(newCustomerViewModel);
                frm.Text = "新增会员信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var newCustomerDto = _mapper.Map<CustomerDto>(newCustomerViewModel);
                    var id = await _customerService.CreateAsync(newCustomerDto);
                    if (id != null)
                    {
                        var customerDto = await _customerService.GetAsync(id);
                        var memeberViewMode = _mapper.Map<CustomerViewModel>(customerDto);
                        Customers.Add(memeberViewMode);
                        bdsCustomers.ResetBindings(false);
                        bdsCustomers.Position = Customers.IndexOf(memeberViewMode);
                    }
                }
            };

            menuModify.Click += async (s, a) =>
            {
                if (CurrentCustomer == null)
                {
                    return;
                }

                using var frm = new NewForm(CurrentCustomer);
                frm.Text = "修改会员信息";
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var customerDto = _mapper.Map<CustomerDto>(CurrentCustomer);
                    await _customerService.UpdateAsync(customerDto);
                    bdsCustomers.ResetBindings(false);
                    //bdsCustomers.Position = Customers.IndexOf(CurrentCustomer);
                }
            };

            menuFind.Click += (s, a) =>
            {
                if (Customers.Count == 0) { return; }

                using var frm = new FindForm(Customers);
                var result = frm.ShowDialog();
                if (result == DialogResult.OK && frm.Index != null)
                {
                    bdsCustomers.Position = Customers.IndexOf(frm.Index);
                }
            };

            menuEcho.Click += (s, a) =>
            {
                using var scope = _serviceProvider.CreateScope();
                scope.ServiceProvider.GetRequiredService<EchoForm>().ShowDialog();
            };
        }

        // Fix multiple current changed event triggers problem when the form first loads.
        private CustomerViewModel? _previousCustomerViewModel;

        private async void bdsCustomers_CurrentChanged(object sender, EventArgs e)
        {
            if (bdsCustomers.Current is CustomerViewModel customer && _previousCustomerViewModel != customer)
            {
                _previousCustomerViewModel = customer;

                var detailDtos = await _customerDetailService.GetByCustomerIdAsync(customer.Id!);
                var detailViewModels = _mapper.Map<IEnumerable<CustomerDetailViewModel>>(detailDtos);
                customer.Details.Clear();
                customer.Details.AddRange(detailViewModels);
                bdsCustomerDetails.DataSource = null;
                bdsCustomerDetails.DataSource = customer.Details;
            }
        }
    }
}
