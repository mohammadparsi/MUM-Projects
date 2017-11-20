
namespace Infrastructure
{
    public static class baghali
    {
        public static System.Web.IHtmlString someExtentionMethod
            (this System.Web.Mvc.HtmlHelper helper, string something)
        {
            string strResult = string.Format("<div class={0}></div>", something);
            return (helper.Raw(strResult));
        }
    }
}