using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AspIT.Company.Server.Config
{
    public abstract class Config<Type> where Type : Config<Type>
    {
        /// <summary>
        /// Loads a configuration
        /// </summary>
        /// <returns>A configuration</returns>
        public static Type Load()
        {
            string path = $"Configs/{typeof(Type).Name}.xml";

            if(!File.Exists(path))
            {
                LogHelper.AddLog("Server config does not exist.");
                return null;
            }

            using(FileStream stream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Type));
                return serializer.Deserialize(stream) as Type;
            }
        }

        /// <summary>
        /// Loads a configuration
        /// </summary>
        /// <returns>A configuration</returns>
        public static Type Load(string filePath)
        {
            if(!File.Exists(filePath))
            {
                LogHelper.AddLog("Server config does not exist.");
                return null;
            }

            using(FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Type));
                return serializer.Deserialize(stream) as Type;
            }
        }

        /// <summary>
        /// Saves the configuration with the name as the class name
        /// </summary>
        public void Save()
        {
            string path = "Configs";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using(FileStream stream = new FileStream(Path.Combine(path, $"{typeof(Type).Name}.xml"), FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Type));
                serializer.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <param name="filename">The filename only</param>
        public void Save(string filename)
        {
            string path = "Configs";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using(FileStream stream = new FileStream(Path.Combine(path, $"{filename}.xml"), FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Type));
                serializer.Serialize(stream, this);
            }
        }
    }
}
