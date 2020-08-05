using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Group10.AbpZeroTemplate.Application;
using Abp.Application.Services.Dto;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_PTX.Dto;
using GSoft.AbpZeroTemplate.Helpers;
namespace Group10.AbpZeroTemplate.Web.Core.Services.CM_PTX
{
    public interface IPTXAppService : IApplicationService
    {
        IDictionary<string, object> PhieuThueXe_Delete(string PTXID);

        PagedResultDto<PhieuThueXe_DTO> PhieuThueXe_Search(string ntxcode, string ntxname, string xecode, string xename, string license);


    }
    public class PTXAppService : BaseService, IPTXAppService
    {
        [AbpAuthorize(Group10PermissionsConst.Pages_Administration_PTX_Delete)]
        public IDictionary<string, object> PhieuThueXe_Delete(string PTXID)
        {
            return procedureHelper.GetData<dynamic>("PhieuThueXe_Delete", new
            {
                PTX_ID = PTXID
            }).FirstOrDefault();
        }

        public PagedResultDto<PhieuThueXe_DTO> PhieuThueXe_Search(string ntxcode, string ntxname, string xecode, string xename, string license)
        {

            var list = procedureHelper.GetData<PhieuThueXe_DTO>("PhieuThueXe_Search", new {
                NTX_CODE = ntxcode,
                NTX_NAME = ntxname,
                XE_CODE = xecode,
                XE_NAME = xename,
                XE_LICENSE_PLATE = license
            }).ToList();
            var totalCount = list.Count();


            return new PagedResultDto<PhieuThueXe_DTO>(
               totalCount,
               list
            );
        }
       
    }
}
