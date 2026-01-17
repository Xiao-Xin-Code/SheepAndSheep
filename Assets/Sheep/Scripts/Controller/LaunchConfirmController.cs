using JetBrains.Annotations;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LaunchConfirmController : MonoController
    {
        [SerializeField] LaunchConfirmView _view;

        LevelModel _levelModel;
        AssetSystem _assetSystem;


        public override void Init()
        {
            _levelModel = this.GetModel<LevelModel>();
            _assetSystem = this.GetSystem<AssetSystem>();

            _view.RegisterLaunchPressedEvent(OnLaunchPressed);
            _view.RegisterClosePressedEvent(OnClosePressed);
            this.RegisterEvent<JoinEvent>(JoinCallBack);

            gameObject.SetActive(false);
        }


        private void JoinCallBack(JoinEvent evt)
        {
            gameObject.SetActive(true);
        }


        private void OnLaunchPressed()
        {
            this.SendCommand(new LaunchTransitionCommand(Hide));
            //初始化数据
            _levelModel.isLevelOver = false;
            string[] level = _assetSystem.GetLevel();
            Debug.Log(level.Length + level[0]);
            Debug.Log(level[0].Split('#').Length);
            string[] split = level[0].Split('#')[1].Split('|');
            _levelModel.blockCount = int.Parse(split[0]);
            _levelModel.width = int.Parse(split[1].Split(',')[0]);
            _levelModel.height = int.Parse(split[1].Split(',')[1]);
            _levelModel.center = new Vector2(int.Parse(split[2].Split(',')[0]), int.Parse(split[2].Split(',')[1]));
            _levelModel.level = level;

			this.SendCommand<LaunchLevelCommand>();
        }

        private void OnClosePressed()
        {
            gameObject.SetActive(false);
        }


        private void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}


