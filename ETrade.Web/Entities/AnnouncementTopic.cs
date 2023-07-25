namespace ETrade.Entities
{
	public class AnnouncementTopic
	{
		public Announcement announcement { get; set; }
		public int AnnouncementId;
		public Topic topic { get; set; }
		public int TopicId;
	}
}
