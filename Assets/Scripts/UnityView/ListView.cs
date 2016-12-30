using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityView.Collections;

namespace UnityView
{
    // 列表视图，每一行的高度可不相同，只支持垂直滚动
    public class ListView : AbsAdapterView<IListAdapter>, IElasticAdapterView
    {
        public ListView()
        {
            // 初始化数组缓存数组
            HeightCache = new float[CacheSize];
            AnchorCache = new float[CacheSize];
            for (int i = 0; i < CacheSize; i++)
            {
                HeightCache[i] = -1;
                AnchorCache[i] = -1;
            }
        }

        protected int AnchorStartIndex = 0;
        protected int AnchorEndIndex = 20;
        // 高度缓存数组
        protected float[] HeightCache;
        // 起点缓存数组
        protected float[] AnchorCache;

        public override void ScrollToTop(int index)
        {
            if (index < 0 || index >= AnchorCache.Length)
            {
                ContentTransform.anchoredPosition = Vector2.zero;
                return;
            }
            ContentTransform.anchoredPosition = new Vector2(0, AnchorCache[index]);
            StartIndex = GetStartIndex();
            VisibleItemCount = GetVisibleItemCount();
        }

        public override IListAdapter Adapter
        {
            get { return base.Adapter; }
            set
            {
                ListAdapter = value;
                CacheSize = Adapter.GetCount();
                if (HeightCache.Length < CacheSize)
                {
                    float[] heightArray = new float[CacheSize];
                    float[] anchorArray = new float[CacheSize];
                    HeightCache.CopyTo(heightArray, 0);
                    AnchorCache.CopyTo(anchorArray, 0);
                    HeightCache = heightArray;
                    AnchorCache = anchorArray;
                }
                VisibleItemCount = GetVisibleItemCount();
                float anchor = 0;
                for (int i = 0; i < CacheSize; i++)
                {
                    float height = Adapter.GetItemSize(i);
                    HeightCache[i] = height;
                    AnchorCache[i] = anchor;
                    anchor += height;
                }
                Reload();
            }
        }

        private UILayout _headerView;
        public override UILayout HeaderView
        {
            get { return _headerView; }
            set
            {
                _headerView = value;
                value.RectTransform.SetParent(ContentTransform);
                value.RectTransform.anchoredPosition = Vector2.zero;
                CalculateContentSize();
            }
        }

        private UILayout _footerView;
        public override UILayout FooterView
        {
            get { return _footerView; }
            set
            {
                _footerView = value;
                CalculateContentSize();
            }
        }

        public void OnItemSizeChanged()
        {
            float anchor = 0;
            for (int i = 0; i < CacheSize; i++)
            {
                float height = Adapter.GetItemSize(i);
                HeightCache[i] = height;
                AnchorCache[i] = anchor;
                anchor += height;
            }
        }

        public void OnItemSizeChanged(int index)
        {
            if (CacheSize <= index)
            {
                throw new IndexOutOfRangeException("改变的元素超过ListView的最大容量");
            }
            HeightCache[index] = Adapter.GetItemSize(index);
            float anchor = index > 0 ? AnchorCache[index - 1] : 0;
            for (int i = index; i < CacheSize; i++)
            {
                AnchorCache[index] = anchor;
                anchor += HeightCache[i];
            }
        }

        public override int GetVisibleItemCount()
        {
            if (CacheSize == 0) return 0;
            int v = 0;
            int startIndex = StartIndex;
            float height = 0;
            while (startIndex < CacheSize && height < Height)
            {
                height += HeightCache[startIndex];
                startIndex++;
                v++;
            }
            return v + 1;
        }

        protected override Vector2 GetItemAnchorPostion(int index)
        {
            return new Vector2(0, -AnchorCache[index]) - new Vector2(0, HeaderViewSize);
        }

        protected override int GetStartIndex()
        {
            return GetStartIndex(ContentTransform.anchoredPosition.y - HeaderViewSize);
        }
        protected int GetStartIndex(float position)
        {
            StartIndex = Algorithm.BinarySearchFloat(AnchorCache, 0, CacheSize, position);
            if (StartIndex < 0) StartIndex = 0;
            return StartIndex;
        }

        public override ILayout GetItem(int index)
        {
            if (index < VisibleItems.Count)
            {
                ILayout convertView = VisibleItems[index];
                var transform = convertView.GetRectTransform();
                if (false == transform.gameObject.activeSelf)
                {
                    transform.gameObject.SetActive(true);
                }
                transform.sizeDelta = new Vector2(Width, HeightCache[StartIndex + index]);
                return convertView;
            }
            else
            {
                ILayout convertView = Adapter.GetConvertView(index, null);
                var transform = convertView.GetRectTransform();
                transform.SetParent(ContentTransform);
                transform.pivot = transform.anchorMin = transform.anchorMax = new Vector2(0, 1);
                transform.sizeDelta = new Vector2(Width, HeightCache[StartIndex + index]);
                VisibleItems.Add(convertView);
                return convertView;
            }
        }

        protected override Vector2 CalculateContentSize()
        {
            if (CacheSize == 0) return Vector2.zero;
            ContentTransform.sizeDelta = new Vector2(Width, HeaderViewSize + FooterViewSize + AnchorCache[CacheSize - 1] + HeightCache[CacheSize - 1]);
            return ContentTransform.sizeDelta;
        }

        public override void OnItemClick(PointerEventData eventData)
        {

        }
    }
}
