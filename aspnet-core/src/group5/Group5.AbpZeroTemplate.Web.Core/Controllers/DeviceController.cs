using Abp.AspNetCore.Mvc.Controllers;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_DEVICES;
using Group5.AbpZeroTemplate.Web.Core.Services.CM_DEVICES.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group5.AbpZeroTemplate.Application.Controllers
{

    [Route("api/[controller]/[action]")]
    public class DeviceController : AbpController
    {
        private readonly IDeviceAppService deviceAppService;

        public DeviceController(IDeviceAppService deviceAppService)
        {
            this.deviceAppService = deviceAppService;
        }

        [HttpPost]
        public List<CM_DEV_DTO> DEVICE_Search(string index,[FromBody] CM_DEV_DTO filterInput)
        {
            return deviceAppService.DEVICE_Search(index,filterInput);
        }

        [HttpPost]
        public List<dynamic> DEVICE_Get_All_Branch()
        {
            return deviceAppService.DEVICE_Get_All_Branch();
        }

        [HttpPost]
        public List<dynamic> DEVICE_Insert([FromBody] CM_DEV_DTO input)
        {
            return deviceAppService.DEVICE_Insert(input);
        }

        [HttpPost]
        public List<dynamic> DEVICE_Update([FromBody] CM_DEV_DTO input)
        {
            return deviceAppService.DEVICE_Update(input);
        }


        [HttpPost]
        public IDictionary<string, object> DEVICE_Delete(string devID)
        {
            return deviceAppService.DEVICE_Delete(devID);
        }

        [HttpPost]
        public List<string> DEVICE_Get_All_Name()
        {
            return deviceAppService.DEVICE_Get_All_Name();
        }

        [HttpPost]
        public List<CM_DEV_DTO> G2_DEVICE_GETALL()
        {
            return deviceAppService.G2_DEVICE_GETALL();
        }
        [HttpPost]
        public IDictionary<string, object> DEVICE_Approve(int check, string devID)
        {
            return deviceAppService.DEVICE_Approve(check, devID);
        }
        [HttpPost]
        public string Get_Current_ID()
        {
            return deviceAppService.Get_Current_ID();
        }
    }
}