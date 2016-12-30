using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityView.Component
{
    public struct UIBorder
    {
        public Color Color;
        public Vector2 Offset;
        public bool UseGraphicAlpha;

        public UIBorder(Color color, float offset, bool useGraphicAlpha = false)
        {
            Color = color;
            Offset = new Vector2(offset, offset);
            UseGraphicAlpha = useGraphicAlpha;
        }

        public UIBorder(Color color, Vector2 offset, bool useGraphicAlpha = false)
        {
            Color = color;
            Offset = offset;
            UseGraphicAlpha = useGraphicAlpha;
        }
    }
}
