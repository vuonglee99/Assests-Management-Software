using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Group7.AbpZeroTemplate.Application;
using Group7.AbpZeroTemplate.Web.Core.Services.YEU_CAU_SUA_CHUA.DTO;
using GSoft.AbpZeroTemplate.Helpers;

namespace Group7.AbpZeroTemplate.Web.Core.Services.YEU_CAU_SUA_CHUA
{
    public interface IYeuCauSuaChuaAppService : IApplicationService
    {
        List<dynamic> YCSCByTBVTId(string YCSC_TBVT_ID); //Lay lich su sua chua theo ma thiet bi
        dynamic YCSCById(string YCSC_MA_YCSC);  //Lay chi tiet yeu cau sua chua theo ma yeu cau
        dynamic YCSCInsert(YEU_CAU_SUA_CHUA_DTO input);
        dynamic YCSCUpdate(YEU_CAU_SUA_CHUA_DTO input);
        dynamic YCSCDelete();   //Xoa yeu cau sua chua khong duoc duyet
        dynamic YCSCDeleteSingle(string YCSC_ID);
        List<dynamic> YCSCGetAll();
        List<dynamic> YCSCSearchWithOption(string toSearch, string searchType);
    }
    public class YeuCauSuaChuaAppService : BaseService, IYeuCauSuaChuaAppService
    {
        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_YCSC_View)]
        public List<dynamic> YCSCByTBVTId(string YCSC_TBVT_ID)
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_ByTBVTId", new
            {
                YCSC_TBVT_ID = YCSC_TBVT_ID
            });
        }
        
        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_YCSC_View)]
        public dynamic YCSCById(string YCSC_MA_YCSC)
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_ById", new
            {
                YCSC_MA_YCSC = YCSC_MA_YCSC
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_YCSC_Add)]
        public dynamic YCSCInsert(YEU_CAU_SUA_CHUA_DTO input)
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_Insert", input).FirstOrDefault();
        }

        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_YCSC_Delete)]
        public dynamic YCSCDelete()
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_Delete", new { });
        }

        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_YCSC_Update)]
        public dynamic YCSCUpdate(YEU_CAU_SUA_CHUA_DTO input)
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_Update", input);
        }
        [AbpAuthorize(Group7PermissionsConst.Pages_Administration_YCSC_View)]
        public List<dynamic> YCSCGetAll()
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_GetAll", new { });
        }

        public List<dynamic> YCSCSearchWithOption(string toSearch, string searchType)
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_SearchWithOption", new { TO_SEARCH = toSearch, SEARCH_TYPE = searchType });
        }

        public dynamic YCSCDeleteSingle(string YCSC_ID)
        {
            return procedureHelper.GetData<dynamic>("YeuCauSuaChua_DeleteSingle", new { YCSC_ID = YCSC_ID });
        }
    }
}
