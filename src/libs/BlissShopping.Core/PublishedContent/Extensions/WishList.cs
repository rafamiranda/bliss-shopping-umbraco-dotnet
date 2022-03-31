using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace BlissShopping.Core.PublishedContent
{
	public partial class WishList
	{
		public List<ArticlesItem> Products { get; set; } = new List<ArticlesItem>();
    }
}
