using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCApp.Fliters
{
    public class HeaderFooterFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewResult vwResult = filterContext.Result as ViewResult;
            if (vwResult != null)
            {
                BaseViewModel bvm = vwResult.Model as BaseViewModel;
                if (bvm != null)
                {
                    bvm.FooterData = new FooterViewModel();
                    bvm.FooterData.CompanyName = "Aspire";
                    bvm.FooterData.Year = DateTime.Now.Year;
                    bvm.UserName = HttpContext.Current.User.Identity.Name;
                }
            }  
        }
    }
}