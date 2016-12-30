using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityView
{
    public class GridView : AbsAdapterView<IAdapter>
    {
        public int Row { get; protected set; }
        public int Column { get; protected set; }

        protected Vector2 GridItemSize = new Vector2(100, 100);

        public Vector2 ItemSize
        {
            get { return GridItemSize; }
            set
            {
                ScrollRect.StopMovement();
                GridItemSize = value;
                if (Adapter == null) return;
                CalculateContentSize();
                RepositionItems();
            }
        }

        public override void ScrollToTop(int index)
        {
            switch (Orentation)
            {
                case Orentation.Horizontal:
                    index = index/Row;
                    ContentTransform.anchoredPosition = new Vector2(index * (Spacing.x + ItemSize.x) + Spacing.x, 0);
                    break;
                case Orentation.Vertical:
                    index = index / Column;
                    ContentTransform.anchoredPosition = new Vector2(0, index * (Spacing.y + ItemSize.y) + Spacing.y);
                    break;
            }
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
                case Orentation.Horizontal:
                    v = (Mathf.CeilToInt((Width - Spacing.x) / (ItemSize.x + Spacing.x)) + 1) * Row;
                    break;
                case Orentation.Vertical:
                    v = (Mathf.CeilToInt((Height - Spacing.y) / (ItemSize.y + Spacing.y)) + 1) * Column;
                    break;
            }
            if (StartIndex + v > CacheSize) v = CacheSize - StartIndex;
            return v;
        }

        protected override Vector2 GetItemAnchorPostion(int index)
        {
            int offsetIndex = 0;
            switch (Orentation)
            {
                case Orentation.Horizontal:
                    offsetIndex = index % Row;
                    return new Vector2(offsetIndex * (ItemSize.x + Spacing.x) + Spacing.x, -(index / Row * (ItemSize.y + Spacing.y)) - Spacing.y);
                case Orentation.Vertical:
                    offsetIndex = index % Column;
                    return new Vector2(offsetIndex * (ItemSize.x + Spacing.x) + Spacing.x, -(index / Column * (ItemSize.y + Spacing.y)) - Spacing.y);
            }
            return Vector2.zero;
        }

        protected override int GetStartIndex()
        {
            float anchor;
            switch (Orentation)
            {
                case Orentation.Horizontal:
                    anchor = -ContentTransform.anchoredPosition.x;
                    StartIndex = (int)(anchor / (ItemSize.x + Spacing.x));
                    if (StartIndex < 0) StartIndex = 0;
                    if (StartIndex > Column - 1) StartIndex = Column - 1;
                    StartIndex *= Row;
                    break;
                case Orentation.Vertical:
                    anchor = ContentTransform.anchoredPosition.y;
                    StartIndex = (int)(anchor / (ItemSize.y + Spacing.y));
                    if (StartIndex < 0) StartIndex = 0;
                    if (StartIndex > Row - 1) StartIndex = Row - 1;
                    StartIndex *= Column;
                    break;
            }
            VisibleItemCount = GetVisibleItemCount();
            return StartIndex;
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
                RectTransform transform = convertView.GetRectTransform();
                transform.SetParent(ContentTransform);
                transform.pivot = transform.anchorMin = transform.anchorMax = new Vector2(0, 1);
                transform.sizeDelta = ItemSize;
                VisibleItems.Add(convertView);
                return convertView;
            }
        }

        protected override Vector2 CalculateContentSize()
        {
            switch (Orentation)
            {
                case Orentation.Horizontal:
                    // 水平滚动时，先确定行，再确定列
                    Row = (int)((Height - Spacing.y) / (ItemSize.y + Spacing.y));
                    Column = (CacheSize + Row - 1) / Row;
                    ContentTransform.sizeDelta = new Vector2(Column * (ItemSize.x + Spacing.x) + Spacing.x, Height);
                    break;
                case Orentation.Vertical:
                    // 竖直滚动时，先确定列，再确定行
                    Column = (int)((Width - Spacing.x) / (ItemSize.x + Spacing.x));
                    Row = (CacheSize + Column - 1) / Column;
                    ContentTransform.sizeDelta = new Vector2(Width, Row * (ItemSize.y + Spacing.y) + Spacing.y);
                    break;
            }
            VisibleItemCount = GetVisibleItemCount();
            return ContentSize;
        }

        public override void OnItemClick(PointerEventData eventData)
        {

        }
    }
}
