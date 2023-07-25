using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ETrade.TagHelpers
{
	public class EmailTagHelper : TagHelper
	{
		private const string EmailDomain = "esrabingol.com ";
		public string MailTo { get; set; }
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "a";
			var address = MailTo + "@" + EmailDomain;
			output.Attributes.SetAttribute("href", "MailTo" + address);
			output.Content.SetContent(address);
		}
	}
}
