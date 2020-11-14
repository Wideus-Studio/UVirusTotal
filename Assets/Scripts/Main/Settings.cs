using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game {
    [System.Serializable]
    public class Settings {
        public static string apikey = "";
        public static string language = "en";
        public static int maxScanList = 0;

        public static void Apply(string apikey, int maxScanList, string language = "ru") {
            VirusTotal.API.APIKEY = apikey;
            VirusTotal.API.maxScansList = maxScanList;

            Settings.apikey = VirusTotal.API.APIKEY;
            Settings.maxScanList = VirusTotal.API.maxScansList;
            Settings.language = language;

            Game.Saves.SavesManager.SaveData();
        }

        public static void Load() {
            VirusTotal.API.APIKEY = Settings.apikey;
            VirusTotal.API.maxScansList = Settings.maxScanList;
        }
    }
}
