using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    partial class SearchJob
    {
        void Start()
        {
            throw new NotImplementedException();

            Status = SearchStatus.Running;
        }

        void Pause()
        {
            throw new NotImplementedException();
        }

        void Cancel()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<IEnumerable<TrackedFile>> 
            SplitForThreads(IEnumerable<TrackedFile> list, int subLists)
        {
            //TODO: pretty sure I'm going to need this elsewhere,
            //I should maybe have a utils class instead?
            throw new NotImplementedException();
        }

        private void MainCycle()
        {

        }
    }
}
