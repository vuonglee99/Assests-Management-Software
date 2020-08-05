using Abp.Application.Services;
using Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2.AbpZeroTemplate.Web.Core.Services.THIET_BIs
{
    public interface IThietBiAppService : IApplicationService
    {
        THIET_BI_DTO ThietBiById(string thietbiId);
        List<dynamic> ThietBiUpdate(THIET_BI_DTO input);
        List<THIET_BI_DTO> GetAllThietBi();
        //get all thiết bị của một đơn vị
        List<THIET_BI_DTO> GetAllThietBiDonVi(string id_DV);
    }
    public class ThietBiAppService : BaseService, IThietBiAppService
    {
        public List<THIET_BI_DTO> GetAllThietBi()
        {
            //Console.WriteLine("thiet bi get all");
            return procedureHelper.GetData<THIET_BI_DTO>("ThietBi_GetAll", new { });
        }

        public List<THIET_BI_DTO> GetAllThietBiDonVi(string id_DV)
        {
            return procedureHelper.GetData<THIET_BI_DTO>("GetAllThietBiDonVi", new { TB_DV = id_DV });
        }

        public THIET_BI_DTO ThietBiById(string thietbiId)
        {
            return procedureHelper.GetData<THIET_BI_DTO>("ThietBi_ById", new { TB_ID = thietbiId }).FirstOrDefault();
        }

        public List<dynamic> ThietBiUpdate(THIET_BI_DTO input)
        {
            return procedureHelper.GetData<dynamic>("ThietBi_Update", input);
        }
    }
}
