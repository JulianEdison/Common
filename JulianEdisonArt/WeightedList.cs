using System;
using System.Collections.Generic;

namespace JulianEdisonArt
{
    [Serializable]
    public class WeightedList<T>
    {
        public List<WeightedListItem<T>> Items = new();

        public List<T> ToList()
        {
            List<T> newList = new();
            foreach (WeightedListItem<T> item in Items)
                newList.AddRange(item.WeightedList());
            return newList;
        }

        public T Random()
        {
            return ToList().Random();
        }

        public bool Empty => Items.Count == 0;
        public bool NotEmpty => !Empty;
    }

    [Serializable]
    public class WeightedListItem<T>
    {
        public T Item;
        public int Weight = 1;

        public WeightedListItem() { }

        public WeightedListItem(T item, int weight)
        {
            Item = item;
            Weight = weight;
        }

        public List<T> WeightedList()
        {
            List<T> newList = new();
            for (int n = 0; n < Weight; n++)
                newList.Add(Item);
            return newList;
        }

        public static List<T> FullyWeightedList(List<WeightedListItem<T>> items)
        {
            List<T> newList = new();
            foreach (WeightedListItem<T> item in items)
                newList.AddRange(item.WeightedList());
            return newList;
        }
    }


}