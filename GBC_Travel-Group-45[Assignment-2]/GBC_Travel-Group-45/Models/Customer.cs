using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



public class Customer
{


    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public string GovernmentId { get; set; }

   





}
