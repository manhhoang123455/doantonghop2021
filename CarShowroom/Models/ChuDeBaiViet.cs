namespace CarShowroom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("topic")]
    public partial class ChuDeBaiViet
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int parentid { get; set; }
        public int status { get; set; }
    }
}
