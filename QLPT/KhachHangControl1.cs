using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void dgvDanhSachKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void dgvDanhSachKH_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Enable các ô textbox để người dùng nhập liệu
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            cbbGioiTinh.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtCanCuoc.Enabled = true;
            txtDiaChi.Enabled = true;
            cbbMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;

            // Enable các nút cần thiết
            btnLuu.Enabled = true;
            btnSua.Enabled = false; // Nếu đang trong chế độ thêm, thì không cho phép sửa
            btnXoa.Enabled = false; // Nếu đang trong chế độ thêm, thì không cho phép xóa

            // Xóa nội dung của các ô textbox
            Reset_KhachHang_data();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Enable các ô textbox để người dùng có thể sửa đổi
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            cbbGioiTinh.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtCanCuoc.Enabled = true;
            txtDiaChi.Enabled = true;
            cbbMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;

            // Enable nút "Lưu" để lưu lại thông tin sau khi sửa đổi
            btnLuu.Enabled = true;

            // Disable nút "Sửa" và "Xóa" trong khi đang trong chế độ sửa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
    }
}
