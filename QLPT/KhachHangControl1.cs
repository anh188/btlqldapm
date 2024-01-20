using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLPT
{
    public partial class KhachHangControl1 : UserControl
    {
        public KhachHangControl1()
        {
            InitializeComponent();
            dgvDanhSachKH_Load();
            DisableKhachHangControls();
        }
        private void DisableKhachHangControls()
        {
            // Vô hiệu hóa các ô textbox
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            cbbGioiTinh.Enabled = false;
            txtSoDienThoai.Enabled = false;
            txtCanCuoc.Enabled = false;
            txtDiaChi.Enabled = false;
            cbbMaPhong.Enabled = false;
            txtTenPhong.Enabled = false;

            // Vô hiệu hóa các nút
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void dgvDanhSachKH_Load()
        {
            string sql = "select* from KhachHang";
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery(sql);
            dgvDanhSachKH.DataSource = dt;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void TimKiemTheoMaKH(string maPhong)
        {
            string sql = "SELECT * FROM KhachHang WHERE MaKH LIKE '%' + @maKH + '%'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new object[] { maPhong });

            // Luôn cập nhật DataSource, dù có dữ liệu hay không
            dgvDanhSachKH.DataSource = dt;
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ô tìm kiếm
            string maKHCanTim = txtTimKiem.Text.Trim();

            // Kiểm tra xem ô tìm kiếm có rỗng không
            if (string.IsNullOrEmpty(maKHCanTim))
            {
                MessageBox.Show("Vui lòng nhập mã phòng cần tìm kiếm!");
                return;
            }

            // Thực hiện tìm kiếm
            TimKiemTheoMaKH(maKHCanTim);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ô tìm kiếm
            string currentSearchText = txtTimKiem.Text.Trim();

            // Kiểm tra xem ô tìm kiếm có rỗng không
            if (string.IsNullOrEmpty(currentSearchText))
            {
                // Nếu rỗng, load lại toàn bộ dữ liệu
                dgvDanhSachKH_Load();
            }
            else
            {
                // Nếu không rỗng, thực hiện tìm kiếm
                TimKiemTheoMaKH(currentSearchText);
            }
        }

        private void Reset_KhachHang_data()
        {
            txtMaKH.Text = txtTenKH.Text = cbbGioiTinh.Text = txtSoDienThoai.Text = txtCanCuoc.Text = txtDiaChi.Text = cbbMaPhong.Text = txtTenPhong.Text = "";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Reset_KhachHang_data();
            DisableKhachHangControls();

        }
        private void XoaKhachHang(string maKH)
        {
            try
            {
                // Thực hiện câu truy vấn DELETE
                string sql = "DELETE FROM KhachHang WHERE MaKH = @maKH";
                object[] parameters = { maKH };
                int rowsAffected = DataProvider.Instance.ExecuteNonQuery(sql, parameters);

                if (rowsAffected > 0)
                {
                    // Hiển thị thông báo khi xóa thành công
                    MessageBox.Show("Đã xóa khách hàng thành công!");

                    // Load lại danh sách khách hàng sau khi xóa
                    dgvDanhSachKH_Load();

                    // Reset các ô textbox và combobox về trạng thái ban đầu
                    Reset_KhachHang_data();
                }
                else
                {
                    MessageBox.Show("Không thể xóa khách hàng. Vui lòng kiểm tra lại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Lấy mã khách hàng cần xóa
            string maKHCanXoa = txtMaKH.Text.Trim();

            // Kiểm tra xem mã khách hàng có rỗng không
            if (string.IsNullOrEmpty(maKHCanXoa))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            // Hiển thị hộp thoại xác nhận xóa
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                // Nếu người dùng đồng ý, thực hiện xóa
                XoaKhachHang(maKHCanXoa);
            }
            else
            {
                // Người dùng không đồng ý, không thực hiện xóa
            }
        }
        int ChucNang = 0;
        private void btnThem_Click(object sender, EventArgs e)
        {
            ChucNang = 1;
            // Enable các ô textbox để người dùng nhập liệu
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            cbbGioiTinh.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtCanCuoc.Enabled = true;
            txtDiaChi.Enabled = true;
            cbbMaPhong.Enabled = true;
            txtTenPhong.Enabled = false;

            // Enable các nút cần thiết
            btnLuu.Enabled = true;
            btnSua.Enabled = false; // Nếu đang trong chế độ thêm, thì không cho phép sửa
            btnXoa.Enabled = false; // Nếu đang trong chế độ thêm, thì không cho phép xóa

            // Xóa nội dung của các ô textbox
            Reset_KhachHang_data();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ChucNang = 2;
            // Enable các ô textbox để người dùng có thể sửa đổi
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            cbbGioiTinh.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtCanCuoc.Enabled = true;
            txtDiaChi.Enabled = true;
            cbbMaPhong.Enabled = true;
            txtTenPhong.Enabled = false;

            // Enable nút "Lưu" để lưu lại thông tin sau khi sửa đổi
            btnLuu.Enabled = true;

            // Disable nút "Sửa" và "Xóa" trong khi đang trong chế độ sửa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void KhachHangControl1_Load(object sender, EventArgs e)
        {
            dgvDanhSachKH_Load();

            // Load dữ liệu cho ComboBox GioiTinh
            LoadDataForComboBoxGioiTinh();

            // Load dữ liệu cho ComboBox MaPhong
            LoadDataForComboBoxMaPhong();

            // Thêm sự kiện SelectedIndexChanged cho cbbMaPhong
            cbbMaPhong.SelectedIndexChanged += new EventHandler(cbbMaPhong_SelectedIndexChanged);

            DisableKhachHangControls();
        }
        private void LoadDataForComboBoxGioiTinh()
        {
            // Tạo dữ liệu mẫu cho ComboBox GioiTinh
            List<string> gioiTinhList = new List<string> { "Nam", "Nữ" };

            // Đặt dữ liệu cho ComboBox GioiTinh
            cbbGioiTinh.DataSource = gioiTinhList;
        }

        private void LoadDataForComboBoxMaPhong()
        {
            // Lấy dữ liệu từ SQL cho ComboBox MaPhong
            string sql = "SELECT MaPhong FROM Phong";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql);

            // Đặt dữ liệu cho ComboBox MaPhong
            cbbMaPhong.DataSource = dt;
            cbbMaPhong.DisplayMember = "MaPhong";
            cbbMaPhong.ValueMember = "MaPhong";
        }

        private void dgvDanhSachKH_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvDanhSachKH.CurrentRow.Index;
            txtMaKH.Text = dgvDanhSachKH.Rows[i].Cells[0].Value.ToString().Trim();
            txtTenKH.Text = dgvDanhSachKH.Rows[i].Cells[1].Value.ToString().Trim();
            cbbGioiTinh.Text = dgvDanhSachKH.Rows[i].Cells[2].Value.ToString().Trim();
            txtSoDienThoai.Text = dgvDanhSachKH.Rows[i].Cells[3].Value.ToString().Trim();
            txtCanCuoc.Text = dgvDanhSachKH.Rows[i].Cells[4].Value.ToString().Trim();
            txtDiaChi.Text = dgvDanhSachKH.Rows[i].Cells[5].Value.ToString().Trim();
            cbbMaPhong.Text = dgvDanhSachKH.Rows[i].Cells[6].Value.ToString().Trim();
            txtTenPhong.Text = dgvDanhSachKH.Rows[i].Cells[7].Value.ToString().Trim();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void dgvDanhSachKH_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvDanhSachKH.CurrentRow.Index;
            txtMaKH.Text = dgvDanhSachKH.Rows[i].Cells[0].Value.ToString().Trim();
            txtTenKH.Text = dgvDanhSachKH.Rows[i].Cells[1].Value.ToString().Trim();
            cbbGioiTinh.Text = dgvDanhSachKH.Rows[i].Cells[2].Value.ToString().Trim();
            txtSoDienThoai.Text = dgvDanhSachKH.Rows[i].Cells[3].Value.ToString().Trim();
            txtCanCuoc.Text = dgvDanhSachKH.Rows[i].Cells[4].Value.ToString().Trim();
            txtDiaChi.Text = dgvDanhSachKH.Rows[i].Cells[5].Value.ToString().Trim();
            cbbMaPhong.Text = dgvDanhSachKH.Rows[i].Cells[6].Value.ToString().Trim();
            txtTenPhong.Text = dgvDanhSachKH.Rows[i].Cells[7].Value.ToString().Trim();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void cbbMaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có giá trị được chọn không
            if (cbbMaPhong.SelectedItem != null)
            {
                string maPhong = cbbMaPhong.SelectedValue.ToString();
                LayTenPhongTuMaPhong(maPhong);
            }
        }
        private void LayTenPhongTuMaPhong(string maPhong)
        {
            // Thực hiện truy vấn SQL để lấy tên phòng từ mã phòng
            string sql = "SELECT TenPhong FROM Phong WHERE MaPhong = @maPhong";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new object[] { maPhong });

            // Kiểm tra xem có dữ liệu trả về không
            if (dt.Rows.Count > 0)
            {
                // Lấy giá trị tên phòng từ DataTable và hiển thị nó trong TextBox
                string tenPhong = dt.Rows[0]["TenPhong"].ToString();
                txtTenPhong.Text = tenPhong;
            }
            else
            {
                // Nếu không có dữ liệu trả về, có thể hiển thị một giá trị mặc định hoặc xóa nội dung TextBox
                // Ví dụ: txtTenPhong.Text = "Không tìm thấy tên phòng";
                txtTenPhong.Text = "";
            }
        }
        private bool IsValidDataKhachHang()
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) ||
                string.IsNullOrWhiteSpace(txtTenKH.Text) ||
                string.IsNullOrWhiteSpace(cbbGioiTinh.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                string.IsNullOrWhiteSpace(txtCanCuoc.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(cbbMaPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            // Kiểm tra tính duy nhất của Mã Khách Hàng khi thêm mới
            if (ChucNang == 1 && !IsMaKhachHangUnique(txtMaKH.Text.Trim()))
            {
                MessageBox.Show("Mã khách hàng đã tồn tại. Vui lòng chọn mã khác!");
                return false;
            }

            return true;
        }
        private bool IsMaKhachHangUnique(string maKH)
        {
            string sqlCheckDuplicate = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @maKH";
            int countDuplicate = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sqlCheckDuplicate, new object[] { maKH }));
            return countDuplicate == 0;
        }
        private void CapNhatTenPhong()
        {
            // Kiểm tra xem có giá trị được chọn không
            if (cbbMaPhong.SelectedItem != null)
            {
                string maPhong = cbbMaPhong.SelectedValue.ToString();
                LayTenPhongTuMaPhong(maPhong);
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!IsValidDataKhachHang())
            {
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-HR0MVSB\\SQLEXPRESS;Initial Catalog=quanlynhatro;Integrated Security=True";

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();

                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.CommandType = CommandType.Text;

                        if (ChucNang == 1) // Thêm
                        {
                            sqlCmd.CommandText = "INSERT INTO KhachHang (MaKH, TenKH, GioiTinh, SoDienThoai, CanCuoc, DiaChi, MaPhong) VALUES (@maKH, @tenKH, @gioiTinh, @soDienThoai, @canCuoc, @diaChi, @maPhong)";
                        }
                        else if (ChucNang == 2) // Sửa
                        {
                            sqlCmd.CommandText = "UPDATE KhachHang SET TenKH=@tenKH, GioiTinh=@gioiTinh, SoDienThoai=@soDienThoai, CanCuoc=@canCuoc, DiaChi=@diaChi, MaPhong=@maPhong WHERE MaKH=@maKH";
                        }

                        sqlCmd.Parameters.AddWithValue("@maKH", txtMaKH.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@tenKH", txtTenKH.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@gioiTinh", cbbGioiTinh.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@soDienThoai", txtSoDienThoai.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@canCuoc", txtCanCuoc.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@diaChi", txtDiaChi.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@maPhong", cbbMaPhong.SelectedValue.ToString().Trim());
                        sqlCmd.Parameters.AddWithValue("@tenPhong", txtTenPhong.Text.Trim());

                        sqlCmd.Connection = sqlCon;

                        int kq = sqlCmd.ExecuteNonQuery();

                        if (kq > 0)
                        {
                            if (ChucNang == 1)
                            {
                                MessageBox.Show("Thêm khách hàng thành công!");
                            }
                            else if (ChucNang == 2)
                            {
                                MessageBox.Show("Cập nhật khách hàng thành công!");
                            }

                            dgvDanhSachKH_Load();
                            Reset_KhachHang_data();
                            DisableKhachHangControls();
                            // Sau khi thêm hoặc cập nhật, cập nhật tên phòng
                            LayTenPhongTuMaPhong(cbbMaPhong.SelectedValue.ToString());
                        }
                        else
                        {
                            if (ChucNang == 1)
                            {
                                MessageBox.Show("Thêm khách hàng thất bại!");
                            }
                            else if (ChucNang == 2)
                            {
                                MessageBox.Show("Cập nhật khách hàng thất bại!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
