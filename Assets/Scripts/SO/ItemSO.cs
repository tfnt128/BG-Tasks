using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    //begin pre-written code
    public int ID => GetInstanceID();
    //end pre-written code

    [field: SerializeField]
    public Sprite ItemImage { get; set; }
    [field: SerializeField]
    public string ItemName;
    [field: SerializeField]
    public string ItemDescription;

    [field: SerializeField]
    public int itemValue;

}
