using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjectDetection
{
    public static class CatalogUtil
    {
        private static string CATALOG_ITEM_PATTERN = "item {\n  name: \"(?<name>.*)\"\n  id: (?<id>\\d+)\n  display_name: \"(?<displayName>.*)\"\n}";

        /// <summary>
        /// Reads catalog of well-known objects from text file.
        /// </summary>
        /// <param name="file">path to the text file</param>
        /// <returns>collection of items</returns>

        public static IEnumerable<CatalogItem> ReadCatalogItems(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            using (StreamReader reader = new StreamReader(stream))
            {
                string text = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(text))
                {
                    yield break;
                }

                Regex regex = new Regex(CATALOG_ITEM_PATTERN);
                var matches = regex.Matches(text);
                foreach (Match match in matches)
                {
                    var name = match.Groups[1].Value;
                    var id = int.Parse(match.Groups[2].Value);
                    var displayName = match.Groups[3].Value;
                    yield return new CatalogItem
                    {
                        Id = id,
                        Name = name,
                        DisplayName = displayName
                    };

                }
            }
        }
    }
}
