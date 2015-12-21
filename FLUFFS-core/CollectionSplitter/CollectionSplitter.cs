using System.Collections.Generic;
using System.Linq;

namespace CollectionSplitter
{
    public class CollectionSplitter<T>
    {
        public IEnumerable<IEnumerable<T>> SplitCollection
            (IEnumerable<T> masterList, int childCollections)
        {
            int itemCount = masterList.Count();

            if (itemCount == 0)
            {
                List<IEnumerable<T>> result = new List<IEnumerable<T>>();
                result.Add(masterList);
                return result;
            }

            if (itemCount <= childCollections)
            {
                return masterList.Select((f, i) => new { f, i })
                .GroupBy(x => x.i % masterList.Count())
                .Select(g => g.Select(x => x.f).ToList()).ToList();
            }

            return masterList.Select((f, i) => new { f, i })
                .GroupBy(x => x.i % childCollections)
                .Select(g => g.Select(x => x.f).ToList()).ToList();
        }
    }

}
