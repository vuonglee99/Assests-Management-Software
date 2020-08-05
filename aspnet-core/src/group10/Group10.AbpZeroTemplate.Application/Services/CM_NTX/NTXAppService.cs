using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Group10.AbpZeroTemplate.Application;
using Abp.Application.Services.Dto;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_NTX.Dto;
using GSoft.AbpZeroTemplate.Helpers;
namespace Group10.AbpZeroTemplate.Web.Core.Services.CM_NTX
{
    public interface INTXAppService : IApplicationService
    {
        NguoiThueXe_DTO NguoiThueXe_ByXeId_HienTai(string Id);
        PagedResultDto<NguoiThueXe_DTO> NguoiThueXe_ByXeID(string Id);
        IDictionary<string, object> NguoiThueXe_Delete(string ntxID);
        IDictionary<string, object> NguoiThueXe_Insert(NguoiThueXe_DTO input);
        IDictionary<string, object> NguoiThueXe_Update(NguoiThueXe_DTO input);
        PagedResultDto<NguoiThueXe_DTO> NguoiThueXe_Search(NguoiThueXe_DTO filterInput);
        NguoiThueXe_DTO NguoiThueXe_ById(string Id);
        string NguoiThueXe_GetSizeCol(string colname);
    }
    public class NTXAppService : BaseService, INTXAppService
    {
        public NguoiThueXe_DTO NguoiThueXe_ById(string Id)
        {
            return procedureHelper.GetData<NguoiThueXe_DTO>("NguoiThueXe_ById", new { NTX_ID = Id }).ToList().FirstOrDefault();
        }
        [AbpAuthorize(Group10PermissionsConst.Pages_Administration_NTX_Delete)]
        public IDictionary<string, object> NguoiThueXe_Delete(string ntxID)
        {
            return procedureHelper.GetData<dynamic>("NguoiThueXe_Delete", new
            {
                NTX_ID = ntxID
            }).FirstOrDefault();
        }

        public string NguoiThueXe_GetSizeCol(string colname)
        {
            return procedureHelper.GetData<string>("NguoiThueXe_GetSizeCol", new { COL_NAME = colname }).FirstOrDefault();
        }

        public IDictionary<string, object> NguoiThueXe_Insert(NguoiThueXe_DTO input)
        {
            input.RECORD_STATUS = "1";
            return procedureHelper.GetData<dynamic>("NguoiThueXe_Insert", input).FirstOrDefault();
        }

        public PagedResultDto<NguoiThueXe_DTO> NguoiThueXe_Search(NguoiThueXe_DTO filterInput)
        {
            if (filterInput.RECORD_STATUS == null)
            {
                filterInput.RECORD_STATUS = "1";
            }
            var list = procedureHelper.GetData<NguoiThueXe_DTO>("NguoiThueXe_Search", filterInput).ToList();
            var totalCount = list.Count();


            return new PagedResultDto<NguoiThueXe_DTO>(
               totalCount,
               list
            );
        }
        [AbpAuthorize(Group10PermissionsConst.Pages_Administration_NTX_Update)]
        public IDictionary<string, object> NguoiThueXe_Update(NguoiThueXe_DTO input)
        {
            return procedureHelper.GetData<dynamic>("NguoiThueXe_Update", input).FirstOrDefault();
        }
        public NguoiThueXe_DTO NguoiThueXe_ByXeId_HienTai(string Id)
        {
            return procedureHelper.GetData<NguoiThueXe_DTO>("NguoiThueXe_ByXeId_HienTai", new { XE_ID = Id }).ToList().FirstOrDefault();
        }
        public PagedResultDto<NguoiThueXe_DTO> NguoiThueXe_ByXeID(string Id)
        {
            
            var list = procedureHelper.GetData<NguoiThueXe_DTO>("NguoiThueXe_ByXeID", new { XE_ID = Id }).ToList();
            var totalCount = list.Count();


            return new PagedResultDto<NguoiThueXe_DTO>(
               totalCount,
               list
            );
        }
    }
}
