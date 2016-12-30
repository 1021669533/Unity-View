using UnityEngine;

namespace UnityView.Tools
{
    public static class UITools
    {
        // 根据字符串解析颜色
        public static Color ParseColor(string color)
        {
            return new Color();
        }
        // 根据三维向量解析颜色，透明度默认为1
        public static Color ParseColor(Vector3 vector3)
        {
            return new Color(vector3.x / 255f, vector3.y / 255f, vector3.z / 255f, 1f);
        }
    }

    public static class Extension
    {
        public static bool Between(this float num, float n1, float n2)
        {
            if (n1 > n2)
            {
                return num >= n2 && num <= n1;
            }
            return num >= n1 && num <= n2;
        }
    }
}
