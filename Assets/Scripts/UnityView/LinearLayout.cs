using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityView
{
    public enum LinearWrapMode
    {
        MatchParent,
        WrapContent
    }

    public class LinearLayoutInfo : ILayout
    {
        public readonly ILayout AttachView;
        public int Weight { set; get; }

        public LinearLayoutInfo(ILayout view, int weight)
        {
            AttachView = view;
            Weight = weight;
        }

        public RectTransform GetRectTransform()
        {
            return AttachView.GetRectTransform();
        }

        public void Destory()
        {
            AttachView.Destory();
        }

        public float Width
        {
            get { return AttachView.Width; }
        }

        public float Height
        {
            get { return AttachView.Height; }
        }
    }

    public class LinearLayout : UIView
    {
        public Orentation Orentation;

        protected float ContentCacheSize = 0;

        public LinearLayout(UILayout layout) : base(layout)
        {

        }

        public void AddSubview(ILayout view)
        {
            AddSubview(view, -1);
        }

        public void AddSubview(ILayout view, int weight)
        {
            Subviews.Add(new LinearLayoutInfo(view, weight));

            var rt = view.GetRectTransform();
            Rect rect = GetRect(rt);
            rt.SetParent(RectTransform);
            rt.anchorMin = rt.anchorMax = UIConstant.TopLeftVector2;
            rt.sizeDelta = rect.size;

            switch (Orentation)
            {
                case Orentation.Vertical:
                    rt.anchoredPosition = new Vector2(view.Width * 0.5f, -view.Height * 0.5f - ContentCacheSize);
                    ContentCacheSize += rect.height;
                    RectTransform.sizeDelta = new Vector2(Width, ContentCacheSize);

                    break;
                case Orentation.Horizontal:
                    break;
            }
        }

        public void AddSubview(ILayout view, LayoutParams parameters)
        {
            
        }

        public struct LayoutParams
        {
            public int Weight;
            public LinearWrapMode WrapMode;
        }
    }
}
