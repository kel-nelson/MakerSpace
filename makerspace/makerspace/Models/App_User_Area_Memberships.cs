namespace makerspace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class App_User_Area_Memberships
    {
        public int id { get; set; }

        public int user_id { get; set; }

        public int area_id { get; set; }

        public int membership_type_id { get; set; }

        public virtual App_Areas App_Areas { get; set; }

        public virtual App_Membership_Types App_Membership_Types { get; set; }

        public virtual App_User_Profiles App_User_Profiles { get; set; }
    }
}
