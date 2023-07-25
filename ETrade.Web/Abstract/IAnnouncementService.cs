using System.Linq.Expressions;
using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETrade.Entities;

namespace ETrade.Abstract
{
	public interface IAnnouncementService
	{
		List<Announcement> GetAll();
		Announcement GetById(int id);
		void Add(Announcement entity);
		void Update(Announcement entity);
		void Delete(Announcement entity);
	}

}
