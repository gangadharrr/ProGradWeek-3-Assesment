using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Data;
using System.Security.Principal;
using System.Transactions;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Text;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.ComponentModel.DataAnnotations;

namespace ProGradWeek_3_Assesment
{
    
    public class College_Sports_Management_System_DB
    {
        public static string connetionString = @"Data Source=5CG9410FJD;Initial Catalog=College Sports Management System;Integrated Security=True;Encrypt=False;";
        public void AddSports(List<string> Data)
        {
            string TableName = "SPORTS";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();
                //CREATE TABLE IF DOES NOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{TableName}' and xtype='U')" +
                        $"CREATE TABLE {TableName} (SPORT_ID INT,SPORT_NAME VARCHAR(30),PRIMARY KEY (SPORT_ID))";

                    cmd.ExecuteNonQuery();
                }
                //ADDING THE SPORT INTO TABLE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try 
                    { 
                        cmd.CommandText = $"INSERT INTO {TableName} VALUES({Data[0]},'{Data[1]}')";
                    
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Inserted SuccessFully");
                    }
                    catch(SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
        }
        public void AddPlayers(List<string> Data)
        { 
            string TableName = "PLAYERS";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();
                //CREATE TABLE IF DOES NOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {
                   
                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{TableName}' and xtype='U') " +
                        $"CREATE TABLE {TableName} (PLAYER_ID INT,PLAYER_NAME VARCHAR(30),PRIMARY KEY (PLAYER_ID))";

                    cmd.ExecuteNonQuery();
                }
                 //ADDING THE Player INTO TABLE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try 
                    { 
                        cmd.CommandText = $"INSERT INTO {TableName} VALUES({Data[0]},'{Data[1]}')";
                    
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Inserted SuccessFully");
                    }
                    catch(SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
        }
        public void AddTournament(List<string> Data)
        {
            string TableName = "TOURNAMENTS";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();
                //CREATE TABLE IF DOES NOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{TableName}' and xtype='U') " +
                        $"CREATE TABLE {TableName} (TOURNAMENT_ID INT,TOURNAMENT_NAME VARCHAR(30),PRIMARY KEY (TOURNAMENT_ID))";

                    cmd.ExecuteNonQuery();
                }
                //ADDING THE TOUNAMENT INTO TABLE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = $"INSERT INTO {TableName} VALUES({Data[0]},'{Data[1]}')";

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Inserted SuccessFully");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
        }
        public void AddScoreBoard(List<string> Data)
        {
            string TableName = "SCOREBOARD";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();
                //CREATE TABLE IF DOES NOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{TableName}' and xtype='U') " +
                        $"CREATE TABLE {TableName} (SPORT_ID INT,SPORT_NAME VARCHAR(30),TOURNAMENT_ID INT,TOURNAMENT_NAME VARCHAR(30))";

                    cmd.ExecuteNonQuery();
                }
                string PlayerName;
                //FETCHING PLAYER NAME
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT PLAYER_NAME FROM PLAYERS WHERE PLAYER_ID={Data[2]}";
                    PlayerName = cmd.ExecuteScalar().ToString();
                }
                //ADDING ROW IF DOESNOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {TableName} (SPORT_ID,SPORT_NAME,TOURNAMENT_ID ,TOURNAMENT_NAME)" +
                                    $" SELECT {Data[0]},S.SPORT_NAME,{Data[1]},T.TOURNAMENT_NAME FROM SPORTS AS S,TOURNAMENTS AS T " +
                                       $"WHERE S.SPORT_ID={Data[0]} AND T.TOURNAMENT_ID={Data[1]}";

                    cmd.ExecuteNonQuery();
                }//ADDING PLAYER AS COLUMN IF DOESNOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"IF NOT EXISTS(SELECT* FROM INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = '{TableName}' AND COLUMN_NAME = '{PlayerName}')"
                        + $"ALTER TABLE {TableName} ADD {PlayerName} int NULL";

                    cmd.ExecuteNonQuery();
                }
                
                    
                //ADDING PLAYER SCORE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                    cmd.CommandText = $"UPDATE {TableName} SET {PlayerName}={Data.Last()} WHERE SPORT_ID={Data[0]} AND TOURNAMENT_ID={Data[1]}";
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Updated Successfully");
                }


            }
        }
        
        public void RemovePlayers(int ID)
        {
            string TableName = "PLAYERS";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();
                //CREATE TABLE IF DOES NOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{TableName}' and xtype='U') " +
                        $"CREATE TABLE {TableName} (PLAYER_ID INT,PLAYER_NAME VARCHAR(30),PRIMARY KEY (PLAYER_ID))";

                    cmd.ExecuteNonQuery();
                }
                //DELETING THE Player FROM TABLE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = $"DELETE FROM {TableName} WHERE PLAYER_ID={ID}";

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("DELETED SuccessFully");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
        }
        public void RemoveTournament(int ID)
        {
            string TableName = "TOURNAMENTS";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();
                //CREATE TABLE IF DOES NOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{TableName}' and xtype='U') " +
                        $"CREATE TABLE {TableName} (TOURNAMENT_ID INT,TOURNAMENT_NAME VARCHAR(30),PRIMARY KEY (TOURNAMENT_ID))";

                    cmd.ExecuteNonQuery();
                }
                //DELETEING THE TOUNAMENT FROM TABLE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = $"DELETE FROM {TableName} WHERE TOURNAMENT_ID={ID}";

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("DELETED SuccessFully");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

            }
        }

    }
    public class Program
    {
        public static string connetionString = @"Data Source=5CG9410FJD;Initial Catalog=College Sports Management System;Integrated Security=True;Encrypt=False;";
        static void Main(string[] args)
        {

            /* using (SqlConnection conn = new SqlConnection(connetionString))
             {
                 conn.Open();
                 using (SqlCommand cmd = conn.CreateCommand())
                 {
                     cmd.CommandText = "display_details";
                     cmd.CommandType = CommandType.StoredProcedure;

                     SqlDataReader reader = cmd.ExecuteReader();
                     while (reader.Read())
                     {
                         for (int i = 0; i < reader.FieldCount; i++)
                         {
                             Console.Write(reader[i].ToString().Trim() + " ");
                         }
                         Console.WriteLine();
                     }
                     reader.Close();
                     Console.WriteLine();
                     cmd.CommandText = "sortby_column";
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.Add(new SqlParameter("@table_name", "employee"));
                     cmd.Parameters.Add(new SqlParameter("@column_name", "salary"));
                     cmd.Parameters.Add(new SqlParameter("@sortby", "desc"));
                     reader = cmd.ExecuteReader();
                     while (reader.Read())
                     {
                         for (int i = 0; i < reader.FieldCount; i++)
                         {
                             Console.Write(reader[i].ToString().Trim() + " ");
                         }
                         Console.WriteLine();
                     }
                 }
             }*/

            College_Sports_Management_System_DB Db = new College_Sports_Management_System_DB();
           //Db.AddSports(new List<string>() { "1", "Badminton" });
            //Db.AddPlayers(new List<string>() { "1", "Player1" });
            //Db.AddTournament(new List<string>() { "1", "SUMMER_2023" });
            //Db.AddScoreBoard(new List<string>() { "1", "1","1","35" });
            Db.RemovePlayers(1);
            Db.RemoveTournament(1);

        }
    }
}