using System.Linq;
using System.Linq.Dynamic;

namespace Dtx.Kendo
{
	public static class HtmlHelpers
	{
		static HtmlHelpers()
		{
		}

		public static GridResult<T> ParseGridData<T>(System.Linq.IQueryable<T> collection)
		{
			GridPost oGridPost = new GridPost();

			// **************************************************
			//foreach (GridGroup oGridGroup in oGridPost.Group)
			//{
			//	collection =
			//		collection.GroupBy(oGridGroup.Field, oGridGroup.Field);
			//}
			// **************************************************

			// **************************************************
			foreach (GridSort oGridSort in oGridPost.Sort)
			{
				collection =
					collection.OrderBy(oGridSort.Field + " " + oGridSort.Dir);
			}
			// **************************************************

			System.Collections.Generic.List<T> oGridData;
			try
			{
				oGridData =
					collection.Skip(oGridPost.Skip).Take(oGridPost.Take).ToList();
			}
			catch
			{
				// If the collection is not ordered skip take will fail
				// All My Collections have a primary key named id
				collection =
					collection.OrderBy("InsertDateTime desc");

				oGridData =
					collection.Skip(oGridPost.Skip).Take(oGridPost.Take).ToList();
			}

			GridResult<T> oGridResult = new GridResult<T>();

			oGridResult.Data = oGridData;
			//oGridResult.Sort = oGridPost.Sort;
			//oGridResult.Group = oGridPost.Group;
			oGridResult.Total = collection.Count();

			return (oGridResult);
		}
	}
}
