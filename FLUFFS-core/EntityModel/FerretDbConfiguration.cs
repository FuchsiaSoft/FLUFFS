using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    class FerretDbConfiguration : DbConfiguration
    {
        public FerretDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new FerretExecutionStrategy());
        }
    }
}
