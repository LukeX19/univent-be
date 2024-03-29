﻿namespace Univent.Api
{
    public static class ApiRoutes
    {
        //all controllers will have the same base route string
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";
    
        public static class UserProfiles
        {
            public const string IdRoute = "{id}";
            public const string Unnaproved = "Unapproved";
            public const string ApproveRoute = "{id}/Approve";
        }

        public static class Universities
        {
            public const string IdRoute = "{id}";
        }

        public static class Ratings
        {
            public const string IdRoute = "{id}";
            public const string UserIdRoute = "User/{id}/Average";
        }

        public static class Events
        {
            public const string AvailableRoute = "Available";
            public const string IdRoute = "{id}";
            public const string UserIdRoute = "User/{id}";
            public const string CancelRoute = "{id}/CancelEvent";
        }

        public static class EventTypes
        {
            public const string IdRoute = "{id}";
        }

        public static class EventParticipant
        {
            public const string BothIdsRoute = "Event/{id_event}/User/{id_participant}";
            public const string EventIdRoute = "Event/{id_event}/Participants";
            public const string UserProfileIdRoute = "User/{id_participant}/EnrolledEvents";
            public const string FeedbackRoute = "Event/{id_event}/Feedback";
        }

        public static class Identity
        {
            public const string Login = "Login";
            public const string Registration = "Registration";
            public const string ChangePassword = "ChangePassword";
        }
    }
}
