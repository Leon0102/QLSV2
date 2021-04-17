using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF_BT2.DTO;

namespace WF_BT2.DAL
{
    class DAL_QLSV
    {
        public List<SV> GetListSV_DAL()
        {
            List<SV> data = new List<SV>();
            string query = "Select * from dbo.SV";
            foreach(DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add(GetSV(i));
            }
            return data;
        }
        public List<SV> GetListSVfromLSH_DAL(string ms)
        {
            
            List<SV> data = new List<SV>();
            string query = "Select * from SV where ID_Lop ='"+ms+"'";
            foreach (DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add(GetSV(i));
            }
            return data;
        }
        public SV GetSV(DataRow i)
        {
            return new SV
            {
                MSSV = i["MSSV"].ToString(),
                NameSV = i["NameSV"].ToString(),
                NgaySinh = Convert.ToDateTime(i["NS"]),
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                Gender = Convert.ToBoolean(i["Gender"])
            };
        }
        public List<LSH> GetListLSH_DAL()
        {
            List<LSH> data = new List<LSH>();
            string query = "Select * from LopSH";
            foreach (DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add(GetLSH(i));
            }
            return data;
        }
        public LSH GetLSH(DataRow i)
        {
            return new LSH
            {
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                TenLop = i["NameLop"].ToString()
            };
        }
        public void ADDSVDAL(SV s)
        {
            string date = String.Format("{0}-{1}-{2}", s.NgaySinh.Year, s.NgaySinh.Month, s.NgaySinh.Day);
            string query = String.Format("Insert into dbo.SV(MSSV,NameSV,Gender,NS,ID_Lop) Values("+s.MSSV+", N'"+s.NameSV+"', N'"+s.Gender+"', N'"+date+"', N'"+s.ID_Lop+"')");
            DBHelper.Instance.ExecuteDB(query);
        }
        public void DelSVDAL(SV s)
        {
            string sql = "Delete dbo.SV where MSSV ='" + s.MSSV + "'";
            DBHelper.Instance.ExecuteDB(sql);
        }
        public void UpdateSVDAL(SV s)
        {
            string date = String.Format("{0}-{1}-{2}", s.NgaySinh.Year, s.NgaySinh.Month, s.NgaySinh.Day);
            string sql =  String.Format( "Update dbo.SV Set NameSV=N'"+s.NameSV+"',Gender=N'"+s.Gender+"',NS=N'"+date+"',ID_Lop=N'"+s.ID_Lop+"' where MSSV =N'" + s.MSSV +"'");
            DBHelper.Instance.ExecuteDB(sql);
        }
        public SV GetSVByMSSV(string ms)
        {
            SV s = new SV();
            string sql = "Select * from SV where MSSV =N'" + ms + "'";
            return GetSV(DBHelper.Instance.GetRecords(sql).Rows[0]);
        }
        public List<SV> GetListSV_DAL(int ID_Lop, string Name)
        {
            List<SV> data = new List<SV>();
            if (ID_Lop == 0 && Name == "")
            {
                data = GetListSV_DAL();
            }
            else
            {
                foreach (SV i in GetListSV_DAL())
                {
                    if (Name != "")
                    {
                        if (i.NameSV.Contains(Name))
                        {
                            data.Add(new SV
                            {
                                NameSV = i.NameSV,
                                MSSV = i.MSSV,
                                Gender = i.Gender,
                                NgaySinh = i.NgaySinh,
                                ID_Lop = i.ID_Lop
                            });
                        }
                    }
                    else
                    {
                        if (i.ID_Lop == ID_Lop)
                        {
                            data.Add(new SV
                            {
                                NameSV = i.NameSV,
                                MSSV = i.MSSV,
                                Gender = i.Gender,
                                NgaySinh = i.NgaySinh,
                                ID_Lop = i.ID_Lop
                            });
                        }
                    }
                }

            }
            return data;
        }
    }
}
