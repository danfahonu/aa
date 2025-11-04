using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnLapTrinhQuanLy.Data;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormHeThongTaiKhoanKeToan : Form
    {
        private bool isAdding = false;
        private string selectedTK = null; // Lưu lại tài khoản đang được chọn

        public FormHeThongTaiKhoanKeToan()
        {
            InitializeComponent();
        }

        private void FormHeThongTaiKhoanKeToan_Load(object sender, EventArgs e)
        {
            LoadTreeView();
            SetInputMode(false);
        }

        #region Xử lý dữ liệu và TreeView

        private void LoadTreeView()
        {
            try
            {
                tvTaiKhoan.Nodes.Clear();
                string query = "SELECT SOTK, TENTK, TK_ME FROM DM_TAIKHOANKETOAN ORDER BY SOTK";
                DataTable dt = DbHelper.Query(query);

                // Thêm các node gốc (cấp 1, không có TK_ME)
                foreach (DataRow row in dt.Rows)
                {
                    if (row["TK_ME"] == DBNull.Value || string.IsNullOrEmpty(row["TK_ME"].ToString()))
                    {
                        TreeNode node = new TreeNode($"{row["SOTK"]} - {row["TENTK"]}");
                        node.Name = row["SOTK"].ToString(); // Lưu SOTK vào Name để dễ tìm
                        tvTaiKhoan.Nodes.Add(node);
                        // Gọi đệ quy để thêm các node con
                        AddChildNodes(node, dt);
                    }
                }
                tvTaiKhoan.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cây tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddChildNodes(TreeNode parentNode, DataTable dt)
        {
            string parentTK = parentNode.Name;
            foreach (DataRow row in dt.Rows)
            {
                if (row["TK_ME"].ToString() == parentTK)
                {
                    TreeNode childNode = new TreeNode($"{row["SOTK"]} - {row["TENTK"]}");
                    childNode.Name = row["SOTK"].ToString();
                    parentNode.Nodes.Add(childNode);
                    // Tiếp tục gọi đệ quy cho node con này
                    AddChildNodes(childNode, dt);
                }
            }
        }

        private void ClearInputs()
        {
            txtSoTK.Text = "";
            txtTenTK.Text = "";
            txtTKMe.Text = "";
            numCap.Value = 1;
            txtGhiChu.Text = "";
        }

        #endregion

        #region Quản lý trạng thái giao diện (UX)

        private void SetInputMode(bool enable)
        {
            txtSoTK.ReadOnly = !isAdding;
            txtTenTK.ReadOnly = !enable;
            txtTKMe.ReadOnly = !enable; // TK cha thường được xác định khi thêm, không tự sửa
            numCap.Enabled = enable;
            txtGhiChu.ReadOnly = !enable;

            btnLuu.Enabled = enable;
            btnHuy.Enabled = enable;
            btnThem.Enabled = !enable;
            btnSua.Enabled = !enable;
            btnXoa.Enabled = !enable;
        }

        #endregion

        #region Sự kiện

        private void tvTaiKhoan_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                selectedTK = e.Node.Name; // Lấy SOTK từ Name của Node
                try
                {
                    string query = "SELECT * FROM DM_TAIKHOANKETOAN WHERE SOTK = @SoTK";
                    DataTable dt = DbHelper.Query(query, DbHelper.Param("@SoTK", selectedTK));
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtSoTK.Text = row["SOTK"].ToString();
                        txtTenTK.Text = row["TENTK"].ToString();
                        txtTKMe.Text = row["TK_ME"].ToString();
                        numCap.Value = Convert.ToDecimal(row["CAP"]);
                        txtGhiChu.Text = row["GHICHU"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hiển thị chi tiết tài khoản: " + ex.Message);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearInputs();
            // Gợi ý TK cha là TK đang chọn
            if (selectedTK != null)
            {
                txtTKMe.Text = selectedTK;
                numCap.Value = txtTKMe.Text.Length + 1; // Gợi ý cấp
            }
            SetInputMode(true);
            txtSoTK.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedTK))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetInputMode(true);
            txtTenTK.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedTK))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM DM_TAIKHOANKETOAN WHERE SOTK = @SoTK";
                    DbHelper.Execute(query, DbHelper.Param("@SoTK", selectedTK));

                    LoadTreeView();
                    ClearInputs();
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSoTK.Text) || string.IsNullOrWhiteSpace(txtTenTK.Text))
                {
                    MessageBox.Show("Số và Tên tài khoản không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isAdding)
                {
                    string query = @"
                        INSERT INTO DM_TAIKHOANKETOAN (SOTK, TENTK, CAP, TK_ME, GHICHU)
                        VALUES (@SoTK, @TenTK, @Cap, @TkMe, @GhiChu)";
                    DbHelper.Execute(query,
                        DbHelper.Param("@SoTK", txtSoTK.Text),
                        DbHelper.Param("@TenTK", txtTenTK.Text),
                        DbHelper.Param("@Cap", numCap.Value),
                        DbHelper.Param("@TkMe", string.IsNullOrEmpty(txtTKMe.Text) ? (object)DBNull.Value : txtTKMe.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text)
                    );
                }
                else
                {
                    string query = @"
                        UPDATE DM_TAIKHOANKETOAN SET
                            TENTK = @TenTK, CAP = @Cap, TK_ME = @TkMe, GHICHU = @GhiChu
                        WHERE SOTK = @SoTK";
                    DbHelper.Execute(query,
                        DbHelper.Param("@TenTK", txtTenTK.Text),
                        DbHelper.Param("@Cap", numCap.Value),
                        DbHelper.Param("@TkMe", string.IsNullOrEmpty(txtTKMe.Text) ? (object)DBNull.Value : txtTKMe.Text),
                        DbHelper.Param("@GhiChu", txtGhiChu.Text),
                        DbHelper.Param("@SoTK", txtSoTK.Text)
                    );
                }
                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTreeView();
                SetInputMode(false);
                isAdding = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!isAdding && tvTaiKhoan.SelectedNode != null)
            {
                tvTaiKhoan_AfterSelect(null, new TreeViewEventArgs(tvTaiKhoan.SelectedNode));
            }
            else
            {
                ClearInputs();
            }
            SetInputMode(false);
            isAdding = false;
        }

        #endregion
    }
}