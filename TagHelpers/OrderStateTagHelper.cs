using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.TagHelpers
{
    public class OrderStateTagHelper : TagHelper
    {
        //private IUrlHelperFactory urlHelperFactory;

        //public OrderStateTagHelper(IUrlHelperFactory helperFactory)
        //{
        //    urlHelperFactory = helperFactory;
        //}

        //[ViewContext]
        //[HtmlAttributeNotBound]
        //public ViewContext ViewContext { get; set; }
        //public string PageAction { get; set; }

        public OrderState State { get; set; }
        //public bool IsColorState { get; set; }
        //public SelectList SelectStates { get; set; }
        //public bool IsLink { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            //IList<SelectListItem> list = Enum
            //    .GetValues(typeof(OrderState))
            //    .Cast<OrderState>()
            //    .Select(x => new SelectListItem
            //    {
            //        Text = x.ToString(),
            //        Value = ((int)x).ToString()
            //    }).ToList();

            //list.Insert(0, new SelectListItem
            //{
            //    Text = "All",
            //    Value = "-1",
            //    Selected = true
            //});

            //SelectList SelectStates = new SelectList(list, "Value", "Text", (int)State);

            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            // TagBuilder a = new TagBuilder("a");
            TagBuilder i = new TagBuilder("i");
            //a.InnerHtml.Append(State.ToString());

            string[] states =
            {
                    "bi bi-cart-dash-fill bg-secondary",
                    "bi bi-truck bg-light",
                    "bi bi-shop-window bg-primary",
                    "bi bi-bookmark-check bg-success",
                    "bi bi-x-circle bg-danger",
                    "bi bi-arrow-return-left bg-warning"
            };

            string currentState = states[(int)State];
            i.AddCssClass(currentState + " ps-3 pe-3 pt-1 pb-1 rounded rounded-3 text-nowrap");
            i.InnerHtml.Append(" " + State.ToString());

            output.Content.SetHtmlContent(i);
            //return base.ProcessAsync(context, output);
        }
    }
}
