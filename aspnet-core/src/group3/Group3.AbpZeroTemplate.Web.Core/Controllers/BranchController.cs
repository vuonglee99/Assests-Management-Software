using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Group3.AbpZeroTemplate.Web.Core.Services.Branches;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BranchController : AbpController
    {
        private readonly IGroup03BranchAppService _group03BranchAppService = null;
        public BranchController(IGroup03BranchAppService group03BranchAppService)
        {
            _group03BranchAppService = group03BranchAppService;
        }

        [HttpGet]
        public PagedResultDto<Group03BranchDTO> BranchSearch(Group03BranchFilter filter)
        {
            return _group03BranchAppService.Group03SearchBranch(filter);
        }

        [HttpDelete]    
        public IDictionary<string, object> BranchDelete(string BRANCH_ID)
        {
            return _group03BranchAppService.Group03DeleteBranch(BRANCH_ID);
        }
    }
}
