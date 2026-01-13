using QMVC;

namespace Sheep
{
    public class HomeController : MonoController
    {
        HomeView _view;


        public override void Init()
        {
            _view.RegisterBeginPressedEvent(OnBeginPressed);
        }


        private void OnBeginPressed()
        {
            this.SendCommand<InitLevelCommand>();
        }


    }
}


