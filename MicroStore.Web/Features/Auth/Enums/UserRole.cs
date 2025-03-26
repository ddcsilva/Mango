using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Features.Auth.Enums;

public enum UserRole
{
    [Display(Name = "Administrador")]
    ADMIN,

    [Display(Name = "Cliente")]
    CUSTOMER
}