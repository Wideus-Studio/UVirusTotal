using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace Game.Saves {
    public class SavesManager {
        public static Save save = new Save();
        private static string destination = Application.persistentDataPath + "/save.json";
        public static BinaryFormatter binaryFormatter = new BinaryFormatter();

        public static void SaveData() {
            save.apikey = Settings.apikey;
            save.language = Settings.language;
            save.maxScanList = Settings.maxScanList;
            save.scans = VirusTotal.API.scans;

            string json = JsonConvert.SerializeObject(save);
            System.IO.File.WriteAllText(destination, json);
        }

        public static void LoadData() {
            try {
                string json = System.IO.File.ReadAllText(destination);
                save = JsonConvert.DeserializeObject<Save>(json);
                Game.Settings.Apply(save.apikey, save.maxScanList, save.language);
                VirusTotal.API.scans = save.scans;
            } catch (Exception exception) {
                Debug.Log(exception);
                Game.Settings.Apply("", 10, "en");
            }
        }
    }

    [System.Serializable]
    public class Save {
        public string apikey;
        public string language;
        public int maxScanList;

        public List<VirusTotal.ScanResult> scans;
    }
}
