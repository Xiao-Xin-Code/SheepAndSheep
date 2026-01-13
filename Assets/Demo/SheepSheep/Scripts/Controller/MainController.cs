using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace SheepSheep
{

    public class MainController : MonoController
    {
        #region UI

        [SerializeField] Button joinInBtn;

        
        [SerializeField] RectTransform slider;

        #endregion


        GridsManagerSystem gridsSystem;



        protected override void Init()
        {
            gridsSystem = this.GetSystem<GridsManagerSystem>();
            joinInBtn.onClick.AddListener(JoinInPressed);
        }




        public void UpdateProgressBar()
        {

        }


        public void JoinInPressed()
        {
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(0, 0), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(0, 4), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(0, 8), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(4, 0), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(4, 4), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(4, 8), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(8, 0), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(8, 4), new Vector2(0, -0.15f), 1);
            gridsSystem.CreateHeadUpBlock(2, new Vector2Int(8, 8), new Vector2(0, -0.15f), 1);

            gameObject.SetActive(false);
        }
    }

}

