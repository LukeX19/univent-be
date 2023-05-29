﻿namespace Univent.Api.Contracts.Event.Responses
{
    public record EventResponse
    {
        public Guid EventID { get; set; }
        public Guid UserProfileID { get; set; }
        public Guid EventTypeID { get; set; }
        public string EventName { get; set; }
        public int MaximumParticipants { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string EventTypeName { get; set; }
        public string EventTypePicture { get; set; }
        public double AverageRating { get; set; }
    }
}
