namespace Models.Cms
{
	public interface ITag
	{
		string Tags { get; set; }

		System.Collections.Generic.IList<Tag> TagList { get; set; }
	}
}
