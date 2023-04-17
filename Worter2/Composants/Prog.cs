using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ConsoleTables;

namespace Worter2
{
    internal class Prog
    {
        public void addWords()
        {

        }
        public static DataTable DataTableItems(List<Words> collection)
        {
            var table = new DataTable();
            table.Columns.Add("English", typeof(string));
            table.Columns.Add("Deutsch", typeof(string));
            table.Columns.Add("Français", typeof(string));

            foreach (var item in collection)
            {

                //Console.WriteLine("\n{0,-10} | {1,-10} | {2,-10}", item.English, item.Deutsch, item.Francais);
                table.Rows.Add( item.English, item.Deutsch, item.Francais);
            }
            return table;
            
        }
        public void Main(List<Words> Words)
        {
            //Console.OutputEncoding = Encoding.UTF8;
            var data = DataTableItems(Words);
            string[] columnNames = data.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

            DataRow[] rows = data.Select();

            var table = new ConsoleTable(columnNames);
            foreach (DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }
            table.Write(Format.MarkDown);
            table.Write(Format.Alternative);
            table.Write(Format.Minimal);
            table.Write(Format.Default);
            Console.Read();
        }
    }
}
