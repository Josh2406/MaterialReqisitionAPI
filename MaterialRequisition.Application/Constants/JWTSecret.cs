using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialRequisition.Application.Constants
{
    public static class JWTSecret
    {
        /// <summary>
        /// <b>JWT Token Secret Key</b> <br/><br/>
        /// Generated from HMAC: <br/>
        /// <code>
        /// var hmac = new HMACSHA256();<br/>
        /// var key = Convert.ToBase64String(hmac.Key);
        /// </code>
        /// </summary>
        public const string Key = "VzBE2bzCw+DHYd2tkfoKFJ2tmeXZKh1SyPKDWogA+BBMqtedGrztcrq0WyQZsD9t+r/cpmX9/PPgPOqKhDYh4A==";
    }
}
