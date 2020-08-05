using Abp.AspNetCore.Mvc.Controllers;
using Group15.AbpZeroTemplate.Web.Core.Services.Apartments;
using Group15.AbpZeroTemplate.Web.Core.Services.Apartments.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Apartment_Controller : AbpController
    {
        private IApartmentService apartmentService;

        public Apartment_Controller(IApartmentService apartmentService)
        {
            this.apartmentService = apartmentService;
        }

        [HttpPost]
        public List<Apartment_DTO> GetAll()
        {
            return this.apartmentService.GetAll();
        }
    }
}
