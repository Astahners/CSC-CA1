using System.Web;
using System.Web.Mvc;

namespace CSC_Assignment1_Task4_TalentRestfulWebService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
