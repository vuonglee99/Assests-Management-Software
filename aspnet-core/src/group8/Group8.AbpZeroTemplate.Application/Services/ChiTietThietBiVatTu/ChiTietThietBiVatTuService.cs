using Abp.Application.Services;
using Abp.Authorization;
using Group8.AbpZeroTemplate.Application;
using Group8.AbpZeroTemplate.Web.Core.Services.ChiTietThietBiVatTu.DTO;
using GSoft.AbpZeroTemplate.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Group8.AbpZeroTemplate.Web.Core.Services.ChiTietThietBiVatTu
{
    public interface IChiTietThietBiVatTuService : IApplicationService
    {
        ThietBiVatTu_Dto CTTBVT_ById(string tbvt_ma);
        List<dynamic> CTTBVT_Insert(ThietBiVatTu_Dto input);
        List<dynamic> CTTBVT_Update(ThietBiVatTu_Dto input);
        List<dynamic> CTTBVT_Delete(string tbvt_ma);
    }
    public class ChiTietThietBiVatTuService : BaseService, IChiTietThietBiVatTuService
    {
        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_Delete)]
        public List<dynamic> CTTBVT_Delete(string tbvt_ma)
        {
            return procedureHelper.GetData<dynamic>("ThietBiVatTu_Delete",
                new
                {
                    TBVT_MA_TBVT = tbvt_ma
                });
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_Insert)]
        public List<dynamic> CTTBVT_Insert(ThietBiVatTu_Dto input)
        {
            return procedureHelper.GetData<dynamic>("ThietBiVatTu_Insert", input);
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_Update)]
        public List<dynamic> CTTBVT_Update(ThietBiVatTu_Dto input)
        {
            return procedureHelper.GetData<dynamic>("ThietBiVatTu_Update", input);
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ThietBiVatTu_ById)]
        public ThietBiVatTu_Dto CTTBVT_ById(string tbvt_ma)
        {

            return procedureHelper.GetData<ThietBiVatTu_Dto>("ThietBiVatTu_ById",
                new
                {
                    TBVT_MA_TBVT = tbvt_ma
                }).FirstOrDefault();
        }
    }
}
