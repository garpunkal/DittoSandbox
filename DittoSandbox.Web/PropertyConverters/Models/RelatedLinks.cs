using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DittoSandbox.Web.PropertyConverters.Models
{
    public class RelatedLinks : IEnumerable<RelatedLink>
    {
        private readonly List<RelatedLink> _relatedLinks;

        public RelatedLinks(List<RelatedLink> relatedLinks)
        {
            _relatedLinks = relatedLinks;
        }

        public bool Any() => Enumerable.Any(this);

        public IEnumerator<RelatedLink> GetEnumerator()
        {
            return _relatedLinks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}