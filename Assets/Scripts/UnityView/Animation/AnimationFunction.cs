using UnityEngine;

namespace UnityView.Animation
{
    public interface IAnimationFunction
    {
        float Solve(float x);
    }
    /// <summary>
    /// 一个动画函数将把输入的 [0, 1] 范围的值映射为输出一个 [0, 1] 范围的值
    /// </summary>
    public abstract class AnimationFunction : IAnimationFunction
    {
        public float DomainX = 1f;
        public float DomainY = 1f;

        public float Solve(float x)
        {
            return Calculate(x * DomainX) / DomainY;
        }
        protected abstract float Calculate(float x);
    }

    public abstract class TimeBasedFunction : IAnimationFunction
    {
        public abstract float Solve(float x);
    }
    // 线性函数
    public class AnimationFunctionLinear : AnimationFunction
    {
        protected override float Calculate(float x)
        {
            return x;
        }
    }
    // 渐入渐出函数
    public class AnimationFunctionEaseInOut : AnimationFunction
    {
        public AnimationFunctionEaseInOut()
        {
            DomainX = 1f;
            DomainY = 0.5f;
        }

        protected override float Calculate(float x)
        {
            if (x < 0.5f) return x * x;
            //if (x < 1.5f) return x - 0.25f;
            x = DomainX - x;
            return DomainY - x * x;
        }
    }
    // 渐入函数
    public class AnimationFunctionEaseIn : AnimationFunction
    {
        public AnimationFunctionEaseIn()
        {
            DomainX = 2f;
            DomainY = 1.75f;
        }

        protected override float Calculate(float x)
        {
            if (x < 0.5f) return x * x;
            return x - 0.25f;
        }
    }
    // 渐出函数
    public class AnimationFunctionEaseOut : AnimationFunction
    {
        public AnimationFunctionEaseOut()
        {
            DomainX = 2f;
            DomainY = 1.75f;
        }
        protected override float Calculate(float x)
        {
            if (x < 1.5f) return x;
            return 1.5f + x * x;
        }
    }
    // 弹簧函数
    public class AnimationFunctionSpring : AnimationFunction
    {
        public readonly float Beta;
        public readonly float T;
        public AnimationFunctionSpring(float beta = 1f, float t = 2f)
        {
            DomainX = 6;
            DomainY = 1;
            Beta = beta;
            T = t;
        }
        protected override float Calculate(float x)
        {
            return 1 - Mathf.Exp(-x * Beta) * Mathf.Cos(Mathf.PI * T * x);
        }
    }
}
