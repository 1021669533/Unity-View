using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityView.Event;

namespace UnityView
{
    public abstract class AbsAdapterView<T> : UIView, IPointerClickHandler where T : IAdapter
    {
        // 缓存的元素个数
        protected int CacheSize = 20;
        // 滚动方向
        public virtual Orentation Orentation
        {
            get
            {
                return ScrollRect.vertical ? Orentation.Vertical : Orentation.Horizontal;
            }
            set
            {
                if (Orentation == value) return;
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
            }
        }
        // 是否开启边界弹性
        public bool BounceEnable
        {
            set
            {
                ScrollRect.movementType = value ? ScrollRect.MovementType.Elastic : ScrollRect.MovementType.Clamped;
            }
            get
            {
                return ScrollRect.movementType == ScrollRect.MovementType.Clamped;
            }
        }
        // 间隔
        public Vector2 Spacing = Vector2.zero;

        public Mask Mask;

        protected ScrollRect ScrollRect;
        protected RectTransform ContentTransform;

        // 滚动到指定元素的顶端
        public abstract void ScrollToTop(int index);

        // 获取内容区域的大小
        public Vector2 ContentSize
        {
            get { return ContentTransform.sizeDelta; }
        }

        public virtual float ItemAnchorPosition
        {
            get
            {
                switch (Orentation)
                {
                    case Orentation.Horizontal:
                        return ContentTransform.anchoredPosition.x + HeaderView.Width;
                    default:
                        return ContentTransform.anchoredPosition.y + HeaderView.Height;
                }
            }
        }

        public List<ILayout> CacheItems = new List<ILayout>();
        public List<ILayout> VisibleItems = new List<ILayout>();

        protected int LastStartIndex = 0;

        // 当前显示的元素个数
        protected virtual int CurrentShowItemCount
        {
            get
            {
                int showItemCount = CacheSize - StartIndex;
                return VisibleItemCount < showItemCount ? VisibleItemCount : showItemCount;
            }
        }

        protected PointerClickHandler ClickEventHandler;
        public OnItemSelectedListener OnItemSelectedListener;

        protected T ListAdapter;
        public virtual T Adapter
        {
            get
            {
                return ListAdapter;
            }
            set
            {
                ListAdapter = value;
                CacheSize = value.GetCount();
                GetVisibleItemCount();
                CalculateContentSize();
                RepositionItems();
            }
        }

        public abstract UILayout HeaderView { get; set; }

        public virtual float HeaderViewSize 
        {
            get
            {
                if (HeaderView == null) return 0;
                return Orentation == Orentation.Vertical ? HeaderView.Height : HeaderView.Width;
            }
        }
        public abstract UILayout FooterView { get; set; }
        public virtual float FooterViewSize
        {
            get
            {
                if (FooterView == null) return 0;
                return Orentation == Orentation.Vertical ? FooterView.Height : FooterView.Width;
            }
        }

        protected AbsAdapterView()
        {
            ScrollRect = UIObject.AddComponent<ScrollRect>();
            ScrollRect.onValueChanged.AddListener(OnScroll);
            ClickEventHandler = UIObject.AddComponent<PointerClickHandler>();
            ClickEventHandler.AbsAdapterView = this;
            var contentView = new UILayout { UIObject = { name = "Content View" } };
            AddSubview(contentView);
            ContentTransform = contentView.RectTransform;
            ContentTransform.pivot = ContentTransform.anchorMin = ContentTransform.anchorMax = new Vector2(0, 1);
            ContentTransform.anchoredPosition = Vector2.zero;
            ScrollRect.content = ContentTransform;
            ScrollRect.vertical = true;
            ScrollRect.horizontal = false;
            BackgroundColor = Color.black;
            Mask = UIObject.AddComponent<Mask>();
            Mask.showMaskGraphic = false;
        }

        public virtual void CacheUpdate() { }
        // 当前起始元素的索引
        protected int StartIndex = 0;
        // ScrollView滚动事件
        protected virtual void OnScroll(Vector2 position)
        {
            StartIndex = GetStartIndex();
            if (StartIndex < 0)
            {
                StartIndex = 0;
                return;
            }
            // 计算起始索引
            if (StartIndex == LastStartIndex)
            {
                var vc = VisibleItemCount;
                VisibleItemCount = GetVisibleItemCount();
                if (vc != VisibleItemCount)
                {
                    RepositionItems();
                }
            }
            else
            {
                RepositionItems();
                LastStartIndex = StartIndex;
            }
        }

        public float VisibleItemHeight { get; protected set; }
        public int VisibleItemCount { get; protected set; }
        public abstract int GetVisibleItemCount();
        protected abstract Vector2 GetItemAnchorPostion(int index);
        protected abstract int GetStartIndex();
        public abstract ILayout GetItem(int index);

        // 重载表视图的数据
        public virtual void RepositionItems()
        {
            if (Adapter == null) return;
            if (StartIndex < 0) StartIndex = 0;
            for (int i = 0; i < CurrentShowItemCount; ++i)
            {
                var item = GetItem(i);
                item.GetRectTransform().anchoredPosition = GetItemAnchorPostion(StartIndex + i);
                // 更新View
                Adapter.GetConvertView(StartIndex + i, item);
            }
            HideNonuseableItems();
        }

        public virtual void Reload()
        {
            ScrollRect.StopMovement();
            if (Adapter == null) return;
            CacheSize = Adapter.GetCount();
            StartIndex = GetStartIndex();
            CalculateContentSize();
            int count = Mathf.Min(VisibleItemCount, CacheSize);
            if (VisibleItems.Count > count)
            {
                for (int i = count; i < VisibleItems.Count; i++)
                {
                    if (VisibleItems[i].GetRectTransform().gameObject.activeSelf)
                    {
                        VisibleItems[i].GetRectTransform().gameObject.SetActive(false);
                    }
                }
            }
            for (int i = 0; i < count; ++i)
            {
                var item = GetItem(i);
                var transform = item.GetRectTransform();
                transform.pivot = transform.anchorMin = transform.anchorMax = new Vector2(0, 1);
                transform.anchoredPosition = GetItemAnchorPostion(i + StartIndex);
                Adapter.GetConvertView(i + StartIndex, item);
            }
            HideNonuseableItems();
        }

        public virtual void HideNonuseableItems()
        {
            for (int i = CurrentShowItemCount; i < VisibleItems.Count; i++)
            {
                GameObject convertView = VisibleItems[i].GetRectTransform().gameObject;
                if (convertView.activeSelf)
                {
                    convertView.SetActive(false);
                }
            }
        }

        // 计算ContentSize并进行赋值
        protected abstract Vector2 CalculateContentSize();
        public void OnPointerClick(PointerEventData eventData)
        {
            OnItemClick(eventData);
        }

        public abstract void OnItemClick(PointerEventData eventData);
    }
    public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public IPointerClickHandler AbsAdapterView;
        public void OnPointerClick(PointerEventData eventData)
        {
            AbsAdapterView.OnPointerClick(eventData);
        }
    }

    public interface IAdapter
    {
        int GetCount();
        ILayout GetConvertView(int position, ILayout convertView);
    }
    // 列表视图（高度可变）适配器
    public interface IListAdapter : IAdapter
    {
        float GetItemSize(int index);
    }
    // 表格视图适配器
    public interface IGridAdapter : IAdapter
    {
        Vector2 GetItemSize(int index);
    }
    // 单元表视图适配器
    public interface ISectionListAdapter : IListAdapter
    {

    }
    // 高度可变的列表适配器
    public interface IElasticAdapterView
    {
        void OnItemSizeChanged();
        void OnItemSizeChanged(int index);
    }
}
