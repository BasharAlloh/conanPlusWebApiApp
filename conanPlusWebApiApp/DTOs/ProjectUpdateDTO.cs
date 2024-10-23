using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class ProjectUpdateDTO
{
    [Required]
    public int ProjectId { get; set; }

    [Required]
    [MaxLength(200)]
    public string ProjectTitle { get; set; }

    public IFormFile ExternalImage { get; set; }  
    public IFormFile InternalImage { get; set; } 

    [MaxLength(300)]
    public string ProjectLink { get; set; }

    [Required]
    public int ServiceId { get; set; }

    [Required]
    public int FilterId { get; set; }

}
