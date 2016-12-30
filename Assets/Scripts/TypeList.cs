using UnityEngine;
using UnityView;

namespace Assets.Scripts
{
    public class TypeListAdapter : IListAdapter
    {
        public string[] Types;
        public UIGroup<UILayout> Group; 

        public TypeListAdapter(string[] strings, UIGroup<UILayout> group)
        {
            Types = strings;
            Group = group;
        }
        public int GetCount()
        {
            return Types.Length;
        }

        public ILayout GetConvertView(int position, ILayout convertView)
        {
            ButtonView cell = convertView as ButtonView;
            if (cell == null)
            {
                cell = new ButtonView();
                cell.FontSize = 18;
                cell.AddListener(() =>
                {
                    Group.Select(position);
                });
                UILayout.AddUnderLine(cell.GetRectTransform(), Screen.height / 720f, Color.white);
            }
            cell.Title = "Part" + (position + 1) + "." + Types[position];
            return cell;
        }

        public float GetItemSize(int index)
        {
            return Screen.height * 0.1f;
        }
    }
}
