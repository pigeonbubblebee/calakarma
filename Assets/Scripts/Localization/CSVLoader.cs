using System.Text.RegularExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader
{
    private TextAsset csvFile;
    private char lineSeperator = '\n';
    private char surround = '"';
    private string[] fieldSeperator = {"\",\""};

    // Loads The Localization File
    public void loadCSV() {
        csvFile = Resources.Load<TextAsset>("localization");
    }

    // Creates A Dictionary That Stores The Id To Its Correct Value
    public Dictionary<string, string> getDictionaryValues(string attributeId) {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string[] lines = csvFile.text.Split(lineSeperator);

        int attributeIndex = -1;
        string[] headers = lines[0].Split(fieldSeperator, StringSplitOptions.None);

        for(int i = 0; i < headers.Length; i++) {
            if(headers[i].Contains(attributeId)) {
                attributeIndex=i;
                break;
            }
        }
        
        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))"); // Creates Regular Expression To Split The Text

        for(int i = 1; i  < lines.Length;i++) {
            string line = lines[i];
            string[] fields = CSVParser.Split(line);

            for(int f = 0; f < fields.Length; f++) {
                fields[f] = fields[f].TrimStart(' ', surround);
                fields[f] = fields[f].TrimEnd(surround, (char)13);
            }

            if(fields.Length>attributeIndex) {
                var key = fields[0];

                if(dictionary.ContainsKey(key)) {continue;}

                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }
}
