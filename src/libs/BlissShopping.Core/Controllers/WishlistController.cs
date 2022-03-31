using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Cms.Core.Security;
using BlissShopping.Core.PublishedContent;
using MediatR;
using System.Threading;
using BlissShopping.Core.Features;
using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models;

namespace BlissShopping.Core.Controllers
{
    public class WishlistController : RenderController
    {
        protected IMediator _mediator { get; private set; }
        public WishlistController(
            ILogger<WishlistController> logger, ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IMediator mediator)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ValidateUmbracoFormRouteString]
        public async Task<IActionResult> HandleSubmit(AddWishlistProduct.CommandRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return CurrentTemplate(CurrentPage);
            }

            var response = await _mediator.Send(request, cancellationToken);
            
            //if (response.Status)
            //{
            //    var queryString = QueryString.Create("success", "true");

            //    return RedirectToCurrentUmbracoPage(queryString);
            //}

            //return RedirectToCurrentUmbracoPage();
            return CurrentTemplate(CurrentPage);
        }

        public override IActionResult Index()
        {
            var request = new WishlistProducts.CommandRequest
            {
                EventId = 1
            };
            
            var response = _mediator.Send(request).ConfigureAwait(false);
            var result = response.GetAwaiter().GetResult();

            return CurrentTemplate(result);
        }
    }
}
