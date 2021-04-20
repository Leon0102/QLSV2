using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_BT2.DTO
{
    class SV
    {
        public SV(string mSSV, string nameSV, DateTime bD, int lopSH, bool gender)
        {
            MSSV = mSSV;
            NameSV = nameSV;
            NgaySinh = bD;
            ID_Lop = lopSH;
            Gender = gender;
        }
        public SV() { }
        public string MSSV { get; set; }
        public string NameSV { get; set; }
        public DateTime NgaySinh { get; set; }
        public int ID_Lop { get; set; }
        public bool Gender { get; set; }
        public static bool Compare_MSSVCaoThap(object o1, object o2)
        {
            return Convert.ToInt32(((SV)o1).MSSV) < Convert.ToInt32(((SV)o2).MSSV);
        }
        public static bool Compare_MSSVThapCao(object o1, object o2)
        {
            return Convert.ToInt32(((SV)o1).MSSV) > Convert.ToInt32(((SV)o2).MSSV);
        }
        public static bool Compare_NameZA(object o1, object o2)
        {
            if (string.Compare(((SV)o1).NameSV, ((SV)o2).NameSV) < 0)
                return true;
            else
                return false;
        }
        public static bool Compare_NameAZ(object o1, object o2)
        {
            if (string.Compare(((SV)o1).NameSV, ((SV)o2).NameSV) > 0)
                return true;
            else
                return false;
        }
    }
}
