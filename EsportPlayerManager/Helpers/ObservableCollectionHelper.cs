using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserMenager.Helpers;

public static class ObservableCollectionHelper
{
    public static void AddRange<T>(this ObservableCollection<T> lista, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            lista.Add(item);
        }
    }
}