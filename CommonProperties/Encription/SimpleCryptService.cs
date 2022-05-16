using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonProperties.Encription
{
    public class SimpleCryptService : SimpleCryptServiceBase
    {
        protected override string EncryptionKey => "HELLOMyWebAPI0123456987";

        public static SimpleCryptService Factory()
        {
            return new SimpleCryptService();
        }
    }
}
