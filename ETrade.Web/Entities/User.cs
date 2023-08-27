using ETrade.Abstract;
using System;
using System.Collections.Generic;

namespace ETrade.Entities
{
	//User(kullanıcı) verilerini,parola vs.
	public class User : IEntity  //has dbset table, bu kısım PERSONEL
	{
		
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string First_Name { get; set; }
		public string Last_Name { get; set; }
		public long Telephone { get; set; }
		public DateTime Created_At { get; set; }
		public DateTime Modified_At { get; set; }

		public List<Product> products { get; set; } //kullanıcıda birden çok ürün olabilir

	}

	


}
