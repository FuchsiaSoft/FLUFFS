using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    class FerretExecutionStrategy : DbExecutionStrategy
    {
        public FerretExecutionStrategy() { }

        public FerretExecutionStrategy(int maxRetryCount, TimeSpan maxDelay) : base(maxRetryCount, maxDelay) { }

        protected override bool ShouldRetryOn(Exception exception)
        {
            bool retry = false;

            SqlException sqlException = exception as SqlException;
            if (sqlException != null)
            {
                int[] errorsToRetry =
                {
                    1205,  //Deadlock
                    -2,    //Timeout
                };
                if (sqlException.Errors.Cast<SqlError>().Any(x => errorsToRetry.Contains(x.Number)))
                {
                    retry = true;
                }
                else
                {
                    retry = false; //just don't bother.
                }
            }
            if (exception is TimeoutException)
            {
                retry = true;
            }
            return retry;
        }
    }
}
