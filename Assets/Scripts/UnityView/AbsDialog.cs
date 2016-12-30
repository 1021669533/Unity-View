using UnityEngine;

namespace UnityView
{
    public abstract class AbsDialog : UILayout
    {
        // 背景按钮
        public ButtonView BackgroundButton;
        // 是否开启区域外点击消失
        //private bool _enableOutboundDismiss = false;
        //public bool EnableOutboundDismiss
        //{
        //    set
        //    {
        //        if (value)
        //        {
        //            BackgroundButton.OnClickListener.RemoveListener(Destory);
        //            BackgroundButton.OnClickListener.AddListener(Destory);
        //        }
        //        else
        //        {
        //            BackgroundButton.OnClickListener.RemoveAllListeners();
        //        }
        //        _enableOutboundDismiss = value;
        //    }
        //    get
        //    {
        //        return _enableOutboundDismiss;
        //    }
        //}

        public UIView ContentView;

        public static float AppearanceWidth = 240f;
        public static float AppearanceHeight = 125f;

        protected AbsDialog() : this(AppearanceWidth, AppearanceHeight)
        {
        }

        protected AbsDialog(float width, float height)
        {
            Name = "Dialog";
            RectFill(RectTransform);
            BackgroundButton = new ButtonView(this) { UIObject = { name = "Background View" } };
            RectFill(BackgroundButton);
            BackgroundButton.BackgroundColor = Color.clear;
            ContentView = new UIView(this);
            ContentView.Name = "Content View";
            ContentView.UIRect = Component.UIRect.Center(width, height);
        }

        public virtual void Show()
        {
            UIObject.SetActive(true);
        }

        public virtual void Dismiss()
        {
            UIObject.SetActive(false);
        }
    }
}
