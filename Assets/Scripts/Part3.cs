using UnityEngine;
using UnityView;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Part3 : ListView
    {
        public Part3()
        {
            Debug.Log(UICreator.MainRect.ToString());
            UIRect = UICreator.MainRect;
        }
    }

    public class Part3Adapter : IListAdapter
    {
        public readonly Color[] RandomColor = new Color[50];
        public readonly float[] RandomHeight = new float[50];
        public Part3Adapter()
        {
            for (int i = 0; i < 50; i++)
            {
                RandomColor[i] = Random.ColorHSV();
                RandomHeight[i] = 50f + Random.value * 100f;
            }
        }
        public int GetCount()
        {
            return 50;
        }

        public ILayout GetConvertView(int position, ILayout convertView)
        {
            ButtonView cell = convertView as ButtonView;
            if (cell == null)
            {
                cell = new ButtonView();
                cell.FontSize = 25;
                cell.TitleTextComponent.alignment = TextAnchor.MiddleCenter;
            }
            cell.Title = "Item:" + position;
            cell.BackgroundColor = RandomColor[position];
            return cell;
        }

        public float GetItemSize(int index)
        {
            return RandomHeight[index];
        }
    }
}
