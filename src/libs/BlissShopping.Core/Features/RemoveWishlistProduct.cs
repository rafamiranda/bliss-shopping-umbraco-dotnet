using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlissShopping.Core.Database;
using BlissShopping.Core.Database.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlissShopping.Core.Features
{
    public static class RemoveWishlistProduct
    {
        public class CommandRequest : IRequest<CommandResponse>
        {
            //public PageEventForm Content { get; set; }
            //Table Name EventRecords
            [HiddenInput]
            public int EventId { get; set; }

            public int UserId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int ProductQuantity { get; set; }

        }

        public class CommandResponse
        {
            public bool Status { get; set; }
        }

        public class Handler : IRequestHandler<CommandRequest, CommandResponse>
        {
            private readonly BlissShoppingContext _dbContext;
            private readonly IHttpContextAccessor _httpContextAccessor;
            //private readonly IMapper _mapper;


            public Handler(BlissShoppingContext dbContext, IHttpContextAccessor httpContextAccessor)//, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
                //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<CommandResponse> Handle(CommandRequest request, CancellationToken cancellationToken)
            {
                var name = _httpContextAccessor.HttpContext.User.Identity.Name;

                var userId = _httpContextAccessor.GetUserId();

                if (userId.HasValue)
                {
                    var productToRemove = _dbContext.Wishlist.FirstOrDefault(p => p.ProductId == request.ProductId && p.UserId == userId);

                    if (productToRemove != null)
                    {
                        _dbContext.Remove(productToRemove);
                        var resultCount = await _dbContext.SaveChangesAsync(cancellationToken);
                        return new CommandResponse { Status = resultCount == 1 };
                    }

                    return new CommandResponse { Status = false };
                }

                return new CommandResponse { Status = false };
            }
        }
    }
}
