using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity_Base.Hilfsklassen
{
    public class NavMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string pageName)
        {
            var links = GetLinksForPage(pageName);
            return View(links);
        }

        private Dictionary<string, string> GetLinksForPage(string pageName)
        {
            Dictionary<string, string> links = null;
            switch (pageName)
            {
                case "AML":
                    links = new Dictionary<string, string> {
                    { "Link 3", "AML" },
                };
                    break;

                case "SystemUnitClassLib":
                    links = new Dictionary<string, string> {
                    { "Link 3", "SystemUnitClassLib" },
                };

                    break;
            }

            return links;
        }
    }
}
