using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigment.Models
{
    [Table("gymSessions")]
    public class gymSession
    {
        [Key]
        public int gymSession_ID { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string gymSession_Category { get; set; }

        [Required(ErrorMessage = "Trainer Name is required")]
        public string gymSession_TrainerName { get; set; }

        [Required(ErrorMessage = "Slots is required")]
        public int gymSession_Slots { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime gymSession_Date { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public string gymSession_Time { get; set; }

        public string gymSession_Trainer_Image_URL { get; set; }

        public string gymSession_Trainer_Image_S3Key { get; set; }

        public bool gymSession_IsImageUpdated { get; set; }

        public string gymSession_Trainer_Prev_Image_URL { get; set; }
    }
}
