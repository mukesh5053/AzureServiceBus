using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceBus.SharedLib
{
    public record  PaymentEmail ([Required, EmailAddress]string EmailTo, [Required, Range(1, 500000, ErrorMessage = "Has To Be > 1 and < 500000")] int Amount, string Message);

    public record PaymentReminderEmail([Required, EmailAddress] string EmailTo, [Required, Range(1, 500000, ErrorMessage = "Has To Be > 1 and < 500000")] int Amount, string Message);

}
