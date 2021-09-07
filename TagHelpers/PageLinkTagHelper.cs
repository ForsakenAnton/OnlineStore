using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        //public PageViewModel<Product> PageListModel { get; set; }
        //public PageViewModel<Comment> PageCommentsModel { get; set; }
        public PageViewModel PageListModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination justify-content-center");

            TagBuilder back = CreateTag(" << ", urlHelper);
            if (!PageListModel.HasPreviousPage)
            {
                back.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(back);

            //TagBuilder currentItem = CreateTag(PageListModel.PageNumber.ToString(), urlHelper);

            ////////////////////////////////////////////////////
            for (int i = 1; i <= PageListModel.PageNumber; i++)
            {
                if ((PageListModel.PageNumber - 3 <= i) || (i == 1) || (PageListModel.PageNumber == i))
                {
                    TagBuilder Item = CreateTag(i.ToString(), urlHelper);
                    tag.InnerHtml.AppendHtml(Item);
                }
            }
            //if (PageListModel.HasPreviousPage)
            //{
            //    TagBuilder prevItem = CreateTag((PageListModel.PageNumber - 1).ToString(), urlHelper);
            //    tag.InnerHtml.AppendHtml(prevItem);
            //}
            ////////////////////////////////////////////////////

            //tag.InnerHtml.AppendHtml(currentItem);

            for (int i = PageListModel.PageNumber + 1; i <= PageListModel.TotalPages; i++)
            {
                if ((PageListModel.PageNumber + 3 >= i) || (i == PageListModel.TotalPages) || (PageListModel.PageNumber == i))
                {
                    TagBuilder nextItem = CreateTag(i.ToString(), urlHelper);
                    tag.InnerHtml.AppendHtml(nextItem);
                }
            }
            //if (PageListModel.HasNextPage)
            //{
            //    TagBuilder nextItem = CreateTag((PageListModel.PageNumber + 1).ToString(), urlHelper);
            //    tag.InnerHtml.AppendHtml(nextItem);
            //}

            TagBuilder forward = CreateTag(" >> ", urlHelper);
            if (!PageListModel.HasNextPage)
            {
                forward.AddCssClass("disabled");
            }
            tag.InnerHtml.AppendHtml(forward);

            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(string pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == this.PageListModel.PageNumber.ToString())
            {
                item.AddCssClass("active");
            }
            else
            {
                if(pageNumber == " << ")
                {
                    PageUrlValues["page"] = PageListModel.PageNumber - 1;
                }
                else if (pageNumber == " >> ")
                {
                    PageUrlValues["page"] = PageListModel.PageNumber + 1;
                }
                else
                {
                    PageUrlValues["page"] = pageNumber;
                }

                link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
