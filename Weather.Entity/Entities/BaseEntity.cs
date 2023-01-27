
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.Domain.Entities
{
	public abstract class BaseEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		[Key]
		public Guid? Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public bool IsDeleted { get; set; }
	}
}
