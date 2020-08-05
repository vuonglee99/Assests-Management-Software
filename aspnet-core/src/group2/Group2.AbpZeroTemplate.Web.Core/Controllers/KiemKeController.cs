using Abp.AspNetCore.Mvc.Controllers;
using Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs;
using Group2.AbpZeroTemplate.Web.Core.Services.CHI_TIET_BAN_KIEM_KEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.KIEM_KEs;
using Group2.AbpZeroTemplate.Web.Core.Services.KIEM_KEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class KiemKeController : AbpController
    {
        private readonly IKiemKeAppService kiemkeAppService;
        private readonly IChiTietBanKiemKeAppService chiTietBanKiemKe;
        private readonly IThietBiAppService thietbiAppService;

        public KiemKeController(IKiemKeAppService kiemkeAppService)
        {
            this.kiemkeAppService = kiemkeAppService;
        }
        [HttpPost]
        public dynamic BKK_CHECK(string dv_QL, string bkk_ID, DateTime ngay_TAO)
        {
            return kiemkeAppService.BKK_CHECK(dv_QL, bkk_ID, ngay_TAO);
        }
        [HttpPost]
        public KIEM_KE_DTO KiemKeById(string kiemkeId)
        {
            return kiemkeAppService.KiemKeById(kiemkeId);
        }
        [HttpPost]
        public dynamic KiemKeUpdate([FromBody]KIEM_KE_DTO input)
        {
            return kiemkeAppService.KiemKeUpdate(input);
        }
        [HttpPost]
        public dynamic KiemKeInsert([FromBody]KIEM_KE_DTO input)
        {
            
            List<THIET_BI_DTO> lsttb = thietbiAppService.GetAllThietBiDonVi(input.KK_MADONVI);
            for(int i=0;i<lsttb.Count;i++)
            {
                CHI_TIET_BAN_KIEM_KE_DTO ctkk = new CHI_TIET_BAN_KIEM_KE_DTO
                {
                    CTBKK_MA_TB = lsttb[i].TB_ID,
                    CTBKK_BKK_ID =""
                };
                chiTietBanKiemKe.ChiTietKiemKeInsert(ctkk);
            }
            
            return kiemkeAppService.KiemKeInsert(input);
        }
        /*[HttpPost]
        public dynamic KiemKeOfflineProcess(string DONVI_ID, string BKK_CODE, DateTime NGAY_TAO, string listtb)
        {
            return kiemkeAppService.KiemKeOfflineProcess(DONVI_ID, BKK_CODE, NGAY_TAO, listtb);
        }*/
        [HttpPost]
        public dynamic GetKiemKeDateAndCode(string branch_ID)
        {
            return kiemkeAppService.GetKiemKeDateAndCode(branch_ID);
        }
        [HttpPost]
        public List<string> GetBranchIdFromUserId(int id)
        {
            return kiemkeAppService.GetBranchIdFromUserId(id);
        }
        [HttpPost]
        public dynamic KiemKeOfflineProcess([FromBody]CUSTOM_DTO input)
        {
            return kiemkeAppService.KiemKeOfflineProcess(input);
        }
    }
}
