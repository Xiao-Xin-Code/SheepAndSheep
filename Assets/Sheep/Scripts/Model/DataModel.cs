using QMVC;

namespace Sheep
{

    public class DataModel : AbstractModel
    {
        public BindableProperty<bool> MusicIsOn = new BindableProperty<bool>(true);
        public BindableProperty<bool> SfxIsOn = new BindableProperty<bool>(true);
        public BindableProperty<bool> ShakeIsOn = new BindableProperty<bool>(false);

        public BindableProperty<string> Theme = new BindableProperty<string>();


        protected override void OnInit()
        {
            
        }
    }
}


