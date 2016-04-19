using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCitizen.Model.Shared.Moscow
{
    public class MoscowDistrict
    {
        public static MoscowDistrict TSAO = new MoscowDistrict("ЦАО");
        public static MoscowDistrict SAO = new MoscowDistrict("CАО");
        public static MoscowDistrict SVAO = new MoscowDistrict("СВАО");
        public static MoscowDistrict VAO = new MoscowDistrict("ВАО");
        public static MoscowDistrict UVAO = new MoscowDistrict("ЮВАО");
        public static MoscowDistrict UAO = new MoscowDistrict("ЮАО");
        public static MoscowDistrict UZAO = new MoscowDistrict("ЮЗАО");
        public static MoscowDistrict ZAO = new MoscowDistrict("ЗАО");
        public static MoscowDistrict SZAO = new MoscowDistrict("CЗАО");
        public static MoscowDistrict ZelAO = new MoscowDistrict("ЗелАО");
        public static MoscowDistrict TiNAO = new MoscowDistrict("ТиНАО");
        public static MoscowDistrict Total = new MoscowDistrict("Вся Москва");

        public static ICollection<MoscowDistrict> AllDistricts = new List<MoscowDistrict> {
            TSAO,
            SAO,
            SVAO,
            VAO,
            UVAO,
            UAO,
            UZAO,
            ZAO,
            SZAO,
            ZelAO,
            TiNAO,
            Total};

        internal string _district = String.Empty;
        private MoscowDistrict(string district)
        {
            this._district = district;
        }

        public static implicit operator MoscowDistrict(string district)
        {
            if (district == null) return null;

            return AllDistricts.FirstOrDefault(item => item._district == district) ?? Total;
        }

        public static implicit operator string(MoscowDistrict district)
        {
            return district != null ? district.ToString() : null;
        }

        public override string ToString()
        {
            return _district;
        }

        public override bool Equals(object obj)
        {
            var pair = obj as MoscowDistrict;
            return pair != null && pair._district == _district;
        }

        public override int GetHashCode()
        {
            return this._district.GetHashCode();
        }
    }
}
