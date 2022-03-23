using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

using Rock;
using Rock.Data;
using Rock.Model;
using Rock.Rest;
using Rock.Rest.Filters;
using Rock.Web.Cache;

namespace christfellowshipchurch.DigitalPlatformPlugin.Rest
{
    public class ChristFellowshipDigitalPlatformController : ApiControllerBase
    {

        [HttpGet]
        [EnableQuery]
        [Authenticate, Secured]
        [System.Web.Http.Route("api/ChristFellowshipDigitalPlatform/test")]
        public String Test( string input )
        {
            return input;
        }

    }
}
