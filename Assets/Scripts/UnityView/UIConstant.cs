using UnityEngine;

namespace UnityView
{
    public static class UIConstant
    {
        public const float Div2 = 0.5f;
        public const float Div3 = 1 / 3f;
        public const float Div4 = 0.25f;
        public const float Div5 = 0.2f;
        public const float Div6 = 1 / 6f;
        public const float Div7 = 1 / 7f;
        public const float Div8 = 0.125f;
        public const float Div9 = 1 / 9f;
        public const float Div10 = 0.1f;
        public const float Div11 = 1 / 11f;
        public const float Div12 = 1 / 12f;
        public const float Div13 = 1 / 13f;
        public const float Div14 = 1 / 14f;
        public const float Div15 = 1 / 15f;
        public const float Div16 = 1 / 16f;
        public const float Div17 = 1 / 17f;
        public const float Div18 = 1 / 18f;
        public const float Div19 = 1 / 19f;
        public const float Div20 = 0.05f;

        public static readonly float ScreenWidthDiv2 = Screen.width / 2f;
        public static readonly float ScreenWidthDiv4 = Screen.width / 4f;
        public static readonly float ScreenWidthDiv8 = Screen.width / 8f;
        public static readonly float ScreenWidthDiv16 = Screen.width / 16f;
        public static readonly float ScreenHeightDiv2 = Screen.height / 2f;
        public static readonly float ScreenHeightDiv4 = Screen.height / 4f;
        public static readonly float ScreenHeightDiv8 = Screen.height / 8f;
        public static readonly float ScreenHeightDiv16 = Screen.height / 16f;

        // 屏幕长宽比
        public static readonly float AspectRatio = Screen.height / (float)Screen.width;
        public static readonly float AntiAspectRatio = Screen.width / (float)Screen.height;

        public static readonly Vector2 TopLeftVector2 = new Vector2(0, 1);

        public const float AnimationDuration = 0.25f;

        public const int UILayer = 5;

        public static float AspectWidthToHeight(float aspect)
        {
            return aspect * AntiAspectRatio;
        }
        public static float AspectHeightToWidth(float aspect)
        {
            return aspect * AspectRatio;
        }

        public static float LengthWidthToHeight(float length)
        {
            return length * AspectRatio;
        }

        public static float LengthHeightToWidth(float length)
        {
            return length * AntiAspectRatio;
        }

        public static readonly float UnitScreenLength = Mathf.Min(Screen.width, Screen.height) / 720f;
        public static readonly float UnitScreeWidth = Screen.width > Screen.height ? 1280f : 720f;
        public static readonly float UnitScreeHeight = Screen.width > Screen.height ? 720f : 1280f;
    }
}
