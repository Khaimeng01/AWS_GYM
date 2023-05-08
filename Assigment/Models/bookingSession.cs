using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigment.Models
{
    [Table("bookingSessions")]
    public class bookingSession
    {
        [Key]
        public int bookingSession_ID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string bookingSession_Username { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string bookingSession_CustomerName { get; set; }

        [Required(ErrorMessage = "Training category is required")]
        public string bookingSession_Category { get; set; }

        [Required(ErrorMessage = "Trainer name is required")]
        public string bookingSession_TrainerName { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public string bookingSession_Time { get; set; }

        [Required(ErrorMessage = "Trainer name is required")]
        public DateTime bookingSession_Date { get; set; }

        //public static implicit operator bookingSession(List<bookingSession> v)
        //{
        //    throw new NotImplementedException();
        //}

        //public static implicit operator bookingSession(List<bookingSession> v)
        //{
        //    throw new NotImplementedException();
        //}

        //public string gymSession_Trainer_Image_URL { get; set; }

        //public string gymSession_Trainer_Image_S3Key { get; set; }

        //public bool gymSession_IsImageUpdated { get; set; }

        //public string gymSession_Trainer_Prev_Image_URL { get; set; }
    }
}