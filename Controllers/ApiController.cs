using System;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Plugin.Sample.OrderSearch.Commands;
using Sitecore.Commerce.Core;

namespace Plugin.Sample.OrderSearch.Controllers
{
    public class ApiController : CommerceODataController
    {
        public ApiController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) 
            : base(serviceProvider, globalEnvironment)
        {
        }
        
        [HttpPost]
        [ODataRoute("GetLatestOrders()", RouteName = CoreConstants.CommerceApi)]
        public async Task<IActionResult> GetLatestOrders([FromBody] ODataActionParameters value)
        {
            var result = await Command<GetLatestOrdersCommand>().Process(CurrentContext);
            
            return new ObjectResult(result);
        }
    }
}