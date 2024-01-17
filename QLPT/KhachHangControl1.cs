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
    }
}
