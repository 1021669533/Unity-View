using UnityEngine;
using UnityView;
using UnityView.Animation;
using UnityView.Component;

namespace Assets.Scripts
{
    public class Part5 : UILayout
    {
        public ButtonView RotateToView;

        public ButtonView RotateAngleView;

        public ButtonView EaseInOutView;

        public ButtonView TextColorView;

        public ButtonView ImageColorView;

        public ButtonView RectView;

        public ButtonView ScaleView;

        public ButtonView SpringView;

        public Part5()
        {
            UIRect = UICreator.MainRect;
            Name = "Animation Panel";

            RotateToView = new ButtonView(this);
            RotateToView.UIRect = new UIRect(10, 10, 100, 100);
            RotateToView.Title = "Rotate to";
            RotateToView.BackgroundColor = Color.gray;
            RotateToView.AddListener(RotateTo);

            RotateAngleView = new ButtonView(this);
            RotateAngleView.UIRect = new UIRect(130, 10, 100, 100);
            RotateAngleView.Title = "旋转45度";
            RotateAngleView.BackgroundColor = Color.gray;
            RotateAngleView.AddListener(RotateAngle);

            EaseInOutView = new ButtonView(this);
            EaseInOutView.UIRect = new UIRect(250, 10, 100, 100);
            EaseInOutView.Title = "渐入渐出旋转";
            EaseInOutView.BackgroundColor = Color.gray;
            EaseInOutView.AddListener(EaseInOutRotate);

            TextColorView = new ButtonView(this);
            TextColorView.UIRect = new UIRect(10, 130, 100, 100);
            TextColorView.Title = "文字颜色渐变";
            TextColorView.BackgroundColor = Color.gray;
            TextColorView.AddListener(TextColorAnimation);

            ImageColorView = new ButtonView(this);
            ImageColorView.UIRect = new UIRect(130, 130, 100, 100);
            ImageColorView.Title = "背景颜色渐变";
            ImageColorView.BackgroundColor = Color.gray;
            ImageColorView.AddListener(ImageColorAnimation);

            RectView = new ButtonView(this);
            RectView.UIRect = new UIRect(10, 250, 100, 100);
            RectView.Title = "变形位移动画";
            RectView.BackgroundColor = Color.gray;
            RectView.AddListener(TransformAnimation);

            ScaleView = new ButtonView(this);
            ScaleView.UIRect = new UIRect(10, 370, 100, 100);
            ScaleView.Title = "缩放动画";
            ScaleView.BackgroundColor = Color.gray;
            ScaleView.AddListener(ScaleAnimation);

            SpringView = new ButtonView(this);
            SpringView.UIRect = new UIRect(130, 370, 100, 100);
            SpringView.Title = "弹簧动画";
            SpringView.BackgroundColor = Color.gray;
            SpringView.AddListener(SpringAnimation);
        }

        public void RotateTo()
        {
            // 动画持续1秒
            RotateToView.Rotation = Quaternion.identity;
            UIAnimation.Animate(() =>
            {
                RotateToView.Rotation = Quaternion.Euler(new Vector3(0, 0, 75));
            }, 1);
        }

        public void RotateAngle()
        {
            // 默认为0.25秒
            UIAnimation.Animate(() =>
            {
                RotateAngleView.RotateAngle(45);
            });
        }

        public void EaseInOutRotate()
        {
            // 动画持续1秒，延时0.2秒
            UIAnimation.Animate(() =>
            {
                EaseInOutView.RotateAngle(1800);
            }, 2, 0.2f, AnimationEase.EaseInEaseOut, null);
        }
        // 文字渐变
        public void TextColorAnimation()
        {
            UIAnimation.Animate(() =>
            {
                TextColorView.TitleColor = TextColorView.TitleColor == Color.black ? Color.white : Color.black;
            }, 0.5f);
        }
        // 背景渐变
        public void ImageColorAnimation()
        {
            UIAnimation.Animate(() =>
            {
                ImageColorView.BackgroundColor = ImageColorView.BackgroundColor == Color.black ? Color.gray : Color.black;
            }, 0.5f);
        }

        public void TransformAnimation()
        {
            UIAnimation.Animate(() =>
            {
                RectView.UIRect = new UIRect(310, 250, 200, 100);
            }, 1, 0, AnimationEase.EaseInEaseOut, () =>
            {
                UIAnimation.Animate(() =>
                {
                    RectView.UIRect = new UIRect(10, 250, 100, 100);
                }, 1, 0, AnimationEase.EaseInEaseOut, null);
            });
        }
        public void ScaleAnimation()
        {
            UIAnimation.Animate(() =>
            {
                ScaleView.Scale = ScaleView.Scale == Vector3.one ? 0.5f * Vector3.one : Vector3.one;
            }, 0.1f, 0, AnimationEase.EaseInEaseOut, ScaleAnimation);
        }
        // 弹簧动画，可通过调整 AnimationFunctionSpring 的参数实现不同特性的弹簧效果
        public void SpringAnimation()
        {
            SpringView.UIRect = new UIRect(130, 370, 100, 100);
            UIAnimation.Animate(() =>
            {
                SpringView.UIRect = new UIRect(300, 370, 100, 100);
            }, 1f, 0, AnimationEase.Spring, null);
        }
    }
}
