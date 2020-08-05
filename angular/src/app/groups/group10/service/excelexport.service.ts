import { Injectable } from '@angular/core';
import * as ExcelJS from 'exceljs/dist/exceljs.min.js';

import * as fs from 'file-saver';
import { copyStyles } from '@angular/animations/browser/src/util';
import * as  moment from "moment";
@Injectable({
  providedIn: 'root'
})
export class ExcelExportService {

  constructor() {

   }

   async generateExcelNtx(data) {
    const header = ['stt', 'hoten', 'diachi', 'mantx', 'gioitinh', 'ngaysinh', 'cmnd', 'gplx'];
    // Create workbook and worksheet
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet();
    // Cell Style : Fill and Header


    let dateFormat = require('dateformat');
    let now = new Date();
    ;

    let MMDDYY = "xxx";
    var FileName = "NguoiThueXe_" + dateFormat(now, "shortDate");

    const title = 'Danh sách người thuê xe ';
    // Add new row
    let titleRow = worksheet.addRow([title]);

    // Set font, size and style in title row.
    titleRow.font = { name: 'Times New Roman', size:15, bold: true, };

    // Blank Row
    worksheet.addRow([]);

    //Add row with current date
    let subTitleRow = worksheet.addRow(['Ngày:'+dateFormat(now, 'shortDate') ]);

    worksheet.addRow([]);

    const headerRow = worksheet.addRow(['STT', 'Họ tên', 'Địa chỉ', 'Mã NTX', 'Giới tính', 'Ngày sinh', 'CMND', 'GPLX ']);
    headerRow.eachCell((cell, number) => {
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: {
                argb: 'FFFFFFFF'
            },
            bgColor: {
                argb: 'FFFFFFFF'
            },
        };
        cell.font = {
            color: {
                argb: '00000000',
            },
            bold: true
        }
        cell.border = {
            top: {
                style: 'thin'
            },
            left: {
                style: 'thin'
            },
            bottom: {
                style: 'thin'
            },
            right: {
                style: 'thin'
            }
        };
    });
    console.log("Data here: ");

    data.forEach(d => {
        var rowValues=[];

        for(let i =0; i<8; i++)
        {
           // console.log(header[i]);
            rowValues[i]=d[header[i]];
        }
        const row = worksheet.addRow(rowValues);

        row.eachCell((cell, number) => {
            cell.border = {
                top: {
                    style: 'thin'
                },
                left: {
                    style: 'thin'
                },
                bottom: {
                    style: 'thin'
                },
                right: {
                    style: 'thin'
                }
            };
        });
    });

    worksheet.getCell('A1').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.getCell('A3').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.mergeCells('A1:B1');
    worksheet.mergeCells('A3:B3');

