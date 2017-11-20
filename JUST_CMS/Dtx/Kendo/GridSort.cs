namespace Dtx.Kendo
{
	public class GridSort : System.Object
	{
		public GridSort()
		{
		}

		public GridSort(string field, string dir)
		{
			Dir = dir;
			Field = field;
		}

		public string Dir { get; set; }
		public string Field { get; set; }
	}
}
