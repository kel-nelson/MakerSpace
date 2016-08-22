namespace makerspace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class App_Membership_Types
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public App_Membership_Types()
        {
            App_User_Area_Memberships = new HashSet<App_User_Area_Memberships>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(25)]
        public string title { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<App_User_Area_Memberships> App_User_Area_Memberships { get; set; }
    }
}
