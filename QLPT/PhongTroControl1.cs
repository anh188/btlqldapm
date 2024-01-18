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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QLPT
{
    public partial class PhongTroControl1 : UserControl
    {
        public PhongTroControl1()
        {
            InitializeComponent();
            dgvDanhSachPhong_Load();
            DisablePhongTroControls();
        }
        private void DisablePhongTroControls()
        {
            // Vô hiệu hóa các ô textbox
            txtMaPhong.Enabled = false;
            txtTenPhong.Enabled = false;
            txtGiaPhong.Enabled = false;
            cbbTrangThai.Enabled = false;

            // Vô hiệu hóa các nút
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void dgvDanhSachPhong_Load()
        {
            string sql = "select* from Phong";
            DataTable dt = new DataTable();
            dt = DataProvider.Instance.ExecuteQuery(sql);
            dgvDanhSachPhong.DataSource = dt;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Enable các ô textbox để người dùng nhập liệu
            txtMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;
            txtGiaPhong.Enabled = true;
            cbbTrangThai.Enabled = true;

            // Enable các nút cần thiết
            btnLuu.Enabled = true;
            btnSua.Enabled = false; // Nếu đang trong chế độ thêm, thì không cho phép sửa
            btnXoa.Enabled = false; // Nếu đang trong chế độ thêm, thì không cho phép xóa

            // Xóa nội dung của các ô textbox
            Reset_PhongTro_data();
        }

        private void TimKiemTheoMaPhong(string maPhong)
        {
            string sql = "SELECT * FROM Phong WHERE MaPhong LIKE '%' + @maPhong + '%'";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new object[] { maPhong });

            // Luôn cập nhật DataSource, dù có dữ liệu hay không
            dgvDanhSachPhong.DataSource = dt;
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ô tìm kiếm
            string maPhongCanTim = txtTimKiem.Text.Trim();

            // Kiểm tra xem ô tìm kiếm có rỗng không
            if (string.IsNullOrEmpty(maPhongCanTim))
            {
                MessageBox.Show("Vui lòng nhập mã phòng cần tìm kiếm!");
                return;
            }

            // Thực hiện tìm kiếm
            TimKiemTheoMaPhong(maPhongCanTim);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ ô tìm kiếm
            string currentSearchText = txtTimKiem.Text.Trim();

            // Kiểm tra xem ô tìm kiếm có rỗng không
            if (string.IsNullOrEmpty(currentSearchText))
            {
                // Nếu rỗng, load lại toàn bộ dữ liệu
                dgvDanhSachPhong_Load();
            }
            else
            {
                // Nếu không rỗng, thực hiện tìm kiếm
                TimKiemTheoMaPhong(currentSearchText);
            }
        }
     

        private void dgvDanhSachPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvDanhSachPhong.CurrentRow.Index;
            txtMaPhong.Text = dgvDanhSachPhong.Rows[i].Cells[0].Value.ToString().Trim();
            txtTenPhong.Text = dgvDanhSachPhong.Rows[i].Cells[1].Value.ToString().Trim();
            txtGiaPhong.Text = dgvDanhSachPhong.Rows[i].Cells[2].Value.ToString().Trim();
            cbbTrangThai.Text = dgvDanhSachPhong.Rows[i].Cells[3].Value.ToString().Trim();
            btnLuu.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Enable các ô textbox để người dùng có thể sửa đổi
            txtMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;
            txtGiaPhong.Enabled = true;
            cbbTrangThai.Enabled = true;

            // Enable nút "Lưu" để lưu lại thông tin sau khi sửa đổi
            btnLuu.Enabled = true;

            // Disable nút "Sửa" và "Xóa" trong khi đang trong chế độ sửa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void dgvDanhSachPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvDanhSachPhong.CurrentRow.Index;
            txtMaPhong.Text = dgvDanhSachPhong.Rows[i].Cells[0].Value.ToString().Trim();
            txtTenPhong.Text = dgvDanhSachPhong.Rows[i].Cells[1].Value.ToString().Trim();
            txtGiaPhong.Text = dgvDanhSachPhong.Rows[i].Cells[2].Value.ToString().Trim();
            cbbTrangThai.Text = dgvDanhSachPhong.Rows[i].Cells[3].Value.ToString().Trim();
            btnLuu.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void Reset_PhongTro_data()
        {
            txtMaPhong.Text = txtTenPhong.Text = txtGiaPhong.Text = cbbTrangThai.Text = txtTimKiem.Text = "";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Reset_PhongTro_data();
            DisablePhongTroControls();
        }
        private void XoaPhongTro(string maPhong)
        {
            try
            {
                // Thực hiện câu truy vấn DELETE
                string sql = "DELETE FROM Phong WHERE MaPhong = @maPhong";
                object[] parameters = { maPhong };
                int rowsAffected = DataProvider.Instance.ExecuteNonQuery(sql, parameters);

                if (rowsAffected > 0)
                {
                    // Hiển thị thông báo khi xóa thành công
                    MessageBox.Show("Đã xóa phòng thành công!");

                    // Load lại danh sách phòng sau khi xóa
                    dgvDanhSachPhong_Load();

                    // Reset các ô textbox và combobox về trạng thái ban đầu
                    Reset_PhongTro_data();
                }
                else
                {
                    MessageBox.Show("Không thể xóa phòng. Vui lòng kiểm tra lại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Lấy mã phòng cần xóa
            string maPhongCanXoa = txtMaPhong.Text.Trim();

            // Kiểm tra xem mã phòng có rỗng không
            if (string.IsNullOrEmpty(maPhongCanXoa))
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!");
                return;
            }

            // Hiển thị hộp thoại xác nhận xóa
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                // Nếu người dùng đồng ý, thực hiện xóa
                XoaPhongTro(maPhongCanXoa);
            }
            else
            {
                // Người dùng không đồng ý, không thực hiện xóa
            }
        }
    }
}


