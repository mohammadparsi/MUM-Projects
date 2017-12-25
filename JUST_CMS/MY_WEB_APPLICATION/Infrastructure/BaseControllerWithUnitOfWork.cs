namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1392/04/26
	/// 
	/// </summary>
	public class BaseControllerWithUnitOfWork : BaseController
	{
		public BaseControllerWithUnitOfWork()
		{
		}

		private DAL.UnitOfWork _unitOfWork;
		protected virtual DAL.UnitOfWork UnitOfWork
		{
			get
			{
				if (_unitOfWork == null)
				{
					_unitOfWork = new DAL.UnitOfWork();
				}
				return (_unitOfWork);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (_unitOfWork != null)
			{
				_unitOfWork.Dispose();
				_unitOfWork = null;
			}

			base.Dispose(disposing);
		}
	}
}
