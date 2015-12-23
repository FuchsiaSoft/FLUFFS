using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    partial class TrackedFile
    {
        public static explicit operator TrackedFile(TrackedFilesDue_Result v)
        {
            using (DbModelContainer db = new DbModelContainer())
            {
                return db.TrackedFiles.Find(v.Id);
            }
        }
    }
}
