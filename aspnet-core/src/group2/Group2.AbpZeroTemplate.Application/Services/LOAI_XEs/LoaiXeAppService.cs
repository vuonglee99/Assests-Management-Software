using Abp.Application.Services;
using Abp.Authorization;
using Group2.AbpZeroTemplate.Application;
using Group2.AbpZeroTemplate.Web.Core.Services.LOAI_XEs.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Web.Core.Services.LOAI_XEs
{
    public interface ILoaiXeAppService : IApplicationService
    {
        //List<LOAI_XE_DTO> LoaiXeSearch(LOAI_XE_DTO filterInput);
        LOAI_XE_DTO LoaiXeById(string loaixeId);
        dynamic LoaiXeInsert(LOAI_XE_DTO input);
        dynamic LoaiXeUpdate(LOAI_XE_DTO input);
        dynamic LoaiXeDelete(string loaixeId);
        dynamic LoaiXe_Approve(string loaixeCode);
        dynamic LoaiXe_UnApprove(string loaixeCode);
    }
    public class LoaiXeAppService : BaseService, ILoaiXeAppService
    {
        public char[] specialKey = { '"', '<' ,'>' ,'#', '{', '}', (char)92, '^', '~', '[', ']', '`' };
        [AbpAuthorize(Group2PermissionsConst.Pages_Administration_LoaiXe_View)]
        public LOAI_XE_DTO LoaiXeById(string loaixeId)
        {
            return procedureHelper.GetData<LOAI_XE_DTO>("LoaiXe_ById", new
            {
                LX_ID = loaixeId
            }).FirstOrDefault();
        }


        [AbpAuthorize(Group2PermissionsConst.Pages_Administration_LoaiXe_Add)]
        public dynamic LoaiXeInsert(LOAI_XE_DTO input)
        {

            foreach (char x in specialKey)
            {
                if(input.LX_TEN.Contains(x) || input.LX_MO_TA.Contains(x))
                {
                    return null;
                    break;
                }
            }
            return procedureHelper.GetData<dynamic>("LoaiXe_Insert", input).FirstOrDefault();
        }
        /*
        public List<LOAI_XE_DTO> LoaiXeSearch(LOAI_XE_DTO filterInput)
        {
            return procedureHelper.GetData<LOAI_XE_DTO>("LoaiXe_Search", filterInput);
        }*/
        [AbpAuthorize(Group2PermissionsConst.Pages_Administration_LoaiXe_Delete)]
        public dynamic LoaiXeDelete(string loaixeId)
        {
            return procedureHelper.GetData<dynamic>("LoaiXe_Delete", new { LX_ID = loaixeId });
        }


        [AbpAuthorize(Group2PermissionsConst.Pages_Administration_LoaiXe_Update)]
        public dynamic LoaiXeUpdate(LOAI_XE_DTO input)
        {
            foreach (char x in specialKey)
            {
                if (input.LX_TEN.Contains(x) || input.LX_MO_TA.Contains(x))
                {
                    return null;
                    break;
                }
            }
            return procedureHelper.GetData<dynamic>("LoaiXe_Update", input);
        }

        [AbpAuthorize(Group2PermissionsConst.Pages_Administration_LoaiXe_Approve)]
        public dynamic LoaiXe_Approve(string loaixeCode)
        {
            return procedureHelper.GetData<dynamic>("LoaiXe_Approve", new { LX_CODE = loaixeCode });
        }
        [AbpAuthorize(Group2PermissionsConst.Pages_Administration_LoaiXe_UnApprove)]
        public dynamic LoaiXe_UnApprove(string loaixeCode)
        {
            return procedureHelper.GetData<dynamic>("LoaiXe_UnApprove", new { LX_CODE = loaixeCode });
        }
    }
}
