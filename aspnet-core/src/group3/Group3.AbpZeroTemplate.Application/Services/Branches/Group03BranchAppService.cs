using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Services.Branches
{
    public interface IGroup03BranchAppService : IApplicationService
    {
        dynamic Group03DeleteBranch(string branchId);
        PagedResultDto<Group03BranchDTO> Group03SearchBranch(Group03BranchFilter filter);
    }

    public class Group03BranchAppService : Group3AppServiceBase, IGroup03BranchAppService
    {
        [AbpAuthorize(Group3PermissionsConst.Pages_Administration_Branches_Delete)]
        public dynamic Group03DeleteBranch(string branchId)
        {
            return procedureHelper.GetData<dynamic>(
                "Branch_Delete", new { BRANCH_ID = branchId }
            ).FirstOrDefault();
        }


        [AbpAuthorize(Group3PermissionsConst.Pages_Administration_Branches)]
        public PagedResultDto<Group03BranchDTO> Group03SearchBranch(Group03BranchFilter filter)
        {
            var branches = procedureHelper.GetData<Group03BranchDTO>(
                "Branch_Search",  new
                {
                    Search = filter.Search,
                    BRANCH_CODE = filter.BranchCode,
                    BRANCH_NAME = filter.BranchName,
                    BRANCH_STATUS = filter.BranchStatus,
                    Sorting = filter.Sorting,
                    RECORD_STATUS = 1
                });
            int totalCount = branches.Count;
            int fromIndex = filter.SkipCount - 1;
            int toIndex = fromIndex + filter.MaxResultCount;

            List<Group03BranchDTO> branchesOffset = new List<Group03BranchDTO>();
            for (int i = fromIndex; i < toIndex && i < branches.Count; i++)
                branchesOffset.Add(branches[i]);

            var branchesListDto = ObjectMapper.Map<List<Group03BranchDTO>>(branchesOffset);
            return new PagedResultDto<Group03BranchDTO>(
                totalCount, branchesListDto);
        }
    }
}
