using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemRegistry : MonoBehaviour
{
    static Dictionary<string, ItemData> itemRegistry = new Dictionary<string, ItemData>();
    static bool init_ = false;

    static void init() {
        var items = Resources.LoadAll("Item");

        foreach(var item in items) {
            ItemData i = item as ItemData;
            itemRegistry.Add(i.id, i);
        }

        init_ = true;
    }

    public static ItemData getItem(string id) {
        if(!init_) init();

        ItemData value = ScriptableObject.CreateInstance(typeof(ItemData)) as ItemData;
        itemRegistry.TryGetValue(id, out value);
        return value;
    }
}
