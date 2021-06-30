using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceBus.SharedLib
{
    public class Email
    {
        [Required, EmailAddress]
        public string EmailTo { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

    }
}
