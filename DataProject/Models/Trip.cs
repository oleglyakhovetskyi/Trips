namespace DataProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trip")]
    public partial class Trip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trip()
        {
            Pass_in_trip = new HashSet<Pass_in_trip>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int trip_no { get; set; }

        [Display(Name ="Company Name")]
        public int ID_comp { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name ="Plane")]
        public string plane { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "From Town")]
        public string town_from { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "To Town")]
        public string town_to { get; set; }
        [Display(Name = "Departure time")]
        public DateTime time_out { get; set; }

        [Display(Name = "Arrival time")]
        public DateTime time_in { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pass_in_trip> Pass_in_trip { get; set; }
    }
}
