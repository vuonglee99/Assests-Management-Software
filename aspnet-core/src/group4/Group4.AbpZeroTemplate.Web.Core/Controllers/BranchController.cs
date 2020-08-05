using Abp.AspNetCore.Mvc.Controllers;
using Group4.AbpZeroTemplate.Web.Core.Services.CM_BRANCHs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Group4.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BranchController : AbpController
    {
        private readonly IBranchAppService branchAppService;

        public BranchController(IBranchAppService branchAppService)
        {
            this.branchAppService = branchAppService;
        }

        [HttpPost]
        public CM_BRANCH_DTO BranchById(string branchId)
        {
            return branchAppService.CM_Branch_ById(branchId);
        }

        //[HttpPost]
        //public List<CM_BRANCH_DTO> CM_BRANCH_Search([FromBody]CM_BRANCH_DTO filterInput)
        //{
        //    return branchAppService.CM_Branch_Search(filterInput);
        //}

        [HttpPost]
        public List<dynamic> BranchInsert([FromBody]CM_BRANCH_DTO input)
        {
            return branchAppService.CM_Branch_Insert(input);
        }

        [HttpPost]
        public List<dynamic> BranchUpdate([FromBody]CM_BRANCH_DTO input)
        {
            return branchAppService.CM_Branch_Update(input);
        }

        //[HttpPost]
        //public List<dynamic> BranchDelete(string branchId)
        //{
        //    return branchAppService.CM_Branch_Delete(branchId);
        //}

        [HttpPost]
        public List<CM_BRANCH_DTO> CM_Branch_GetFatherBranchByBranchType(string input)
        {
            return branchAppService.CM_Branch_GetFatherBranchByBranchType(input);
        }
    }
}
