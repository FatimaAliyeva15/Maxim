using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim_Core.Models
{
	public class Blog: BaseEntity
	{
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string Name { get; set; }
		[Required]
		[StringLength(250)]
		public string Description { get; set; }
		public string? ImgUrl { get; set; }
		[NotMapped]
		public IFormFile? ImgFile { get; set; }
	}
}
