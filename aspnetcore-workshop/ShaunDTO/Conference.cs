using System.ComponentModel.DataAnnotations;

namespace ShaunDTO
{
  public class Conference
  {
    public int ID { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }
  }
}
