namespace PadocaGestor.Infrastructure.Models;

public class Roles
{
    
    public long Id { get; set; }
    public string Role { get; set; }
    
    public ICollection<RolesUsuario> RolesUsuarios { get; set; }
}