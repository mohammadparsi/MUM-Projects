namespace Models
{
	public class Message : BaseExtendedEntity
	{
		public Message()
		{
		}

		// **********
		// **********
		// **********
		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Post),
		//	Name = Resources.Strings.MessageKeys.RelatedMessage)]
		public virtual Message RelatedMessage { get; set; }
		// **********

		// **********
		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Post),
		//	Name = Resources.Strings.MessageKeys.RelatedMessage)]
		public System.Guid? RelatedMessageId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Post),
		//	Name = Resources.Strings.MessageKeys.SenderUser)]
		public virtual User SenderUser { get; set; }
		// **********

		// **********
		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Post),
		//	Name = Resources.Strings.MessageKeys.SenderUser)]
		public System.Guid? SenderUserId { get; set; }
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Post),
		//	Name = Resources.Strings.MessageKeys.RecipientUser)]
		public virtual User RecipientUser { get; set; }
		// **********

		// **********
		//[System.ComponentModel.DataAnnotations.Display
		//	(ResourceType = typeof(Resources.Post),
		//	Name = Resources.Strings.MessageKeys.RecipientUser)]
		public System.Guid RecipientUserId { get; set; }
		// **********
		// **********
		// **********

		public string Subject { get; set; }

		public string Body { get; set; }

		public bool IsDraft { get; set; }

		public System.DateTime SendDateTime { get; set; }

		public bool IsRead { get; set; }

		public System.DateTime ReadDateTime { get; set; }

		public bool IsDeletedBySender { get; set; }

		public System.DateTime DeleteDateTimeBySender { get; set; }

		public bool IsDeletedByRecipient { get; set; }

		public System.DateTime DeleteDateTimeByRecipient { get; set; }

		public bool NotifySenderAfterReadingByRecipient { get; set; }

		public bool NotifySenderAfterDeletingByRecipient { get; set; }

		public virtual System.Collections.Generic.IList<Message> RepliedMessages { get; set; }
	}
}
