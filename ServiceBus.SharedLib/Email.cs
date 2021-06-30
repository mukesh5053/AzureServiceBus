using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceBus.SharedLib
{
    public record  Email ([Required, EmailAddress]string EmailTo, string Subject, [Required]string Body );

    
}
