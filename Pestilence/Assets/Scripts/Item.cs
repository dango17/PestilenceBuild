using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DO
{
    public class Item : ScriptableObject
    {
        [Header("Item Infomation")]
        public Sprite itemIcon;
        public string itemName; 
    }
}

