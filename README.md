# Mini Customer Management API

Bài thực hành Lab 01 - môn Lập trình ASP.NET Core. 
Xây dựng một hệ thống Web API quản lý thông tin khách hàng, phân loại thành viên và xử lý truy vấn dữ liệu.

## Thông tin sinh viên
- **Họ và tên:** Đậu Quang Anh
- **MSSV:** 22110014
- **Trường:** VNU-HCM University of Science

---

## Các tính năng nổi bật

Bài thực hành này không chỉ đáp ứng 100% các yêu cầu cơ bản mà còn được tích hợp các tính năng nâng cao nhằm tối ưu hóa hiệu suất xử lý dữ liệu:

- [x] **Core System:** Kiểm tra sức khỏe hệ thống (Health Check), đọc biến môi trường và cấu hình AppSettings.

- [x] **Static Files:** Phục vụ các file tĩnh qua Middleware.

- [x] **Thống kê nâng cao:** Tự động tính toán điểm trung bình hệ thống và định danh Khách hàng VIP (Top Customer).

- [x] **Endpoint truy vấn đa năng:** Tích hợp thành công **Tìm kiếm (Search)**, **Sắp xếp (Sort)** và **Phân trang (Pagination)** vào chung một Endpoint gốc.

- [x] **Tối ưu hiệu năng:** Sử dụng luồng `IQueryable` và LINQ (Deferred Execution) để trì hoãn thực thi giúp tiết kiệm bộ nhớ khi xử lý lượng dữ liệu lớn.

---

## Công nghệ & Kiến trúc
- **Framework:** .NET 8.0 / 9.0 (ASP.NET Core Web API)
- **Ngôn ngữ:** C# 12+
- **Kiến trúc:** Separation of Concerns (Tách biệt logic nghiệp vụ vào `CustomerService` và điều hướng tại `CustomersController`).
- **Công cụ test:** Swagger UI / Postman / Trình duyệt.

---

## Hướng dẫn cài đặt

1. **Clone repository về máy local:**
   ```bash
   git clone git@github-qanh-khtn:qanh-khtn/mini-customer-api.git

2. **Di chuyển vào thư mục chứa mã nguồn:**
   ```bash
   cd MiniCustomerManager.Api
   ```

3. **Khởi chạy ứng dụng:**
   ```bash
   dotnet run
   ```

4. **Trải nghiệm API:**
   Mở trình duyệt và truy cập vào giao diện Swagger tự động sinh tại:
   `http://localhost:<port>/swagger` *(Thay `<port>` bằng cổng hiển thị trên Terminal của bạn, ví dụ: 5221)*

---

## Bảng danh sách Endpoints (API Reference)

| HTTP Method | Endpoint | Mô tả chức năng |
| :--- | :--- | :--- |
| `GET` | `/health` | Kiểm tra trạng thái hoạt động của hệ thống |
| `GET` | `/env` | Xem thông tin môi trường hoạt động (Development/Production) |
| `GET` | `/config` | Đọc cấu hình thông tin từ file `appsettings.json` |
| `GET` | `/hello.txt` | Trả về file tĩnh minh chứng hoạt động của StaticFiles Middleware |
| `GET` | `/api/customers/stats` | Xem thống kê tổng quan (Tổng số, Điểm trung bình, Khách hàng VIP) |
| `GET` | `/api/customers` | Lấy danh sách khách hàng (Có phân loại trạng thái: Active, At Risk, Inactive) |

### Hướng dẫn sử dụng "Siêu Endpoint" (`GET /api/customers`)
Endpoint gốc hỗ trợ nhận các tham số `[FromQuery]` để truy vấn kết hợp:

- **Tìm kiếm:** `?search=Platinum` (Tìm theo tên hoặc hạng thành viên)
- **Sắp xếp:** `?sortBy=points&isDesc=true` (Xếp theo name, tier, points)
- **Phân trang:** `?page=1&pageSize=2` (Lấy trang 1, mỗi trang 2 người)
- **Truy vấn kết hợp:** `GET /api/customers?search=Platinum&sortBy=points&isDesc=true&page=1&pageSize=2`