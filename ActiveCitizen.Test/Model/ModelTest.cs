using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActiveCitizen.Model.Shared;
using ActiveCitizen.Model.Shared.Moscow;
using System.Reflection;

namespace ActiveCitizen.Test.Model
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void FeedItemStyleAllStylesIsComplete()
        {
            var type = typeof(FeedItemStyle);
            var props = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var prop in props)
            {
                if (prop.FieldType != type) continue;
                
                var style = (FeedItemStyle)prop.GetValue(null);

                Assert.IsTrue(FeedItemStyle.AllStyles.Contains(style),
                    string.Format("FeedItemStyle.AllStyles collection missing '{0}' value", prop.Name));
            }
        }

        [TestMethod]
        public void MoscowDistrictsAllDistrictsIsComplete()
        {
            var type = typeof(MoscowDistrict);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                if (field.FieldType != type) continue;

                var district = (MoscowDistrict)field.GetValue(null);

                Assert.IsTrue(MoscowDistrict.AllDistricts.Contains(district),
                    string.Format("MoscowDistrict.AllDistricts does not contain '{0}' value", field.Name));
            }


        }
    }
}
