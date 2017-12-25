namespace ViewModels.Areas.Cms.Post
{
	/// <summary>
	/// Version: 1.0.3
	/// Update Date: 1393/04/30
	/// 
	/// </summary>
	public class IndexViewModel : System.Object
	{
		public IndexViewModel()
		{
		}

		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		public bool IsActive { get; set; }
		// **********

		// **********
		public bool IsVerified { get; set; }
		// **********

		// **********
		public bool IsFeatured { get; set; }
		// **********

		// **********
		public bool IsDeleted { get; set; }
		// **********

		// **********
		public System.Guid CategoryId { get; set; }
		// **********

		// **********
		public string CategoryName { get; set; }
		// **********

		// **********
		public string Title { get; set; }
		// **********

		// **********
		public System.Guid AuthorUserId { get; set; }
		// **********

		// **********
		// **********
		// **********
		public string AuthorUserGender { get; set; }
		// **********

		// **********
		public string AuthorUserLastName { get; set; }
		// **********

		// **********
		public string AuthorUserFirstName { get; set; }
		// **********

		// **********
		public string AuthorUserFullName
		{
			get
			{
				string strResult = AuthorUserGender;

				if (string.IsNullOrWhiteSpace(strResult) == false)
				{
					strResult += " ";
				}

				strResult += AuthorUserFirstName;

				if (string.IsNullOrWhiteSpace(strResult) == false)
				{
					strResult += " ";
				}

				strResult += AuthorUserLastName;

				return (strResult);
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		public int? CommentCount { get; set; }
		// **********

		// **********
		public string XCommentCount
		{
			get
			{
				string strResult = "-----";

				if (CommentCount.HasValue)
				{
					strResult =
						Dtx.Text.Convert.DisplayFormattedNumber(CommentCount.Value);
				}

				return (strResult);
			}
		}
		// **********

		// **********
		// **********
		// **********
		public System.DateTime InsertDateTime { get; set; }
		// **********

		// **********
		public System.DateTime? UpdateDateTime { get; set; }
		// **********

		// **********
		public string XLastUpdateDateTime
		{
			get
			{
				string strResult = string.Empty;

				if(UpdateDateTime.HasValue)
				{
					strResult =
						Dtx.Calendar.Utility.DisplayDateTime(UpdateDateTime, displayTime: true);
				}
				else
				{
					strResult =
						Dtx.Calendar.Utility.DisplayDateTime(InsertDateTime, displayTime: true);
				}

				return (strResult);
			}
		}
		// **********
		// **********
		// **********
	}
}
