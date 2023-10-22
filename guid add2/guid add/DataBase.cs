using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Генератор_заданий
{
    internal class DataBase
    {
        private MySqlConnection _connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=guid");

        public void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return _connection;
        }

        public DataView Select(string sqlString)
        {
            var ret = new DataView();
            OpenConnection();
            try
            {
                var table = new DataTable();
                var adapter = new MySqlDataAdapter();
                var command = new MySqlCommand(sqlString, GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                ret = table.DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return ret;
        }

        public DataView Insert(string sqlString)
        {
            var ret = new DataView();

            OpenConnection();
            try
            {
                var command = new MySqlCommand(sqlString, GetConnection());

                command.ExecuteNonQuery();
                MessageBox.Show("Операция выполнена");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                CloseConnection();
            }

            return ret;
        }


    }
}
