﻿using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kamael.Packets.CSV
{
    public class classheader
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class Class
    {
        private static IEnumerable<classheader> _records;

        public static string className(uint intID)
        {
            return "No Value";

            string result = string.Empty;
            string id = intID.ToString();

            if (_records == null)
            {
                using (StreamReader sr = new StreamReader(@"CSV\Class_Name.csv"))
                {
                    CsvReader csv = new CsvReader(sr);
                    _records = csv.GetRecords<classheader>().ToList();
                }
            }

        IDSearch:
            foreach (classheader r in _records)
            {
                if (r.Id == id)
                {
                    result = r.Name;
                    break;
                }
            }

            if (result.Length > 0 && result.Substring(0, 1) == "@")
            {
                id = (result.Substring(1, (result.Length - 1)));
                goto IDSearch;
            }
            return result;
        }
    }
}