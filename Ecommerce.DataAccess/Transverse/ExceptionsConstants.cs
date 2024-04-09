using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Transverse
{
      internal static class ExceptionsConstants
    {
        public static readonly string MessageBadParameterAnomaly = "Bad parameter exception  {0}";
        public static readonly string MessageErrorDatabaseReading = "Read SQL Data error: {0}";
        public static readonly string MessageErrorDatabaseWriting = "Write SQL Data error:  {0}";  
        public static readonly string MessageErrorDatabaseToBusiness = "Error of mapping Database to Business entity : {0}";
        public static readonly string MessageMappingErrorBusinessToDatabase = "Error of mapping Business entity to Database entity";
    }
}
