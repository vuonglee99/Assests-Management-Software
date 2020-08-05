using Abp.Application.Services;
using Abp.Authorization;
using Group0.AbpZeroTemplate.Application;
using Group0.AbpZeroTemplate.Web.Core.Services.CM_XEs.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group0.AbpZeroTemplate.Web.Core.Services.CM_XEs
{
    public interface IXeAppService : IApplicationService
    {
        List<CM_XE_DTO> CM_XE_Search(CM_XE_DTO filterInput);
        CM_XE_DTO CM_XE_ById(string xeId);
        List<dynamic> CM_XE_Insert(CM_XE_DTO input);
        List<dynamic> CM_XE_Update(CM_XE_DTO input);
        List<dynamic> CM_XE_Delete(string xeId);
        List<dynamic> CM_XE_Approve(string xeId, string checkerId);
    }
    public class XeAppService : BaseService, IXeAppService
    {

        [AbpAuthorize(Group0PermissionsConst.Pages_Administration_Xe_Approve)]
        public List<dynamic> CM_XE_Approve(string xeId, string checkerId)
        {
            return procedureHelper.GetData<dynamic>("CM_XE_Approve", new
            {
                XE_ID = xeId,
                CHECKER_ID = checkerId
            });
        }

        public CM_XE_DTO CM_XE_ById(string xeId)
        {
            return procedureHelper.GetData<dynamic>("CM_XE_ById", new
            {
                XE_ID = xeId
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group0PermissionsConst.Pages_Administration_Xe_Delete)]
        public List<dynamic> CM_XE_Delete(string xeId)
        {
            return procedureHelper.GetData<dynamic>("CM_XE_Delete", new
            {
                XE_ID = xeId
            });
        }

        [AbpAuthorize(Group0PermissionsConst.Pages_Administration_Xe_Add)]
        public List<dynamic> CM_XE_Insert(CM_XE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("CM_XE_Insert", input);
        }

        public List<CM_XE_DTO> CM_XE_Search(CM_XE_DTO filterInput)
        {
            return procedureHelper.GetData<CM_XE_DTO>("CM_XE_Search", filterInput);
        }

        [AbpAuthorize(Group0PermissionsConst.Pages_Administration_Xe_Update)]
        public List<dynamic> CM_XE_Update(CM_XE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("CM_XE_Update", input);
        }
    }
}
