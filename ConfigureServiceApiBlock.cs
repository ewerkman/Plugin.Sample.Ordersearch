using System.Collections.Generic;
using Microsoft.AspNet.OData.Builder;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sample.OrderSearch
{
    public class ConfigureServiceApiBlock : SyncPipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        public override ODataConventionModelBuilder Run(ODataConventionModelBuilder arg, CommercePipelineExecutionContext context)
        {
            var getLatestOrdersAction = arg.Function("GetLatestOrders");
            getLatestOrdersAction.ReturnsCollection<Order>();
            
            return arg;
        }
    }
}