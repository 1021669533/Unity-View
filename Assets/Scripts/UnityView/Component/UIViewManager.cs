using System.Collections.Generic;
using UnityEngine;
using UnityView.Animation;

namespace UnityView.Component
{
    /// <summary>
    /// View 管理器，存放管理视图的基本信息
    /// </summary>
    public class UIViewManager : MonoBehaviour
    {
        public GameObject Canvas
        {
            get
            {
                return UICanvas.GetInstance().UIObject;
            }
        }

        public UILayout RootView
        {
            get { return UICanvas.GetInstance(); }
        }

        public Font Font;

        private static UIViewManager _instance;

        public static readonly float UnitWidth = Screen.width / 1280f;
        public static readonly float UnitHeight = Screen.height / 720f;

        void Awake()
        {
            _instance = this;
        }

        public static UIViewManager GetInstance()
        {
            return _instance ?? FindObjectOfType<UIViewManager>();
        }

        public static void Discard()
        {
            UICanvas.Discard();
            _instance = null;
        }

        // 弹出提示管理
        protected Queue<UIToast> ToastQueue = new Queue<UIToast>();
        protected UIToast CurrentToast = null;
        public void ShowToast(UIToast toast)
        {
            ToastQueue.Enqueue(toast);
        }

        public static void DiscardToast()
        {
            if (_instance == null) return;
            _instance.ToastQueue.Clear();
            _instance.CurrentToast = null;
        }

        protected void ToastUpdate()
        {
            if (CurrentToast == null)
            {
                if (ToastQueue.Count <= 0) return;
                CurrentToast = ToastQueue.Dequeue();
                CurrentToast.UIObject.SetActive(true);
                CurrentToast.BackgroundColor = new Color(CurrentToast.BackgroundColor.r,
                    CurrentToast.BackgroundColor.g, CurrentToast.BackgroundColor.b, 0);
                CurrentToast.TitleTextComponent.color = new Color(CurrentToast.TitleTextComponent.color.r,
                    CurrentToast.TitleTextComponent.color.g, CurrentToast.TitleTextComponent.color.b, 0);
            }
            else
            {
                if (CurrentToast.UpdateLifeTime(Time.deltaTime)) CurrentToast = null;
            }
        }

        // 动画管理
        protected List<AnimationGroup> AliveAnimation = new List<AnimationGroup>();
        protected List<AnimationGroup> ObsoleteAnimation = new List<AnimationGroup>();

        public void AppendAnimation(AnimationGroup group)
        {
            AliveAnimation.Add(group);
        }
        public void Update()
        {
            ToastUpdate();
            foreach (var group in AliveAnimation)
            {
                group.Update();
                if (group.IsFinish()) ObsoleteAnimation.Add(group);
            }
            if (ObsoleteAnimation.Count > 0)
            {
                foreach (var group in ObsoleteAnimation)
                {
                    AliveAnimation.Remove(group);
                    group.Finish();
                }
                ObsoleteAnimation.Clear();
            }
        }
    }
}
