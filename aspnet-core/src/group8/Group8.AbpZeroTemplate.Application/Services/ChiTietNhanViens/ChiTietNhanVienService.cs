using Abp.Application.Services;
using Abp.Authorization;
using Group8.AbpZeroTemplate.Application;
using Group8.AbpZeroTemplate.Web.Core.Services.ChiTietNhanViens.dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Web.Core.Services.ChiTietNhanViens
{
    public interface IChiTietNhanVienService : IApplicationService
    {
        dynamic CTNV_ById(String manv);
        List<ChiTietNhanVien_DTO> CTNV_Search(ChiTietNhanVien_DTO filter_input);
        List<dynamic> CTNV_Insert(ChiTietNhanVien_DTO input);
        List<dynamic> CTNV_Update(ChiTietNhanVien_DTO input);
        dynamic CTNV_Delete(string manv);
        List<dynamic> CTNV_GetAll();
    }
    public class ChiTietNhanVienService : BaseService, IChiTietNhanVienService
    {
        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_ById)]
        public dynamic CTNV_ById(string manv)
        {
            return procedureHelper.GetData<dynamic>("CTNV_ById",
                new
                {
                    CTNV_MANV = manv
                }).FirstOrDefault();
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Delete)]
        public dynamic CTNV_Delete(string manv)
        {
            return procedureHelper.GetData<dynamic>("CTNV_Delete",
                new
                {
                    CTNV_MANV = manv
                });
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_GetAll)]
        public List<dynamic> CTNV_GetAll()
        {
            return procedureHelper.GetData<dynamic>("CTNV_GetAll", "s");
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Insert)]
        public List<dynamic> CTNV_Insert(ChiTietNhanVien_DTO input)
        {
            return procedureHelper.GetData<dynamic>("CTNV_Insert", input);
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Search)]
        public List<ChiTietNhanVien_DTO> CTNV_Search(ChiTietNhanVien_DTO filter_input)
        {
            return procedureHelper.GetData<ChiTietNhanVien_DTO>("CTNV_Search", filter_input);
        }

        [AbpAuthorize(Group8PermissionsConst.Pages_Administration_ChiTietNhanVien_Update)]
        public List<dynamic> CTNV_Update(ChiTietNhanVien_DTO input)
        {
            return procedureHelper.GetData<dynamic>("CTNV_Update", input);
        }
    }
}
