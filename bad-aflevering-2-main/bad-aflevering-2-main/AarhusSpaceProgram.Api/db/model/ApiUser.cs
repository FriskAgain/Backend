using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarhusSpaceProgram.db.model;

public class ApiUser : IdentityUser
{
    [MaxLength(100)]
    public string? FullName{get; set;}
}