namespace Dtx.Kendo
{
	public class GridGroup : System.Object
	{
		public GridGroup()
		{
		}

		public GridGroup(string field, string dir)
		{
			Dir = dir;
			Field = field;
		}

		public string Dir { get; set; }
		public string Field { get; set; }
	}
}
