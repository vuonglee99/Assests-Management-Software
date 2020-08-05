using Abp.Application.Services;
using Abp.Dependency;
using GSoft.AbpZeroTemplate.Helpers.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GSoft.AbpZeroTemplate.Helpers
{
    public class BaseService : ApplicationService
    {
        protected IProcedureHelper procedureHelper;
        public BaseService()
        {
            this.procedureHelper = IocManager.Instance.Resolve<IProcedureHelper>();
        }

        //public AppUser GetCurrentUser()
        //{
        //    return procedureHelper.GetData<AppUser>("Nhom05_GET_USER_BRANCH_BYUSERID", new
        //    {
        //        USER_ID = AbpSession.UserId
        //    }).FirstOrDefault();
        //}

    }
}
