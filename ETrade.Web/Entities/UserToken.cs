using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ETrade.Web.Entities
{
	//user authentication token bilgisi
	public class UserToken: IdentityUserToken<int>
	{
    }
}
