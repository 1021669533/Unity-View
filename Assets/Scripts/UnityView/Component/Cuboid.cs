using System.Text;
using UnityEngine;
using UnityView.Tools;

namespace UnityView.Component
{
    public struct Cuboid
    {
        public float OriginX;
        public float OriginY;
        public float OriginZ;
        public float Length;
        public float Width;
        public float Height;
        public Vector3 Origin
        {
            get
            {
                return new Vector3(OriginX, OriginY, OriginZ);
            }
        }

        public Vector3 Size
        {
            get
            {
                return new Vector3(Length, Width, Height);
            }
        }

        public Cuboid(Vector3 origin, Vector3 size)
        {
            OriginX = origin.x;
            OriginY = origin.y;
            OriginZ = origin.z;
            Length = size.x;
            Height = size.y;
            Width = size.z;
        }
        public Cuboid(Vector3 origin, float length, float height, float width)
        {
            OriginX = origin.x;
            OriginY = origin.y;
            OriginZ = origin.z;
            Length = length;
            Height = height;
            Width = width;
        }

        public Cuboid(float originX, float originY, float originZ, float length, float height, float width)
        {
            OriginX = originX;
            OriginY = originY;
            OriginZ = originZ;
            Length = length;
            Height = height;
            Width = width;
        }

        public static Cuboid operator +(Cuboid cuboid, Vector3 vector3)
        {
            cuboid.OriginX += vector3.x;
            cuboid.OriginY += vector3.y;
            cuboid.OriginZ += vector3.z;
            return cuboid;
        }

        public static Cuboid operator -(Cuboid cuboid, Vector3 vector3)
        {
            cuboid.OriginX -= vector3.x;
            cuboid.OriginY -= vector3.y;
            cuboid.OriginZ -= vector3.z;
            return cuboid;
        }

        public static bool Contains(Cuboid cuboid, Vector3 point)
        {
            return point.x.Between(cuboid.OriginX, cuboid.OriginX + cuboid.Length) &&
                   point.y.Between(cuboid.OriginY, cuboid.OriginY + +cuboid.Height) &&
                   point.z.Between(cuboid.OriginZ, cuboid.OriginZ + cuboid.Width);
        }

        public override string ToString()
        {
            var builder = new StringBuilder("Cuboid:");
            builder.Append("{ Origin:[ x:")
                .Append(OriginX)
                .Append(", y:")
                .Append(OriginY)
                .Append(", z:")
                .Append(OriginZ)
                .Append(" ], Length：")
                .Append(Length)
                .Append(", Width：")
                .Append(Width)
                .Append(" Height：")
                .Append(Height)
                .Append(" }");
            return builder.ToString();
        }
    }
}
