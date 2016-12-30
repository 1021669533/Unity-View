using UnityEngine;
using UnityEngine.UI;
using UnityView.Component;

namespace UnityView
{
    public class ImageView : UIView
    {

        public Image.Type ImageType
        {
            get
            {
                return ImageComponent.type;
            }
            set
            {
                ImageComponent.type = value;
            }
        }

        public ImageView()
        {
            ImageComponent = UIObject.AddComponent<Image>();
        }

        public ImageView(UILayout layout) : base(layout) { }
    }
}
