using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace Oxtail.Utils
{
    public static class SaveLoadManager
    {
        private static string SaveFilePath => Application.persistentDataPath + "/save.data";
        private static string SettingsFilePath => Application.persistentDataPath + "/settings.data";

        public static void SaveSettings(SerializedSettings settings)
        {
            try
            {
                FileStream dataStream = new FileStream(SettingsFilePath, FileMode.Create);

                BinaryFormatter converter = new BinaryFormatter();
                converter.Serialize(dataStream, settings);
                dataStream.Close();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static bool LoadSettings(out SerializedSettings settings)
        {
            settings = null;

            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    FileStream dataStream = new FileStream(SettingsFilePath, FileMode.Open);

                    BinaryFormatter converter = new BinaryFormatter();
                    settings = converter.Deserialize(dataStream) as SerializedSettings;
                    dataStream.Close();

                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return false;
        }
    }

    [Serializable]
    public abstract class SerializedSettings
    {

    }
}
