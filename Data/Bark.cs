namespace Barker.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bark
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(128)]
        public string Text { get; set; }


        public int Rebarks { get; set; }

        public DateTime DateTimePosted { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
