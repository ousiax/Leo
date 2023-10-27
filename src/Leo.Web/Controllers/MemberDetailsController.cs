using Alyio.AspNetCore.ApiMessages;
using AutoMapper;
using Leo.Data.Domain.Dtos;
using Leo.Data.Domain.Models;
using Leo.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace Leo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberDetailsController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMemberDetailService _memberDetailService;
        private readonly IMapper _mapper;

        public MemberDetailsController(IMemberDetailService memberDetailService, IMapper mapper, ILogger<MembersController> logger)
        {
            _memberDetailService = memberDetailService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<MemberDetailDto?> GetByIdAsync(Guid id)
        {
            var memberDetail = await _memberDetailService.GetByIdAsync(id) ?? throw new NotFoundMessage();
            return _mapper.Map<MemberDetailDto?>(memberDetail);
        }

        [HttpPost]
        public async Task<CreatedMessage> CreateAsync([FromBody] MemberDetailDto detailDto)
        {
            var detail = _mapper.Map<MemberDetail>(detailDto);
            var id = await _memberDetailService.CreateAsync(detail);
            return this.CreatedMessageAtAction(nameof(GetByIdAsync), new { id }, id.ToString());
        }
    }
}