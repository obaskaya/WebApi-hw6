using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entitites
{
	public class Author
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string BirthDay { get; set; }
	
	}
}
