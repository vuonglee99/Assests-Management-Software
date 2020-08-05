using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Contracts
{
    public interface IContractsAppService : IApplicationService
    {
        dynamic Approve(ContractApproveRequest body);
        dynamic Create(ContractCreateRequest body);
        dynamic Delete(string id);
        ContractTableDTO GetByID(string id);
        PagedResultDto<ContractPagingSearchDTO> PagingSearch(
            ContractPagingSearchRequest query);
        dynamic Update(ContractUpdateRequest body);
    }
    public class ContractsAppService : Group3AppServiceBase, IContractsAppService
    {
        [AbpAuthorize(Group3PermissionsConst.Contracts_Approve)]
        public dynamic Approve(ContractApproveRequest body)
        {
            throw new NotImplementedException();
        }

        [AbpAuthorize(Group3PermissionsConst.Contracts_Create)]
        public dynamic Create(ContractCreateRequest body)
        {
            throw new NotImplementedException();
        }

        [AbpAuthorize(Group3PermissionsConst.Contracts_Delete)]
        public dynamic Delete(string id)
        {
            throw new NotImplementedException();
        }

        [AbpAuthorize(Group3PermissionsConst.Contracts)]
        public ContractTableDTO GetByID(string id)
        {
            throw new NotImplementedException();
        }

        [AbpAuthorize(Group3PermissionsConst.Contracts)]
        public PagedResultDto<ContractPagingSearchDTO> PagingSearch(ContractPagingSearchRequest query)
        {
            throw new NotImplementedException();
        }

        [AbpAuthorize(Group3PermissionsConst.Contracts_Update)]
        public dynamic Update(ContractUpdateRequest body)
        {
            throw new NotImplementedException();
        }
    }
}
