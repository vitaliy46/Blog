using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            int range = 4;

            // leftCount - число страниц слева, rightCount - число страниц справа от текущей
            int leftCount = pagingInfo.CurrentPage - 1;
            int rightCount = pagingInfo.TotalPages - pagingInfo.CurrentPage;
            // Число страниц слева и справа в навигации
            int leftRange;
            int rightRange;

            // Увеличиваем leftRange или rightRange чтобы leftRange + rightRange = range
            if (leftCount >= range)
            {
                leftRange = range;

                if (rightCount >= range)
                {
                    rightRange = range;
                }
                else
                {
                    rightRange = rightCount;
                    // Оставшееся число страниц не должно превышать leftRange
                    leftRange = 2 * range - rightCount >= leftCount ? leftRange : 2 * range - rightCount;
                }
            }
            else
            {
                leftRange = leftCount;
                // Оставшееся число страниц не должно превышать rightRange
                rightRange = 2 * range - leftCount >= rightCount ? rightCount : 2 * range - leftCount;
            }

            // В начало
            TagBuilder startTag = new TagBuilder("a");
            startTag.MergeAttribute("href", pageUrl(1));
            startTag.InnerHtml = "<<";
            startTag.AddCssClass("btn btn-default");
            result.Append(startTag.ToString());

            // Предыдущая страница
            TagBuilder previousTag = new TagBuilder("a");
            int previousIndex = pagingInfo.CurrentPage - 1;
            if (previousIndex >= 1)
            {
                previousTag.MergeAttribute("href", pageUrl(previousIndex));
            }
            else
            {
                previousTag.MergeAttribute("href", "#");
            }
            previousTag.InnerHtml = "<";
            previousTag.AddCssClass("btn btn-default");
            result.Append(previousTag.ToString());

            // Диапазон из 2 * range + 1 (текущая страница) страниц
            for (int i = pagingInfo.CurrentPage - leftRange; i <= pagingInfo.CurrentPage + rightRange; i++)
            {
                // Помечаем "..." крайние элементы диапазона если leftRange или leftRange > range
                if ((i == pagingInfo.CurrentPage - leftRange && pagingInfo.CurrentPage > range) || 
                    (i == pagingInfo.CurrentPage + rightRange && pagingInfo.CurrentPage <= pagingInfo.TotalPages - range))
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", "#");
                    tag.InnerHtml = "...";
                    tag.AddCssClass("btn btn-default");
                    result.Append(tag.ToString());
                }
                else
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.MergeAttribute("href", pageUrl(i));
                    tag.InnerHtml = i.ToString();
                    // Добавляем оформление если номер страницы совпадает с текущим
                    if (i == pagingInfo.CurrentPage)
                    {
                        tag.AddCssClass("selected");
                        tag.AddCssClass("btn-primary");
                    }
                    tag.AddCssClass("btn btn-default");
                    result.Append(tag.ToString());
                }
            }
            
            // Следующая страница
            TagBuilder nextTag = new TagBuilder("a");
            int nextIndex = pagingInfo.CurrentPage + 1;
            if (nextIndex <= pagingInfo.TotalPages)
            {
                nextTag.MergeAttribute("href", pageUrl(nextIndex));
            }
            else
            {
                nextTag.MergeAttribute("href", "#");
            }
            nextTag.InnerHtml = ">";
            nextTag.AddCssClass("btn btn-default");
            result.Append(nextTag.ToString());

            // В конец
            TagBuilder endTag = new TagBuilder("a");
            endTag.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
            endTag.InnerHtml = ">>";
            endTag.AddCssClass("btn btn-default");
            result.Append(endTag.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}