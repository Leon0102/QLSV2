using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF_BT2.BLL;
using WF_BT2.DTO;

namespace WF_BT2.GUI
{
    public partial class TTSV : Form
    {
        public delegate void MyDel(int id, string ms);
        public MyDel d { get; set; }
        public string MSSV { get;  set; }

        public TTSV(string ms)
        {
            InitializeComponent();
            LoadLSH();
            if(ms != null)
            {
                SetGUI(ms);
                MSSV = ms;
                txbMSSV.Enabled = false;
            }    
            
        }
        public void SetGUI(string ms)
        {
            BLL_QLSV bll = new BLL_QLSV();
            if (bll.GetSVbyMSSV(ms) != null)
            {
                // Binding
                SV s = bll.GetSVbyMSSV(ms);
                txbMSSV.Text = s.MSSV;
                txbName.Text = s.NameSV;
                dtNgaySinh.Value = s.NgaySinh;
                if (s.Gender == true)
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }
                cbbLopSH.Text = ((CBBItem)cbbLopSH.Items[s.ID_Lop - 1]).Text;
            }
        }
        public void LoadLSH()
        {
            BLL_QLSV bll = new BLL_QLSV();
            if (cbbLopSH.Items != null)
            {
                cbbLopSH.Items.Clear();
            }
            cbbLopSH.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (LSH i in bll.GetListLSH_BLL())
            {
                cbbLopSH.Items.Add(new CBBItem
                {
                    Value = i.ID_Lop,
                    Text = i.TenLop
                });
                cbbLopSH.SelectedIndex = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BLL_QLSV bll = new BLL_QLSV();
            string MSSV = txbMSSV.Text;
            string NameSV = txbName.Text;
            bool Gender = rbMale.Checked;
            DateTime BD = Convert.ToDateTime(dtNgaySinh.Value);
            int LopSH = ((CBBItem)cbbLopSH.Items[cbbLopSH.SelectedIndex]).Value;
            SV s = new SV(MSSV, NameSV, BD, LopSH, Gender);
            bll.ExecuteDBB(s);
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
