using MySql.Data.MySqlClient;
using UnityEngine;

public class DBInterface : MonoBehaviour
{
    public static DBInterface Instance;

    private static MySqlConnectionStringBuilder stringBuilder;

    [SerializeField]
    private string Server;
    [SerializeField]
    private string Database;
    [SerializeField]
    private string UserID;
    [SerializeField]
    private string Password;

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        stringBuilder = new MySqlConnectionStringBuilder();
        stringBuilder.Server = Server;
        stringBuilder.Database = Database;
        stringBuilder.UserID = UserID;
        stringBuilder.Password = Password;
    }

    public static void ExecuteQuery(string Query)
    {
        using (MySqlConnection connection = new MySqlConnection(stringBuilder.ConnectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Query;

                //command.Parameters.AddWithValue("@tableName", playerName);
                command.ExecuteNonQuery();
 
                connection.Close();
            }
            catch( System.Exception ex)
            {
                Debug.LogError("DBInterface: Could not create Table" + System.Environment.NewLine + ex.Message);
            }
        }
    }

    public static void CreateTable(string tableName)
    {
        string Query = "CREATE TABLE " + tableName + "(attempt int, PRIMARY KEY(attempt))";
        ExecuteQuery(Query);
    }

    public static void InsertValueInExistingAttempt(string tableName, string colName, int attempt, float time)
    {
        string Query = "UPDATE " + tableName + " SET " + colName + "="+time+" WHERE attempt=" + attempt + " ";
        ExecuteQuery(Query);
    }

    public static void InsertNewAttempt(string tableName, string colName, int attempt, float time)
    {
        string Query = "INSERT INTO " + tableName + " (attempt, " + colName + ") VALUES (" + attempt + ", " + time + ")";
        ExecuteQuery(Query);
    }

    public static void AddColumn(string tableName,string colName, string type)
    {
        string Query = "ALTER TABLE " + tableName + " ADD " + colName + " "+type;
        ExecuteQuery(Query);
    }

    public static bool ifExist(string Query)
    {
        using (MySqlConnection connection = new MySqlConnection(stringBuilder.ConnectionString))
        {
            bool isTable = false;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Query;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {

                    //int x = (int)(reader[0]);
                    int x = reader.GetInt32(0);
                    if (x != 0)
                    {
                        isTable = true;
                    }
                }
                connection.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("DBInterface: Could not check if Table exists" + System.Environment.NewLine + ex.Message);
            }
            return isTable;
        }
    }

    public static bool ifTableExist(string tableName)
    {
        string Query = "SELECT count(*) FROM information_schema.TABLES WHERE(TABLE_SCHEMA ='u785071734_Autistic') AND(TABLE_NAME = '" + tableName + "')";
        return (ifExist(Query));
    }

    public static bool ifColumnExist(string tableName ,string colName)
    {
        string Query = "Select count(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE(TABLE_NAME = '" + tableName + "') AND (COLUMN_NAME = '" + colName + "')";
        return (ifExist(Query));
    }

    public static int rowCount(string Query)
    {
        using (MySqlConnection connection = new MySqlConnection(stringBuilder.ConnectionString))
        {
            int count = 0;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = Query;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {

                    //int x = (int)(reader[0]);
                    count = reader.GetInt32(0);
                    
                }
                connection.Close();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("DBInterface: Could not check if Table exists" + System.Environment.NewLine + ex.Message);
            }
            return count;
        }
    }

    public static int nullCounter(string tableName, string colName)
    {
        string Query = "SELECT count(*) FROM " + tableName + " WHERE " + colName + " IS NOT NULL";
        return (rowCount(Query));
    }
}
