using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Group7.AbpZeroTemplate.Application;
using Group7.AbpZeroTemplate.Web.Core.Services.NHA_CUNG_UNG.DTO;
using GSoft.AbpZeroTemplate.Helpers;

namespace Group7.AbpZeroTemplate.Web.Core.Services.NHA_CUNG_UNG
{
    public interface INhaCungUngAppService : IApplicationService
    {
        NHA_CUNG_UNG_DTO NhaCungUngById(string NCU_Ma_NCU);
        dynamic NhaCungUngInsert(NHA_CUNG_UNG_DTO input);
        dynamic NhaCungUngUpdate(NHA_CUNG_UNG_DTO input);
        dynamic NhaCungUngDelete(string NCU_Ma_NCU);
        List<NHA_CUNG_UNG_DTO> NhaCungUngGetAll();
    }
    public class NhaCungUngAppService : BaseService, INhaCungUngAppService
    {
        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_NhaCungUng_View)]
        public NHA_CUNG_UNG_DTO NhaCungUngById(string NCU_Ma_NCU)
        {
            return procedureHelper.GetData<NHA_CUNG_UNG_DTO>("NhaCungUng_ById", new
            {
                NCU_MA_NCU = NCU_Ma_NCU
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_NhaCungUng_Add)]
        public dynamic NhaCungUngInsert(NHA_CUNG_UNG_DTO input)
        {

            //foreach (char x in specialKey)
            //{
            //    if (input.LX_TEN.Contains(x) || input.LX_MO_TA.Contains(x))
            //    {
            //        return null;
            //        break;
            //    }
            //}
            return procedureHelper.GetData<dynamic>("NhaCungUng_Insert", input).FirstOrDefault();
        }

        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_NhaCungUng_Delete)]
        public dynamic NhaCungUngDelete(string NCU_Ma_NCU)
        {
            return procedureHelper.GetData<dynamic>("NhaCungUng_Delete", new { NCU_MA_NCU = NCU_Ma_NCU });
        }

        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_NhaCungUng_Update)]
        public dynamic NhaCungUngUpdate(NHA_CUNG_UNG_DTO input)
        {
            //foreach (char x in specialKey)
            //{
            //    if (input.NCU_TEN.Contains(x) || input.LX_MO_TA.Contains(x))
            //    {
            //        return null;
            //    }
            //}
            return procedureHelper.GetData<dynamic>("NhaCungUng_Update", input);
        }
        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_NhaCungUng_View)]
        public List<NHA_CUNG_UNG_DTO> NhaCungUngGetAll()
        {
            return procedureHelper.GetData<NHA_CUNG_UNG_DTO>("NhaCungUng_GetAll", new { });
        }
    }
}
