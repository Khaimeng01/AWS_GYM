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
        public string gymSession_Category { get; set; }

        public string gymSession_TrainerName { get; set; }

        public int gymSession_Slots { get; set; }

        public DateTime gymSession_Date { get; set; }

        public string gymSession_Time { get; set; }

        public string gymSession_Trainer_Image_URL { get; set; }

        public string gymSession_Trainer_Image_S3Key { get; set; }

        public bool IsImageUpdated { get; set; }

        public string gymSession_Trainer_Prev_Image_URL { get; set; }
    }
}
