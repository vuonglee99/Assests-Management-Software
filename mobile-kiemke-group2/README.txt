* Để chạy app trên server localhost:
   - App và server asp.net core phải cùng wifi.
   - ASP.NET: Chỉnh GSoft.AbpZeroTemplate.Web.Host/Startup/Program.cs hàm CreateWebHostBuilder	
	Chỉnh sửa/thêm 1 middleware .UseUrls("http://localhost:5000", "http://192.168.1.135:5000")
	(192.168.1.135 là ip LAN của laptop)
   - XAMARIN: Chỉnh trong QRScan/QRScan/ViewModels/Definitions.cs
	Chỉnh địa chỉ của biến Localhost là ip LAN của laptop

* Để chạy app trên server web
   - Chỉnh địa chỉ của biến Localhost là: http://k21.phanmemquanlytaisan.com/api/

* Tài khoản để login vào app:
  user: group2@gmail.com
  pass: 123qwe
  Tài khoản này thuộc Branch000000003 - Khu vực Miền Trung

  user: brhaiduong
  pass: 123qwe
  Tài khoản này thuộc Branch000000006 - Chi Nhánh Hải Dương

  user: branchhcm
  pass: 123qwe
  Tài khoản này thuộc Branch000000013 - Chi nhánh TP Hồ Chí Minh

