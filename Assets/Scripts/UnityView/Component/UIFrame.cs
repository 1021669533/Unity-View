using UnityEngine;

namespace UnityView.Component
{
    public struct UIFrame
    {
        public Vector2 Origin;
        public Vector2 Size;

        public UIFrame(Vector2 origin, Vector2 size)
        {
            Origin = origin;
            Size = size;
        }

        public float Width
        {
            get { return Size.x; }
        }

        public float Height
        {
            get { return Size.y; }
        }
        public UIFrame(float originX, float originY, float width, float height)
        {
            Origin = new Vector2(originX * UICanvas.DesignUnit, originY * UICanvas.DesignUnit);
            Size = new Vector2(width * UICanvas.DesignUnit, height * UICanvas.DesignUnit);
        }
    }
}
