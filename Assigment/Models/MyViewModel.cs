using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assigment.Models
{
    public class MyViewModel
    {
        public List<gymSession> GymSession { get; set; }
        public bookingSession BookingSession { get; set; }
    }
}