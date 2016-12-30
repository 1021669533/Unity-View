using UnityEngine;
using UnityEngine.UI;
using UnityView.Component;

namespace UnityView
{
    public class TextView : UIView
    {
        public readonly Text TextComponent;

        public string Text
        {
            set
            {
                TextComponent.text = value;
            }
            get
            {
                return TextComponent.text;
            }
        }

        public Color TextColor
        {
            set { TextComponent.color = value; }
            get { return TextComponent.color; }
        }

        public Font Font
        {
            set
            {
                TextComponent.font = value;
            }
            get
            {
                return TextComponent.font;
            }
        }

        public int FontSize
        {
            set
            {
                TextComponent.fontSize = (int)(value * UICanvas.FontCoefficient);
            }
            get
            {
                return TextComponent.fontSize;
            }
        }

        public TextAnchor Alignment
        {
            set
            {
                TextComponent.alignment = value;
            }
            get
            {
                return TextComponent.alignment;
            }
        }

        public TextView() : this(UIViewManager.GetInstance().RootView) { }

        public TextView(UILayout layout)
            : base(layout)
        {
            TextComponent = UIObject.AddComponent<Text>();
            TextComponent.alignment = TextAnchor.MiddleLeft;
            Font = UIViewManager.GetInstance().Font;
        }

        public TextView(GameObject gameObject)
            : base(gameObject)
        {
            TextComponent = gameObject.GetComponent<Text>() ?? gameObject.AddComponent<Text>();
            Font = UIViewManager.GetInstance().Font;
        }

        public static GameObject BaseTextView()
        {
            var gameObject = BaseView();
            var text = gameObject.AddComponent<Text>();
            text.font = UIViewManager.GetInstance().Font;
            text.alignment = TextAnchor.MiddleCenter;
            return gameObject;
        }

        public static int NormalizedFontSize(float fontSize)
        {
            return Mathf.RoundToInt(fontSize * UICanvas.FontCoefficient);
        }
    }
}
