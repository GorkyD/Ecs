using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssetProvider
{
    public List<T> LoadAll<T>(string path) where T : Object => 
        Resources.LoadAll<T>(path).ToList();

    public T Load<T>(string path) where T : Object => 
        Resources.Load<T>(path);
}
