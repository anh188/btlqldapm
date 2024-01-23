using QLPT.DTO;
using QLPT.SQLCommand;
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
    public partial class HoaDonDienControl1 : UserControl
    {
        HoaDonDienCommand hoaDonDienCommand = new HoaDonDienCommand();

        public HoaDonDienControl1()
        {
            InitializeComponent();
            Load();
        }

        new void Load()
        {
            GetListHoaDonDien();
            GetHeaderText();
            DisableItems();
            ClearItems();
        }

        void DisableItems()
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        void EnableItems()
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        void ClearItems()
        {
            txtMaHDDien.Text = "";
            cbbMaPhong.Text = "";
            txtTenPhong.Text = "";
            dtpNgayTinhTien.Value = DateTime.Now;
            txtSoDienCu.Text = "0";
            txtSoDienMoi.Text = "0";
            txtSoSD.Text = "0";
            txtGiaDien.Text = "0";
            txtTienDien.Text = "0";
            txtTimKiem.Text = "";

        }

        void GetListHoaDonDien()
        {
            dtgvHoaDonDien.DataSource = hoaDonDienCommand.GetListHoaDonDien();
        }

        void GetHeaderText()
        {
            dtgvHoaDonDien.Columns[0].HeaderText = "Mã hóa đơn";
            dtgvHoaDonDien.Columns[1].HeaderText = "Mã phòng";
            dtgvHoaDonDien.Columns[2].HeaderText = "Tên phòng";
            dtgvHoaDonDien.Columns[3].HeaderText = "Ngày tính tiền";
            dtgvHoaDonDien.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            dtgvHoaDonDien.Columns[4].HeaderText = "Chỉ số điện cũ";
            dtgvHoaDonDien.Columns[5].HeaderText = "Chỉ số điện mới";
            dtgvHoaDonDien.Columns[6].HeaderText = "Chỉ số điện sử dụng";
            dtgvHoaDonDien.Columns[7].HeaderText = "Giá điện";
            dtgvHoaDonDien.Columns[8].HeaderText = "Tổng tiền";

        }

        private void dtgvHoaDonDien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dtgvHoaDonDien.CurrentRow.Index;
            txtMaHDDien.Text = dtgvHoaDonDien.Rows[i].Cells[0].Value.ToString().Trim();
            cbbMaPhong.Text = dtgvHoaDonDien.Rows[i].Cells[1].Value.ToString().Trim();
            txtTenPhong.Text = dtgvHoaDonDien.Rows[i].Cells[2].Value.ToString().Trim();
            dtpNgayTinhTien.Value = (DateTime)dtgvHoaDonDien.Rows[i].Cells[3].Value;
            txtSoDienCu.Text = dtgvHoaDonDien.Rows[i].Cells[4].Value.ToString().Trim();
            txtSoDienMoi.Text = dtgvHoaDonDien.Rows[i].Cells[5].Value.ToString().Trim();
            txtSoSD.Text = dtgvHoaDonDien.Rows[i].Cells[6].Value.ToString().Trim();
            txtGiaDien.Text = dtgvHoaDonDien.Rows[i].Cells[7].Value.ToString().Trim();
            txtTienDien.Text = dtgvHoaDonDien.Rows[i].Cells[8].Value.ToString().Trim();

            EnableItems();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            HoaDonDienDTO hoaDonDienDTO = new HoaDonDienDTO();
            hoaDonDienDTO.maHoaDonDien = txtMaHDDien.Text;
            hoaDonDienDTO.maPhong = cbbMaPhong.Text;
            hoaDonDienDTO.tenPhong = txtTenPhong.Text;
            hoaDonDienDTO.ngayTinhTien = dtpNgayTinhTien.Value;
            hoaDonDienDTO.chiSoDienCu = Convert.ToInt32(txtSoDienCu.Text);
            hoaDonDienDTO.chiSoDienMoi = Convert.ToInt32(txtSoDienMoi.Text);
            hoaDonDienDTO.chiSoDienSuDung = Convert.ToInt32(txtSoSD.Text);
            hoaDonDienDTO.giaDien = Convert.ToInt32(txtGiaDien.Text);
            hoaDonDienDTO.tienDien = Convert.ToInt32(txtTienDien.Text);

            if (string.IsNullOrEmpty(txtMaHDDien.Text))
            {
                MessageBox.Show("Mã hóa đơn không được để trống");
                return;
            }

            if (string.IsNullOrEmpty(cbbMaPhong.Text))
            {
                MessageBox.Show("Mã phòng không được để trống");
                return;
            }

            if (dtpNgayTinhTien.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày tính tiền không hợp lệ");
                return;
            }

            if (string.IsNullOrEmpty(txtSoDienCu.Text))
            {
                MessageBox.Show("Số điện cũ không hợp lệ");
                return;
            }

            if (string.IsNullOrEmpty(txtSoDienMoi.Text))
            {
                MessageBox.Show("Số điện mới không hợp lệ");
                return;
            }

            if (txtSoSD.Text == "")
            {
                MessageBox.Show("Số điện sử dụng không hợp lệ");
                return;
            }

            try
            {
                if (hoaDonDienCommand.ThemHoaDonDien(hoaDonDienDTO))
                {
                    MessageBox.Show("Thêm hóa đơn thành công");
                    Load();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Mã hóa đơn đã tồn tại");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            HoaDonDienDTO hoaDonDienDTO = new HoaDonDienDTO();
            hoaDonDienDTO.maHoaDonDien = txtMaHDDien.Text;
            hoaDonDienDTO.maPhong = cbbMaPhong.Text;
            hoaDonDienDTO.tenPhong = txtTenPhong.Text;
            hoaDonDienDTO.ngayTinhTien = dtpNgayTinhTien.Value;
            hoaDonDienDTO.chiSoDienCu = Convert.ToInt32(txtSoDienCu.Text);
            hoaDonDienDTO.chiSoDienMoi = Convert.ToInt32(txtSoDienMoi.Text);
            hoaDonDienDTO.chiSoDienSuDung = Convert.ToInt32(txtSoSD.Text);
            hoaDonDienDTO.giaDien = Convert.ToInt32(txtGiaDien.Text);
            hoaDonDienDTO.tienDien = Convert.ToInt32(txtTienDien.Text);

            try
            {
                if (hoaDonDienCommand.SuaHoaDonDien(hoaDonDienDTO))
                {
                    MessageBox.Show("Sửa hóa đơn thành công");
                    Load();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (hoaDonDienCommand.XoaHoaDonDien(txtMaHDDien.Text))
            {
                MessageBox.Show("Xóa hóa đơn thành công");
                Load();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dtgvHoaDonDien.DataSource = hoaDonDienCommand.TimTheoMaPhong(txtTimKiem.Text);
        }
    }
}
