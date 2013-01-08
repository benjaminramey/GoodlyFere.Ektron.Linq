#region Usings

using System;
using System.Linq;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using GoodlyFere.Ektron.Linq.Tests.Model;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Integration
{
    [SmartForm("Practice")]
    public class Practice : ModelWidget
    {
        #region Public Properties

        [SmartFormProperty("/root/PracticeInformation/Address")]
        public string Address { get; set; }

        [SmartFormProperty("/root/PracticeInformation/OfficeHours")]
        public string Hours { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [SmartFormProperty("/root/PracticeInformation/PracticeName")]
        public string Name { get; set; }

        [SmartFormProperty("/root/PracticeInformation/PhoneNumber")]
        public string PhoneNumber { get; set; }

        [SmartFormProperty("/root/PracticeInformation/PracticePhoto/img/@src")]
        public string Photo { get; set; }

        #endregion

        //[MetadataProperty("MapLatitude")]
    }
}