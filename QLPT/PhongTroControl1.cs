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
    }
}


