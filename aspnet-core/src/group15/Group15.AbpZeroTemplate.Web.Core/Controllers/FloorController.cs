using Abp.AspNetCore.Mvc.Controllers;
using Group15.AbpZeroTemplate.Application.Services.CM_Floors;
using Group15.AbpZeroTemplate.Web.Core.Services;
using Group15.AbpZeroTemplate.Web.Core.Services.Export;
using GSoft.AbpZeroTemplate;
using GSoft.AbpZeroTemplate.Dto;
using GWebsite.AbpZeroTemplate.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group15.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FloorController : AbpController
    {
        private IFloorService floorService;
        private Excel excel;
        private IAppFolders appFolders;

        public FloorController(IFloorService floorService, IAppFolders appFolders)
        {
            this.floorService = floorService;
            this.excel = new Excel();
            this.appFolders = appFolders;
        }

        [HttpPost]
        public ServiceResult Create([FromBody]FloorCreate_DTO createInput)
        {
            return this.floorService.Create(createInput);
        }

        [HttpPost]
        public List<Floor_DTO> GetAll()
        {
            return this.floorService.GetAll();
        }

        [HttpPost]
        public List<Floor_DTO> GetByBuildingId([FromQuery]string buildingId)
        {
            return this.floorService.GetByBuildingID(buildingId);
        }

        [HttpPost]
        public Floor_DTO GetById([FromQuery]string floorId)
        {
            return this.floorService.GetById(floorId);
        }

        [HttpPost]
        public ServiceResult Update([FromBody]FloorUpdate_DTO updateInput)
        {
            return this.floorService.Update(updateInput);
        }

        [HttpPost]
        public ServiceResult ApproveAdd([FromQuery]string floorId)
        {
            return this.floorService.Floor_Approve_Add(floorId);
        }

        [HttpPost]
        public ServiceResult CancelApproveAdd([FromQuery] string floorId)
        {
            return this.floorService.Floor_Approve_Add_Cancel(floorId);
        }

        [HttpPost]
        public ServiceResult ApproveUpdate([FromQuery] string floorId)
        {
            return this.floorService.Floor_Approve_Update(floorId);
        }

        [HttpPost]
        public ServiceResult CancelApproveUpdate([FromQuery] string floorTypeId)
        {
            return this.floorService.Floor_Approve_Update_Cancel(floorTypeId);
        }

        [HttpPost]
        public Floor_DTO GetApproveOfId([FromQuery] string floorTypeId)
        {
            return this.floorService.Floor_GetApproveById(floorTypeId);
        }

        [HttpPost]
        public ServiceResult Delete([FromQuery]string floorId)
        {
            return this.floorService.Delete(floorId);
        }

        [HttpPost]
        public ServiceResult ApproveDelete([FromQuery]string floorId)
        {
            return this.floorService.Floor_Approve_Delete(floorId);
        }

        [HttpPost]
        public ServiceResult CancelApproveDelete([FromQuery]string floorId)
        {
            return this.floorService.Floor_Approve_Delete_Cancel(floorId);
        }

        [HttpGet]
        public FileDto Export([FromQuery]string buildingId)
        {
            var listFloors = this.floorService.GetByBuildingID(buildingId);

            var floorTable = this.GetFloorTable(listFloors);

            var floorTemplate = Excel.CreateTemplate<Floor_Template>();

            floorTemplate.SetBuildingId(buildingId);
            floorTemplate.SetTableData(floorTable, "DANH SÁCH TẦNG NHÀ");

            this.excel.Serialize(floorTemplate);

            //Stream stream = this.excel.Write();
            string fileName = $"Floors_{buildingId}.xlsx";
            FileDto file = new FileDto(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            string path = Path.Combine(this.appFolders.TempFileDownloadFolder, file.FileToken);
            this.excel.SaveFile(path, out string error);
            this.excel.Close();

            return file;
        }


        private DataTable GetFloorTable(List<Floor_DTO> floors)
        {
            DataTable table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("MÃ", typeof(string));
            table.Columns.Add("TÊN", typeof(string));
            table.Columns.Add("LOẠI TẦNG", typeof(string));
            table.Columns.Add("TÌNH TRẠNG", typeof(string));
            table.Columns.Add("GHI CHÚ", typeof(string));

            for (int i = 0; i < floors.Count; i++)
            {
                string floorStatus = floors[i].Floor_STATUS == "0" ? "CHƯA DUYỆT" : "ĐÃ DUYỆT";
                table.Rows.Add(
                    i + 1,
                    floors[i].Floor_CODE,
                    floors[i].Floor_NAME,
                    floors[i].FloorType_NAME,
                    floorStatus,
                    floors[i].Floor_NOTE);
            }

            return table;
        }
    }
}
