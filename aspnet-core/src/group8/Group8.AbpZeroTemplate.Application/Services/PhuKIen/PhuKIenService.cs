using Abp.Application.Services;
using Abp.Authorization;
using Group8.AbpZeroTemplate.Application;
using Group8.AbpZeroTemplate.Web.Core.Services.PhuKien.dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Web.Core.Services.PhuKien
{
    public interface IPhuKienService : IApplicationService
    {
        PhuKien_DTO PhuKien_ById(String manv);
        List<PhuKien_DTO> PhuKien_Search(PhuKien_DTO filter_input);
        List<dynamic> PhuKien_Insert(PhuKien_DTO input);
        List<dynamic> PhuKien_Update(PhuKien_DTO input);
        dynamic PhuKien_Delete(string manv);
        List<PhuKien_DTO> PhuKien_GetAll();
        List<PhuKien_DTO> PhuKien_GetBy_TBVT_ID(int tbvt_id);
    }
    public class PhuKienService : BaseService, IPhuKienService
    {
        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_PhuKien_ById)]
        public PhuKien_DTO PhuKien_ById(string mapk)
        {
            return procedureHelper.GetData<PhuKien_DTO>("PhuKien_ById",
                new
                {
                    PHU_KIEN_MA_PK = mapk
                }).FirstOrDefault();
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_PhuKien_Delete)]
        public dynamic PhuKien_Delete(string manv)
        {
            return procedureHelper.GetData<dynamic>("PhuKien_Delete",
                new
                {
                    PHU_KIEN_MA_PK = manv
                });
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_PhuKien_GetAll)]
        public List<PhuKien_DTO> PhuKien_GetAll()
        {
            return procedureHelper.GetData<PhuKien_DTO>("PhuKien_GetAll", null);
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_PhuKien_ById)]
        public List<PhuKien_DTO> PhuKien_GetBy_TBVT_ID(int tbvu_id)
        {
            return procedureHelper.GetData<PhuKien_DTO>("PhuKien_GetByTBVT_ID", new { PHU_KIEN_TBVT_ID = tbvu_id });
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_PhuKien_Insert)]
        public List<dynamic> PhuKien_Insert(PhuKien_DTO input)
        {
            return procedureHelper.GetData<dynamic>("PhuKien_Insert", input);
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_PhuKien_Search)]
        public List<PhuKien_DTO> PhuKien_Search(PhuKien_DTO filter_input)
        {
            return procedureHelper.GetData<PhuKien_DTO>("PhuKien_Search", filter_input);
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_PhuKien_Update)]
        public List<dynamic> PhuKien_Update(PhuKien_DTO input)
        {
            return procedureHelper.GetData<dynamic>("PhuKien_Update", input);
        }
    }
}
