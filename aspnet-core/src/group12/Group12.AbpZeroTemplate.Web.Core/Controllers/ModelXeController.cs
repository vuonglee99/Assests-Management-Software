using Abp.AspNetCore.Mvc.Controllers;
using Abp.Dependency;
using Group12.AbpZeroTemplate.Web.Core.Services.MODELXE;
using Group12.AbpZeroTemplate.Web.Core.Services.MODELXE.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group12.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ModelXeController : AbpController
    {
        private readonly IGroup12ModelXeService modelXeService;

        public ModelXeController(IGroup12ModelXeService modelXeService)
        {
            this.modelXeService = modelXeService;
        }

        [HttpPost]
        public List<dynamic> ModelXe_Insert([FromBody] MODELXE_DTO input)
        {
            return modelXeService.ModelXe_Insert(input);
        }

        [HttpPost]
        public List<MODELXE_DTO> ModelXe_GetAll([FromBody]MODELXE_DTO filterInput)
        {
            return modelXeService.ModelXe_GetAll(filterInput);
        }

        [HttpPost]
        public MODELXE_DTO ModelXe_ById(string modelId)
        {
            return modelXeService.ModelXe_ById(modelId);
        }

        [HttpPost]
        public List<dynamic> ModelXe_Update([FromBody]MODELXE_DTO input)
        {
            return modelXeService.ModelXe_Update(input);
        }

        [HttpPost]
        public List<dynamic> ModelXe_Delete(string modelId)
        {
            return modelXeService.ModelXe_Delete(modelId);
        }

        [HttpPost]
        public List<dynamic> ModelXe_MakeCode(string modelCode)
        {
            return modelXeService.ModelXe_MakeCode(modelCode);
        }
    }
}
