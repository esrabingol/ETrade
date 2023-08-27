using ETrade.Abstract;
using System.Collections.Generic;

namespace ETrade.Entities
{
	//çoka çok
	public class Announcement : IEntity
	{
		public int Id { get; set; }
		public string Announcement_Title { get; set; }
		public bool Flag { get; set; }
		public string Announcement_Description { get; set; }

		public List<AnnouncementTopic> announcementTopics { get; set; }


	}
}
