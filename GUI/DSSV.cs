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
using WF_BT2.BLL;
using WF_BT2.DTO;
using WF_BT2.GUI;

namespace WF_BT2
{
    public partial class DSSV : Form
    {
        public DSSV()
        {
            InitializeComponent();
            Show();
            SetCBBShow();
            

        }
        private void Show(int ID_Lop, string ms)
        {
            BLL_QLSV bll = new BLL_QLSV();
            dtgSV.DataSource = bll.GetListSV_BLL(ID_Lop,ms);
            //bll.LoadCBB(cbbLSH);
        }
        public void SetCBBShow()
        {
            BLL_QLSV bll = new BLL_QLSV();
            if (cbbLSH.Items!=null)
            {
                cbbLSH.Items.Clear();
            }
            cbbLSH.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach(LSH i in bll.GetListLSH_BLL())
            {
                cbbLSH.Items.Add(new CBBItem
                {
                    Value = i.ID_Lop,
                    Text = i.TenLop
                }) ;
                cbbLSH.SelectedIndex = 0;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string cnnstr = @"Data Source=DESKTOP-4GRVFSF\SQLEXPRESS;Initial Catalog=QLSV;Integrated Security=True";
            //SqlConnection cnn = new SqlConnection(cnnstr);
            ////string query = "Select * from SV";
            //SqlCommand cmd = new SqlCommand(textBox1.Text, cnn);
            //cnn.Open();
            //cmd.ExecuteNonQuery();
            //cnn.Close();

            //cmd.Parameters.Add("@M", SqlDbType.NVarChar);
            //cmd.Parameters["@M"].Value = textBox1.Text;
            //SqlDataAdapter da = new SqlDataAdapter(cmd);           
            //DataTable dt = new DataTable();
            //cnn.Open();
            //da.Fill(dt);
            //cnn.Close();
            string query = "Select * from SV";
            
            //DBHelper.Instance.ExecuteDB(query);
            dtgSV.DataSource = DBHelper.Instance.GetRecords(query);
            //ShowDS();
        }
        private void ShowData()
        {
            string cnnstr = @"Data Source=DESKTOP-4GRVFSF\SQLEXPRESS;Initial Catalog=QLSV;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(cnnstr);
            string query = "Select * from SV";
            SqlCommand cmd = new SqlCommand(query, cnn);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("MSSV", typeof(string)),
                    new DataColumn("NameSV", typeof(string)),
                    new DataColumn("Gender", typeof(bool)),
                    new DataColumn("NgaySinh", typeof(DateTime)),
                    new DataColumn("ID_Lop", typeof(string)),
            });
            cnn.Open();
            //textBox1.Text = ((int)cmd.ExecuteScalar()).ToString();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                DataRow dr = dt.NewRow();
                dr["MSSV"] = r["MSSV"];
                dr["NameSV"] = r["NameSV"];
                dr["Gender"] = r["Gender"];
                dr["NgaySinh"] = r["NS"];
                dr["ID_Lop"] = r["ID_Lop"];
                dt.Rows.Add(dr);
            }
            cnn.Close();
            dtgSV.DataSource = dt;
        }
        public void ShowDS()
        {
            string cnnstr = @"Data Source=DESKTOP-4GRVFSF\SQLEXPRESS;Initial Catalog=QLSV;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(cnnstr);
            string query = "Select * from SV";
            SqlCommand cmd = new SqlCommand(query, cnn);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            cnn.Open();
            // da.Fill(ds,"SV");
            da.Fill(dt);
            cnn.Close();
            //  dataGridView1.DataSource = ds.Tables["SV"];
            dtgSV.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TTSV fTT = new TTSV(null);
            fTT.ShowDialog();
            Show(0, txbSearch.Text);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItem)cbbLSH.Items[cbbLSH.SelectedIndex]).Value;
            Show(ID_Lop, txbSearch.Text);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            BLL_QLSV bll = new BLL_QLSV();
            DataGridViewSelectedRowCollection data = dtgSV.SelectedRows;
            if (data.Count == 1)
            {
                string MSSV = data[0].Cells["MSSV"].Value.ToString();
                SV s = bll.GetSVbyMSSV(MSSV);
                bll.DELSVBLL(s);
            }
            Show(0,txbSearch.Text);
           
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            BLL_QLSV bll = new BLL_QLSV();
            List<string> LMMS = new List<string>();
            foreach(SV i in bll.GetListSV_BLL())
            {
                LMMS.Add(i.MSSV);
            }
            switch (cbbSort.Text)
            {
                case "Tên, A->Z":
                    dtgSV.DataSource = bll.ListSVSort(LMMS, SV.Compare_NameAZ);
                    break;
                case "Tên, Z->A":
                    dtgSV.DataSource = bll.ListSVSort(LMMS, SV.Compare_NameZA);
                    break;
                case "MSSV, Thấp -> Cao":
                    dtgSV.DataSource = bll.ListSVSort(LMMS, SV.Compare_MSSVThapCao);
                    break;
                case "MSSV, Cao -> Thấp":
                    dtgSV.DataSource = bll.ListSVSort(LMMS, SV.Compare_MSSVCaoThap);
                    break;
                default:
                    break;
            }
            
            

        }

        private void cbbLSH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbLSH.SelectedIndex>=0)
            {
                int ID_Lop = Convert.ToInt32(((CBBItem)cbbLSH.SelectedItem).Value.ToString());
                //DataTable dt = new DataTable();
                BLL_QLSV bll = new BLL_QLSV();
                if (ID_Lop == 0)
                {
                   dtgSV.DataSource = bll.GetListSV_BLL();
                }    
                else
                {
                   dtgSV.DataSource = bll.GetListSVfromLSH_BLL(ID_Lop.ToString());
                }    

            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection data = dtgSV.SelectedRows;
            if (data.Count == 1)
            {
                string MSSV = data[0].Cells["MSSV"].Value.ToString();
                //Form2 fTT = new Form2(MSSV);
                //fTT.ShowDialog();
                //Show(0, txbSearch.Text);
                TTSV fTT = new TTSV(MSSV);
                fTT.ShowDialog();
                Show(0, txbSearch.Text);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItem)cbbLSH.Items[cbbLSH.SelectedIndex]).Value;
            Show(ID_Lop, txbSearch.Text);
        }
    }
}
