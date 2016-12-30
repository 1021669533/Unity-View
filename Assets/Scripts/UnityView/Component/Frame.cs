using UnityEngine;

namespace UnityView.Component
{
    public struct Frame
    {
        public Vector2 Origin;
        public Vector2 Size;

        public Frame(Vector2 origin, Vector2 size)
        {
            Origin = origin;
            Size = size;
        }

        public Frame(float originX, float originY, float width, float height)
        {
            Origin = new Vector2(originX, originY);
            Size = new Vector2(width, height);
        }
    }
}
