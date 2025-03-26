using System.ComponentModel.DataAnnotations;

namespace MicroStore.Web.Features.Auth.Enums;

public enum UserRole
{
    [Display(Name = "Administrador")]
    ADMIN,

    [Display(Name = "Cliente")]
    CUSTOMER
}