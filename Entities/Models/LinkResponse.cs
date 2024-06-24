using Entities.LinkModels;
using System.Dynamic;

namespace Entities.Models
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        public List<ExpandoObject>? ShapedEntities { get; set; }
        public LinkCollectionWrapper<ExpandoObject>? LinkedEntities { set; get; }

        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<ExpandoObject>();
            ShapedEntities = new List<ExpandoObject>();
        }
    }
}
