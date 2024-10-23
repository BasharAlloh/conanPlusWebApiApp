using System.ComponentModel.DataAnnotations;

public class PartnerUpdateDTO
{
    [Key]
    public int PartnerId { get; set; }  
    [Required(ErrorMessage = "Partner name is required")]
    [MaxLength(100)]
    public string PartnerName { get; set; }
    public IFormFile ImageFile { get; set; }  
}
