using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Group7.AbpZeroTemplate.Web.Core.Services.YEU_CAU_SUA_CHUA;
using Group7.AbpZeroTemplate.Web.Core.Services.YEU_CAU_SUA_CHUA.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Group7.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class YeuCauSuaChuaController : AbpController
    {
        private readonly IYeuCauSuaChuaAppService yeuCauSuaChuaAppService;

        public YeuCauSuaChuaController(IYeuCauSuaChuaAppService yeuCauSuaChuaAppService)
        {
            this.yeuCauSuaChuaAppService = yeuCauSuaChuaAppService;
        }

        [HttpPost]
        public List<dynamic> YCSCByTBVTId(string YCSC_TBVT_ID)
        {
            return yeuCauSuaChuaAppService.YCSCByTBVTId(YCSC_TBVT_ID);
        }
        
        [HttpPost]
        public IDictionary<string, object> YCSCById(string YCSC_MA_YCSC)
        {
            return yeuCauSuaChuaAppService.YCSCById(YCSC_MA_YCSC);
        }

        [HttpPost]
        public IDictionary<string, object> YCSCInsert([FromBody]YEU_CAU_SUA_CHUA_DTO input)
        {
            return yeuCauSuaChuaAppService.YCSCInsert(input);
        }
        [HttpPost]
        public List<dynamic> YCSCUpdate([FromBody]YEU_CAU_SUA_CHUA_DTO input)
        {
            return yeuCauSuaChuaAppService.YCSCUpdate(input);
        }
        [HttpPost]
        public List<dynamic> YCSCDelete()
        {
            return yeuCauSuaChuaAppService.YCSCDelete();
        }
        [HttpPost]
        public List<dynamic> YCSCDeleteSingle([FromQuery]string YCSC_ID)
        {
            return yeuCauSuaChuaAppService.YCSCDeleteSingle(YCSC_ID);
        }
        [HttpGet]
        public List<dynamic> YCSCGetAll()
        {
            return yeuCauSuaChuaAppService.YCSCGetAll();
        }
        [HttpGet]
        public List<dynamic> YCSCSearchWithOption([FromQuery] string toSearch, [FromQuery] string searchType)
        {
            return yeuCauSuaChuaAppService.YCSCSearchWithOption(toSearch, searchType);
        }
    }
}
