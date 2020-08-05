﻿using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Dapper;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_CTBD;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_CTBD.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group10.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CTBDController : AbpController
    {
        private readonly ICTBDAppService CTBDAppService;
        public CTBDController(ICTBDAppService CTBDAppService)
        {
            this.CTBDAppService = CTBDAppService;


        }

        [HttpPost]
        public IDictionary<string, object> ChiTietBaoDuong_Insert([FromBody] ChiTietBaoDuong_DTO input)
        {

            return CTBDAppService.ChiTietBaoDuong_Insert(input);
        }

        [HttpPut]
        public IDictionary<string, object> ChiTietBaoDuong_Update([FromBody] ChiTietBaoDuong_DTO input)
        {
            return CTBDAppService.ChiTietBaoDuong_Update(input);
        }

        [HttpPost]
        public IDictionary<string, object> ChiTietBaoDuong_Delete([FromBody] ChiTietBaoDuong_DTO input)
        {
            return CTBDAppService.ChiTietBaoDuong_Delete(input);
        }

        [HttpGet("{id}")]
        public ChiTietBaoDuong_DTO ChiTietBaoDuong_GetById(string id)
        {
            return CTBDAppService.ChiTietBaoDuong_GetById(id);
        }

        [HttpPost]
        public PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Search(string name, string bdID)
        {
            return CTBDAppService.ChiTietBaoDuong_Search(name, bdID);
        }

        [HttpGet("{id}")]
        public PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_ByBD_ID(string id)
        {
            return CTBDAppService.ChiTietBaoDuong_ByBD_ID(id);
        }

        [HttpGet("{id}")]
        public PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_GetApprove_ByBD_ID(string id)
        {
            return CTBDAppService.ChiTietBaoDuong_GetApprove_ByBD_ID(id);
        }

        [HttpPost]
        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Insert(string ctbdId)
        {
            return CTBDAppService.ChiTietBaoDuong_Approve_Insert(ctbdId);
        }
        [HttpPost]
        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Delete(string ctbdCode)
        {
            return CTBDAppService.ChiTietBaoDuong_Approve_Delete(ctbdCode);
        }
        [HttpPost]
        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Update(string ctbdCode)
        {
            return CTBDAppService.ChiTietBaoDuong_Approve_Update(ctbdCode);
        }
        [HttpPost]
        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Insert(string ctbdId, string reason)
        {
            return CTBDAppService.ChiTietBaoDuong_Deny_Insert(ctbdId, reason);
        }
        [HttpPost]
        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Delete(string ctbdCode, string reason)
        {
            return CTBDAppService.ChiTietBaoDuong_Deny_Delete(ctbdCode, reason);
        }
        [HttpPost]
        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Update(string ctbdCode, string reason)
        {
            return CTBDAppService.ChiTietBaoDuong_Deny_Update(ctbdCode, reason);
        }
        [HttpPost]
        public bool isUpdating(string ctbdCode)
        {
            return CTBDAppService.ChiTietBaoDuong_IsUpdating(ctbdCode);
        }

        [HttpPost]
        public ChiTietBaoDuong_DTO ChiTietBaoDuong_Search_ByCode(string ctbdCode)
        {
            return CTBDAppService.ChiTietBaoDuong_Search_ByCode(ctbdCode);
        }
    }
}