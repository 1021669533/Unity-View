using UnityEngine;
using UnityEngine.UI;
using UnityView.Animation;
using UnityView.Component;

namespace UnityView
{
    public class UIView : UILayout
    {
        public CanvasRenderer CanvasRenderer { get; set; }

        private Image _image;
        public Image ImageComponent
        {
            protected set { _image = value; } 
            get
            {
                if (_image == null)
                {
                    _image = UIObject.AddComponent<Image>();
                }
                return _image;
            }
        }
        [Animatable]
        public Color BackgroundColor
        {
            set
            {
                if (ImageComponent == null) return;
                if (UIAnimation.OnAnimate())
                {
                    UIAnimation.Append(new ImageColorAction(ImageComponent, ImageComponent.color, value));
                }
                else
                {
                    ImageComponent.color = value;
                }
            }
            get
            {
                return ImageComponent == null ? Color.clear : ImageComponent.color;
            }
        }

        [Animatable]
        public float Alpha
        {
            set
            {
                if (ImageComponent == null) return;
                if (UIAnimation.OnAnimate())
                {
                    UIAnimation.Append(new AlphaAction(this, ImageComponent.color.a, value));
                }
                else
                {
                    Color c = ImageComponent.color;
                    c.a = value;
                    ImageComponent.color = c;
                }
            }
            get
            {
                if (ImageComponent == null) return 0;
                return ImageComponent.color.a;
            }
        }

        protected Outline OutlineComponent;

        public UIBorder Border
        {
            get
            {
                return new UIBorder(OutlineComponent.effectColor, OutlineComponent.effectDistance, OutlineComponent.useGraphicAlpha);
            }
            set
            {
                if (OutlineComponent == null) OutlineComponent = UIObject.AddComponent<Outline>();
                OutlineComponent.effectColor = value.Color;
                OutlineComponent.effectDistance = value.Offset;
                OutlineComponent.useGraphicAlpha = value.UseGraphicAlpha;
            }
        }

        public Sprite Sprite
        {
            set
            {
                ImageComponent.sprite = value;
            }
            get
            {
                return ImageComponent.sprite;
            }
        }

        /// <summary>
        /// 构造方法
        /// 不传参时，默认以 Canvas 为父物体
        /// 传参时，以参数为父物体
        /// 也可根据传入的 gameObject 构造对象
        /// </summary>
        public UIView() : this(UIViewManager.GetInstance().RootView) { }

        public UIView(RectTransform transform) : base(transform)
        {
            CanvasRenderer = UIObject.AddComponent<CanvasRenderer>();
        }

        public UIView(UILayout layout) : base(layout)
        {
            CanvasRenderer = UIObject.AddComponent<CanvasRenderer>();
        }

        public UIView(GameObject gameObject) : base(gameObject)
        {
            CanvasRenderer = gameObject.GetComponent<CanvasRenderer>() ?? gameObject.AddComponent<CanvasRenderer>();
        }

        public static GameObject BaseView()
        {
            return BaseView("New View");
        }
        public static GameObject BaseView(string name)
        {
            var gameObject = BaseLayout(name);
            gameObject.AddComponent<CanvasRenderer>();
            return gameObject;
        }
    }
}
