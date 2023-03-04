﻿namespace Univent.Api
{
    public class ApiRoutes
    {
        //all controllers will have the same base route string
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
    
        public class UserProfiles
        {
            public const string IdRoute = "{id}";
        }

        public class Universities
        {
            public const string IdRoute = "{id}";
        }

        public class Ratings
        {
            public const string IdRoute = "{id}";
        }

        public class Events
        {
            public const string IdRoute = "{id}";
            public const string CancelRoute = "{id}/cancelevent";
        }

        public class EventTypes
        {
            public const string IdRoute = "{id}";
        }

        public class EventParticipant
        {
            public const string BothIdsRoute = "/Event/{id_event}/Participant/{id_participant}";
            public const string EventIdRoute = "/Event/{id_event}/Participants";
            public const string UserProfileIdRoute = "/Participant/{id_participant}/EnrolledEvents";
        }
    }
}
