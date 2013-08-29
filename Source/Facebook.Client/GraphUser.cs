﻿namespace Facebook.Client
{
    using System;
    using System.Collections.Generic;
    using Facebook.Client.Controls;

    /// <summary>
    /// Provides a strongly-typed representation of a Facebook User as defined by the Graph API.
    /// </summary>
    /// <remarks>
    /// The GraphUser class represents the most commonly used properties of a Facebook User object.
    /// </remarks>
    public class GraphUser : GraphObject
    {
        private Uri profilePictureUrl;

        /// <summary>
        /// Initializes a new instance of the GraphUser class.
        /// </summary>
        public GraphUser()
        {
        }

#if NODYNAMIC
        /// <summary>
        /// Initializes a new instance of the GraphUser class from a JsonObject returned by the Facebook API.
        /// </summary>
        /// <param name="user">The JsonObject representing the Facebook user.</param>
        public GraphUser(object user)
            : base((IDictionary<string, object>)user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			JsonObject json = (JsonObject)user;

			this.Id = json["id"] as string;
			this.Name = json["name"] as string;
			this.UserName = json["username"] as string;
			this.FirstName = json["first_name"] as string;
			this.MiddleName = json["middle_name"] as string;
			this.LastName = json["last_name"] as string;
			this.Birthday = json["birthday"] as string;
			object location = json["location"];
			this.Location = (location != null) ? new GraphLocation(location) : null;
			this.Link = json["link"] as string;
			JsonObject picture = json["picture"] as JsonObject;
			if (picture != null)
			{
				JsonObject data = picture ["data"] as JsonObject;
				Uri.TryCreate(data["url"] as string, UriKind.Absolute, out this.profilePictureUrl);
			}
		}
#else
		/// <summary>
		/// Initializes a new instance of the GraphUser class from a dynamic object returned by the Facebook API.
		/// </summary>
		/// <param name="user">The dynamic object representing the Facebook user.</param>
		public GraphUser(dynamic user)
		: base((IDictionary<string, object>)user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			this.Id = user.id;
			this.Name = user.name;
			this.UserName = user.username;
			this.FirstName = user.first_name;
			this.MiddleName = user.middle_name;
			this.LastName = user.last_name;
			this.Birthday = user.birthday;
			dynamic location = user.location;
			this.Location = (location != null) ? new GraphLocation(location) : null;
			this.Link = user.link;
			var picture = user.picture;
			if (picture != null)
			{
				Uri.TryCreate(picture.data.url, UriKind.Absolute, out this.profilePictureUrl);
			}
		}
#endif

        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Facebook username of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of the user.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthday of the user.
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// Gets or sets the Facebook URL of the user.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the current city of the user.
        /// </summary>
        public GraphLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's profile picture.
        /// </summary>
        public Uri ProfilePictureUrl
        {
            get
            {
                if (this.profilePictureUrl == null)
                {
                    this.profilePictureUrl = new Uri(ProfilePicture.GetBlankProfilePictureUrl(true), UriKind.RelativeOrAbsolute);
                }

                return this.profilePictureUrl;
            }

            set
            {
                this.profilePictureUrl = value;
            }
        }
    }
}
