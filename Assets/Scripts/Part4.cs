using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityView;

namespace Assets.Scripts
{
    public class Part4 : GridView
    {
        public Part4()
        {
            UIRect = UICreator.MainRect;
            Spacing = new Vector2(25f, 25f);
        }

    }

    public class Part4Adapter : IAdapter
    {
        public int GetCount()
        {
            return 300;
        }

        public ILayout GetConvertView(int position, ILayout convertView)
        {
            ButtonView cell = convertView as ButtonView;
            if (cell == null)
            {
                cell = new ButtonView();
                cell.Name = "Cell " + position;
                cell.FontSize = 25;
                cell.BackgroundColor = new Color(1, 1, 1, 0.5f);
            }
            cell.Title = "Items:" + position;
            return cell;
        }
    }
}
