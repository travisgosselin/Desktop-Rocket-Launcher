using System.Collections.Generic;
using System.Web.Http;

using RocketLauncher.Host.Logic;
using RocketLauncher.Host.Logic.Model;

namespace RocketLauncher.Host.Web.Api
{
    public class SequenceController : ApiController
    {
        private readonly SequenceService _service = new SequenceService();

        [Route("api/launcher/{launcherName}/sequence")]
        public IEnumerable<Sequence> Get(string launcherName) 
        {
            return _service.GetSequences(launcherName);
        }

        [Route("api/launcher/{launcherName}/sequence")]
        public void Post(string launcherName, Sequence sequence)
        {
            _service.Add(sequence);
        }

        [HttpDelete]
        [Route("api/launcher/{launcherName}/sequence/{id}")]
        public void Delete(string launcherName, string id)
        {
            _service.Remove(launcherName, id);
        }
    }
}