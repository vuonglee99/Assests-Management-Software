﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Group10.AbpZeroTemplate.Application;
using Abp.Application.Services.Dto;
using Group10.AbpZeroTemplate.Web.Core.Services.CM_CTBD.Dto;
using GSoft.AbpZeroTemplate.Helpers;
namespace Group10.AbpZeroTemplate.Web.Core.Services.CM_CTBD
{
    public interface ICTBDAppService : IApplicationService
    {

        ChiTietBaoDuong_DTO ChiTietBaoDuong_GetById(string Id);
        IDictionary<string, object> ChiTietBaoDuong_Delete(ChiTietBaoDuong_DTO input);
        IDictionary<string, object> ChiTietBaoDuong_Insert(ChiTietBaoDuong_DTO input);
        IDictionary<string, object> ChiTietBaoDuong_Update(ChiTietBaoDuong_DTO input);
        PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Search(string name, string bdID);
        PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_ByBD_ID(string Id);
        PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_GetApprove_ByBD_ID(string Id);
        List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Insert(string Id);
        List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Delete(string code);
        List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Update(string code);
        List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Insert(string Id, string reason);
        List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Delete(string code, string reason);
        List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Update(string code, string reason);
        ChiTietBaoDuong_DTO ChiTietBaoDuong_Search_ByCode(string code);
        bool ChiTietBaoDuong_IsUpdating(string code);


    }
    public class CTBDAppService : BaseService, ICTBDAppService
    {
        public ChiTietBaoDuong_DTO ChiTietBaoDuong_GetById(string Id)
        {
            ChiTietBaoDuong_DTO row = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_GETBYID", new { CTBD_ID = Id, RECORD_STATUS = '1' }).ToList().FirstOrDefault();
            if(row != null)
            {
                return row;
            }
            else
            {
                row = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_GETBYID", new { CTBD_ID = Id, RECORD_STATUS = '0' }).ToList().FirstOrDefault();
                return row;
            }
        }
        [AbpAuthorize(Group10PermissionsConst.Pages_Administration_CTBD_Delete)]
        public IDictionary<string, object> ChiTietBaoDuong_Delete(ChiTietBaoDuong_DTO input)
        {
            return procedureHelper.GetData<dynamic>("CTBaoDuong_DELETE", input).FirstOrDefault();
        }

       

        public IDictionary<string, object> ChiTietBaoDuong_Insert(ChiTietBaoDuong_DTO input)
        {
            input.RECORD_STATUS = "1";
            return procedureHelper.GetData<dynamic>("CTBaoDuong_INSERT", input).FirstOrDefault();
        }

        public PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Search(string name, string bdID)
        {
            var list = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_SEARCH", new { CTBD_NAME = name, BD_ID = bdID }).ToList();
            var totalCount = list.Count();
            return new PagedResultDto<ChiTietBaoDuong_DTO>(
               totalCount,
               list
            );
        }

        [AbpAuthorize(Group10PermissionsConst.Pages_Administration_CTBD_Update)]
        public IDictionary<string, object> ChiTietBaoDuong_Update(ChiTietBaoDuong_DTO input)
        {
            return procedureHelper.GetData<dynamic>("CTBaoDuong_UPDATE", input).FirstOrDefault();
        }

        public PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_ByBD_ID(string Id)
        {
            List<ChiTietBaoDuong_DTO> list = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBD_GetByBD_ID", new { BD_ID = Id }).ToList();
            var totalCount = list.Count();

            return new PagedResultDto<ChiTietBaoDuong_DTO>(
               totalCount,
               list
            );
        }

        public PagedResultDto<ChiTietBaoDuong_DTO> ChiTietBaoDuong_GetApprove_ByBD_ID(string Id)
        {
            List<ChiTietBaoDuong_DTO> list = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_GETALL", new { BD_ID = Id }).ToList();
            var totalCount = list.Count();

            return new PagedResultDto<ChiTietBaoDuong_DTO>(
               totalCount,
               list
            );
        }

        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Insert(string Id)
        {
            return procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_APPROVE_INSERT", new ChiTietBaoDuong_DTO { CTBD_ID = Id });
        }

        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Delete(string code)
        {
            return procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_APPROVE_DELETE", new ChiTietBaoDuong_DTO { CTBD_CODE = code });
        }

        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Approve_Update(string code)
        {
            return procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_APPROVE_UPDATE", new ChiTietBaoDuong_DTO { CTBD_CODE = code });
        }

        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Insert(string Id, string reason)
        {
            return procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_DENY_INSERT", new ChiTietBaoDuong_DTO { CTBD_ID = Id, REASON = reason });
        }

        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Delete(string code, string reason)
        {
            return procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_DENY_DELETE", new ChiTietBaoDuong_DTO { CTBD_CODE = code, REASON = reason });
        }

        public List<ChiTietBaoDuong_DTO> ChiTietBaoDuong_Deny_Update(string code, string reason)
        {
            return procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_DENY_UPDATE", new ChiTietBaoDuong_DTO { CTBD_CODE = code, REASON = reason });
        }
        public bool ChiTietBaoDuong_IsUpdating(string code)
        {
            var list = procedureHelper.GetData<dynamic>("CTBaoDuong_SEARCHBYCODE", new ChiTietBaoDuong_DTO { CTBD_CODE = code });
            if (list.Count() > 1)
                return true;
            return false;
        }

        public ChiTietBaoDuong_DTO ChiTietBaoDuong_Search_ByCode(string code)
        {
            ChiTietBaoDuong_DTO row = procedureHelper.GetData<ChiTietBaoDuong_DTO>("CTBaoDuong_SEARCHBYCODE", new ChiTietBaoDuong_DTO { CTBD_CODE = code }).ToList().FirstOrDefault();
            return row;
        }
    }
}