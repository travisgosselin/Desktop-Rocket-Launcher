using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using RocketLauncher.Host.Logic;
using RocketLauncher.Host.Logic.Model;

namespace RocketLauncher.Host.Web.Api
{
    public class LauncherController : ApiController
    {
        private readonly LauncherService _service = new LauncherService();

        public IEnumerable<Launcher> Get()
        {
            return _service.GetLaunchers();
        }

        public void Post([FromBody]Launcher launcher)
        {
            _service.CreateLauncher(launcher.Name);
        }

        public void Delete(string id)
        {
            _service.RemoveLauncher(id);
        }
    }
}