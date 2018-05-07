using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShaunFrontEnd.Pages.Models
{
  public class Attendee : ShaunDTO.Attendee
  {

    [DisplayName("First Name")]
    public override string FirstName { get => base.FirstName; set => base.FirstName = value; }

    [DisplayName("Last Name")]
    public override string LastName { get => base.LastName; set => base.LastName = value; }

    [DisplayName("Email Address")]
    [DataType(DataType.EmailAddress)]
    public override string EmailAddress { get => base.EmailAddress; set => base.EmailAddress = value; }

  }
}
