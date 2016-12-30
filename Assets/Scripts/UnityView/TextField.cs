using UnityEngine;
using UnityEngine.UI;

namespace UnityView
{
    public class TextField : UIView
    {
        public readonly InputField InputComponent;

        public readonly TextView TextView;

        private TextView _placeHolderTextView;
        public TextView PlaceHolderTextView
        {
            get { return _placeHolderTextView; }
            private set
            {
                _placeHolderTextView = value;
                _placeHolderTextView.TextComponent.fontSize = FontSize;
                _placeHolderTextView.Alignment = Aligment;
            }
        }

        public TextAnchor Aligment
        {
            set
            {
                TextView.Alignment = value;
                if (_placeHolderTextView != null)
                {
                    _placeHolderTextView.Alignment = value;
                }
            }
            get { return TextView.Alignment; }
        }

        public int FontSize
        {
            set
            {
                TextView.FontSize = value;
                if (_placeHolderTextView != null)
                {
                    _placeHolderTextView.FontSize = value;
                }
            }
            get { return TextView.FontSize; }
        }

        public string PlaceHolder
        {
            set
            {
                if (PlaceHolderTextView == null)
                {
                    PlaceHolderTextView = new TextView(this);
                    RectFill(PlaceHolderTextView);
                    InputComponent.placeholder = PlaceHolderTextView.TextComponent;
                }
                PlaceHolderTextView.Text = value;
            }
            get
            {
                return PlaceHolderTextView.Text;
            }
        }
        public TextField()
        {
            InputComponent = UIObject.AddComponent<InputField>();
            TextView = new TextView(this);
            RectFill(TextView);

            // 关联的Text组件
            InputComponent.textComponent = TextView.TextComponent;
        }

        public TextField(UILayout layout) : base(layout)
        {
            InputComponent = UIObject.AddComponent<InputField>();
            TextView = new TextView(this);
            RectFill(TextView);
            InputComponent.textComponent = TextView.TextComponent;
        }

        public TextField(GameObject gameObject)
        {

        }
    }
}
