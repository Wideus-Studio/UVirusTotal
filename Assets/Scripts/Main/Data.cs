using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Assets {
    public class Data {
        public static CResources resources = Resources.Load<CResources>("Assets");

        public static CWord GetWordByKey(string key) {
            for (int i = 0; i < resources.words.Length; i++)
                if (resources.words[i].key == key) return resources.words[i];
            return null;
        }

        public static String GetStringByLanguage(string key) {
            CWord word = GetWordByKey(key);
            if (word == null) return "Unknown";

            for (int i = 0; i < word.languageWords.Length; i++)
                if (word.languageWords[i].languageKey == Game.Settings.language) return word.languageWords[i].languageValue;

            return "Unknown";
        }
    }
}
