using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Commerce.Plugin.Orders;
using Sitecore.Commerce.Plugin.Pricing;
using Sitecore.Commerce.Plugin.Search;

namespace Plugin.Sample.OrderSearch.Commands
{
    public class GetLatestOrdersCommand : CommerceCommand
    {
        private readonly CommerceCommander _commander;

        public GetLatestOrdersCommand(CommerceCommander commander)
        {
            _commander = commander;
        }

        public virtual async Task<IEnumerable<Order>> Process(CommerceContext commerceContext)
        {
            // Filter order on not pending status
            var filterQuery = new FilterQuery(new AndFilterNode(new NotFilterNode(new EqualsFilterNode(
                    new FieldNameFilterNode("status"),
                    new FieldValueFilterNode("Pending"))),
                new GreaterThanFilterNode(new FieldNameFilterNode("orderplaceddate"),
                    new FieldValueFilterNode(
                        DateTime.Now.AddMonths(-1)
                            .ToString(SearchConstants.DateTimeSearchFormat, CultureInfo.InvariantCulture),
                        FilterNodeValueType.Date))));

            var scope = SearchScopePolicy.GetPolicyByType(commerceContext, commerceContext.Environment, typeof(Order));

            var searchResults = await _commander.Command<SearchEntitiesCommand>()
                .Process<Order>(commerceContext, scope.Name, new SearchQuery(), filterQuery).ConfigureAwait(false);

            return searchResults.OrderByDescending(o => o.OrderPlacedDate);
        }
    }
}