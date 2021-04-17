using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF_BT2.DAL;
using WF_BT2.DTO;

namespace WF_BT2.BLL
{
    class BLL_QLSV
    {
        public List<SV> GetListSV_BLL()
        {
            DAL_QLSV dal = new DAL_QLSV();
            return dal.GetListSV_DAL();
        }
        public List<LSH> GetListLSH_BLL()
        {
            DAL_QLSV dal = new DAL_QLSV();
            return dal.GetListLSH_DAL();
        }
        public SV GetSVbyMSSV(string ms)
        {
            DAL_QLSV dal = new DAL_QLSV();
            return dal.GetSVByMSSV(ms);
        }
        public void ADDSVBLL(SV s)
        {
            DAL_QLSV dal = new DAL_QLSV();
            dal.ADDSVDAL(s);
        }
        public void DELSVBLL(SV s)
        {
            DAL_QLSV dal = new DAL_QLSV();
            dal.DelSVDAL(s);
        }
        public void UPDATESVBLL(SV s)
        {
            DAL_QLSV dal = new DAL_QLSV();
            dal.UpdateSVDAL(s);
        }
        public List<SV> GetListSV_BLL(int ID_Lop, string Name)
        {
            DAL_QLSV dal = new DAL_QLSV();
            return dal.GetListSV_DAL(ID_Lop, Name);
        }
        public List<SV> GetListSVfromLSH_BLL(string ms)
        {
            DAL_QLSV dal = new DAL_QLSV();
            return dal.GetListSVfromLSH_DAL(ms);
        }
        public List<SV> GetListSV_Dtgv(List<string> LMMS)
        {
            DAL_QLSV dal = new DAL_QLSV();
            List<SV> data = new List<SV>();
            foreach (string i in LMMS)
            {
                data.Add(dal.GetSVByMSSV(i));
            }
            return data;
        }
        public bool CheckMSSV(SV s1, SV s2)
        {
            if(Convert.ToInt32(s1.MSSV)>Convert.ToInt32(s2.MSSV))
            {
                return true;
            }
            return false;
        }
        public List<SV> ListSVSort(List<string> ms)
        {
            DAL_QLSV dal = new DAL_QLSV();
            List<SV> data = dal.GetListSV_DAL();
            for (int i = 0; i < data.Count - 1; ++i)
            {
                for (int j = i + 1; j < data.Count; ++j)
                {
                        if(CheckMSSV( data[i],data[j]))
                        { 
                        SV temp = data[i];
                        data[i] = data[j];
                        data[j] = temp;
                        }
                }
            }
            return data;
        }
        public bool ChekMSSV(string ms)
        {
            foreach (SV i in GetListSV_BLL())
            {
                if (i.MSSV==ms)
                {
                    return true;
                }
            }
            return false;
        }
        public void ExecuteDBB(SV s)
        {
            bool check = false;
            // CRUD
            foreach (SV i in GetListSV_BLL())
            {
                if (i.MSSV == s.MSSV)
                {
                    check = true;
                }
            }
            if (check == true)
            {
                // Edit, Done
                UPDATESVBLL(s);
            }
            else
            {
                // Add, Done
                ADDSVBLL(s);
            }
        }
    }
}
