using Abp.Application.Services;
//using Group7.AbpZeroTemplate.Web.Core.Services.LOAI_XEs.Dto;
using Group2.AbpZeroTemplate.Web.Core.Services.LOAI_XEs.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group7.AbpZeroTemplate.Web.Core.Services.LOAI_XEs
{
    public interface ILoaiXeAppService : IApplicationService
    {
        List<LOAI_XE_DTO> LOAI_XE_Search(LOAI_XE_DTO filterInput);
        List<LOAI_XE_DTO> SearchWithOption(string toSearch, string searchType);

        /*
        LOAI_XE_DTO LOAI_XE_ByID(string loaiXeID);
        dynamic LOAI_XE_Insert(LOAI_XE_DTO input);
        dynamic LOAI_XE_Update(LOAI_XE_DTO input);
        dynamic LOAI_XE_Delete(string loaiXeID);
        */
        List<LOAI_XE_DTO> GetAll();
    }

    public class LoaiXeAppService : BaseService, ILoaiXeAppService
    {
        public char[] specialKey = { '"', '<', '>', '#', '{', '}', (char)92, '^', '~', '[', ']', '`' };

        public List<LOAI_XE_DTO> LOAI_XE_Search(LOAI_XE_DTO filterInput)
        {
            return procedureHelper.GetData<LOAI_XE_DTO>("LOAI_XE_SEARCH", filterInput);
        }

        public List<LOAI_XE_DTO> SearchWithOption(string toSearch, string searchType)
        {
            return procedureHelper.GetData<LOAI_XE_DTO>("LoaiXe_SearchWithOption", new { TO_SEARCH = toSearch, SEARCH_TYPE = searchType });
        }

        public List<LOAI_XE_DTO> GetAll()
        {
            return procedureHelper.GetData<LOAI_XE_DTO>("LoaiXe_GetAll", new { });
        }
    }

}
