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
using static Azure.Core.HttpHeader;
using static System.Formats.Asn1.AsnWriter;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace ProGradWeek_3_Assesment
{
    /*College sports management system using c#.net

    PROJECT DESCRIPTION: The College Sports Management System’s objective is to provide a which manages the activity of many sports
            at a time.It also manages the registration process and announcement of the results.
        Modules
        Add sports
    We can add new sports into the system so that we are able to retrieve them later during the registration process.The sports 
            added would be viewed during the creation of a new intra- college or university tournament.

    Add Scoreboard
    We add a scoreboard so that the students can view it and the results of each match are announced here so that there will be 
                only one platform for the results. This would reduce the chaos during the score announcement.



    Add Tournament

    Each tournament from an intra-college or a university can be added here. It later would help in the registration of any sports
                in that tournament. While adding a new tournament the system would show the set of sports that are entered into the
            system by the director of the sports so only those sports can be included in the tournament while creation.

    Remove Sports

    This module will help in the removal of any sports that the sports director thinks are not needed in the system.The removed 
        sports would not be shown anywhere in the system that includes during the addition of a new tournament.

    Edit Scoreboard

    The added scoreboards would be updated here.This module helps in updating the scores on the scoreboard.Only the scoreboard
        which is added using the add scoreboard module would be present here and only these scoreboards can be updated.We won’t 
            be able to add a new scoreboard here.
        Remove players

    This module would remove each player after each round of the tournament.So that only the existing player will be present and 
                the one that is not qualified for the next round would be removed using this module.This would give a clear picture 
                    of the qualified players. As well as the player from the college team I can also be removed.

    Remove Tournament

    After each tournament in the college or a university, we should remove it, so that there won’t be any confusion between different
                    tournaments which are going to be held later on.This module would help in removing all the details of the deleted
                    tournament.



    <Optional if time permits>



    Registration Individual

    This module would help in the registration of individual sports events held in the tournament. We selected 
        the tournament in which we want to be part and the sports in which we want to participate in and the player 
        would add his name and the required details asked in the registration form.After all these processes then we can 
        click on the submit button and the student has registered for the tournament that they wish to participate in.

    Registration Group

    This module would help in the registration of group sports events held in the tournament. We selected the tournament in 
        which we want to be part and the sports in which we want to participate in and the set of player’s names would be added
        and the other required details asked for in the registration form.After all these processes then we can click on the submit
        button and the student has registered for the tournament that they wish to participate in.

    Payment

    This module would help to book.So that the students wouldn’t have to stand in a queue or have hard cash in hand in order to do
        any payment to the sports department. By using this module we are reducing a lot of paperwork and we are giving the students
            the liberty of doing the payment from wherever they are.

    This College Sports Resource Booking Project is related to the College Sports Management System Project*/
    public class IndividualRegistrationData
    {
        public int TOURNAMENT_ID;
        public int PLAYER_ID;
        public List<int> SPORT_IDS;
        public IndividualRegistrationData(int TOURNAMENT_ID,int PLAYER_ID,List<int> SPORT_IDS)
        {
            this.TOURNAMENT_ID = TOURNAMENT_ID;
            this.PLAYER_ID = PLAYER_ID;
            this.SPORT_IDS = SPORT_IDS;

        }
    }
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
        public void AddOrEditScoreBoard(List<string> Data)
        {
            //Data list takes sport_id,tournament_id,player_id,score
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
                //ADDING PLAYER AS COLUMN IF DOESNOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"IF NOT EXISTS(SELECT* FROM INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME = '{TableName}' AND COLUMN_NAME = '{PlayerName}')"
                        + $"ALTER TABLE {TableName} ADD {PlayerName} int NULL";

                    cmd.ExecuteNonQuery();
                }
                //ADDING ROW IF DOESNOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"IF NOT EXISTS (SELECT* FROM {TableName} WHERE  SPORT_ID = {Data[0]} AND TOURNAMENT_ID = {Data[1]}) "+
                                    $"BEGIN INSERT INTO {TableName} (SPORT_ID,SPORT_NAME,TOURNAMENT_ID ,TOURNAMENT_NAME,{PlayerName})" +
                                    $" SELECT {Data[0]},S.SPORT_NAME,{Data[1]},T.TOURNAMENT_NAME,{Data.Last()} FROM SPORTS AS S,TOURNAMENTS AS T " +
                                       $"WHERE S.SPORT_ID={Data[0]} AND T.TOURNAMENT_ID={Data[1]} END"+
                                       $" ELSE BEGIN UPDATE {TableName} SET {PlayerName}={Data.Last()} WHERE SPORT_ID={Data[0]} AND TOURNAMENT_ID={Data[1]} END";

                    cmd.ExecuteNonQuery();
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
        public void RegistrationIndidvidual(IndividualRegistrationData Data)
        {
            string TableName = "REGISTRATIONS";
            using (SqlConnection conn = new SqlConnection(connetionString))
            {
                conn.Open();
                //CREATE TABLE IF DOES NOT EXIST
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{TableName}' and xtype='U')" +
                        $"CREATE TABLE {TableName} (TOURNAMENT_ID INT,PLAYER_ID INT,SPORT_ID INT)";

                    cmd.ExecuteNonQuery();
                }
                //ADDING THE REGISTRATIONS INTO TABLE
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        foreach (int Sport_Id in Data.SPORT_IDS)
                        { 
                            cmd.CommandText = $"INSERT INTO {TableName} VALUES({Data.TOURNAMENT_ID},{Data.PLAYER_ID},{Sport_Id})";
                            cmd.ExecuteNonQuery();
                        }
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
        public void RegistrationGroup(List<IndividualRegistrationData> Data)
        {
            College_Sports_Management_System_DB Db = new College_Sports_Management_System_DB();
            //REGISTARTION WITH INDIVIDUAL DATA LIST AND METHOD
            foreach (IndividualRegistrationData person_data in Data)
            {
                Db.RegistrationIndidvidual(person_data);
            }
        }
    }
    public class Program
    {
        public static College_Sports_Management_System_DB Db = new College_Sports_Management_System_DB();
        public static void AddSportsData()
        {
            Db.AddSports(new List<string>() { "1", "Badminton" });
            Db.AddSports(new List<string>() { "2", "Table Tennis" });
            Db.AddSports(new List<string>() { "3", "Tennis" });
            Db.AddSports(new List<string>() { "4", "Boxing" });
            Db.AddSports(new List<string>() { "5", "Swimming" });
            Db.AddSports(new List<string>() { "6", "Shooting" });
            Db.AddSports(new List<string>() { "7", "Chess" });
            Db.AddSports(new List<string>() { "8", "Weight Lifting" });
        }
        public static void AddPlayersData()
        {
            Db.AddPlayers(new List<string>() { "1", "Player1" });
            Db.AddPlayers(new List<string>() { "2", "Player2" });
            Db.AddPlayers(new List<string>() { "3", "Player3" });
            Db.AddPlayers(new List<string>() { "4", "Player4" });
            Db.AddPlayers(new List<string>() { "5", "Player5" });
            Db.AddPlayers(new List<string>() { "6", "Player6" });
            Db.AddPlayers(new List<string>() { "7", "Player7" });
            Db.AddPlayers(new List<string>() { "8", "Player8" });
        }
        public static void AddTournamentData()
        {
            Db.AddTournament(new List<string>() { "1", "SUMMER_2023" });
            Db.AddTournament(new List<string>() { "2", "WINTER_2023" });
        }
        public static void AddScoreboardData()
        {
            Db.AddOrEditScoreBoard(new List<string>() { "1", "1", "1", "35" });
            Db.AddOrEditScoreBoard(new List<string>() { "1", "1", "2", "35" });
            Db.AddOrEditScoreBoard(new List<string>() { "1", "1", "1", "25" });
            Db.AddOrEditScoreBoard(new List<string>() { "1", "2", "1", "77" });
            Db.AddOrEditScoreBoard(new List<string>() { "1", "2", "2", "55" });
            Db.AddOrEditScoreBoard(new List<string>() { "1", "2", "1", "66" });
            Db.AddOrEditScoreBoard(new List<string>() { "3", "1", "1", "47" });
            Db.AddOrEditScoreBoard(new List<string>() { "3", "1", "2", "57" });
            Db.AddOrEditScoreBoard(new List<string>() { "3", "1", "1", "27" });
            Db.AddOrEditScoreBoard(new List<string>() { "3", "2", "1", "99" });
            Db.AddOrEditScoreBoard(new List<string>() { "3", "2", "2", "88" });
            Db.AddOrEditScoreBoard(new List<string>() { "3", "2", "1", "72" });
        }
        public static void Registations()
        {   
            IndividualRegistrationData Player_data_1=new IndividualRegistrationData(1,1,new List<int> {1,2,3,5,6,7});
            IndividualRegistrationData Player_data_2=new IndividualRegistrationData(2,5,new List<int> {3,4,5,6,7});
            IndividualRegistrationData Player_data_3=new IndividualRegistrationData(1,7,new List<int> {3,1,6,2});

            Db.RegistrationIndidvidual(Player_data_1);
            Db.RegistrationGroup(new List<IndividualRegistrationData> { Player_data_2,Player_data_3});
        }
        static void Main(string[] args)
        {
            AddSportsData();
            AddPlayersData();
            AddTournamentData();
            AddScoreboardData();
            Registations();
            Db.RemovePlayers(1);
            Db.RemoveTournament(1);

        }
    }
}