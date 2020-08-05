using Abp.Application.Services;
using Abp.Authorization;
using Group10.AbpZeroTemplate.Application;
using Abp.Application.Services.Dto;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_NSX.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group10.AbpZeroTemplate.Web.Core.Services.CM_NSX
{
    public interface INSXAppService : IApplicationService
    {

        IDictionary<string, object> CM_NSX_Delete(string nsxID);
        IDictionary<string, object> CM_NSX_Insert(CM_NSX_DTO input);
        IDictionary<string, object> CM_NSX_Update(CM_NSX_DTO input);
        PagedResultDto<CM_NSX_DTO> CM_NSX_Search(CM_NSX_DTO filterInput);
        CM_NSX_DTO CM_NSX_ById(string Id);
        string CM_NSX_GetSizeCol(string colname);
    }
    public class NSXAppService : BaseService, INSXAppService
    {
       

        public CM_NSX_DTO CM_NSX_ById(string Id)
        {
            return procedureHelper.GetData<CM_NSX_DTO>("NhaSanXuat_ById", new { NSX_ID = Id }).ToList().FirstOrDefault();
        }

         [AbpAuthorize(Group10PermissionsConst.Pages_Administration_NSX_Delete)]
        public IDictionary<string, object> CM_NSX_Delete(string nsxID)
        {
            return procedureHelper.GetData<dynamic>("NhaSanXuat_Delete", new
            {
                NSX_ID = nsxID
            }).FirstOrDefault();
        }
        [AbpAuthorize(Group10PermissionsConst.Pages_Administration_NSX_Add)]
        public IDictionary<string,object> CM_NSX_Insert(CM_NSX_DTO input)
        {
            input.RECORD_STATUS = "1";
            return procedureHelper.GetData<dynamic>("NhaSanXuat_Insert", input).FirstOrDefault();
        }

      

      
        [AbpAuthorize(Group10PermissionsConst.Pages_Administration_NSX_Update)]
        public IDictionary<string, object>  CM_NSX_Update(CM_NSX_DTO input)
        {
            return procedureHelper.GetData<dynamic>("NhaSanXuat_Update", input).FirstOrDefault();
        }
        public string CM_NSX_GetSizeCol(string colname)
        {
            return procedureHelper.GetData<string>("NhaSanXuat_GetSizeCol", new { COL_NAME = colname }).FirstOrDefault();
        }
        public PagedResultDto<CM_NSX_DTO> CM_NSX_Search(CM_NSX_DTO filterInput)
        {
            if (filterInput.RECORD_STATUS == null)
            {
                filterInput.RECORD_STATUS = "1";
            }
            var list = procedureHelper.GetData<CM_NSX_DTO>("NhaSanXuat_Search", filterInput).ToList();
            var totalCount = list.Count();


            return new PagedResultDto<CM_NSX_DTO>(
               totalCount,
               list
            );
        }
    }
}
