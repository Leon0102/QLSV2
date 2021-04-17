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
    }
}
