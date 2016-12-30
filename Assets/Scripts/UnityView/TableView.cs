using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityView
{
    // 表格视图，每一行（列）的大小均相等，可支持水平与垂直布局
    public class TableView : AbsAdapterView<IAdapter>
    {
        public Alignment Alignment;
        public TableView()
        {
            BounceEnable = true;
        }

        public override Orentation Orentation
        {
            get { return base.Orentation; }
            set
            {
                if (Orentation == value) return;
                VisibleItemCount = value == Orentation.Vertical
                    ? Mathf.CeilToInt(Height / TableItemSize) : Mathf.CeilToInt(Width / TableItemSize);
                foreach (var convertView in VisibleItems)
                {
                    switch (value)
                    {
                        case Orentation.Horizontal:
                            convertView.GetRectTransform().sizeDelta = new Vector2(TableItemSize, Height);
                            break;
                        case Orentation.Vertical:
                            convertView.GetRectTransform().sizeDelta = new Vector2(Width, TableItemSize);
                            break;
                    }
                }
                switch (value)
                {
                    case Orentation.Horizontal:
                        ScrollRect.horizontal = true;
                        ScrollRect.vertical = false;
                        break;
                    case Orentation.Vertical:
                        ScrollRect.horizontal = false;
                        ScrollRect.vertical = true;
                        break;
                }
                VisibleItemCount = GetVisibleItemCount();
                ContentTransform.anchoredPosition = Vector2.zero;
                Reload();
                base.Orentation = value;
            }
        }

        protected float TableItemSize = Mathf.Min(Screen.width, Screen.height) * 0.125f;
        public float ItemSize
        {
            get { return TableItemSize; }
            set
            {
                TableItemSize = value;
                CacheUpdate();
                Reload();
            }
        }

        public override float Width
        {
            get { return base.Width; }
            protected set
            {
                base.Width = value;
                if (Orentation == Orentation.Horizontal) VisibleItemCount = Mathf.CeilToInt(value / TableItemSize) + 1;
            }
        }

        public override float Height
        {
            get { return base.Height; }
            protected set
            {
                base.Height = value;
                if (Orentation == Orentation.Vertical) VisibleItemCount = Mathf.CeilToInt(value / TableItemSize) + 1;
            }
        }

        public override void CacheUpdate()
        {
            base.CacheUpdate();
            VisibleItemCount = Orentation == Orentation.Vertical
                ? Mathf.CeilToInt(Height / TableItemSize) : Mathf.CeilToInt(Width / TableItemSize);
            VisibleItemCount = VisibleItemCount + 1;
            foreach (var convertView in VisibleItems)
            {
                switch (Orentation)
                {
                    case Orentation.Horizontal:
                        convertView.GetRectTransform().sizeDelta = new Vector2(TableItemSize, Height);
                        break;
                    case Orentation.Vertical:
                        convertView.GetRectTransform().sizeDelta = new Vector2(Width, TableItemSize);
                        break;
                }
            }
        }

        public override void ScrollToTop(int index)
        {
            ContentTransform.anchoredPosition = Orentation == Orentation.Vertical
                ? new Vector2(0, ItemSize * index)
                : new Vector2(ItemSize * index, 0);
            StartIndex = GetStartIndex();
            VisibleItemCount = GetVisibleItemCount();
        }

        public override UILayout HeaderView
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public override UILayout FooterView
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public override int GetVisibleItemCount()
        {
            int v = 0;
            switch (Orentation)
            {
                case Orentation.Vertical:
                    v = (int)(Height / ItemSize) + 2;
                    break;
                case Orentation.Horizontal:
                    v = (int)(Width / ItemSize) + 2;
                    break;
            }
            if (StartIndex + v > CacheSize) v = CacheSize - StartIndex;
            return v;
        }

        protected override Vector2 GetItemAnchorPostion(int index)
        {
            switch (Orentation)
            {
                case Orentation.Horizontal:
                    return new Vector2(index * ItemSize, 0);
                case Orentation.Vertical:
                    return new Vector2(0, -index * ItemSize);
            }
            return Vector2.zero;
        }

        protected override int GetStartIndex()
        {
            return
                GetStartIndex(Orentation == Orentation.Vertical
                    ? ContentTransform.anchoredPosition.y
                    : -ContentTransform.anchoredPosition.x);
        }
        protected int GetStartIndex(float position)
        {
            if (position < 0 || CacheSize == 0) return 0;
            var startIndex = Mathf.FloorToInt(position / TableItemSize);
            if (startIndex > CacheSize - 1) startIndex = CacheSize - 1;
            return startIndex;
        }

        public override ILayout GetItem(int index)
        {
            if (index < VisibleItems.Count)
            {
                ILayout convertView = VisibleItems[index];
                if (false == convertView.GetRectTransform().gameObject.activeSelf)
                {
                    convertView.GetRectTransform().gameObject.SetActive(true);
                }
                return VisibleItems[index];
            }
            else
            {
                ILayout convertView = Adapter.GetConvertView(index, null);
                var transform = convertView.GetRectTransform();
                transform.SetParent(ContentTransform);
                transform.pivot = transform.anchorMin = transform.anchorMax = new Vector2(0, 1);
                switch (Orentation)
                {
                    case Orentation.Horizontal:
                        transform.sizeDelta = new Vector2(TableItemSize, Height);
                        break;
                    case Orentation.Vertical:
                        transform.sizeDelta = new Vector2(Width, TableItemSize);
                        break;
                }
                VisibleItems.Add(convertView);
                return convertView;
            }
        }

        protected override Vector2 CalculateContentSize()
        {
            switch (Orentation)
            {
                case Orentation.Vertical:
                    ContentTransform.sizeDelta = new Vector2(Width, TableItemSize * CacheSize);
                    break;
                case Orentation.Horizontal:
                    ContentTransform.sizeDelta = new Vector2(TableItemSize * CacheSize, Height);
                    break;
            }
            return ContentTransform.sizeDelta;
        }

        public override void OnItemClick(PointerEventData eventData)
        {
            OnItemSelectedListener(Mathf.FloorToInt((Screen.height - eventData.position.y - Origin.y - HeaderView.Height + ContentTransform.anchoredPosition.y) / ItemSize));
        }
    }
}
