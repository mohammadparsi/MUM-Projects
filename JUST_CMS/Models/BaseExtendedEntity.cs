namespace Models
{
	/// <summary>
	/// Version: 1.2.7
	/// Update Date: 1393/02/28
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	[System.Serializable]
	public abstract class BaseExtendedEntity : BaseEntity, IBaseExtendedEntity
	{
		public BaseExtendedEntity()
		{
			Ordering = 10000;
			InsertDateTime = Dtx.Core.Utility.GetNow();
		}

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.IsSystem)]
		public bool IsSystem { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.Ordering)]

		[System.ComponentModel.DataAnnotations.Required
			(ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName = Resources.Strings.MessagesKeys.Required)]

		[System.ComponentModel.DataAnnotations.RegularExpression
			(RegularExpressions.RegularExpressionForUnsignedInteger,
			ErrorMessageResourceType = typeof(Resources.Messages),
			ErrorMessageResourceName =
			Resources.Strings.MessagesKeys.RegularExpressionForUnsignedInteger)]
		public int Ordering { get; set; }
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.InsertDateTime)]
		public virtual System.DateTime InsertDateTime { get; protected internal set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.CreatorUserId)]
		public System.Guid? CreatorUserId { get; protected internal set; }
		// **********

		// **********
		[System.NonSerialized]
		private User _creatorUser;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.CreatorUserId)]
		public virtual User CreatorUser
		{
			get
			{
				return (_creatorUser);
			}
			protected internal set
			{
				_creatorUser = value;
			}
		}
		// **********

		// **********
		public void SetInsertDateTime(System.Guid? userId, System.DateTime? dateTime = null)
		{
			System.DateTime? dtm = dateTime;
			if (dtm == null)
			{
				dtm = Dtx.Core.Utility.GetNow();
			}

			CreatorUserId = userId;
			InsertDateTime = dtm.Value;
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.UpdateDateTime)]
		public virtual System.DateTime? UpdateDateTime { get; protected internal set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.EditorUserId)]
		public System.Guid? EditorUserId { get; protected internal set; }
		// **********

		// **********
		[System.NonSerialized]
		private User _editorUser;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.EditorUserId)]
		public virtual User EditorUser
		{
			get
			{
				return (_editorUser);
			}
			protected internal set
			{
				_editorUser = value;
			}
		}
		// **********

		// **********
		public void SetUpdateDateTime(System.Guid? userId, System.DateTime? dateTime = null)
		{
			System.DateTime? dtm = dateTime;

			if (dtm == null)
			{
				dtm = Dtx.Core.Utility.GetNow();
			}

			UpdateDateTime = dtm;

			if (userId.HasValue)
			{
				EditorUserId = userId;
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.IsActive)]
		public bool IsActive { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.ActiveDateTime)]
		public System.DateTime? ActiveDateTime { get; protected internal set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.ActivatorUserId)]
		public System.Guid? ActivatorUserId { get; protected internal set; }
		// **********

		// **********
		[System.NonSerialized]
		private User _activatorUser;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.ActivatorUserId)]
		public virtual User ActivatorUser
		{
			get
			{
				return (_activatorUser);
			}
			protected internal set
			{
				_activatorUser = value;
			}
		}
		// **********

		// **********
		public void SetIsActive
			(bool isActive, System.Guid userId, System.DateTime dateTime)
		{
			if (isActive)
			{
				if (IsActive == false)
				{
					IsActive = true;
					ActiveDateTime = dateTime;
					ActivatorUserId = userId;
				}
			}
			else
			{
				if (IsActive)
				{
					IsActive = false;
					ActiveDateTime = dateTime;
					ActivatorUserId = userId;
				}
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.IsVerified)]
		public bool IsVerified { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.VerifyDateTime)]
		public System.DateTime? VerifyDateTime { get; protected internal set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.VerifierUserId)]
		public System.Guid? VerifierUserId { get; protected internal set; }
		// **********

		// **********
		[System.NonSerialized]
		private User _verifierUser;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.VerifierUserId)]
		public virtual User VerifierUser
		{
			get
			{
				return (_verifierUser);
			}
			protected internal set
			{
				_verifierUser = value;
			}
		}
		// **********

		// **********
		public void SetIsVerified
			(bool isVerified, System.Guid userId, System.DateTime dateTime)
		{
			if (isVerified)
			{
				if (IsVerified == false)
				{
					IsVerified = true;
					VerifyDateTime = dateTime;
					VerifierUserId = userId;
				}
			}
			else
			{
				if (IsVerified)
				{
					IsVerified = false;
					VerifyDateTime = dateTime;
					VerifierUserId = userId;
				}
			}
		}
		// **********
		// **********
		// **********

		// **********
		// **********
		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.IsDeleted)]
		public bool IsDeleted { get; set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.DeleteDateTime)]
		public System.DateTime? DeleteDateTime { get; protected internal set; }
		// **********

		// **********
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.RemoverUserId)]
		public System.Guid? RemoverUserId { get; protected internal set; }
		// **********

		// **********
		[System.NonSerialized]
		private User _removerUser;
		[System.ComponentModel.DataAnnotations.Display
			(ResourceType = typeof(Resources.BaseEntity),
			Name = Resources.Strings.BaseEntityKeys.RemoverUserId)]
		public virtual User RemoverUser
		{
			get
			{
				return (_removerUser);
			}
			protected internal set
			{
				_removerUser = value;
			}
		}
		// **********

		// **********
		public void SetIsDeleted
			(bool isDeleted, System.Guid userId, System.DateTime dateTime)
		{
			if (isDeleted)
			{
				if (IsDeleted == false)
				{
					IsDeleted = true;
					RemoverUserId = userId;
					DeleteDateTime = dateTime;
				}
			}
			else
			{
				if (IsDeleted)
				{
					IsDeleted = false;
					RemoverUserId = userId;
					DeleteDateTime = dateTime;
				}
			}
		}
		// **********
		// **********
		// **********
	}
}
