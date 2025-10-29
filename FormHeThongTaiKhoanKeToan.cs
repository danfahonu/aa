using DoAnLapTrinhQuanLy.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DoAnLapTrinhQuanLy.GuiLayer
{
    public partial class FormHeThongTaiKhoanKeToan : Form
    {
        private enum FormMode { View, Edit, New }
        private FormMode _currentMode = FormMode.View;
        private DataTable _dt;

        public FormHeThongTaiKhoanKeToan()
        {
            InitializeComponent();
        }

        private void FormHeThongTaiKhoanKeToan_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataAndBuildTree();
                LoadTKMeComboBox();
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadDataAndBuildTree()
        {
            _dt = DbHelper.Query("SELECT SOTK, TENTK, CAP, TK_ME, GHICHU FROM dbo.DM_TAIKHOANKETOAN");

            treeViewTK.Nodes.Clear();
            var topLevelNodes = _dt.AsEnumerable().Where(row => row.IsNull("TK_ME"));

            foreach (DataRow dr in topLevelNodes)
            {
                TreeNode node = new TreeNode($"{dr["SOTK"]} - {dr["TENTK"]}");
                node.Tag = dr["SOTK"].ToString();
                treeViewTK.Nodes.Add(node);
                AddChildNodes(node, dr["SOTK"].ToString());
            }
            treeViewTK.ExpandAll();
        }

        private void AddChildNodes(TreeNode parentNode, string parentId)
        {
            var childRows = _dt.AsEnumerable().Where(row => !row.IsNull("TK_ME") && row["TK_ME"].ToString() == parentId);
            foreach (DataRow dr in childRows)
            {
                TreeNode node = new TreeNode($"{dr["SOTK"]} - {dr["TENTK"]}");
                node.Tag = dr["SOTK"].ToString();
                parentNode.Nodes.Add(node);
                AddChildNodes(node, dr["SOTK"].ToString());
            }
        }

        private void LoadTKMeComboBox()
        {
            DataTable dtTKMe = _dt.Copy();
            DataRow emptyRow = dtTKMe.NewRow();
            emptyRow["SOTK"] = DBNull.Value;
            emptyRow["TENTK"] = "-- Không có --";
            dtTKMe.Rows.InsertAt(emptyRow, 0);
            cboTKMe.DataSource = dtTKMe;
            cboTKMe.ValueMember = "SOTK";
            cboTKMe.DisplayMember = "TENTK";
        }

        // *** HÀM MỚI ĐỂ GỠ BỎ BINDING ***
        private void ClearDataBindings()
        {
            foreach (Control c in grpChiTiet.Controls)
            {
                c.DataBindings.Clear();
            }
        }

        // *** HÀM MỚI ĐỂ GÁN LẠI BINDING ***
        private void DataBindingControl()
        {
            ClearDataBindings(); // Luôn xóa binding cũ trước khi gán mới
            if (treeViewTK.SelectedNode?.Tag != null)
            {
                string soTK = treeViewTK.SelectedNode.Tag.ToString();
                DataRow dr = _dt.AsEnumerable().FirstOrDefault(row => row["SOTK"].ToString() == soTK);
                if (dr != null)
                {
                    txtSoTK.Text = dr["SOTK"].ToString();
                    txtTenTK.Text = dr["TENTK"].ToString();
                    numCap.Value = Convert.ToDecimal(dr["CAP"]);
                    cboTKMe.SelectedValue = dr["TK_ME"] ?? DBNull.Value;
                    txtGhiChu.Text = dr["GHICHU"].ToString();
                }
            }
        }


        private void SetFormMode(FormMode mode)
        {
            _currentMode = mode;
            bool isEditing = (mode != FormMode.View);

            txtTenTK.ReadOnly = !isEditing;
            numCap.Enabled = isEditing;
            cboTKMe.Enabled = isEditing;
            txtGhiChu.ReadOnly = !isEditing;
            txtSoTK.ReadOnly = (mode != FormMode.New);

            btnMoi.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && treeViewTK.SelectedNode != null;
            btnLuu.Enabled = isEditing;
            btnXoa.Enabled = !isEditing && treeViewTK.SelectedNode != null;
            treeViewTK.Enabled = !isEditing;

            if (mode == FormMode.New)
            {
                ClearInputControls();
                txtSoTK.Focus();
            }
        }

        private void ClearInputControls()
        {
            txtSoTK.Text = "";
            txtTenTK.Text = "";
            numCap.Value = 1;
            cboTKMe.SelectedValue = DBNull.Value;
            txtGhiChu.Text = "";
        }

        private void treeViewTK_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            if (_currentMode != FormMode.View) SetFormMode(FormMode.View);

            // Thay vì binding, chúng ta gán dữ liệu trực tiếp để kiểm soát tốt hơn
            DataBindingControl();
        }

        private void btnMoi_Click(object sender, EventArgs e) => SetFormMode(FormMode.New);
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (treeViewTK.SelectedNode != null) SetFormMode(FormMode.Edit);
        }
        private void btnNap_Click(object sender, EventArgs e)
        {
            LoadDataAndBuildTree();
            LoadTKMeComboBox();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoTK.Text) || string.IsNullOrWhiteSpace(txtTenTK.Text))
            {
                MessageBox.Show("Số và Tên tài khoản không được để trống.", "Cảnh báo");
                return;
            }

            try
            {
                var p = new[] {
                    DbHelper.Param("@soTK", txtSoTK.Text.Trim()),
                    DbHelper.Param("@tenTK", txtTenTK.Text.Trim()),
                    DbHelper.Param("@cap", Convert.ToInt16(numCap.Value)),
                    DbHelper.Param("@tkMe", cboTKMe.SelectedValue ?? (object)DBNull.Value),
                    DbHelper.Param("@ghiChu", txtGhiChu.Text.Trim())
                };

                if (_currentMode == FormMode.New)
                {
                    string sql = "INSERT INTO dbo.DM_TAIKHOANKETOAN (SOTK, TENTK, CAP, TK_ME, GHICHU) VALUES (@soTK, @tenTK, @cap, @tkMe, @ghiChu)";
                    DbHelper.Execute(sql, p);
                }
                else // Edit
                {
                    string sql = "UPDATE dbo.DM_TAIKHOANKETOAN SET TENTK=@tenTK, CAP=@cap, TK_ME=@tkMe, GHICHU=@ghiChu WHERE SOTK=@soTK";
                    DbHelper.Execute(sql, p);
                }

                LoadDataAndBuildTree();
                LoadTKMeComboBox();
                SetFormMode(FormMode.View);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Số tài khoản này đã tồn tại.", "Lỗi");
                else
                    MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (treeViewTK.SelectedNode != null && MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DbHelper.Execute("DELETE FROM dbo.DM_TAIKHOANKETOAN WHERE SOTK=@soTK", DbHelper.Param("@soTK", txtSoTK.Text));
                    LoadDataAndBuildTree();
                    LoadTKMeComboBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa tài khoản này. Có thể do đã có phát sinh giao dịch hoặc tài khoản con.\n\nChi tiết: " + ex.Message, "Lỗi ràng buộc dữ liệu");
                }
            }
        }
    }
}