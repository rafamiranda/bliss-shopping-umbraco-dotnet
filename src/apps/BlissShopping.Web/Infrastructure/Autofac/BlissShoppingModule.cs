using Autofac;
using BlissShopping.Core.Infrastructure.AutoFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlissShopping.Web.Infrastructure.Autofac
{
    public class BlissShoppingModule : CoreModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
