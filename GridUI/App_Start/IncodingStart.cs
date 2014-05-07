using GridUI;
using GridUI.Controllers;
using GridUI.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(IncodingStart), "PreStart")]

namespace GridUI {
    public static class IncodingStart {
        public static void PreStart() {
            Bootstrapper.Start();
            new DispatcherController(); // init routes
        }
    }
}