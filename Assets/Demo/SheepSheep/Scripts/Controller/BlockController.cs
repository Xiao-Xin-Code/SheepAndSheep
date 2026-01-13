using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SheepSheep
{
    public class BlockController : MonoController, IPointerClickHandler
    {
        #region UI
        [SerializeField] SpriteRenderer bgRenderer;
        [SerializeField] SpriteRenderer iconRenderer;
        [SerializeField] SpriteRenderer maskRenderer;
        #endregion


        #region ÊôÐÔ
        private bool interactable = true;
        private string theme;
        private string content;
        private Vector2Int occupiedCells;

        public bool Interactable
        {
            get
            {
                return interactable;
            }
            set
            {
                interactable = value;
                maskRenderer.enabled = !value;
            }
        }
        public string Theme { get; set; }
        public string Content { get; set; }

        public Vector2Int[] OccupiedCells { get; set; }






        #endregion


        GridsManagerSystem gridsSystem;
        VesselSystem vesselSystem;


        protected override void Init()
        {
            gridsSystem = this.GetSystem<GridsManagerSystem>();
            vesselSystem = this.GetSystem<VesselSystem>();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (Interactable)
            {
                Debug.Log("µã»÷");
                gridsSystem.Remove(this);
                //vesselSystem.Place(this);

                this.SendCommand(new BlockClickCommand(this));
            }
        }

       
    }


    public static class BlockControllerExtension
    {
        public static bool TypeEquals(this BlockController self, BlockController other)
        {
            return self.Theme == other.Theme && self.Content == other.Content;
        }
    }
}

