using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class PartnerCreateDTO
{
    [Required(ErrorMessage = "Partner name is required")]
    [MaxLength(100)]
    public string PartnerName { get; set; }

    [Required(ErrorMessage = "Image file is required")]
    public IFormFile ImageFile { get; set; }
}
