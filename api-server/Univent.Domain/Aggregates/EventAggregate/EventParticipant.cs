﻿using Univent.Domain.Aggregates.UserAggregate;

namespace Univent.Domain.Aggregates.EventAggregate
{
    public class EventParticipant
    {
        public Guid EventID { get; private set; }
        public Guid UserProfileID { get; private set; }
        public Event Event { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public bool hasProvidedFeedback { get; private set; }

        //Constructor
        public EventParticipant()
        {
        }

        //Factory method
        public static EventParticipant CreateEventParticipant(Guid eventID, Guid userProfileID)
        {
            var newEventParticipant = new EventParticipant
            {
                 EventID = eventID,
                 UserProfileID = userProfileID,
                 hasProvidedFeedback = false
            };

            return newEventParticipant;
        }

        public void UserProvidedFeedback()
        {
            hasProvidedFeedback = true;
        }
    }
}