    worksheet.eachRow((row)=>{
        row.eachCell((cell)=>{
            cell.alignment ={ vertical: 'middle', horizontal: 'center' };
        })
    });
    worksheet.getColumn(1).width = 10;
    worksheet.getColumn(2).width = 20;
    worksheet.getColumn(3).width = 15;
    worksheet.getColumn(4).width = 15;
    worksheet.getColumn(5).width = 15;
    worksheet.getColumn(6).width = 15;
    worksheet.getColumn(7).width = 15;
    worksheet.getColumn(8).width = 15;
    worksheet.eachCe
    workbook.xlsx.writeBuffer().then((data: any) => {
        const blob = new Blob([data], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        });



        fs.saveAs(blob, FileName + '.xlsx');
    });
  }

  async generateExcelNtxDetail(data, maNtx, hoTen, cmnd, gplx) {
    const header = ['stt', 'maxe', 'tenxe', 'mauxe', 'hangsx', 'bienso'];
    // Create workbook and worksheet
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet();
    // Cell Style : Fill and Header


    let dateFormat = require('dateformat');
    let now = new Date();


    let MMDDYY = "xxx";
    var FileName = "LichSuThueXe_" +  moment(now).format("DD/MM/YYYY");

    const title = 'Lịch sử thuê xe ';
    // Add new row
    let titleRow = worksheet.addRow([title]);

    // Set font, size and style in title row.
    titleRow.font = { name: 'Times New Roman', size:15, bold: true, };

    worksheet.addRow([]);
    // Blank Row
    worksheet.addRow(['Mã NTX ', maNtx]);
    worksheet.addRow(['Họ và tên', hoTen]);
    worksheet.addRow(['Số CMND',cmnd]);
    worksheet.addRow(['Số GPLX', gplx]);

    //Add row with current date
    let subTitleRow = worksheet.addRow(['Ngày', dateFormat(now, 'shortDate') ]);

    worksheet.addRow([]);

    let listXe = worksheet.addRow(['Danh sách xe đã thuê'])
    listXe.font = { name: 'Times New Roman', size:15, bold: true, };

    worksheet.addRow([]);

    const headerRow = worksheet.addRow(['STT', 'Mã xe', 'Tên xe', 'Màu xe', 'Hãng SX', 'Biển số']);
    headerRow.eachCell((cell, number) => {
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: {
                argb: 'FFFFFFFF'
            },
            bgColor: {
                argb: 'FFFFFFFF'
            },
        };
        cell.font = {
            color: {
                argb: '00000000',
            },
            bold: true
        }
        cell.alignment ={ vertical: 'middle', horizontal: 'center' };
        cell.border = {
            top: {
                style: 'thin'
            },
            left: {
                style: 'thin'
            },
            bottom: {
                style: 'thin'
            },
            right: {
                style: 'thin'
            }
        };
    });


    data.forEach(d => {
        var rowValues=[];
        console.log(d);
        for(let i =0; i<6; i++)
        {
           // console.log(header[i]);
            rowValues[i]=d[header[i]];
            if(d[header[i]]==null)
            {
                rowValues[i]="";
            }

        }
        const row = worksheet.addRow(rowValues);

        row.eachCell((cell, number) => {
            cell.border = {
                top: {
                    style: 'thin'
                },
                left: {
                    style: 'thin'
                },
                bottom: {
                    style: 'thin'
                },
                right: {
                    style: 'thin'
                }
            };
        });
    });

    worksheet.getCell('A1').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.getCell('A3').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.mergeCells('A1:B1');

    worksheet.mergeCells('A9:B9');
    worksheet.eachRow((row)=>{
        row.eachCell((cell)=>{
            cell.alignment ={ vertical: 'middle', horizontal: 'left' };
        })
    });
    worksheet.getColumn(1).width = 10;
    worksheet.getColumn(2).width = 20;
    worksheet.getColumn(3).width = 15;
    worksheet.getColumn(4).width = 15;
    worksheet.getColumn(5).width = 15;
    worksheet.getColumn(6).width = 15;

    worksheet.eachCe
    workbook.xlsx.writeBuffer().then((data: any) => {
        const blob = new Blob([data], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        });
        fs.saveAs(blob, FileName + '.xlsx');
    });
  }


  async generateExcelDstx(data) {

    const header = ['stt', 'nguoithue', 'xe', 'ngaythue', 'hantra', 'ngaytra', 'giathue'];
    // Create workbook and worksheet
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet();
    // Cell Style : Fill and Header


    let dateFormat = require('dateformat');
    let now = new Date();
    ;

    let MMDDYY = "xxx";
    var FileName = "DanhSachPhieuThueXe_" + dateFormat(now, "shortDate");

    const title = 'Danh sách Thuê Xe  ';
    // Add new row
    let titleRow = worksheet.addRow([title]);

    // Set font, size and style in title row.
    titleRow.font = { name: 'Times New Roman', size:15, bold: true, };

    // Blank Row
    worksheet.addRow([]);

    //Add row with current date
    let subTitleRow = worksheet.addRow(['Ngày:'+dateFormat(now, 'shortDate') ]);

    worksheet.addRow([]);

    const headerRow = worksheet.addRow(['STT', 'Người thuê xe', 'Xe', 'Ngày thuê', 'Hạn trả', 'Ngày trả', 'Giá thuê']);
    headerRow.eachCell((cell, number) => {
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: {
                argb: 'FFFFFFFF'
            },
            bgColor: {
                argb: 'FFFFFFFF'
            },
        };
        cell.font = {
            color: {
                argb: '00000000',
            },
            bold: true
        }
        cell.border = {
            top: {
                style: 'thin'
            },
            left: {
                style: 'thin'
            },
            bottom: {
                style: 'thin'
            },
            right: {
                style: 'thin'
            }
        };
    });
    console.log("Data here: ");

    data.forEach(d => {
        var rowValues=[];

        for(let i =0; i<7; i++)
        {
           // console.log(header[i]);
            rowValues[i]=d[header[i]];
        }
        const row = worksheet.addRow(rowValues);

        row.eachCell((cell, number) => {
            cell.border = {
                top: {
                    style: 'thin'
                },
                left: {
                    style: 'thin'
                },
                bottom: {
                    style: 'thin'
                },
                right: {
                    style: 'thin'
                }
            };
        });
    });

    worksheet.getCell('A1').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.getCell('A3').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.mergeCells('A1:B1');
    worksheet.mergeCells('A3:B3');

    worksheet.eachRow((row)=>{
        row.eachCell((cell)=>{
            cell.alignment ={ vertical: 'middle', horizontal: 'center' };
        })
    });

    worksheet.getColumn(1).width = 10;
    worksheet.getColumn(2).width = 20;
    worksheet.getColumn(3).width = 15;
    worksheet.getColumn(4).width = 15;
    worksheet.getColumn(5).width = 15;
    worksheet.getColumn(6).width = 15;
    worksheet.getColumn(7).width = 15;
    worksheet.getColumn(7).alignment={ vertical: 'middle', horizontal: 'right'}
    worksheet.getColumn(8).width = 15;

    worksheet.eachCe
    workbook.xlsx.writeBuffer().then((data: any) => {
        const blob = new Blob([data], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        });



        fs.saveAs(blob, FileName + '.xlsx');
    });
  }

  async generateExcelLichSuDaTHue(data, maXe, tenXe, mauXe, bienSo) {

    const header = ['stt', 'mantx', 'hoten', 'gioitinh', 'diachi', 'ngaysinh', 'cmnd', 'gplx'];
    // Create workbook and worksheet
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet();
    // Cell Style : Fill and Header


    let dateFormat = require('dateformat');
    let now = new Date();


    let MMDDYY = "xxx";
    var FileName = "LichSuDaThueXe_" + dateFormat(now, "shortDate");

    const title = 'Lịch sử người đã thuê xe';
    // Add new row
    let titleRow = worksheet.addRow([title]);

    // Set font, size and style in title row.
    titleRow.font = { name: 'Times New Roman', size:15, bold: true, };

    worksheet.addRow([]);
    // Blank Row
    worksheet.addRow(['Mã xe ', maXe]);
    worksheet.addRow(['Tên xe', tenXe]);
    worksheet.addRow(['Màu xe',mauXe]);
    worksheet.addRow(['Biển số xe', bienSo]);

    //Add row with current date
    let subTitleRow = worksheet.addRow(['Ngày',  moment(now).format("DD/MM/YYYY") ]);

    worksheet.addRow([]);

    let listXe = worksheet.addRow(['Danh sách người đã thuê']);
    listXe.font = { name: 'Times New Roman', size:15, bold: true, };

    worksheet.addRow([]);

    const headerRow = worksheet.addRow(['STT', 'Mã người thuê', 'Họ và tên', 'Giới tính','Địa chỉ', 'Ngày sinh', 'CMND', 'GPLX']);
    headerRow.eachCell((cell, number) => {
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: {
                argb: 'FFFFFFFF'
            },
            bgColor: {
                argb: 'FFFFFFFF'
            },
        };
        cell.font = {
            color: {
                argb: '00000000',
            },
            bold: true
        }
        cell.alignment ={ vertical: 'middle', horizontal: 'center' };
        cell.border = {
            top: {
                style: 'thin'
            },
            left: {
                style: 'thin'
            },
            bottom: {
                style: 'thin'
            },
            right: {
                style: 'thin'
            }
        };
    });


    data.forEach(d => {
        var rowValues=[];
        console.log(d);
        for(let i =0; i<8; i++)
        {
           // console.log(header[i]);
            rowValues[i]=d[header[i]];
            if(d[header[i]]==null)
            {
                rowValues[i]="";
            }

        }
        const row = worksheet.addRow(rowValues);

        row.eachCell((cell, number) => {
            cell.border = {
                top: {
                    style: 'thin'
                },
                left: {
                    style: 'thin'
                },
                bottom: {
                    style: 'thin'
                },
                right: {
                    style: 'thin'
                }
            };
        });
    });

    worksheet.getCell('A1').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.getCell('A3').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.mergeCells('A1:B1');

    worksheet.mergeCells('A9:B9');
    worksheet.eachRow((row)=>{
        row.eachCell((cell)=>{
            cell.alignment ={ vertical: 'middle', horizontal: 'left' };
        })
    });
    worksheet.getColumn(1).width = 10;
    worksheet.getColumn(2).width = 20;
    worksheet.getColumn(3).width = 15;
    worksheet.getColumn(4).width = 15;
    worksheet.getColumn(5).width = 15;
    worksheet.getColumn(6).width = 15;

    worksheet.eachCe
    workbook.xlsx.writeBuffer().then((data: any) => {
        const blob = new Blob([data], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        });
        fs.saveAs(blob, FileName + '.xlsx');
    });
  }

  async generateExcelChiTietBaoDuong(data, maXe, tongChiPhi, donVi, diaDiem, thoiGian, maBd) {

    const header = ['stt', 'madichvu', 'tendichvu', 'soluong', 'dongia', 'thanhtien', 'mabaoduong'];
    // Create workbook and worksheet
    const workbook = new ExcelJS.Workbook();
    const worksheet = workbook.addWorksheet();
    // Cell Style : Fill and Header


    let dateFormat = require('dateformat');
    let now = new Date();


    let MMDDYY = "xxx";
    var FileName = "DanhSachChiTietBaoDuong_" + maBd+ moment(now).format("DD/MM/YYYY");

    const title = 'Danh sách chi tiết bảo dưỡng ';
    // Add new row
    let titleRow = worksheet.addRow([title]);

    // Set font, size and style in title row.
    titleRow.font = { name: 'Times New Roman', size:15, bold: true, };

    worksheet.addRow([]);
    // Blank Row
    worksheet.addRow(['Mã xe ', maXe]);
    worksheet.addRow(['Tổng chi phí ', tongChiPhi]);
    worksheet.addRow(['Đơn vị thực hiện',donVi]);
    worksheet.addRow(['Địa điểm', diaDiem]);
    worksheet.addRow(['Thời gian',moment(thoiGian).format("DD/MM/YYYY") ]);
    //Add row with current date
    let subTitleRow = worksheet.addRow(['Ngày xuất', dateFormat(now, 'shortDate') ]);

    worksheet.addRow([]);

    let listXe = worksheet.addRow(['Danh sách chi tiết bảo dưỡng']);
    listXe.font = { name: 'Times New Roman', size:15, bold: true, };

    worksheet.addRow([]);

    const headerRow = worksheet.addRow(['STT', 'Mã dịch vụ', 'Tên dịch vụ', 'Số lượng','Đơn giá', 'Thành tiền', 'Mã bảo dưỡng']);
    headerRow.eachCell((cell, number) => {
        cell.fill = {
            type: 'pattern',
            pattern: 'solid',
            fgColor: {
                argb: 'FFFFFFFF'
            },
            bgColor: {
                argb: 'FFFFFFFF'
            },
        };
        cell.font = {
            color: {
                argb: '00000000',
            },
            bold: true
        }
        cell.alignment ={ vertical: 'middle', horizontal: 'center' };
        cell.border = {
            top: {
                style: 'thin'
            },
            left: {
                style: 'thin'
            },
            bottom: {
                style: 'thin'
            },
            right: {
                style: 'thin'
            }
        };
    });


    data.forEach(d => {
        var rowValues=[];
        console.log(d);
        for(let i =0; i<7; i++)
        {
           // console.log(header[i]);
            rowValues[i]=d[header[i]];
            if(d[header[i]]==null)
            {
                rowValues[i]="";
            }

        }
        const row = worksheet.addRow(rowValues);

        row.eachCell((cell, number) => {
            cell.border = {
                top: {
                    style: 'thin'
                },
                left: {
                    style: 'thin'
                },
                bottom: {
                    style: 'thin'
                },
                right: {
                    style: 'thin'
                }
            };
        });
    });

    worksheet.getCell('A1').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.getCell('A3').alignment ={ vertical: 'middle', horizontal: 'center' };
    worksheet.mergeCells('A1:B1');

    worksheet.mergeCells('A9:B9');
    worksheet.eachRow((row)=>{
        row.eachCell((cell)=>{
            cell.alignment ={ vertical: 'middle', horizontal: 'left' };
        })
    });
    worksheet.getColumn(1).width = 15;
    worksheet.getColumn(2).width = 20;
    worksheet.getColumn(3).width = 15;
    worksheet.getColumn(4).width = 15;
    worksheet.getColumn(5).width = 15;
    worksheet.getColumn(6).width = 15;
    worksheet.getColumn(6).alignment={vertical: 'middle', horizontal: 'right'}
    worksheet.getColumn(7).alignment={vertical: 'middle', horizontal: 'right'}
    worksheet.getColumn(7).width = 15;
    worksheet.getColumn(8).width = 15;
    worksheet.eachCe
    workbook.xlsx.writeBuffer().then((data: any) => {
        const blob = new Blob([data], {
            type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
        });
        fs.saveAs(blob, FileName + '.xlsx');
    });
  }


}
