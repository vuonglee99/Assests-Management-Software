using Abp.Application.Services;
using Abp.Authorization;
using Group3.AbpZeroTemplate.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3.AbpZeroTemplate.Web.Core.Enumerated
{
    public interface IEnumeratedTypesAppService : IApplicationService
    {
        dynamic Create(EnumeratedTypeCreateRequest body);
        dynamic Delete(int id);
        IEnumerable<EnumeratedTypeTableDTO> GetByType(string type);
    }
    public class EnumeratedTypesAppService : Group3AppServiceBase, IEnumeratedTypesAppService
    {
        [AbpAuthorize(Group3PermissionsConst.EnumeratedTypes_Create)]
        public dynamic Create(EnumeratedTypeCreateRequest body)
        {
            return procedureHelper.GetData<dynamic>("EnumeratedType_Create", body)
                .FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.EnumeratedTypes_Delete)]
        public dynamic Delete(int id)
        {
            return procedureHelper.GetData<dynamic>(
                "EnumeratedType_Delete", new { ID = id }
            ).FirstOrDefault();
        }

        [AbpAuthorize(Group3PermissionsConst.EnumeratedTypes)]
        public IEnumerable<EnumeratedTypeTableDTO> GetByType(string type)
        {
            return procedureHelper.GetData<EnumeratedTypeTableDTO>(
                "EnumeratedType_GetByType", new { TYPE = type });
        }
    }
}
