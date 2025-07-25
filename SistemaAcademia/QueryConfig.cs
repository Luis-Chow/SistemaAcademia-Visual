using Newtonsoft.Json;

namespace SistemaAcademia
{
    /// <summary>
    /// Representa la configuraci�n de una consulta SQL, incluyendo cl�usulas SELECT, FROM, JOIN, WHERE, GROUP BY, INSERT, UPDATE y DELETE.
    /// Permite mapear configuraciones desde JSON para construir consultas din�micas.
    /// </summary>
    public class QueryConfig
    {
        /// <summary>
        /// Lista de columnas o expresiones a seleccionar en la consulta (cl�usula SELECT).
        /// </summary>
        [JsonProperty("select")]
        public List<string>? Select { get; set; }

        /// <summary>
        /// Tabla principal de la consulta (cl�usula FROM).
        /// </summary>
        [JsonProperty("from")]
        public string? From { get; set; }

        /// <summary>
        /// Lista de configuraciones de JOIN para la consulta.
        /// </summary>
        [JsonProperty("joins")]
        public List<JoinConfig>? Joins { get; set; }

        /// <summary>
        /// Lista de condiciones para la cl�usula WHERE.
        /// </summary>
        [JsonProperty("where")]
        public List<string>? Where { get; set; }

        /// <summary>
        /// Lista de columnas para la cl�usula GROUP BY.
        /// </summary>
        [JsonProperty("group_by")]
        public List<string>? GroupBy { get; set; }

        /// <summary>
        /// Tabla destino para la operaci�n INSERT (cl�usula INSERT INTO).
        /// </summary>
        [JsonProperty("insert_into")]
        public string? InsertInto { get; set; }

        /// <summary>
        /// Lista de columnas para la operaci�n INSERT.
        /// </summary>
        [JsonProperty("columns")]
        public List<string>? Columns { get; set; }

        /// <summary>
        /// Lista de valores para la operaci�n INSERT.
        /// </summary>
        [JsonProperty("values")]
        public List<string>? Values { get; set; }
        
        /// <summary>
        /// Lista de condiciones opcionales para la cl�usula WHERE.
        /// </summary>
        [JsonProperty("where_optional")]
        public List<string>? WhereOptional { get; set; }

        /// <summary>
        /// Lista de columnas o expresiones para la cl�usula ORDER BY.
        /// </summary>
        [JsonProperty("order_by")]
        public List<string>? OrderBy { get; set; }

        /// <summary>
        /// Tabla destino para la operaci�n DELETE (cl�usula DELETE FROM).
        /// </summary>
        [JsonProperty("delete_from")]
        public string? DeleteFrom { get; set; }

        /// <summary>
        /// Expresi�n SET para la operaci�n UPDATE.
        /// </summary>
        [JsonProperty("update_set")]
        public string? UpdateSet { get; set; }

        /// <summary>
        /// Lista de asignaciones para la cl�usula SET en UPDATE.
        /// </summary>
        [JsonProperty("set")]
        public List<string>? Set { get; set; }

        /// <summary>
        /// Diccionario de asignaciones clave-valor para la cl�usula SET en UPDATE.
        /// </summary>
        [JsonProperty("set_dict")]
        public Dictionary<string, string>? SetDict { get; set; }
    }

    /// <summary>
    /// Representa la configuraci�n de un JOIN en una consulta SQL.
    /// </summary>
    public class JoinConfig
    {
        /// <summary>
        /// Nombre de la tabla a unir.
        /// </summary>
        [JsonProperty("table")]
        public string? Table { get; set; }

        /// <summary>
        /// Condici�n ON para el JOIN.
        /// </summary>
        [JsonProperty("on")]
        public string? On { get; set; }

        /// <summary>
        /// Tipo de JOIN (por ejemplo: INNER, LEFT, RIGHT).
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}