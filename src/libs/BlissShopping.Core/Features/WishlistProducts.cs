using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlissShopping.Core.Database;
using BlissShopping.Core.Database.Models;
using BlissShopping.Core.PublishedContent;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Umbraco.Cms.Core.Web;

namespace BlissShopping.Core.Features
{
    public static class WishlistProducts
    {
        public class CommandRequest : IRequest<WishList>
        {
            //public PageEventForm Content { get; set; }
            //Table Name EventRecords
            [HiddenInput]
            public int EventId { get; set; }

        }

        public class Handler : IRequestHandler<CommandRequest, WishList>
        {
            private readonly BlissShoppingContext _dbContext;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IUmbracoContextAccessor _umbracoContextAccessor;

            public Handler(BlissShoppingContext dbContext, IHttpContextAccessor httpContextAccessor, IUmbracoContextAccessor umbracoContextAccessor)//, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
                _umbracoContextAccessor = umbracoContextAccessor ?? throw new ArgumentNullException(nameof(umbracoContextAccessor));
            }

            public async Task<WishList> Handle(CommandRequest request, CancellationToken cancellationToken)
            {
                var name = _httpContextAccessor.HttpContext.User.Identity.Name;
                var userId = _httpContextAccessor.GetUserId();

                _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);
                var wishList = umbracoContext.PublishedRequest.PublishedContent as WishList;
                var productsContents = new List<ArticlesItem>();

                if (wishList != null)
                {
                    if (userId.HasValue)
                    {
                        var products = await _dbContext.Wishlist.Where(u => u.UserId == userId.Value).ToListAsync().ConfigureAwait(false);

                        foreach (var product in products)
                        {
                            if (product != null)
                            {
                                var productContent = umbracoContext.Content.GetById(product.ProductId);

                                if (productContent is ArticlesItem)
                                {
                                    productsContents.Add((ArticlesItem)productContent);
                                }
                            }
                        }
                        wishList.Products = productsContents;
                    }
                }

                return wishList;
            }
        }
    }
}
