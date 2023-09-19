using ETrade.Abstract;
using ETrade.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ETrade.Services
{
	public class AnnouncementService : IAnnouncementService
	{

        //senin veri tabanına erişip veriyi getirecek veriyi ekleyecek şey
        //repositoryindir yani aşağıdaki interfface

        //IAnnouncementRepository bu yani. bunu çağırıp kullancan bunun hangi katmanda oldugu sana baglı
        private readonly IAnnouncementRepository _announcementRepository;
	
		public AnnouncementService(IAnnouncementRepository announcementRepository)
		{
			_announcementRepository = announcementRepository;
		}

		public void Add(Announcement entity)
		{
            _announcementRepository.Create(entity);
		}

		public void Delete(Announcement entity)
		{
			_announcementRepository.Delete(entity);
		}

		public List<Announcement> GetAll()
		{
		
			return _announcementRepository.GetAll(x => x.Flag == true).ToList();
		}

		public Announcement GetById(int id)
		{
			return _announcementRepository.GetById(id);
		}

		public void Update(Announcement entity)
		{
			_announcementRepository.Update(entity);
		}
	}
}
