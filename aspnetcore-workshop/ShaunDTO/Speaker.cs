using System.ComponentModel.DataAnnotations;

namespace ShaunDTO
{
  public class Speaker
  {

    public int ID { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [StringLength(4000)]
    public string Bio { get; set; }

    [StringLength(1000)]
    public virtual string Website { get; set; }

  }

}
