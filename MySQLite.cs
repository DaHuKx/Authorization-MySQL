using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launch
{
    class MySQLite
    {
        static private SQLiteConnection connection = new SQLiteConnection("DataSource=BD.db");

        static public bool LoginAccount(string login, string password)
        {
            connection.Close();
            connection.Open();
            SQLiteCommand SqlCommand = connection.CreateCommand();
            SqlCommand.CommandText = "select * from Users where login='" + login + "' and password='" + password + "'";
            SQLiteDataReader dataReader = SqlCommand.ExecuteReader();
            return dataReader.HasRows;
        }

        static public void RegisterAccount(string Login, string Password, string PhoneNumber)
        {
            connection.Open();
            SQLiteCommand SqlCommand = connection.CreateCommand();
            SqlCommand.CommandText = "select * from Users where login='" + Login + "'";
            SQLiteDataReader dataReader = SqlCommand.ExecuteReader();
            if (dataReader.HasRows)
                throw new Exception("Логин уже используется!");
            dataReader.Close();
            


            SqlCommand.CommandText = "insert into Users ('login', 'password', 'phonenumber') " +
                 "values ('" + Login + "','" + Password + "','" + PhoneNumber + "')";

            SqlCommand.ExecuteNonQuery();
        }
    }
}
