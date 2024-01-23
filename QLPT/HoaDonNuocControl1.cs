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
    public partial class HoaDonNuocControl1 : UserControl
    {
        HoaDonNuocCommand hoaDonNuocCommand = new HoaDonNuocCommand();

        public HoaDonNuocControl1()
        {
            InitializeComponent();
            Load();
        }

        new void Load()
        {
            GetListHoaDonNuoc();
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
            txtMaHDNuoc.Text = "";
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            dtpNgayTinhTien.Value = DateTime.Now;
            txtChiSoNuocCu.Text = "0";
            txtChiSoNuocMoi.Text = "0";
            txtChiSoSD.Text = "0";
            txtGiaNuoc.Text = "0";
            txtTienNuoc.Text = "0";
            txtTK.Text = "";

        }

        void GetListHoaDonNuoc()
        {
            dtgvHoaDonNuoc.DataSource = hoaDonNuocCommand.GetListHoaDonNuoc();
        }

        private void dtgvHoaDonNuoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dtgvHoaDonNuoc.CurrentRow.Index;
            txtMaHDNuoc.Text = dtgvHoaDonNuoc.Rows[i].Cells[0].Value.ToString().Trim();
            txtMaPhong.Text = dtgvHoaDonNuoc.Rows[i].Cells[1].Value.ToString().Trim();
            txtTenPhong.Text = dtgvHoaDonNuoc.Rows[i].Cells[2].Value.ToString().Trim();
            dtpNgayTinhTien.Value = (DateTime)dtgvHoaDonNuoc.Rows[i].Cells[3].Value;
            txtChiSoNuocCu.Text = dtgvHoaDonNuoc.Rows[i].Cells[4].Value.ToString().Trim();
            txtChiSoNuocMoi.Text = dtgvHoaDonNuoc.Rows[i].Cells[5].Value.ToString().Trim();
            txtChiSoSD.Text = dtgvHoaDonNuoc.Rows[i].Cells[6].Value.ToString().Trim();
            txtGiaNuoc.Text = dtgvHoaDonNuoc.Rows[i].Cells[7].Value.ToString().Trim();
            txtTienNuoc.Text = dtgvHoaDonNuoc.Rows[i].Cells[8].Value.ToString().Trim();

            EnableItems();
        }

        void GetHeaderText()
        {
            dtgvHoaDonNuoc.Columns[0].HeaderText = "Mã hóa đơn";
            dtgvHoaDonNuoc.Columns[1].HeaderText = "Mã phòng";
            dtgvHoaDonNuoc.Columns[2].HeaderText = "Tên phòng";
            dtgvHoaDonNuoc.Columns[3].HeaderText = "Ngày tính tiền";
            dtgvHoaDonNuoc.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            dtgvHoaDonNuoc.Columns[4].HeaderText = "Chỉ số nước cũ";
            dtgvHoaDonNuoc.Columns[5].HeaderText = "Chỉ số nước mới";
            dtgvHoaDonNuoc.Columns[6].HeaderText = "Chỉ số nước sử dụng";
            dtgvHoaDonNuoc.Columns[7].HeaderText = "Giá nước";
            dtgvHoaDonNuoc.Columns[8].HeaderText = "Tổng nước";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            HoaDonNuoc hoaDonNuocDTO = new HoaDonNuoc();
            hoaDonNuocDTO.maHoaDonNuoc = txtMaHDNuoc.Text;
            hoaDonNuocDTO.maPhong = txtMaPhong.Text;
            hoaDonNuocDTO.tenPhong = txtTenPhong.Text;
            hoaDonNuocDTO.ngayTinhTien = dtpNgayTinhTien.Value;
            hoaDonNuocDTO.chiSoNuocCu = Convert.ToInt32(txtChiSoNuocCu.Text);
            hoaDonNuocDTO.chiSoNuocMoi = Convert.ToInt32(txtChiSoNuocMoi.Text);
            hoaDonNuocDTO.chiSoNuocSuDung = Convert.ToInt32(txtChiSoSD.Text);
            hoaDonNuocDTO.giaNuoc = Convert.ToInt32(txtGiaNuoc.Text);
            hoaDonNuocDTO.tienNuoc = Convert.ToInt32(txtTienNuoc.Text);

            if (string.IsNullOrEmpty(txtMaHDNuoc.Text))
            {
                MessageBox.Show("Mã hóa đơn không được để trống");
                return;
            }

            if (string.IsNullOrEmpty(txtMaPhong.Text))
            {
                MessageBox.Show("Mã phòng không được để trống");
                return;
            }

            if (dtpNgayTinhTien.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày tính tiền không hợp lệ");
                return;
            }

            try
            {
                if (hoaDonNuocCommand.ThemHoaDonNuoc(hoaDonNuocDTO))
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            HoaDonNuoc hoaDonNuocDTO = new HoaDonNuoc();
            hoaDonNuocDTO.maHoaDonNuoc = txtMaHDNuoc.Text;
            hoaDonNuocDTO.maPhong = txtMaPhong.Text;
            hoaDonNuocDTO.tenPhong = txtTenPhong.Text;
            hoaDonNuocDTO.ngayTinhTien = dtpNgayTinhTien.Value;
            hoaDonNuocDTO.chiSoNuocCu = Convert.ToInt32(txtChiSoNuocCu.Text);
            hoaDonNuocDTO.chiSoNuocMoi = Convert.ToInt32(txtChiSoNuocMoi.Text);
            hoaDonNuocDTO.chiSoNuocSuDung = Convert.ToInt32(txtChiSoSD.Text);
            hoaDonNuocDTO.giaNuoc = Convert.ToInt32(txtGiaNuoc.Text);
            hoaDonNuocDTO.tienNuoc = Convert.ToInt32(txtTienNuoc.Text);

            try
            {
                if (hoaDonNuocCommand.SuaHoaDonNuoc(hoaDonNuocDTO))
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
            if (hoaDonNuocCommand.XoaHoaDonNuoc(txtMaHDNuoc.Text))
            {
                MessageBox.Show("Xóa thành công");
                Load();
            }
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            dtgvHoaDonNuoc.DataSource = hoaDonNuocCommand.TimTheoMaPhong(txtTK.Text);
        }
}
