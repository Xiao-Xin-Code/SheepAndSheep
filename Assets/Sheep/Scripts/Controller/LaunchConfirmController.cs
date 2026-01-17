using Frame;
using QMVC;
using UnityEngine;

namespace Sheep
{
    public class LaunchConfirmController : MonoController
    {
        [SerializeField] LaunchConfirmView _view;

        LevelModel _levelModel;
        AssetSystem _assetSystem;

        LevelSystem _levelSystem;
        PoolSystem _poolSystem;

        public override void Init()
        {
            _levelModel = this.GetModel<LevelModel>();
            _assetSystem = this.GetSystem<AssetSystem>();
            _poolSystem = this.GetSystem<PoolSystem>();
            _levelSystem = this.GetSystem<LevelSystem>();

            _view.RegisterLaunchPressedEvent(OnLaunchPressed);
            _view.RegisterClosePressedEvent(OnClosePressed);
            this.RegisterEvent<JoinEvent>(JoinCallBack);

			MonoService.Instance.AddUpdateListener(() =>
			{
				if (Input.GetKeyDown(KeyCode.R))
				{
                    //ªÿ ’
                    _poolSystem.RecycleAllBlock();
                    _levelSystem.ClearBlocks();
					OnLaunchPressed();
				}
			});

			gameObject.SetActive(false);
		}


        private void JoinCallBack(JoinEvent evt)
        {
            gameObject.SetActive(true);
        }


        private void OnLaunchPressed()
        {
            this.SendCommand(new LaunchTransitionCommand(Hide));
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


