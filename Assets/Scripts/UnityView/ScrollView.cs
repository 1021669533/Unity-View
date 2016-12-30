using UnityEngine;
using UnityEngine.UI;

namespace UnityView
{
    public class ScrollView : UIView
    {
        public ScrollRect ScrollRect;
        public LinearLayout ContentView;
        public RectTransform ContentTransform;

        public Orentation Orentation { set; get; }

        protected float AnchorHeight = 0;

        public Mask Mask;

        public ScrollView() : base()
        {
            ScrollRect = UIObject.AddComponent<ScrollRect>();
            ScrollRect.vertical = true;
            ScrollRect.horizontal = false;
            ContentView = new LinearLayout(this);
            ContentTransform = ContentView.RectTransform;
            ContentView.Name = "Content View";
            ContentTransform.pivot = ContentTransform.anchorMin = ContentTransform.anchorMax = new Vector2(0, 1);
            ContentTransform.anchoredPosition = Vector2.zero;
            ScrollRect.content = ContentView.RectTransform;

            BackgroundColor = Color.black;
            Mask = UIObject.AddComponent<Mask>();
        }

        public new void AddSubview(UILayout view)
        {
            ContentView.AddSubview(view);
            //switch (Orentation)
            //{
            //    case Orentation.Vertical:

            //        ContentView.AddSubview(view);
            //        break;
            //    case Orentation.Horizontal:
            //        AnchorHeight += view.Width;
            //        break;
            //}
        }

        public void Reload()
        {

        }

        public void ScrollToTop()
        {
            ContentView.RectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
