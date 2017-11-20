namespace Dtx.Kendo
{
	public class GridResult<T> : System.Object
	{
		public int Total { get; set; }
		public System.Collections.Generic.IEnumerable<T> Data { get; set; }
		//public System.Collections.Generic.IEnumerable<GridSort> Sort { get; set; }
		//public System.Collections.Generic.IEnumerable<GridGroup> Group { get; set; }
	}
}
