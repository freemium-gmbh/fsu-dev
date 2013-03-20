﻿/*
 * This is a dll was designed mainly as a set of Utilities regarding bug reporting, and application paths,
 * that was designed mainly for the freemium system Utilities, and any of its derivatives 
 */

using System.IO;
using System;

namespace FreemiumUtil
{
    /// <summary>
    /// The <see cref="FreemiumUtil"/> namespace contains a set of Utilities regarding bug reporting, and application paths, 
    /// that was designed mainly for the freemium system Utilities, and any of its derivatives 
    /// </summary>

    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc { }

    /// <summary>
    /// Contains methods to operate with a txt config file
    /// </summary>
    public static class CfgFile
    {
        // We use the common application folder to store the config file.
        private static readonly string cfgFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\FreeSystemUtilities\\";
        private static readonly string cfgFileName = cfgFilePath + "freemium.cfg";

        /// <summary>
        /// Reads the given key from the config file and returns its value.
        /// </summary>
        /// <param name="key">Configuration entry key</param>
        /// <returns></returns>
        public static string Get(string key)
        {
            int dummy;
            return Get(key, out dummy);
        }

        static string Get(string key, out int lineNum)
        {
            if (CheckCfgFile())
            {
                try
                {
                    using (StreamReader reader = new StreamReader(cfgFileName))
                    {
                        string line;
                        for (lineNum = 0; (line = reader.ReadLine()) != null; lineNum++)
                        {
                            string[] entry = line.Split(new[] { ' ', '=' });
                            if (entry.Length > 1 && entry[0] == key)
                                return entry[entry.Length - 1];
                        }
                    }
                }
                catch
                {
                }
            }

            lineNum = -1;
            return null;
        }

        /// <summary>
        /// Sets the given key and value in the config file.
        /// </summary>
        /// <param name="key">Configuration entry key</param>
        /// <param name="value">Configuration entry value</param>
        public static void Set(string key, string value)
        {
            int lineNum;
            if (Get(key, out lineNum) == null)
            {
                try
                {
                    using (StreamWriter writer = File.AppendText(cfgFileName))
                    {
                        writer.WriteLine(key + "=" + value);
                        writer.Close();
                    }
                }
                catch
                {
                }
            }
            else
            {
                string cfg = string.Empty;
                bool hasError = false;
                try
                {
                    using (StreamReader reader = new StreamReader(cfgFileName))
                    {

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] entry = line.Split(new[] { ' ', '=' });
                            if (entry[0] == key)
                                cfg += key + "=" + value + "\r\n";
                            else
                                cfg += line + "\r\n";
                        }
                    }
                }
                catch (Exception)
                {
                    hasError = true;
                }

                if (!hasError)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(cfgFileName))
                        {
                            writer.Write(cfg);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private static bool CheckCfgFile()
        {
            bool configFileValid = true;
            if (!File.Exists(cfgFileName)) configFileValid = false;

            if (configFileValid)
            {
                try
                {
                    FileInfo f = new FileInfo(cfgFileName);
                    if (f.Length == 0) configFileValid = false;
                }
                catch (Exception)
                {
                    configFileValid = false;
                }
            }

            if (!configFileValid)
            {
                // If the config file does not exist or is empty, we try to create a new one.
                if (!Directory.Exists(cfgFilePath))
                {
                    try
                    {
                        Directory.CreateDirectory(cfgFilePath);
                    }
                    catch (Exception)
                    {
                    }
                }

                try
                {
                    using (StreamWriter writer = File.CreateText(cfgFileName))
                    {
                        writer.WriteLine("[Freemiun Config File]");
                        writer.WriteLine("Version=1.4");
                        writer.WriteLine("Lang=de");
                        writer.WriteLine("FirstRun=1");
                        writer.WriteLine("Theme=Blue");
                        writer.WriteLine("MinimizeToTray=0");
                        writer.WriteLine("ShowBaloon=0");
                        writer.WriteLine("ShowContextMenuAnalyze=1");
                        writer.WriteLine("ShowContextMenuDecrypt=1");
                        writer.WriteLine("ShowContextMenuEncrypt=1");
                        writer.WriteLine("ShowContextMenuFindEmptyFolders=1");
                        writer.WriteLine("ShowContextMenuSplit=1");
                        writer.WriteLine("ShowContextMenuWipe=1");
                    }
                    configFileValid = true;
                }
                catch (Exception)
                {
                }
            }
            return configFileValid;
        }
    }
}