using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityView
{
    // 可展开的TableView
    public class ExpandableTableView : AbsAdapterView<IListAdapter>
    {
        public override void ScrollToTop(int index)
        {
            throw new NotImplementedException();
        }

        public override UILayout HeaderView
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override UILayout FooterView
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int GetVisibleItemCount()
        {
            throw new NotImplementedException();
        }

        protected override Vector2 GetItemAnchorPostion(int index)
        {
            return Vector2.zero;
        }

        protected override int GetStartIndex()
        {
            return 0;
        }

        public override void HideNonuseableItems()
        {
            throw new NotImplementedException();
        }

        public override ILayout GetItem(int index)
        {
            throw new NotImplementedException();
        }

        protected override Vector2 CalculateContentSize()
        {
            throw new NotImplementedException();
        }

        public override void OnItemClick(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
