
using GridUI.Infrastructure;
using Incoding.MvcContrib.MVD;

namespace GridUI.Controllers
{
    public class DispatcherController : DispatcherControllerBase
    {
        public DispatcherController()
                : base(typeof(Bootstrapper).Assembly) { }
    }
}