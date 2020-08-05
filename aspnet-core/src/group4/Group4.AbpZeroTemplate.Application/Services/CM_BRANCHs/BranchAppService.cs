using Abp.Application.Services;
using Abp.Authorization;
using Group4.AbpZeroTemplate.Application;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4.AbpZeroTemplate.Web.Core.Services.CM_BRANCHs.Dto
{
    public interface IBranchAppService : IApplicationService
    {

        CM_BRANCH_DTO CM_Branch_ById(string BranchId);
        List<dynamic> CM_Branch_Insert(CM_BRANCH_DTO input);
        List<dynamic> CM_Branch_Update(CM_BRANCH_DTO input);
        List<dynamic> CM_Branch_Delete(string BranchId);
        List<CM_BRANCH_DTO> CM_Branch_GetFatherBranchByBranchType(string branchType);
        //List<CM_BRANCH_DTO> CM_Branch_Search(CM_BRANCH_DTO filterInput);
        //List<dynamic> CM_Branch_Approve(string BranchId, string checkerId);
    }

    [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Branch)]
    public class BranchAppService : BaseService, IBranchAppService
    {
        //public List<dynamic> CM_Branch_Approve(string BranchId, string checkerId)
        //{
        //    throw new NotImplementedException();
        //}

        public CM_BRANCH_DTO CM_Branch_ById(string BranchId)
        {
            return procedureHelper.GetData<CM_BRANCH_DTO>("Branch_ById", new
            {
                BRANCH_ID = BranchId
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Branch_Delete)]

        public List<dynamic> CM_Branch_Delete(string BranchId)
        {
            return procedureHelper.GetData<dynamic>("Branch_Delete", new
            {
                BRANCH_ID = BranchId
            });
        }

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Branch_Add)]

        public List<dynamic> CM_Branch_Insert(CM_BRANCH_DTO input)
        {
            return procedureHelper.GetData<dynamic>("Branch_Insert", input);
        }

        //public List<CM_BRANCH_DTO> CM_Branch_Search(CM_BRANCH_DTO filterInput)
        //{
        //    return procedureHelper.GetData<CM_BRANCH_DTO>("BRANCH_Search", filterInput);
        //}

        [AbpAuthorize(Group4PermissionsConst.Pages_Administration_Branch_Update)]

        public List<dynamic> CM_Branch_Update(CM_BRANCH_DTO input)
        {
            return procedureHelper.GetData<dynamic>("Branch_Update", input);
        }

        public List<CM_BRANCH_DTO> CM_Branch_GetFatherBranchByBranchType(string branchType)
        {
            return procedureHelper.GetData<CM_BRANCH_DTO>("Branch_GetFatherBranchByBranchType", new
            {
                BRANCH_TYPE = branchType
            }).ToList();
        }
    }

}
