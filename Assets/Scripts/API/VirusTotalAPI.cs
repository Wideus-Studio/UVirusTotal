using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using UnityEngine;

/// <summary>
/// Модуль VirusTotalAPI
/// </summary>
namespace VirusTotal {
    /// <summary>
    /// Модуль API
    /// </summary>
    public class API {
        /// <summary>
        /// Клиент для загрузки файла на сервер VirusTotal
        /// </summary>
        static WebClient webClient = new WebClient();

        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "https://www.virustotal.com/vtapi/v2";

        /// <summary>
        /// Ключ api
        /// </summary>
        public static string APIKEY = "057dd0f7b03a8dc72191b32e3d5ee613564a0f5d2f8cd223cd54848fa0ce752d";

        /// <summary>
        /// Файлы, отправленные на сканирование
        /// </summary>
        public static RScan lastScan = null;

        /// <summary>
        /// Максимальное кол-во записей в истории
        /// </summary>
        public static int maxScansList = 15;

        /// <summary>
        /// Результаты сканирования
        /// </summary>
        public static List<ScanResult> scans = new List<ScanResult> { };

        /// <summary>
        /// Отправить файл на проверку
        /// </summary>
        /// <param name="filePath"> - путь к файлу. </param>
        public static void ScanFile(string filePath) {
            try {
                byte[] p_request = webClient.UploadFile($"{URL}/file/scan?apikey={APIKEY}", filePath);
                string s_json = Encoding.ASCII.GetString(p_request);
                lastScan = JsonConvert.DeserializeObject<RScan>(s_json);
            } catch (Exception exception) {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// Отправить URL на проверку
        /// </summary>
        /// <param name="url"> - URL сайта или файла. </param>
        public static void ScanURL(string url) {
            try { 
                string p_request = POST($"{URL}/url/scan?apikey={APIKEY}&url={url}");
                lastScan = JsonConvert.DeserializeObject<RScan>(p_request);
            } catch (Exception exception) {
                Debug.LogError(exception);
            }
        }

        /// <summary>
        /// Получить результат сканирования файла
        /// </summary>
        public static void ReportFile() {
            try {
                string s_json = GET($"{URL}/file/report?apikey={APIKEY}&resource={lastScan.resource}");
                ScanResult scan = JsonConvert.DeserializeObject<ScanResult>(s_json);

                scan.date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                scans.Insert(0, scan);
                if (scans.Count >= maxScansList) scans = SeparateList<ScanResult>(scans, 0, maxScansList);
                lastScan = null;
            } catch (Exception exception) {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// Получить результат сканирования по URL
        /// </summary>
        public static void ReportURL() {
            try {
                string s_json = GET($"{URL}/url/report?apikey={APIKEY}&resource={lastScan.url}");
                ScanResult scan = JsonConvert.DeserializeObject<ScanResult>(s_json);
                scan.date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                scans.Insert(0, scan);
                if (scans.Count >= maxScansList) scans = SeparateList<ScanResult>(scans, 0, maxScansList);
                lastScan = null;
            } catch (Exception exception) {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// Обрезка списка в диапозоне [min, max]
        /// </summary>
        /// <typeparam name="T"> - тип.</typeparam>
        /// <param name="list"> - исходный массив.</param>
        /// <param name="min"> - минимальный индекс.</param>
        /// <param name="max"> - максимальный индекс. </param>
        /// <returns></returns>
        public static List<T> SeparateList<T>(List<T> list , int min, int max) {
            List<T> t_list = new List<T> { };

            int i_min = (min < 0 && min > list.Count) ? 0 : min;
            int i_max = (max < list.Count) ? max : list.Count;
            
            for (int i = i_min; i < i_max; i++) {
                t_list.Add(list[i]);
            }

            return t_list;
        }

        /// <summary>
        /// Простой метод GET
        /// </summary>
        /// <param name="url"> - url.</param>
        /// <returns></returns>
        public static string GET(string url) {
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            WebResponse response = request.GetResponse();

            string t_string = "";

            using (Stream dataStream = response.GetResponseStream()) {
                StreamReader reader = new StreamReader(dataStream);
                t_string += reader.ReadToEnd();
            }

            response.Close();

            return t_string;
        }

        /// <summary>
        /// Простой метод POST
        /// </summary>
        /// <param name="url"> - url.</param>
        /// <returns></returns>
        public static string POST(string url) {
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentLength = 0;
            WebResponse response = request.GetResponse();

            string t_string = "";

            using (Stream dataStream = response.GetResponseStream()) {
                StreamReader reader = new StreamReader(dataStream);
                t_string += reader.ReadToEnd();
            }

            response.Close();

            return t_string;
        }
    }

    /// <summary>
    /// Данные после запуска сканирования
    /// </summary>
    [System.Serializable]
    public class RScan {
        public string scan_id { set; get; }
        public string sha1 { set; get; }
        public string resource { set; get; }
        public string sha256 { set; get; }
        public string permalink { set; get; }
        public string md5 { set; get; }
        public string verbose_msg { set; get; }
        public string url { set; get; }
        public int response_code { set; get; }
    }

    /// <summary>
    /// Отчёт по базе сканирования
    /// </summary>
    [System.Serializable]
    public class Result {
        public string name { set; get; }
        public bool detected { set; get; }
        public string version { set; get; }
        public string result { set; get; }
        public string update { set; get; }
    }

    /// <summary>
    /// Результат сканирования
    /// </summary>
    [System.Serializable]
    public class ScanResult : RScan {
        public Dictionary<string, Result> scans { set; get; }
        public int total { set; get; }
        public int positives { set; get; }
        public string date { set; get; }
    }
}
