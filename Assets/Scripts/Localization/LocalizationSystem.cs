using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem
{
    // Languages
    public enum Language {
        English
    }

    public static Language language = Language.English; // The Current Language

    private static Dictionary<string, string> localizedEN;

    public static bool isInit;

    // Initializes The System By Loading The CSV File And Generating The Dictionaries
    public static void Init() {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.loadCSV();
        
        localizedEN = csvLoader.getDictionaryValues("en");

        isInit = true;
    }

    // Returns The Localized Version Of An Id Based On The Current Language
    public static string getLocalizedValue(string key) {
        if(!isInit) {Init();}

        string value = key;

        switch(language) {
            case Language.English:
                localizedEN.TryGetValue(key, out value);
                break;
        }
        return value;
    }
}
