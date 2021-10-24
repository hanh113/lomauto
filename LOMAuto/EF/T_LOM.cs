namespace LOMAuto.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_LOM
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string DEVICE_ID { get; set; }

        public int? CURRENT_KEY { get; set; }

        public int? CURRENT_MAP { get; set; }

        [StringLength(250)]
        public string CURRENT_STATUS { get; set; }

        public int TARGET_MAP { get; set; }
        public int MINI_MAP { get; set; }
        public string STATUS { get; set; }
        public int CURRENT_LEVEL { get; set; }
        public DateTime? UPDATE_TIME { get; set; }
        public bool IS_AUTO { get; set; }

    }
}
