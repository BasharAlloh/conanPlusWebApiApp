using System.ComponentModel.DataAnnotations;

public class ContactForm
{
    [Key]
    public int MessageId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Subject is required")]
    [MaxLength(200)]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [MaxLength(1000)]
    public string Message { get; set; }

    public bool IsRead { get; set; } = false;
}
