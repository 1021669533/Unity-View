using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityView
{
    public class UIGroup<T> where T : UILayout
    {
        protected List<T> Items = new List<T>();

        public Action<T> ActionOn; 
        public Action<T> ActionOff;
        public UIGroup(Action<T> on, Action<T> off)
        {
            ActionOn = on;
            ActionOff = off;
        }

        public void Add(T t)
        {
            Items.Add(t);
        }

        public void Remove(T t)
        {
            Items.Remove(t);
        }

        public void Select(int index)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (index == i)
                    ActionOn(Items[i]);
                else
                    ActionOff(Items[i]);
            }
        }
        public void Select(T t)
        {
            Debug.Log(t);
            foreach (T item in Items)
            {
                if (item == t)
                {
                    ActionOn(item);
                }
                else
                {
                    ActionOff(item);
                }
            }
        }
    }
}
