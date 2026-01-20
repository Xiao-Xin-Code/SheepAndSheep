using QMVC;

namespace Sheep
{
    public class GameSucceedController : MonoController
    {
        public override void Init()
        {
            this.RegisterEvent<GameSucceedEvent>(OnGameSucceed);
            gameObject.SetActive(false);
        }


        private void OnGameSucceed(GameSucceedEvent evt)
        {
            gameObject.SetActive(true);
        }
    }
}


