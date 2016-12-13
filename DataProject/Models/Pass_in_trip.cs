namespace DataProject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pass_in_trip
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int trip_no { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime date { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_psg { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name ="Place")]
        public string place { get; set; }

        public virtual Passenger Passenger { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
