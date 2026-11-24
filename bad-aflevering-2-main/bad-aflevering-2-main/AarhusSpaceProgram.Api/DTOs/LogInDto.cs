using System.ComponentModel.DataAnnotations;

namespace AarhusSpaceProgram.Api.DTOs;

public class LogInDto
{
    [Required]
    public string UserName{get; set;} = string.Empty;

    [Required]
    public string Password{get; set;} = string.Empty;
}