using Microsoft.Data.SqlClient;
using System.Data;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=work_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();
    Console.WriteLine("Connection is open\n");

    SqlCommand command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM products";

    //SqlDataAdapter adapter = new(command);
    SqlDataAdapter adapter = new("SELECT * FROM products", connection);
    DataSet dataSet = new();
    adapter.Fill(dataSet);

    DataSetPrint(dataSet);

    //DataTable tbl = dataSet.Tables[0];
    //DataRow dataRow = tbl.NewRow();
    //dataRow["title"] = "Mouse";
    //dataRow["price"] = 700.0;

    //tbl.Rows.Add(dataRow);


    //adapter.Update(dataSet);

    //dataSet.Tables[0].Rows.RemoveAt(0);
    DataRow row = dataSet.Tables[0].Rows[dataSet.Tables[0].Rows.Count - 1];
    Console.WriteLine($"{row[0]} {row[1]} {row[2]}");
    //row.Delete();
    row["price"] = 1000.00;

    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
    adapter.Update(dataSet);
    //Console.WriteLine(commandBuilder.GetInsertCommand().CommandText);
    //Console.WriteLine(commandBuilder.GetDeleteCommand().CommandText);
    //Console.WriteLine(commandBuilder.GetUpdateCommand().CommandText);
    Console.WriteLine();
    
    

    //dataSet.Clear();
    //adapter.Fill(dataSet);
    DataSetPrint(dataSet);
}

void DataSetPrint(DataSet dataSet)
{
    foreach (DataTable table in dataSet.Tables)
    {
        foreach (DataColumn column in table.Columns)
            Console.Write($"{column.ColumnName}\t");
        Console.WriteLine();

        foreach (DataRow row in table.Rows)
        {
            var cells = row.ItemArray;
            foreach (var cell in cells)
                Console.Write($"{cell}\t");
            Console.WriteLine();
        }
    }
}

