using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SistemaAcademia
{
    public class JsonLoader
    {
        private readonly Dictionary<string, QueryConfig>? queries;

        public JsonLoader(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"No se encontró el archivo JSON: {path}");

            string json = File.ReadAllText(path);
            queries = JsonConvert.DeserializeObject<Dictionary<string, QueryConfig>>(json);
        }

        public string BuildSql(string key)
        {
            if (queries == null)
                throw new InvalidOperationException("El archivo JSON no contiene datos válidos.");

            if (!queries.TryGetValue(key, out var q))
                throw new KeyNotFoundException($"Consulta '{key}' no existe en el archivo JSON.");

            // INSERT
            if (!string.IsNullOrWhiteSpace(q.InsertInto))
            {
                if (q.Columns == null || q.Values == null || q.Columns.Count != q.Values.Count)
                    throw new InvalidOperationException($"Consulta '{key}' de tipo INSERT está incompleta.");

                return $"INSERT INTO {q.InsertInto} ({string.Join(", ", q.Columns)}) VALUES ({string.Join(", ", q.Values)})";
            }

            // DELETE
            if (!string.IsNullOrWhiteSpace(q.DeleteFrom))
            {
                if (q.Where == null || !q.Where.Any())
                    throw new InvalidOperationException($"Consulta '{key}' de tipo DELETE está incompleta.");

                return $"DELETE FROM {q.DeleteFrom} WHERE " + string.Join(" AND ", q.Where);
            }

            // UPDATE
            if (!string.IsNullOrWhiteSpace(q.UpdateSet))
            {
                string setClause;

                if (q.Set != null && q.Set.Any())
                {
                    setClause = string.Join(", ", q.Set);
                }
                else if (q.SetDict != null && q.SetDict.Any())
                {
                    setClause = string.Join(", ", q.SetDict.Select(kv => $"{kv.Key} = {kv.Value}"));
                }
                else
                {
                    throw new InvalidOperationException($"Consulta '{key}' de tipo UPDATE está incompleta.");
                }

                if (q.Where == null || !q.Where.Any())
                    throw new InvalidOperationException($"Consulta '{key}' de tipo UPDATE sin cláusula WHERE.");

                return $"UPDATE {q.UpdateSet} SET {setClause} WHERE " + string.Join(" AND ", q.Where);
            }

            // SELECT
            if (q.Select == null || string.IsNullOrWhiteSpace(q.From))
                throw new InvalidOperationException($"Consulta '{key}' no contiene cláusula 'select' o 'from'.");

            var sb = new StringBuilder();
            sb.AppendLine("SELECT " + string.Join(", ", q.Select ?? Enumerable.Empty<string>()));
            sb.AppendLine("FROM " + q.From);

            // JOINs con soporte para tipo
            if (q.Joins is not null)
            {
                foreach (var join in q.Joins)
                {
                    if (string.IsNullOrWhiteSpace(join.Table) || string.IsNullOrWhiteSpace(join.On))
                        throw new InvalidDataException($"Consulta '{key}' tiene join incompleto.");

                    string tipoJoin = string.Equals(join.Type, "left", StringComparison.OrdinalIgnoreCase)
                        ? "LEFT JOIN"
                        : "JOIN";

                    sb.AppendLine($"{tipoJoin} {join.Table} ON {join.On}");
                }
            }

            if (q.Where is not null && q.Where.Any())
                sb.AppendLine("WHERE " + string.Join(" AND ", q.Where));

            if (q.GroupBy is not null && q.GroupBy.Any())
                sb.AppendLine("GROUP BY " + string.Join(", ", q.GroupBy));

            if (q.OrderBy is not null && q.OrderBy.Any())
                sb.AppendLine("ORDER BY " + string.Join(", ", q.OrderBy));

            return sb.ToString();
        }



        public string BuildSql(string key, List<NpgsqlParameter> parametros)
        {
            if (queries == null)
                throw new InvalidOperationException("El archivo JSON no contiene datos válidos.");

            if (!queries.TryGetValue(key, out var q))
                throw new KeyNotFoundException($"Consulta '{key}' no existe en el archivo JSON.");

            var sb = new StringBuilder();
            sb.AppendLine("SELECT " + string.Join(", ", q.Select ?? Enumerable.Empty<string>()));
            sb.AppendLine("FROM " + q.From);

            if (q.Joins is not null)
            {
                foreach (var join in q.Joins)
                {
                    if (string.IsNullOrWhiteSpace(join.Table) || string.IsNullOrWhiteSpace(join.On))
                        throw new InvalidDataException($"Consulta '{key}' tiene join incompleto.");

                    string tipoJoin = string.Equals(join.Type, "left", StringComparison.OrdinalIgnoreCase) ? "LEFT JOIN" : "JOIN";
                    sb.AppendLine($"{tipoJoin} {join.Table} ON {join.On}");
                }
            }

            var filtros = new List<string>();

            if (q.Where is not null && q.Where.Any())
                filtros.AddRange(q.Where);

            if (q.WhereOptional is not null && q.WhereOptional.Any())
            {
                foreach (var cond in q.WhereOptional)
                {
                    foreach (var param in parametros)
                    {
                        if (cond.Contains(param.ParameterName) &&
                            param.Value != null &&
                            !string.IsNullOrWhiteSpace(param.Value.ToString()))
                        {
                            filtros.Add(cond);
                            break;
                        }
                    }
                }
            }

            if (filtros.Count > 0)
                sb.AppendLine("WHERE " + string.Join(" AND ", filtros));

            if (q.GroupBy is not null && q.GroupBy.Any())
                sb.AppendLine("GROUP BY " + string.Join(", ", q.GroupBy));

            if (q.OrderBy is not null && q.OrderBy.Any())
                sb.AppendLine("ORDER BY " + string.Join(", ", q.OrderBy));

            return sb.ToString();
        }

    }
}
