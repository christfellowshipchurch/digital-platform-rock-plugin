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
using Ical.Net.DataTypes;

public struct ScheduleOccurrence
{
    public ScheduleOccurrence(DateTime start, DateTime end) : this()
    {
        Start = start;
        End = end;
    }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

namespace christfellowshipchurch.DigitalPlatformPlugin.Rest
{
    public class ChristFellowshipDigitalPlatformController : ApiControllerBase
    {

        /// <summary>
        /// Parses a schedule into upcoming DateTimes. Optionally pass through a specific start and end date for retreiving all dates within the given range.
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        [EnableQuery]
        [Authenticate, Secured]
        [System.Web.Http.Route("api/ChristFellowshipDigitalPlatform/Schedule/ParseDates/{scheduleId}")]
        public ScheduleOccurrence[] ScheduleDates(int scheduleId, DateTime? startTime = null, DateTime? endTime = null)
        {
            var rockContext = new RockContext();
            rockContext.Configuration.ProxyCreationEnabled = false;
            ScheduleService scheduleService = new ScheduleService(rockContext);
            Schedule schedule = scheduleService.Get(scheduleId);

            DateTime start = DateTime.Now;
            if (startTime != null)
            {
                start = (DateTime)startTime;
            }

            IList<Occurrence> occurrences = schedule.GetICalOccurrences(start, endTime);


            return occurrences
                .Select(o => new ScheduleOccurrence(
                    start: o.Period.StartTime.Value,
                    end: o.Period.EndTime.Value)
                ).ToArray();
        }

    }
}
