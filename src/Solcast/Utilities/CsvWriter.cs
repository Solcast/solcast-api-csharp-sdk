// The SDK project does not enable nullable reference types, so this file opts in.
#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Solcast.Utilities
{
    /// <summary>
    /// Writes a series of records as CSV.
    ///
    /// This knows nothing about the client: it takes any records and reads their properties, so it
    /// works for every endpoint that returns a series and keeps working when one gains a field.
    ///
    /// The header is the property names in the order the record declares them, which is the order the
    /// API returns. A field you did not request is null and is written as an empty cell, so the column
    /// count is the same on every row.
    /// </summary>
    public static class CsvWriter
    {
        /// <summary>Round-trippable ISO 8601, so a timestamp reads the same in every locale.</summary>
        private const string TimestampFormat = "O";

        /// <summary>The records as CSV, header first.</summary>
        public static string ToCsv<T>(IEnumerable<T> records)
        {
            if (records is null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            var columns = ColumnsOf(typeof(T));
            var csv = new StringBuilder();

            csv.Append(string.Join(",", columns.Select(column => Escape(column.Name))));

            foreach (var record in records)
            {
                csv.Append('\n');
                csv.Append(string.Join(",", columns.Select(column => Escape(Format(column.GetValue(record))))));
            }

            return csv.ToString();
        }

        /// <summary>
        /// The readable properties of a record. AdditionalData holds whatever the model did not
        /// declare, which is a dictionary rather than a column.
        /// </summary>
        private static PropertyInfo[] ColumnsOf(Type record) =>
            record.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.CanRead && property.GetIndexParameters().Length == 0)
                .Where(property => property.Name != "AdditionalData" && property.Name != "BackingStore")
                .ToArray();

        private static string Format(object? value) => value switch
        {
            null => string.Empty,
            DateTimeOffset timestamp => timestamp.ToString(TimestampFormat, CultureInfo.InvariantCulture),
            DateTime timestamp => timestamp.ToString(TimestampFormat, CultureInfo.InvariantCulture),
            IFormattable number => number.ToString(null, CultureInfo.InvariantCulture),
            IEnumerable<string> values => string.Join(" ", values),
            _ => value.ToString() ?? string.Empty,
        };

        /// <summary>Quotes a cell only when it would otherwise break the row.</summary>
        private static string Escape(string cell) =>
            cell.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0
                ? cell
                : "\"" + cell.Replace("\"", "\"\"") + "\"";
    }
}
