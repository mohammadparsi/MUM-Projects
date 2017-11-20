using System.Linq;

// نوشتن دستور ذيل الزامی است
using System.Web.Mvc.Html;

namespace Infrastructure
{
	public static class MvcHtmlExtensions
	{
		static MvcHtmlExtensions()
		{
		}

		public static System.Web.Mvc.MvcHtmlString TextBoxForSample1<TModel, TValue>
			(this System.Web.Mvc.HtmlHelper<TModel> htmlHelper,
			System.Linq.Expressions.Expression<System.Func<TModel, TValue>> expression,
			bool editable)
		{
			System.Web.Mvc.MvcHtmlString html =
				default(System.Web.Mvc.MvcHtmlString);

			if (editable)
			{
				html =
					System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression);
			}
			else
			{
				html =
					System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression,
					new { @class = "readOnly", @readonly = "read-only" });
			}

			return (html);
		}

		public static System.Web.Mvc.MvcHtmlString TextBoxForSample2<TModel, TProperty>
			(this System.Web.Mvc.HtmlHelper<TModel> htmlHelper,
			System.Linq.Expressions.Expression<System.Func<TModel, TProperty>> expression,
			object htmlAttributes = null
		)
		{
			var varMember =
				expression.Body as System.Linq.Expressions.MemberExpression;

			System.ComponentModel.DataAnnotations.MetadataTypeAttribute varMetadataTypeAttr =
				varMember.Member.ReflectedType.GetCustomAttributes
				(typeof(System.ComponentModel.DataAnnotations.MetadataTypeAttribute), false)
				.FirstOrDefault() as System.ComponentModel.DataAnnotations.MetadataTypeAttribute;

			System.Collections.Generic.IDictionary<string, object> varHtmlAttr = null;

			if (varMetadataTypeAttr != null)
			{
				var stringLength = varMetadataTypeAttr.MetadataClassType
					.GetProperty(varMember.Member.Name).GetCustomAttributes
					(typeof(System.ComponentModel.DataAnnotations.StringLengthAttribute), false)
					.FirstOrDefault() as System.ComponentModel.DataAnnotations.StringLengthAttribute;

				if (stringLength != null)
				{
					varHtmlAttr =
						new System.Web.Routing.RouteValueDictionary(htmlAttributes);

					varHtmlAttr.Add("maxlength", stringLength.MaximumLength);
				}
			}

			return (htmlHelper.TextBoxFor(expression, varHtmlAttr));
		}
	}
}
