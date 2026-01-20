using QMVC;

namespace Sheep
{

    public class GameOverController : MonoController
    {
        public override void Init()
        {

            this.RegisterEvent<GameOverEvent>(OnGameOver);
            gameObject.SetActive(false);
        }




        private void OnGameOver(GameOverEvent evt)
        {
            gameObject.SetActive(false);
            //¼¤»îÊ×Ò³
        }
    }
}


