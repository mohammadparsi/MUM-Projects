using System.Linq;
using System.Data.Entity;

namespace MY_WEB_APPLICATION.Controllers
{
	/// <summary>
	/// Version: 1.0.5
	/// Update Date: 1393/04/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[Infrastructure.ProjectControllerPermission
		(isVisibleJustForProgrammer: false,
		keyName: Resources.Strings.ControllersKeys.Rss)]
	public partial class RssController : Infrastructure.BaseControllerWithUnitOfWork
	{
		public RssController()
		{
		}

		[System.Web.Mvc.HttpGet]
		[Infrastructure.ProjectActionPermission
			(isVisibleJustForProgrammer: false,
			accessType: Models.Enums.AccessTypes.Public,
			keyName: Resources.Strings.ActionsKeys.Display)]
		public virtual System.Web.Mvc.FileResult Index(int? id, bool simple = false)
		{
			Models.Cms.PostCategory oPostCategory = null;

			Models.Cms.SubSystemSettings oSubSystemSettings =
				Infrastructure.Cms.SubSystemSettings.Instance;

			int intRssItemCount = oSubSystemSettings.RssItemCount;

			string strRssImageUrl = oSubSystemSettings.RssImageUrl;
			string strRssImageLink = oSubSystemSettings.RssImageLink;
			string strRssImageTitle = oSubSystemSettings.RssImageTitle;

			string strRssRootCreatorUrl = oSubSystemSettings.RssRootCreatorUrl;

			string strRssChannelLink = oSubSystemSettings.RssChannelLink;
			string strRssChannelTitle = oSubSystemSettings.RssChannelTitle;
			string strRssChannelDescription = oSubSystemSettings.RssChannelDescription;

			// **************************************************
			if (id.HasValue)
			{
				oPostCategory =
					UnitOfWork.CmsUnitOfWork.PostCategoryRepository.Get()
					.Where(current => current.MasterDataCode == id.Value)
					.FirstOrDefault();

				if (oPostCategory == null)
				{
					return (null);
				}

				if (oPostCategory.IsActive == false)
				{
					return (null);
				}

				intRssItemCount = oPostCategory.RssItemCount;

				strRssImageUrl = oPostCategory.RssImageUrl;
				strRssImageLink = oPostCategory.RssImageLink;
				strRssImageTitle = oPostCategory.RssImageTitle;

				strRssRootCreatorUrl = oPostCategory.RssRootCreatorUrl;

				strRssChannelLink = oPostCategory.RssChannelLink;
				strRssChannelTitle = oPostCategory.RssChannelTitle;
				strRssChannelDescription = oPostCategory.RssChannelDescription;
			}
			// **************************************************

			// **************************************************
			Dtx.Rss.Channel oChannel =
				new Dtx.Rss.Channel
					(link: strRssChannelLink,
					title: strRssChannelTitle,
					description: strRssChannelDescription);

			oChannel.Language = CultureName;
			oChannel.PubDate = Infrastructure.Utility.Now;

			Dtx.Rss.Image oImage =
				new Dtx.Rss.Image
					(url: strRssImageUrl,
					link: strRssImageLink,
					title: strRssImageTitle);

			Dtx.Rss.Root oRoot =
				new Dtx.Rss.Root
					(image: oImage,
					channel: oChannel,
					creatorUrl: strRssRootCreatorUrl);
			// **************************************************

			// **************************************************
			System.Collections.Generic.IEnumerable<Models.Cms.Post> oPosts = null;

			if (id.HasValue == false)
			{
				oPosts =
					UnitOfWork.CmsUnitOfWork.PostRepository
					.GetActiveByCultureLcid(CultureLcid)
					.Take(intRssItemCount)
					.ToList()
					;
			}
			else
			{
				oPosts =
					UnitOfWork.CmsUnitOfWork.PostRepository
					.GetActiveByCultureLcidAndCategoryId(CultureLcid, oPostCategory.Id)
					.Take(intRssItemCount)
					.ToList()
					;
			}
			// **************************************************

			Dtx.Rss.Item oItem = null;

			foreach (Models.Cms.Post oPost in oPosts)
			{
				string strLink = string.Empty;

				if (simple)
				{
					strLink =
						strRssChannelLink + "/Post/DisplayForRss/" + oPost.Id;
				}
				else
				{
					strLink =
						strRssChannelLink + "/Post/" + oPost.Id;
				}

				string strPostTitle = oPost.Title;

				oItem = new Dtx.Rss.Item(strPostTitle, strLink);

				oItem.PubDate = oPost.InsertDateTime;

				if (simple == false)
				{
					if (string.IsNullOrEmpty(oPost.Introduction) == false)
					{
						oItem.Description = oPost.Introduction;
					}
				}

				oItem.Guid = new Dtx.Rss.Guid(true, oPost.Id.ToString());

				oItem.Author =
					new Dtx.Rss.Email
						(oPost.AuthorUser.EmailAddress, oPost.AuthorUser.FullName);

				oRoot.Items.Add(oItem);
			}

			System.IO.Stream oStream =
				Dtx.Rss.Utility.Publish(oRoot);

			//// **************************************************
			//Response.Clear();
			//Response.ContentEncoding = System.Text.Encoding.UTF8;
			//Response.ContentType = "text/xml";
			//Response.Flush();
			//// **************************************************

			//oStream.Position = 0;
			return (File(fileStream: oStream, contentType: "text/xml"));
			//return (File(fileStream: oStream, contentType: "text/xml", fileDownloadName: "rss.xml"));
		}
	}
}
