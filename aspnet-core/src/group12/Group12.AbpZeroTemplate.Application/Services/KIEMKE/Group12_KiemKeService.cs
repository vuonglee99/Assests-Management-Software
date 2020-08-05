using Abp.Application.Services;
using Abp.Authorization;
using Group12.AbpZeroTemplate.Application;
using Group12.AbpZeroTemplate.Web.Core.Services.KIEMKE.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group12.AbpZeroTemplate.Web.Core.Services.KIEMKE
{
    public interface IGroup12KiemKeService : IApplicationService
    {
        List<KIEMKE_DTO> KiemKe_GetAll(KIEMKE_DTO filterInput);
        List<dynamic> KiemKe_Insert(KIEMKE_DTO input);
        List<dynamic> KiemKe_Update(KIEMKE_DTO input);
        List<dynamic> KiemKe_Delete(string kiemKeId);
        List<dynamic> KiemKe_Close(string id,string status);

    }
    public class Group12KiemKeService : BaseService, IGroup12KiemKeService
    {

      
        //[AbpAuthorize(Group12PermissionsConst.Pages_Administration_KiemKe_Delete)]
        public List<dynamic> KiemKe_Delete(string kiemKeId)
        {
            return procedureHelper.GetData<dynamic>("KiemKe_Delete", new
            {
                KK_ID = kiemKeId
            });
        }
   
        //[AbpAuthorize(Group12PermissionsConst.Pages_Administration_KiemKe_Add)]
        public List<dynamic> KiemKe_Insert(KIEMKE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("KiemKe_Insert", input);
        }

        public List<KIEMKE_DTO> KiemKe_GetAll(KIEMKE_DTO filterInput)
        {
            return procedureHelper.GetData<KIEMKE_DTO>("KiemKe_GetAll", filterInput);
        }

       // [AbpAuthorize(Group12PermissionsConst.Pages_Administration_KiemKe_Close)]
        public List<dynamic> KiemKe_Close(string id, string status)
        {
            return procedureHelper.GetData<dynamic>("KiemKe_Close", new
            {
                KK_ID = id,
                KK_TRANGTHAI = status 
            });
        }

        
        //[AbpAuthorize(Group12PermissionsConst.Pages_Administration_KiemKe_Update)]
        public List<dynamic> KiemKe_Update(KIEMKE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("KiemKe_Update", input);
        }
    }
}
