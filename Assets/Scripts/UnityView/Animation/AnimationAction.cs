using UnityEngine;
using UnityEngine.UI;
using UnityView.Component;

namespace UnityView.Animation
{
    public interface IAnimationAction
    {
        void Update(float p);
        void Finish();
    }
    // 缩放动画
    public class ScaleAction : IAnimationAction
    {
        public readonly Transform Transform;
        public readonly Vector3 FromValue;
        public readonly Vector3 ToValue;
        public readonly Vector3 D;
        public ScaleAction(Transform transform, Vector3 fromValue, Vector3 toValue)
        {
            Transform = transform;
            FromValue = fromValue;
            ToValue = toValue;
            D = toValue - fromValue;
        }
        public void Update(float p)
        {
            Transform.localScale = FromValue + D * p;
        }

        public void Finish()
        {
            Transform.localScale = ToValue;
        }
    }
    // 透明度动画
    public class AlphaAction : IAnimationAction
    {
        public readonly UIView View;
        public readonly float FromValue;
        public readonly float ToValue;
        public readonly float D;
        public AlphaAction(UIView view, float fromValue, float toValue)
        {
            View = view;
            FromValue = fromValue;
            ToValue = toValue;
            D = toValue - fromValue;
        }

        public void Update(float p)
        {
            View.Alpha = FromValue + D * p;
        }

        public void Finish()
        {
            View.Alpha = ToValue;
        }
    }
    public abstract class ColorAction : IAnimationAction
    {
        public readonly Color FromValue;
        public readonly Color ToValue;
        public readonly Vector4 D;

        protected ColorAction(Color fromValue, Color toValue)
        {
            FromValue = fromValue;
            ToValue = toValue;
            D = new Vector4(toValue.r - fromValue.r, toValue.g - fromValue.g, toValue.b - fromValue.b, toValue.a - fromValue.a);
        }
        public abstract void Update(float p);
        public abstract void Finish();
    }
    // Image渐变动画
    public class ImageColorAction : ColorAction
    {
        public readonly Image Image;
        public ImageColorAction(Image image, Color fromValue, Color toValue)
            : base(fromValue, toValue)
        {
            Image = image;
        }
        public override void Update(float p)
        {
            Vector4 d = D * p;
            Image.color = new Color(FromValue.r + d.x, FromValue.g + d.y, FromValue.b + d.z, FromValue.a + d.w);
        }
        public override void Finish()
        {
            Image.color = ToValue;
        }
    }
    // Text颜色渐变动画
    public class TextColorAction : ColorAction
    {
        public readonly Text Text;
        public TextColorAction(Text text, Color fromValue, Color toValue)
            : base(fromValue, toValue)
        {
            Text = text;
        }

        public override void Update(float p)
        {
            Vector4 d = D * p;
            Text.color = new Color(FromValue.r + d.x, FromValue.g + d.y, FromValue.b + d.z, FromValue.a + d.w);
        }

        public override void Finish()
        {
            Text.color = ToValue;
        }
    }

    // 矩形边框渐变动画
    public class RectAction : IAnimationAction
    {
        public readonly UILayout Layout;
        public readonly Vector2 FromMinAnchor;
        public readonly Vector2 FromMaxAnchor;
        public readonly Vector2 ToMinAnchor;
        public readonly Vector2 ToMaxAnchor;
        public readonly Vector2 DMin;
        public readonly Vector2 DMax;
        public readonly UIRect TargetRect;

        public RectAction(UILayout layout, UIRect target, Vector2 fromMinAchor, Vector2 fromMaxAnchor, Vector2 toMinAchor, Vector2 toMaxAnchor)
        {
            Layout = layout;
            TargetRect = target;
            FromMinAnchor = fromMinAchor;
            FromMaxAnchor = fromMaxAnchor;
            ToMinAnchor = toMinAchor;
            ToMaxAnchor = toMaxAnchor;
            DMin = toMinAchor - fromMinAchor;
            DMax = toMaxAnchor - fromMaxAnchor;
        }

        public void Update(float p)
        {
            Layout.RectTransform.anchorMin = FromMinAnchor + DMin * p;
            Layout.RectTransform.anchorMax = FromMaxAnchor + DMax * p;
            Layout.RectTransform.offsetMin = Layout.RectTransform.offsetMax = Vector2.zero;
        }

        public void Finish()
        {
            Layout.UIRect = TargetRect;
        }
    }
    // Frame动画
    public class FrameAction : IAnimationAction
    {
        public readonly RectTransform Transform;
        public readonly Vector2 FromPos;
        public readonly Vector2 FromSize;
        public readonly Vector2 DPos;
        public readonly Vector2 ToPos;
        public readonly Vector2 ToSize;
        public readonly Vector2 DSize;
        public FrameAction(UILayout layout, Vector2 fromPos, Vector2 fromSize, Vector2 toPos, Vector2 toSize)
        {
            Transform = layout.RectTransform;
            FromPos = fromPos;
            FromSize = fromSize;
            DPos = toPos - fromPos;
            ToPos = toPos;
            ToSize = toSize;
            DSize = toSize - fromSize;
        }
        public void Update(float p)
        {
            Transform.anchorMin = Transform.anchorMax = FromPos + DPos * p;
            Transform.sizeDelta = FromSize + DSize * p;
        }

        public void Finish()
        {
            Transform.anchoredPosition = ToPos;
            Transform.sizeDelta = ToSize;
        }
    }
    // 旋转动画
    public class RotateAction : IAnimationAction
    {
        public readonly UILayout Layout;
        public readonly Quaternion FromValue;
        public readonly Quaternion ToValue;
        public RotateAction(UILayout layout, Quaternion fromValue, Quaternion toValue)
        {
            Layout = layout;
            FromValue = fromValue;
            ToValue = toValue;
        }
        public void Update(float p)
        {
            Layout.Rotation = Quaternion.Lerp(FromValue, ToValue, p);
        }

        public void Finish()
        {
            Layout.Rotation = ToValue;
        }
    }

    public class RotateAngleAction : IAnimationAction
    {
        public readonly RectTransform Transform;
        public readonly float Angle;
        protected float Delta = 0;
        public RotateAngleAction(UILayout layout, Quaternion fromRotation, float angle)
        {
            Transform = layout.RectTransform;
            Angle = angle;
        }
        public void Update(float p)
        {
            Transform.Rotate(Vector3.forward, Angle * (p - Delta));
            Delta = p;
        }

        public void Finish()
        {
            //Transform.rotation = TargetRotation;
        }
    }
}
