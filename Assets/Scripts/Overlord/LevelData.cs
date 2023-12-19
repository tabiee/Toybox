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
        // Use reflection to get the corresponding property in LevelData
        PropertyInfo property = typeof(LevelData).GetProperty(objectTag);

        if (property != null && property.PropertyType == typeof(int))
        {
            // Get the current value of the property
            return (int)property.GetValue(null);
        }
        else
        {
            Debug.LogError("Property not recognized or property not found in LevelData.");
            return 0; // or another default value
        }
    }
    public static void SetCreatureValue(string objectTag, int value)
    {
        // Use reflection to get the corresponding property in LevelData
        PropertyInfo property = typeof(LevelData).GetProperty(objectTag);

        if (property != null && property.PropertyType == typeof(int))
        {
            // Set the value of the property
            property.SetValue(null, value);
        }
        else
        {
            Debug.LogError("Property not recognized or property not found in LevelData.");
        }
    }
}
