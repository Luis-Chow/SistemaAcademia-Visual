using Newtonsoft.Json;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaAcademia
{
    public class QueryConfig
    {
        [JsonProperty("select")]
        public List<string>? Select { get; set; }

        [JsonProperty("from")]
        public string? From { get; set; }

        [JsonProperty("joins")]
        public List<JoinConfig>? Joins { get; set; }

        [JsonProperty("where")]
        public List<string>? Where { get; set; }

        [JsonProperty("group_by")]
        public List<string>? GroupBy { get; set; }

        [JsonProperty("insert_into")]
        public string? InsertInto { get; set; }

        [JsonProperty("columns")]
        public List<string>? Columns { get; set; }

        [JsonProperty("values")]
        public List<string>? Values { get; set; }
        
        [JsonProperty("where_optional")]
        public List<string>? WhereOptional { get; set; }

        [JsonProperty("order_by")]
        public List<string>? OrderBy { get; set; }

        [JsonProperty("delete_from")]
        public string? DeleteFrom { get; set; }

        [JsonProperty("update_set")]
        public string? UpdateSet { get; set; }

        [JsonProperty("set")]
        public List<string>? Set { get; set; }

        [JsonProperty("set_dict")]
        public Dictionary<string, string>? SetDict { get; set; }
    }

    public class JoinConfig
    {
        [JsonProperty("table")]
        public string? Table { get; set; }

        [JsonProperty("on")]
        public string? On { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}