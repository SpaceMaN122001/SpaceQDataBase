using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SpaceQDataBase
{
    public struct Parametr
    {
        public string Variable { get; set; }
        public object Value { get; set; }

        public Parametr(string variable, object value)
        {
            Variable = variable;
            Value = value;
        }
    }

    public class QDataBase : IDisposable
    {
        public SqlConnection SqlConnection { get; }
        private string _Connection;

        public QDataBase(string connenction)
        {
            _Connection = connenction;

            SqlConnection = new SqlConnection(_Connection);

            SqlConnection.Open();
        }

        public void Dispose() { }

        public void ExecuteQuery(string queryCommandText, List<Parametr> parametrs = null)
        {
            SqlCommand command = new SqlCommand(queryCommandText, SqlConnection);

            if (parametrs != null)
            {
                foreach (Parametr parametr in parametrs)
                {
                    command.Parameters.AddWithValue(parametr.Variable, parametr.Value);
                }
            }

            command.ExecuteNonQuery();
        }

        public object GetOneValueQuery(string queryCommandText, int columnNumber, List<Parametr> parametrs = null)
        {
            SqlCommand command = new SqlCommand(queryCommandText, SqlConnection);

            if (parametrs != null)
            {
                foreach (Parametr parametr in parametrs)
                {
                    command.Parameters.AddWithValue(parametr.Variable, parametr.Value);
                }
            }

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                object value = reader.GetValue(columnNumber);

                reader.Close();

                return value;
            }

            reader.Close();

            return null;
        }

        public SqlDataReader GetDataReaderQuery(string queryCommandText, List<Parametr> parametrs = null)
        {
            SqlCommand command = new SqlCommand(queryCommandText, SqlConnection);

            if (parametrs != null)
            {
                foreach (Parametr parametr in parametrs)
                {
                    command.Parameters.AddWithValue(parametr.Variable,parametr.Value);
                }
            }

            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }
    }
}
