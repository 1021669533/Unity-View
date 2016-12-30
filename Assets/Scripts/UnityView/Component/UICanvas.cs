using UnityEngine;
using UnityEngine.UI;

namespace UnityView.Component
{
    public class UICanvas : UILayout
    {
        private static UICanvas _instance;

        public static float CanvasWidth { get; protected set; }
        public static float CanvasHeight { get; protected set; }

        public static float DesignUnit { get; protected set; }
        // 设计宽度，宽度小于高度时等分720，宽度大于高度时等分1280
        public const float DesignWidth = 720f;
        // 设计高度，以设计单位长度为基准的高度值
        public static float DesignHeight { get; protected set; }
        // 字体缩放系数
        public static float FontCoefficient { get; protected set; }

        static UICanvas()
        {
            CanvasWidth = Screen.width;
            CanvasHeight = Screen.height;
            DesignUnit = CanvasWidth / DesignWidth;
            DesignHeight = CanvasHeight / DesignUnit;
        }

        public Canvas Canvas;
        public CanvasScaler CanvasScaler;
        public GraphicRaycaster GraphicRaycaster;
        public static UICanvas GetInstance()
        {
            return _instance ??
                   (_instance = new UICanvas());
        }

        private UICanvas() : base(Object.FindObjectOfType<Canvas>().gameObject ?? new GameObject("Canvas"))
        {
            Canvas = UIObject.GetComponent<Canvas>() ?? UIObject.AddComponent<Canvas>();
            CanvasScaler = UIObject.GetComponent<CanvasScaler>() ?? UIObject.AddComponent<CanvasScaler>();
            GraphicRaycaster = UIObject.GetComponent<GraphicRaycaster>() ?? UIObject.AddComponent<GraphicRaycaster>();
            UpdateParameters();
        }

        public static void Reload()
        {
            Reload(false);
        }
        public static void Reload(bool force)
        {
            _instance = new UICanvas();
            _instance.UpdateParameters();
        }

        public static void Discard()
        {
            _instance = null;
        }

        protected void UpdateParameters()
        {
            Rect rect = GetRect(RectTransform);
            CanvasWidth = rect.width;
            CanvasHeight = rect.height;

            //UnitLength = Mathf.Min(rect.width, rect.height) / 720f;
            //UnitWidth = CanvasWidth / (CanvasWidth > CanvasHeight ? 1280f : 720f);
            //UnitHeight = CanvasHeight / (CanvasWidth > CanvasHeight ? 720f : 1280f);

            DesignUnit = CanvasWidth / DesignWidth;
            DesignHeight = CanvasHeight / DesignUnit;

            FontCoefficient = CanvasHeight / 720f;
        }

        public static float FromDesignLength(float n)
        {
            return n * DesignUnit;
        }
    }
}
