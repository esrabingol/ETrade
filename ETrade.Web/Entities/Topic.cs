using ETrade.Abstract;

namespace ETrade.Entities
{
	public class Topic : IEntity
	{
		public int Id { get; set; }
		public string Topic_Title { get; set; }
		public List<AnnouncementTopic> announcementTopics { get; set; }


	}
}
