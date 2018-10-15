namespace Lab4University
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(10, ErrorMessage = "ПIБ не може бути меншим за 10 символiв ")]
        public string PIB { get; set; }

        public int Group_ID { get; set; }

        public virtual Group Group { get; set; }
    }
}
