using UnityEngine;
using UnityEngine.UI;

namespace Sheep
{
    public class LevelView : BaseView
    {

        [SerializeField] Button setting;
        [SerializeField] Text dateText;
        
        public void SetDate(string date)
        {
            dateText.text = date;
        }
    }

}

