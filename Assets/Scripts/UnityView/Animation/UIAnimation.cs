using System;
using UnityEngine;
using UnityView.Component;

namespace UnityView.Animation
{
    public enum AnimationEase
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInEaseOut,
        Spring
    }

    public delegate void AnimationDelegate();

    public delegate void AnimationCompletion();
    public class UIAnimation
    {
        public const float DefaultDuration = 0.25f;

        protected static bool Animating = false;

        public static bool OnAnimate()
        {
            return Animating;
        }

        public static AnimationFunctionLinear Linear = new AnimationFunctionLinear();
        public static AnimationFunctionEaseIn EaseIn = new AnimationFunctionEaseIn();
        public static AnimationFunctionEaseOut EaseOut = new AnimationFunctionEaseOut();
        public static AnimationFunctionEaseInOut EaseInOut = new AnimationFunctionEaseInOut();
        public static AnimationFunctionSpring Spring = new AnimationFunctionSpring();

        public static AnimationGroup CurrentGroup;
        public static void Animate(AnimationDelegate animation)
        {
            Animate(animation, DefaultDuration, 0, AnimationEase.Linear, null);
        }
        public static void Animate(AnimationDelegate animation, AnimationCompletion completion)
        {
            Animate(animation, DefaultDuration, 0, AnimationEase.Linear, completion);
        }

        public static void Animate(AnimationDelegate animation, float duration, float delay = 0)
        {
            Animate(animation, duration, delay, AnimationEase.Linear, null);
        }

        public static void Animate(AnimationDelegate animation, float duration, float delay, AnimationEase ease, AnimationCompletion completion)
        {
            AnimationFunction function = null;
            switch (ease)
            {
                case AnimationEase.Linear:
                    function = Linear;
                    break;
                case AnimationEase.EaseIn:
                    function = EaseIn;
                    break;
                case AnimationEase.EaseOut:
                    function = EaseInOut;
                    break;
                case AnimationEase.EaseInEaseOut:
                    function = EaseInOut;
                    break;
                case AnimationEase.Spring:
                    function = Spring;
                    break;
            }
            Animate(animation, duration, delay, function, completion);
        }
        public static void Animate(AnimationDelegate animation, float duration, float delay, AnimationFunction function, AnimationCompletion completion)
        {
            CurrentGroup = new AnimationGroup(duration, delay, function, completion);
            BeginAnimate();
            animation();
            EndAnimate();
        }

        public static void BeginAnimate()
        {
            Animating = true;
        }

        public static void EndAnimate()
        {
            Animating = false;
            UIViewManager.GetInstance().AppendAnimation(CurrentGroup);
            CurrentGroup = null;
        }

        public static void Append(IAnimationAction action)
        {
            CurrentGroup.Actions.Add(action);
        }
    }

    public class AnimatableAttribute : Attribute
    {

    }
}
