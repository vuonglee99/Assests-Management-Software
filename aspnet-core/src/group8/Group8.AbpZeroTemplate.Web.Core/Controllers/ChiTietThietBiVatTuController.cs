using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using Group8.AbpZeroTemplate.Application.ViewModel;
using Group8.AbpZeroTemplate.Web.Core.Services.ChiTietThietBiVatTu;
using Group8.AbpZeroTemplate.Web.Core.Services.ChiTietThietBiVatTu.DTO;
using Group8.AbpZeroTemplate.Web.Core.Services.PhuKien;
using Group8.AbpZeroTemplate.Web.Core.Services.PhuKien.dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ChiTietThietBiVatTuController : AbpController
    {
        private readonly IChiTietThietBiVatTuService ChiTietThietBiVatTuService;
        private readonly IPhuKienService PhuKienService;

        public ChiTietThietBiVatTuController(IChiTietThietBiVatTuService chiTietVatTuService, IPhuKienService phuKienService)
        {
            ChiTietThietBiVatTuService = chiTietVatTuService;
            PhuKienService = phuKienService;
        }

        [HttpPost]
        public ThietBiVatTu GetById(string tbvt_ma)
        {
            try
            {
                ThietBiVatTu output;
                ThietBiVatTu_Dto modeldb = ChiTietThietBiVatTuService.CTTBVT_ById(tbvt_ma);

                output = new ThietBiVatTu
                {
                    TBVT_ID = modeldb.TBVT_ID,
                    TBVT_MA_TBVT = modeldb.TBVT_MA_TBVT,
                    TBVT_TEN = modeldb.TBVT_TEN,
                    TBVT_SERIAL = modeldb.TBVT_SERIAL,
                    TBVT_LOAI = modeldb.TBVT_LOAI,
                    TBVT_NGAY_MUA = modeldb.TBVT_NGAY_MUA,
                    TBVT_DVT = modeldb.TBVT_DVT,
                    TBVT_NHAP_THEO_LO = modeldb.TBVT_NHAP_THEO_LO,
                    TBVT_SL_THEO_LO = modeldb.TBVT_SL_THEO_LO,
                    TBVT_HANG_SX = modeldb.TBVT_HANG_SX,
                    TBVT_NAM_SX = modeldb.TBVT_NAM_SX,
                    TBVT_NGAY_TINH_BAO_HANH = modeldb.TBVT_NGAY_TINH_BAO_HANH,
                    TBVT_NGAY_KET_THUC_BAO_HANH = modeldb.TBVT_NGAY_KET_THUC_BAO_HANH,
                    TBVT_NHA_CUNG_CAP = modeldb.TBVT_NHA_CUNG_CAP,
                    TBVT_TINH_TRANG_THIET_BI = modeldb.TBVT_TINH_TRANG_THIET_BI,
                    TBVT_GHI_CHU_TINH_TRANG = modeldb.TBVT_GHI_CHU_TINH_TRANG,
                    TBVT_CAN_BAO_DUONG = modeldb.TBVT_CAN_BAO_DUONG,
                    TBVT_CHU_KY_BAO_DUONG = modeldb.TBVT_CHU_KY_BAO_DUONG,
                    TBVT_NOI_DUNG_BAO_DUONG = modeldb.TBVT_NOI_DUNG_BAO_DUONG,
                    TBVT_TI_LE_HAO_MON = modeldb.TBVT_TI_LE_HAO_MON,
                    TBVT_TINH_TRANG_TBTBVT = modeldb.TBVT_TINH_TRANG_TBTBVT,
                    TBVT_PhuKien = PhuKienService.PhuKien_GetBy_TBVT_ID(modeldb.TBVT_ID)
                };

                if (modeldb == null)
                {
                    return null;
                }

                //output = new ThietBiVatTu
                //{
                //    Console.WriteLine(ex.Message);
                //    Console.WriteLine(ex.StackTrace);

                //    return null;
                //};
                return output;
            }
            catch {
                return null;
            }
        }

        [HttpPost]
        public List<dynamic> Insert([FromBody]ThietBiVatTu input)
        {
            try
            {
                ThietBiVatTu_Dto modeldb = new ThietBiVatTu_Dto
                {
                    TBVT_MA_TBVT = input.TBVT_MA_TBVT,
                    TBVT_TEN = input.TBVT_TEN,
                    TBVT_SERIAL = input.TBVT_SERIAL,
                    TBVT_LOAI = input.TBVT_LOAI,
                    TBVT_NGAY_MUA = input.TBVT_NGAY_MUA,
                    TBVT_DVT = input.TBVT_DVT,
                    TBVT_NHAP_THEO_LO = input.TBVT_NHAP_THEO_LO,
                    TBVT_SL_THEO_LO = input.TBVT_SL_THEO_LO,
                    TBVT_HANG_SX = input.TBVT_HANG_SX,
                    TBVT_NAM_SX = input.TBVT_NAM_SX,
                    TBVT_NGAY_TINH_BAO_HANH = input.TBVT_NGAY_TINH_BAO_HANH,
                    TBVT_NGAY_KET_THUC_BAO_HANH = input.TBVT_NGAY_KET_THUC_BAO_HANH,
                    TBVT_NHA_CUNG_CAP = input.TBVT_NHA_CUNG_CAP,
                    TBVT_TINH_TRANG_THIET_BI = input.TBVT_TINH_TRANG_THIET_BI,
                    TBVT_GHI_CHU_TINH_TRANG = input.TBVT_GHI_CHU_TINH_TRANG,
                    TBVT_CAN_BAO_DUONG = input.TBVT_CAN_BAO_DUONG,
                    TBVT_CHU_KY_BAO_DUONG = input.TBVT_CHU_KY_BAO_DUONG,
                    TBVT_NOI_DUNG_BAO_DUONG = input.TBVT_NOI_DUNG_BAO_DUONG,
                    TBVT_TI_LE_HAO_MON = input.TBVT_TI_LE_HAO_MON,
                    TBVT_TINH_TRANG_TBTBVT = input.TBVT_TINH_TRANG_TBTBVT,
                    RECORD_STATUS = "1"
                };

                var result = ChiTietThietBiVatTuService.CTTBVT_Insert(modeldb);

                Console.WriteLine("ThietBiVatTu {0} inserted with result: ", input.TBVT_MA_TBVT);
                Console.WriteLine(result[0]);


                if (input.TBVT_PhuKien == null)
                {
                    Console.WriteLine("No PhuKien to insert");
                    return result;
                }
                int TBVT_ID = result.FirstOrDefault().TBVTID;
                foreach (var phukien in input.TBVT_PhuKien)
                {
                    Console.WriteLine("PhuKien {0} processing!", phukien.PHU_KIEN_MA_PK);

                    PhuKien_DTO vt_phukien = phukien;
                    vt_phukien.PHU_KIEN_TBVT_ID = TBVT_ID;

                    var phukienResult = PhuKienService.PhuKien_Insert(vt_phukien).FirstOrDefault();
                    Console.WriteLine("PhuKien {0} inserted!", phukien.PHU_KIEN_MA_PK);
                    Console.WriteLine(phukienResult);
                    if (phukienResult.Result < 0)
                    {

                        result.Add(phukienResult);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return new List<dynamic>
                {
                    new { Result = -1, ErrorDesc = "Có lỗi phía hệ thông, vui lòng báo lên quản trị viên"}
                };
            }
        }

        [HttpPost]
        public List<dynamic> Update([FromBody]ThietBiVatTu input)
        {
            try
            {
                ThietBiVatTu_Dto modeldb = new ThietBiVatTu_Dto
                {
                    TBVT_ID = input.TBVT_ID.Value,
                    TBVT_MA_TBVT = input.TBVT_MA_TBVT,
                    TBVT_TEN = input.TBVT_TEN,
                    TBVT_SERIAL = input.TBVT_SERIAL,
                    TBVT_LOAI = input.TBVT_LOAI,
                    TBVT_NGAY_MUA = input.TBVT_NGAY_MUA,
                    TBVT_DVT = input.TBVT_DVT,
                    TBVT_NHAP_THEO_LO = input.TBVT_NHAP_THEO_LO,
                    TBVT_SL_THEO_LO = input.TBVT_SL_THEO_LO,
                    TBVT_HANG_SX = input.TBVT_HANG_SX,
                    TBVT_NAM_SX = input.TBVT_NAM_SX,
                    TBVT_NGAY_TINH_BAO_HANH = input.TBVT_NGAY_TINH_BAO_HANH,
                    TBVT_NGAY_KET_THUC_BAO_HANH = input.TBVT_NGAY_KET_THUC_BAO_HANH,
                    TBVT_NHA_CUNG_CAP = input.TBVT_NHA_CUNG_CAP,
                    TBVT_TINH_TRANG_THIET_BI = input.TBVT_TINH_TRANG_THIET_BI,
                    TBVT_GHI_CHU_TINH_TRANG = input.TBVT_GHI_CHU_TINH_TRANG,
                    TBVT_CAN_BAO_DUONG = input.TBVT_CAN_BAO_DUONG,
                    TBVT_CHU_KY_BAO_DUONG = input.TBVT_CHU_KY_BAO_DUONG,
                    TBVT_NOI_DUNG_BAO_DUONG = input.TBVT_NOI_DUNG_BAO_DUONG,
                    TBVT_TI_LE_HAO_MON = input.TBVT_TI_LE_HAO_MON,
                    TBVT_TINH_TRANG_TBTBVT = input.TBVT_TINH_TRANG_TBTBVT
                };
                Console.WriteLine("update {0} processing", input.TBVT_MA_TBVT);

                var result = ChiTietThietBiVatTuService.CTTBVT_Update(modeldb);

                var updateResult = result.FirstOrDefault();

                if (updateResult.Result < 0)
                {
                    Console.WriteLine("update error: {0}", updateResult.ErrorDesc);
                }


                if (input.TBVT_PhuKien == null)
                {
                    Console.WriteLine("no PhuKien updated", input.TBVT_MA_TBVT);

                    return result;
                }

                foreach (var phukien in input.TBVT_PhuKien)
                {
                    PhuKien_DTO phukienDto = PhuKienService.PhuKien_ById(phukien.PHU_KIEN_MA_PK);
                    if (phukien != null)
                    {
                        Console.WriteLine("updating phukien {0} ", phukien.PHU_KIEN_ID);

                        phukienDto.PHU_KIEN_DVT = phukien.PHU_KIEN_DVT;
                        phukienDto.PHU_KIEN_GHI_CHU = phukien.PHU_KIEN_GHI_CHU;
                        phukienDto.PHU_KIEN_SO_LUONG = phukien.PHU_KIEN_SO_LUONG;
                        phukienDto.PHU_KIEN_TEN = phukien.PHU_KIEN_TEN;

                        var phukienResult = PhuKienService.PhuKien_Update(phukienDto).FirstOrDefault();

                        if (phukienResult.Result < 0)
                        {
                            Console.WriteLine("update failed phukien {0} ", phukien.PHU_KIEN_ID);
                            Console.WriteLine(phukienResult.ErrorDesc);
                        }
                        else
                        {
                            Console.WriteLine("updated phukien {0} ", phukien.PHU_KIEN_ID);
                        }

                    }
                    else
                    {
                        PhuKien_DTO vt_phukien = phukien;
                        vt_phukien.PHU_KIEN_TBVT_ID = updateResult.TBVTID;
                        var phukienResult = PhuKienService.PhuKien_Insert(vt_phukien).FirstOrDefault();

                        if (phukienResult.Result < 0)
                        {
                            Console.WriteLine("insert failed phukien {0} ", phukien.PHU_KIEN_ID);
                            Console.WriteLine(phukienResult.ErrorDesc);
                        }
                        else
                        {
                            Console.WriteLine("inserted phukien {0} ", phukien.PHU_KIEN_ID);
                        }
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return new List<dynamic>
                {
                    new { Result = -1, ErrorDesc = "Có lỗi phía hệ thông, vui lòng báo lên trị viên"}
                };
            }
        }

        [HttpPost]
        public List<dynamic> Delete(string tbvt_ma)
        {
            try
            {
                return ChiTietThietBiVatTuService.CTTBVT_Delete(tbvt_ma);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new List<dynamic>
                {
                    new { Result = -1, ErrorDesc = "Có lỗi phía hệ thông, vui lòng báo lên quản trị viên"}
                };
            }
        }
    }
}
