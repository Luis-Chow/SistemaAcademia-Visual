using Newtonsoft.Json;
using Npgsql;
using System.Data;

public class DBComponent
{
    private readonly string connectionString;
    private readonly NpgsqlConnection connection; // Removed nullable operator
    private NpgsqlTransaction? transaction;

    public DBComponent(string connectionString)
    {
        this.connectionString = connectionString;
        this.connection = new NpgsqlConnection(connectionString); // Properly initialized here
    }

    public static DBComponent FromSettings(string jsonPath)
    {
        if (!File.Exists(jsonPath))
            throw new FileNotFoundException($"Archivo no encontrado: {jsonPath}");

        string json = File.ReadAllText(jsonPath);
        var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        if (config is null)
            throw new InvalidDataException("El contenido del archivo JSON no pudo ser interpretado correctamente.");

        if (!config.TryGetValue("connectionString", out var connectionString) || string.IsNullOrWhiteSpace(connectionString))
            throw new KeyNotFoundException("La clave 'connectionString' no existe o está vacía en el archivo JSON.");

        var db = new DBComponent(connectionString);
        db.Connect();
        return db;
    }

    public void Connect()
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();
    }

    public void BeginTransaction()
    {
        if (connection.State != ConnectionState.Open)
            throw new InvalidOperationException("Conexión no abierta.");

        if (transaction == null)
            transaction = connection.BeginTransaction();
    }

    public void Commit()
    {
        transaction?.Commit();
        Close();
    }

    public void Rollback()
    {
        transaction?.Rollback();
        Close();
    }

    public void Close()
    {
        if (connection.State == ConnectionState.Open)
            connection.Close();
    }

    public DataTable ExecuteQuery(string sql, NpgsqlParameter[]? parameters = null)
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();

        using var cmd = transaction != null
            ? new NpgsqlCommand(sql, connection, transaction)
            : new NpgsqlCommand(sql, connection);

        if (parameters != null)
            cmd.Parameters.AddRange(parameters);

        using var adapter = new NpgsqlDataAdapter(cmd);
        var dt = new DataTable();
        adapter.Fill(dt);
        return dt;
    }


    public int ExecuteNonQuery(string sql, NpgsqlParameter[]? parameters = null)
    {
        using var cmd = transaction != null
            ? new NpgsqlCommand(sql, connection, transaction)
            : new NpgsqlCommand(sql, connection);

        if (parameters != null)
            cmd.Parameters.AddRange(parameters);

        return cmd.ExecuteNonQuery();
    }


    public NpgsqlConnection Connection => connection;
    public NpgsqlTransaction? Transaction => transaction;
}
