﻿namespace Univent.Api.Contracts.UserProfile.Requests
{
    public record UserProfileCreateUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Hometown { get; set; }
    }
}