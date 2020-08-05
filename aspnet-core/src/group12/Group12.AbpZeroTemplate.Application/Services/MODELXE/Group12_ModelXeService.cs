using Abp.Application.Services;
using Abp.Authorization;
using Group12.AbpZeroTemplate.Application;
using Group12.AbpZeroTemplate.Web.Core.Services.MODELXE.Dto;
using GSoft.AbpZeroTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group12.AbpZeroTemplate.Web.Core.Services.MODELXE
{
    public interface IGroup12ModelXeService : IApplicationService
    {
        List<MODELXE_DTO> ModelXe_GetAll(MODELXE_DTO filterInput);
        MODELXE_DTO ModelXe_ById(string modelId);
        List<dynamic> ModelXe_Insert(MODELXE_DTO input);
        List<dynamic> ModelXe_Update(MODELXE_DTO input);
        List<dynamic> ModelXe_Delete(string modelId);
        List<dynamic> ModelXe_MakeCode(string modelCode);
    }
    public class Group12ModelXeService : BaseService, IGroup12ModelXeService
    {

        public MODELXE_DTO ModelXe_ById(string modelId)
        {
            return procedureHelper.GetData<MODELXE_DTO>("ModelXe_ById", new
            {
                MODEL_ID = modelId
            }).FirstOrDefault();
        }

        [AbpAuthorize(Group12PermissionsConst.Pages_Administration_Model_Delete)]
        public List<dynamic> ModelXe_Delete(string modelId)
        {
            return procedureHelper.GetData<dynamic>("ModelXe_Delete", new
            {
                MODEL_ID = modelId
            });
        }

        [AbpAuthorize(Group12PermissionsConst.Pages_Administration_Model_Add)]
        public List<dynamic> ModelXe_Insert(MODELXE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("ModelXe_Insert", input);
        }

        public List<MODELXE_DTO> ModelXe_GetAll(MODELXE_DTO filterInput)
        {
            return procedureHelper.GetData<MODELXE_DTO>("ModelXe_GetAll", filterInput);
        }

        public List<dynamic> ModelXe_MakeCode(string modelCode)
        {
            return procedureHelper.GetData<dynamic>("ModelXe_MakeCode",new
            {
                RESULT=modelCode
            });
        }

        [AbpAuthorize(Group12PermissionsConst.Pages_Administration_Model_Update)]
        public List<dynamic> ModelXe_Update(MODELXE_DTO input)
        {
            return procedureHelper.GetData<dynamic>("ModelXe_Update", input);
        }
    }
}
