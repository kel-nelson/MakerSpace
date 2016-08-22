namespace makerspace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class App_User_Profiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public App_User_Profiles()
        {
            App_User_Area_Memberships = new HashSet<App_User_Area_Memberships>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public DateTime birthdate { get; set; }

        //public DateTime created_on { get; set; }

        [Required]
        [StringLength(128)]
        public string app_user_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<App_User_Area_Memberships> App_User_Area_Memberships { get; set; }
    }
}
