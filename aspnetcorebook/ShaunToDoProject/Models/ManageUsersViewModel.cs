﻿using System.Collections.Generic;


namespace ShaunToDoProject.Models
{
  public class ManageUsersViewModel
  {
    public IEnumerable<ApplicationUser> Administrators { get; set; }
    public IEnumerable<ApplicationUser> Everyone { get; set; }
  }
}