using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class LevelData
{
    public static string levelToLoad { get; set; }
    public static string zoneToLoad { get; set; }
    public static int greenCreature { get; set; }
    public static int yellowCreature { get; set; }
    public static int redCreature { get; set; }
    public static int GetCreatureValue(string objectTag)
    {
        PropertyInfo property = typeof(LevelData).GetProperty(objectTag);

        if (property != null && property.PropertyType == typeof(int))
        {
            return (int)property.GetValue(null);
        }
        else
        {
            Debug.LogError("GetCreatureValue didnt work. fix shit");
            return 0;
        }
    }
    public static void SetCreatureValue(string objectTag, int value)
    {
        PropertyInfo property = typeof(LevelData).GetProperty(objectTag);

        if (property != null && property.PropertyType == typeof(int))
        {
            property.SetValue(null, value);
        }
        else
        {
            Debug.LogError("SetCreatureValue didnt work. fix shit");
        }
    }
}
