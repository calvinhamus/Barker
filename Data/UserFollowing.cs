namespace Barker.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserFollowing")]
    public partial class UserFollowing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string FollowingId { get; set; }

        public virtual AspNetUser UserFollowed { get; set; }

        public virtual AspNetUser UserSelf { get; set; }
    }
}
