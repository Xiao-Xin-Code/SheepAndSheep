using QMVC;

namespace Sheep
{
    public enum GamePattern
    {
        Common
    }


    public class DataModel : AbstractModel
    {
        public BindableProperty<bool> MusicIsOn = new BindableProperty<bool>(true);
        public BindableProperty<bool> SfxIsOn = new BindableProperty<bool>(true);
        public BindableProperty<bool> ShakeIsOn = new BindableProperty<bool>(false);

        public BindableProperty<string> Theme = new BindableProperty<string>("Default");

        public bool skipFirstLevel = false;

        public BindableProperty<GamePattern> _GamePattern = new BindableProperty<GamePattern>(GamePattern.Common);

        public string[] levelPaths;


		protected override void OnInit()
        {
            
        }
    }
}


