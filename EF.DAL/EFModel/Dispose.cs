namespace EF.DAL.EFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dispose")]
    public partial class Dispose
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DisposeName { get; set; }

        [StringLength(50)]
        public string DisposeText1 { get; set; }

        [StringLength(500)]
        public string DisposeText2 { get; set; }

        [StringLength(500)]
        public string DisposeText3 { get; set; }
    }
}
