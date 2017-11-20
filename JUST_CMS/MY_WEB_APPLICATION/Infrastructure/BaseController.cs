﻿using System.Linq;
using System.Data.Entity;

namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.4
	/// Update Date: 1393/02/30
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class BaseController : System.Web.Mvc.Controller
	{
		public BaseController()
		{
			// اين دستور بايد دقيقا در همين محل نوشته شود
			ViewBag.PageMessages = PageMessages;

			System.Globalization.CultureInfo oCultureInfo =
				new System.Globalization.CultureInfo(Sessions.Culture);

			oCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
			oCultureInfo.NumberFormat.PercentDecimalSeparator = ".";
			oCultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";

			System.Threading.Thread.CurrentThread.CurrentCulture = oCultureInfo;
			System.Threading.Thread.CurrentThread.CurrentUICulture = oCultureInfo;
		}

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);

			if (Request != null)
			{
				string strUrl = Request.ServerVariables["HTTP_REFERER"];
				if (string.IsNullOrEmpty(strUrl) == false)
				{
					strUrl = strUrl.ToLower();

					System.Uri oUrl = new System.Uri(strUrl);
					string strDomainName = oUrl.Host;

					DAL.UnitOfWork oUnitOfWork = null;
					try
					{
						oUnitOfWork = new DAL.UnitOfWork();

						Models.HttpReferer oHttpReferer =
							oUnitOfWork.HttpRefererRepository.Get()
							.Where(current => string.Compare(current.DomainName,
								strDomainName, System.StringComparison.InvariantCultureIgnoreCase) == 0)
							.FirstOrDefault();

						if(oHttpReferer == null)
						{
							oHttpReferer =
								new Models.HttpReferer();

							oHttpReferer.Hits = 1;
							oHttpReferer.Url = strUrl;
							oHttpReferer.DomainName = strDomainName;
							oHttpReferer.LastHitDateTime = Infrastructure.Utility.Now;
							oHttpReferer.FirstHitDateTime = Infrastructure.Utility.Now;

							oUnitOfWork.HttpRefererRepository.Insert(oHttpReferer);
						}
						else
						{
							oHttpReferer.Hits++;
							oHttpReferer.Url = strUrl;
							oHttpReferer.LastHitDateTime = Infrastructure.Utility.Now;
						}

						oUnitOfWork.Save();
					}
					catch
					{
					}
					finally
					{
						if (oUnitOfWork != null)
						{
							oUnitOfWork.Dispose();
							oUnitOfWork = null;
						}
					}
				}
			}
		}

		private System.Collections.Generic.IList<Infrastructure.PageMessage> _pageMessages;
		public System.Collections.Generic.IList<Infrastructure.PageMessage> PageMessages
		{
			get
			{
				if (_pageMessages == null)
				{
					_pageMessages =
						new System.Collections.Generic.List<Infrastructure.PageMessage>();
				}
				return (_pageMessages);
			}
		}

		public int CultureLcid
		{
			get
			{
				return (System.Threading.Thread.CurrentThread.CurrentCulture.LCID);
			}
		}

		public string CultureName
		{
			get
			{
				return (System.Threading.Thread.CurrentThread.CurrentCulture.Name);
			}
		}

		public System.Globalization.CultureInfo Culture
		{
			get
			{
				return (System.Threading.Thread.CurrentThread.CurrentCulture);
			}
		}
	}
}
