using UnityEngine;
using UnityView.Component;

namespace UnityView
{
    public struct UIToastTransaction
    {
        public float LifeTime;
    }

    public class UIToast : ButtonView
    {
        public const float ToastDisplayDuration = 2f;

        public static Rect AppearanceRect = new Rect(0.25f, 0.3f, 0.5f, 0.2f);
        public static Color AppearanceBackgroundColor = new Color(0, 0, 0, 0.7f);
        public static Color AppearanceTextColor = Color.white;

        private float _lifeTime;
        public float LifeTime
        {
            set
            {
                _lifeTime = value;
                FadeTime = value < UIConstant.AnimationDuration * 2 + 0.5f ? 0 : UIConstant.AnimationDuration;
            }
            get
            {
                return _lifeTime;
            }
        }
        public float LifeTick = 0;

        protected float FadeTime = UIConstant.AnimationDuration;

//        public override Color BackgroundColor
//        {
//            set
//            {
//                base.BackgroundColor = value;
//                OriginalBackgroundColor = value;
//            }
//            get
//            {
//                return base.BackgroundColor;
//            }
//        }
        public Color TextColor
        {
            set
            {
                OriginalTextColor = value;
                TitleTextComponent.color = value;
            }
            get
            {
                return TitleTextComponent.color;
            }
        }

        protected Color OriginalBackgroundColor;
        protected Color OriginalTextColor;

        protected UIToast(string text, float lifeTime)
        {
            UIObject.SetActive(false);
            Title = text;
            BackgroundColor = AppearanceBackgroundColor;
            OriginalBackgroundColor = AppearanceBackgroundColor;
            TextColor = AppearanceTextColor;
            LifeTime = lifeTime;
            Rect = AppearanceRect;
            FontSize = 25;
        }
        public static void MakeToast(string text)
        {
            MakeToast(text, AppearanceRect);
        }

        public static void MakeToast(string text, Rect rect, float lifeTime = ToastDisplayDuration)
        {
            UIToast toast = new UIToast(text, lifeTime) {Rect = rect};
            UIViewManager.GetInstance().ShowToast(toast);
        }

        // 更新一次时间状态，返回值为该Toast的生命是否结束
        public bool UpdateLifeTime(float delta)
        {
            LifeTick += delta;
            if (LifeTick < FadeTime)
            {
                BackgroundColor = new Color(OriginalBackgroundColor.r, OriginalBackgroundColor.g, OriginalBackgroundColor.b,
                    LifeTick / FadeTime * OriginalBackgroundColor.a);
                TitleTextComponent.color = new Color(OriginalTextColor.r, OriginalTextColor.g, OriginalTextColor.b, LifeTick / FadeTime * OriginalTextColor.a);
            }
            else if (LifeTime - LifeTick < FadeTime)
            {
                float alpha = (LifeTime - LifeTick) / FadeTime * OriginalBackgroundColor.a;
                BackgroundColor = new Color(OriginalBackgroundColor.r, OriginalBackgroundColor.g, OriginalBackgroundColor.b,
                    alpha);
                TitleTextComponent.color = new Color(OriginalTextColor.r, OriginalTextColor.g, OriginalTextColor.b, alpha);
            }
            else
            {
                BackgroundColor = OriginalBackgroundColor;
                TitleTextComponent.color = OriginalTextColor;
            }

            LifeTick += delta;
            if (LifeTick < LifeTime) return false;
            Destory();
            return true;
        }
    }
}
