using MiniServer;
using MiniServer.Results;
using MiniServer.Routing;
using Students.Controllers;

namespace Students
{
    public class Program
    {
        public static void Main(string[] args)
        {
           ServerRoutingTable routes= new ServerRoutingTable();
            routes.Add(HTTP.Enums.HttpRequestMethod.Get,"/",request=>new RedirectResults("/Home/Index"));
            routes.Add(HTTP.Enums.HttpRequestMethod.Get, "/Home/Index", request => new HomeController().Index(request));
            routes.Add(HTTP.Enums.HttpRequestMethod.Post, "/Home/Create", request => new HomeController().Create(request));
            routes.Add(HTTP.Enums.HttpRequestMethod.Get, "/Home/Update", request => new HomeController().Update(request));
            routes.Add(HTTP.Enums.HttpRequestMethod.Post, "/Home/Update", request => new HomeController().Update(request));
            routes.Add(HTTP.Enums.HttpRequestMethod.Get, "/Home/Delete", request => new HomeController().Delete(request));
            routes.Add(HTTP.Enums.HttpRequestMethod.Get, "/About/Index", request => new AboutController().Index(request));

            Server server = new Server(8000, routes);
            server.Run();
        }
    }
}
