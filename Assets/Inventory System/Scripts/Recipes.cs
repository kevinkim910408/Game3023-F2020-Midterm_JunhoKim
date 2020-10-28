using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "ScriptableObjects/Recipes", order = 3)]
public class Recipes : ScriptableObject
{
    [SerializeField]
    public Item[] components;

    [SerializeField]
    public Item result;

}
