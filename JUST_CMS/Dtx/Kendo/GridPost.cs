using Dtx;

namespace Dtx.Kendo
{
	public class GridPost
	{
		public GridPost()
		{
			if (System.Web.HttpContext.Current != null)
			{
				System.Web.HttpRequest oCurrentHttpRequest =
					System.Web.HttpContext.Current.Request;

				int intIndex;

				// **************************************************
				string strSortDir = null;
				string strSortField = null;
				string strSortDirKeyName = null;
				string strSortFieldKeyName = null;

				intIndex = 0;
				do
				{
					strSortDirKeyName =
						string.Format("sort[{0}][dir]", intIndex);

					strSortFieldKeyName =
						string.Format("sort[{0}][field]", intIndex);

					strSortDir = oCurrentHttpRequest[strSortDirKeyName];
					strSortField = oCurrentHttpRequest[strSortFieldKeyName];

					if (string.IsNullOrEmpty(strSortField) == false)
					{
						if (string.IsNullOrEmpty(strSortDir))
						{
							strSortDir = "asc";
						}

						Sort.Add(new GridSort(strSortField, strSortDir));
					}

					intIndex++;
				}
				while (string.IsNullOrEmpty(strSortField) == false);
				// **************************************************

				// **************************************************
				//string strGroupDir = null;
				//string strGroupField = null;
				//string strGroupDirKeyName = null;
				//string strGroupFieldKeyName = null;

				//intIndex = 0;
				//do
				//{
				//	strGroupDirKeyName =
				//		string.Format("group[{0}][dir]", intIndex);

				//	strGroupFieldKeyName =
				//		string.Format("group[{0}][field]", intIndex);

				//	strGroupDir = oCurrentHttpRequest[strGroupDirKeyName];
				//	strGroupField = oCurrentHttpRequest[strGroupFieldKeyName];

				//	if (string.IsNullOrEmpty(strGroupField) == false)
				//	{
				//		if (string.IsNullOrEmpty(strGroupDir))
				//		{
				//			strGroupDir = "asc";
				//		}

				//		Group.Add(new GridGroup(strGroupField, strGroupDir));
				//	}

				//	intIndex++;
				//}
				//while (string.IsNullOrEmpty(strGroupField) == false);
				// **************************************************

				Page = oCurrentHttpRequest["page"].Parse<int>(1);
				Skip = oCurrentHttpRequest["skip"].Parse<int>(0);
				Take = oCurrentHttpRequest["take"].Parse<int>(10);
				PageSize = oCurrentHttpRequest["pageSize"].Parse<int>(10);
			}
		}

		public int Skip { get; set; }
		public int Take { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }

		private System.Collections.Generic.List<GridSort> _sort;
		public System.Collections.Generic.List<GridSort> Sort
		{
			get
			{
				if (_sort == null)
				{
					_sort =
						new System.Collections.Generic.List<GridSort>();
				}
				return (_sort);
			}
		}

		//private System.Collections.Generic.List<GridGroup> _group;
		//public System.Collections.Generic.List<GridGroup> Group
		//{
		//	get
		//	{
		//		if (_group == null)
		//		{
		//			_group =
		//				new System.Collections.Generic.List<GridGroup>();
		//		}
		//		return (_group);
		//	}
		//}
	}
}
