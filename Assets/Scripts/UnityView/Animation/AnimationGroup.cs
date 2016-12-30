using System.Collections.Generic;
using UnityEngine;

namespace UnityView.Animation
{
    public class AnimationGroup
    {
        protected float Tick = 0;
        public readonly float Duration;
        public readonly float Delay;
        public readonly float FinishTime;
        public readonly AnimationFunction Function;
        public readonly List<IAnimationAction> Actions = new List<IAnimationAction>();

        protected AnimationCompletion OnFinish;
        public AnimationGroup(float duration, float delay, AnimationFunction function, AnimationCompletion completion)
        {
            Duration = duration;
            Delay = delay;
            Function = function;
            FinishTime = delay + duration;
            OnFinish = completion;
        }
        public void Update()
        {
            Tick += Time.deltaTime;
            if (Tick < Delay) return;
            float p = (Tick - Delay) / Duration;
            if (p > 1) p = 1;
            p = Function.Solve(p);
            for (int i = 0; i < Actions.Count; i++)
            {
                Actions[i].Update(p);
            }
        }

        public bool IsFinish()
        {
            return Tick >= FinishTime;
        }

        public void Finish()
        {
            foreach (var action in Actions)
            {
                action.Finish();
            }
            if (OnFinish != null) OnFinish();
        }
    }
}
