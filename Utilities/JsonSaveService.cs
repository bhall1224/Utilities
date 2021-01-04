using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Madman.Games.CovidBlaster.Services
{
    public class JsonSaveService
    {
        public static void SaveJson<T>(T obj, string fileName, string dir)
            where T : class
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj);

                string path = $"{dir}/{fileName}";
                Debug.Log($"Path: {path}");
                Debug.Log($"(Json to save: {json}");

                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(json);
                }

                Debug.Log($"{fileName}: save complete...");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static T LoadJsonFile<T>(string fileName, string dir)
            where T : class
        { 
            string fileContents = string.Empty;

            string path = $"{dir}/{fileName}";
            Debug.Log($"Path: {path}");

            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string input = string.Empty;
                    while ((input = sr.ReadLine()) != null)
                    {
                        fileContents += input;
                    }
                }

                if (!string.IsNullOrEmpty(fileContents))
                {
                    var obj = JsonConvert.DeserializeObject<T>(fileContents);
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Debug.LogWarning($"File {fileName} did not exist!");
                return null;
            }
        }

        public static void DeleteFile(string fileName, string dir)
        {
            string path = $"{dir}/{fileName}";
            Debug.Log($"Path: {path}");
            
            if (File.Exists(path))
                File.Delete(path);   
            else
                Debug.LogWarning($"File {fileName} did not exist!");         
        }
    }
}