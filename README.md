# Food-recipe-app
Đồ án môn học Lập trình Windows- Đồ án nhóm gồm 3 thành viên
- #Mục tiêu 
Xây dựng phần mềm giúp lưu trữ và tra cứu công thức nấu ăn.
- #Danh sách thành viên
+ Vũ Xuân Đức
+ Nguyễn Duy Khang
+ Nguyễn Hữu Nghĩa

-#Danh sách chức năng

1. Splash Screen (0.5 điểm)
- Hiển thị thông tin chào mừng mỗi khi ứng dụng khởi chạy.
- Mỗi lần hiện ngẫu nhiên một thông tin thú vị về món ăn / loại đồ ăn.
- Cho phép chọn check “Không hiện hộp thoại này mỗi khi khởi động”. Từ nay về sau đi thẳng vào màn hình HomeScreen luôn.

2. HomeScreen (3 điểm)
- Liệt kê danh sách các món ăn được ưa thích.
- iệt kê toàn bộ danh sách món ăn có phân trang.

3. SearchScreen (2 điểm)
Tìm kiếm món ăn theo tên.

4. DetailScreen (2 điểm)
- Hiển thị chi tiết các bước nấu món ăn.
- Có danh sách hình ảnh dạng carousel, có thể xem video.

5. AddRecipe (2 điểm)
- Cho phép người dùng tự thêm một công thức nấu ăn vào hệ thống
- Tên món
- Thêm các bước làm, với mỗi bước
    + Thêm mô tả bằng text
    + Thêm hình ảnh
    + Thêm link video youtube

#Hướng dẫn lưu trữ 
- Mỗi một công thức sẽ được lưu trong lớp Recipe.
- Một công thức sẽ có nhiều bước, sẽ lưu trong mảng Step.
- Thông tin mỗi bước gồm có:
	+ Danh sách các hình ảnh
	+ Danh sách video (link youtube)
	+ Text hướng dẫn
- Trường boolean cho biết công thức có nằm trong danh sách được ưa thích hay không
