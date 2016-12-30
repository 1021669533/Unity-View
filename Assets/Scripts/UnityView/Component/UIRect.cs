using System.Text;
using UnityEngine;

namespace UnityView.Component
{
    public struct UIRect
    {
        public Vector2 Origin;
        public Vector2 Size;

        public float Top
        {
            get { return Origin.y; }
        }

        public float Left
        {
            get { return Origin.x; }
        }

        public float Right
        {
            get { return Left + Width; }
        }

        public float Bottom
        {
            get { return Top + Height; }
        }

        public float Width
        {
            get { return Size.x; }
        }

        public float Height
        {
            get { return Size.y; }
        }
        
        public UIRect(float originX, float originY, float width, float height)
        {
            Origin = new Vector2(originX * UICanvas.DesignUnit, originY * UICanvas.DesignUnit);
            Size = new Vector2(width * UICanvas.DesignUnit, height * UICanvas.DesignUnit);
        }

        public static UIRect Center(float width, float height)
        {
            return new UIRect((UICanvas.DesignWidth - width) * 0.5f, (UICanvas.DesignHeight - height) * 0.5f, width, height);
        }

        public override string ToString()
        {
            return
                new StringBuilder("( ").Append(Left)
                    .Append(", ")
                    .Append(Top)
                    .Append(", ")
                    .Append(Width)
                    .Append(", ")
                    .Append(Height)
                    .Append(" )")
                    .ToString();
        }
    }
}
