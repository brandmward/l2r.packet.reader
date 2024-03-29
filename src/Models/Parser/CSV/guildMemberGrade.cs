﻿using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kamael.Packets.CSV
{
    public class guildMemberGradeheader
    {
        public int Grade { get; set; }
        public string Name { get; set; }
    }

    public class guildMemberGrade
    {
        private static IEnumerable<guildMemberGradeheader> _records;

        public static string guildMemberGradeName(byte Grade)
        {
            return "No Value";

            if (_records == null)
            {
                using (StreamReader sr = new StreamReader(@"CSV\GuildMemberGrade_Name.csv"))
                {
                    CsvReader csv = new CsvReader(sr);
                    _records = csv.GetRecords<guildMemberGradeheader>().ToList();
                }
            }

            foreach (guildMemberGradeheader r in _records)
            {
                if (r.Grade == Grade)
                {
                    return r.Name;
                }
            }

            return string.Empty;
        }
    }
}