using System;
using System.Collections.Generic;
using MikRedisDB;

namespace PocketChatAdminClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Loop Control Variables
            bool isConnectionEstablished = false;
            bool isAdminFinished = false;
            //Redis Client Handler
            RedisDBController redis = new RedisDBController();
            //Input variables
            string input;
            //Output variables
            string messageToAdmin = null;

            while (isConnectionEstablished == false)
            {
                //Welcome UI
                Console.WriteLine("==========================================================");
                Console.WriteLine("================Welcome to the POCKETCHAT!!===============");
                Console.WriteLine("===================Admin Client Portal====================");
                Console.WriteLine("===============CONNECT=================EXIT===============");
                Console.WriteLine(messageToAdmin);
                Console.Write("> ");
                input = Console.ReadLine();

                //Check for correct input and attempt connection
                switch (input)
                {
                    case "CONNECT":
                    case "connect":
                        Console.Write("Connecting to Redis server. . .");
                        try
                        {
                            redis.SetConfigurationOptions("10.100.58.10", 30433, "433redis!");              //Setup Configuration Options
                            redis.SetupConnection();                                                        //Attempt connection
                        }
                        catch
                        {
                            messageToAdmin = "\t\t---Connection Failed---";
                            break;
                        }
                        Console.WriteLine(" . . .Connected!");
                        messageToAdmin = "\t\t---Connection Succeeded---";
                        isConnectionEstablished = true;
                        break;
                    case "EXIT":
                    case "exit":
                        Console.WriteLine("Thank you for using POCKETCHAT Admin Client Portal. Enter anything to exit.");
                        Console.ReadLine();
                        return;
                    default:
                        messageToAdmin = "\t\t---Invalid Command---";
                        break;
                }
                Console.Clear();
            }

            //Variables for holding general command results
            bool flag = false;
            bool dbEmulationMode = false;
            bool menuOn = true;
            byte menuID = 0;
            int number = 0;
            int numResult = 0;
            uint time = 0;
            long bigNumber = 0;
            DateTime timeNow = new DateTime(1970, 1, 1);
            DateTime timeLater;
            ConsoleKeyInfo ckey;
            string response = null;
            string name;
            string password;
            string[] nameList;
            Dictionary<string, double> rankList;

            //UI Top
            Console.WriteLine("==========================================================");
            Console.WriteLine("================Welcome to the POCKETCHAT!!===============");
            Console.WriteLine("===================Admin Client Portal====================");
            Console.WriteLine("==========================================================");
            Console.WriteLine(messageToAdmin);
            messageToAdmin = null;

            //This while loop runs until the admin wants to quit the program
            while (isAdminFinished == false)
            {
                if (menuOn)                                                                                 //If the menu is on, print the menu
                {
                    switch (menuID)                                                                         //Print the menu in which the admin is located
                    {
                        case 1:
                            Console.WriteLine("\t*****User and Room Information Commands*****");
                            Console.WriteLine("\tuexist \t\t- \tCheck User Existence");
                            Console.WriteLine("\tchklogin \t- \tCheck If User is Logged In");
                            Console.WriteLine("\tchkdummy \t- \tCheck If User is a Dummy");
                            Console.WriteLine("\tchkblock \t- \tCheck If User is Blocked");
                            Console.WriteLine("\tgetrank \t- \tGet User's Rank");
                            Console.WriteLine("\tgetuloc \t- \tGet User's Location");
                            Console.WriteLine("\tgetconnid \t- \tGet User Connection ID");
                            Console.WriteLine("\tgetsusp \t- \tGet User Suspend Time");
                            Console.WriteLine("\tgetmsgcnt \t- \tGet User Message Count");
                            Console.WriteLine("\tgetrmsgcnt \t- \tGet User Message Count at rank");
                            Console.WriteLine("\tgetruser \t- \tGet User at rank");
                            Console.WriteLine("\trexist \t\t- \tCheck Room Existence");
                            Console.WriteLine("\trconuser \t- \tCheck If a User is in a room");
                            Console.WriteLine("\trtitle \t\t- \tGet room title");
                            Console.WriteLine("\trucount \t- \tGet room user count");
                            Console.WriteLine("\trowner \t\t- \tGet room owner");
                            Console.WriteLine("\trsizerank \t- \tGet room size rank");
                            Console.WriteLine("\tback \t\t- \tReturn to Main Menu");
                            break;
                        case 2:
                            Console.WriteLine("\t*****Moderator Commands*****");
                            Console.WriteLine("\tnew \t\t- \tCreate New User");
                            Console.WriteLine("\tdel \t\t- \tDelete User");
                            Console.WriteLine("\trnew \t\t- \tCreate New Room");
                            Console.WriteLine("\trdel \t\t- \tDelete Room");
                            Console.WriteLine("\tchgname \t- \tChange Username");
                            Console.WriteLine("\tchgpass \t- \tChange User Password");
                            Console.WriteLine("\tchgconn \t- \tChange User Connection ID");
                            Console.WriteLine("\tblkuser \t- \tBlock User");
                            Console.WriteLine("\tublkuser \t- \tUnblock User");
                            Console.WriteLine("\tincmsgcnt \t- \tModify a User's Message Count");
                            Console.WriteLine("\tlogin \t\t- \tAttempt User Login");
                            Console.WriteLine("\tlogout \t\t- \tAttempt User Logout");
                            Console.WriteLine("\trchgtitle \t- \tChange Room Title");
                            Console.WriteLine("\trsetown \t- \tChange Room Owner");
                            Console.WriteLine("\tradduse \t- \tAdd user to room");
                            Console.WriteLine("\trremuse \t- \tRemove user from room");
                            Console.WriteLine("\tback \t\t- \tReturn to Main Menu");
                            break;
                        case 3:
                            Console.WriteLine("\t*****DB Statistics Commands*****");
                            Console.WriteLine("\tnuser \t\t- \tGet Number of Users in the UserPool");
                            Console.WriteLine("\tgetulist \t- \tGet User List in the UserPool");
                            Console.WriteLine("\tgetrlist \t- \tGet Top-n Rank List from the UserPool");
                            Console.WriteLine("\tnloguser \t- \tGet Number of Users in the LoginUserPool");
                            Console.WriteLine("\tgetloglist \t- \tGet User List in the LoginUserPool");
                            Console.WriteLine("\tgetsubrlist \t- \tGet Top-n Rank List from UserPool Subset");
                            Console.WriteLine("\trulist \t\t- \tGet Rooms's User List");
                            Console.WriteLine("\trlist \t\t- \tGet Room List");
                            Console.WriteLine("\trranklist \t- \tGet Room Ranking List");
                            Console.WriteLine("\tloblist \t- \tGet Lobby User List");
                            Console.WriteLine("\tback \t\t- \tReturn to Main Menu");
                            break;
                        case 0:
                        default:
                            if (dbEmulationMode == true)
                            {
                                Console.WriteLine("\t*****WARNING: Moderator Mode Permanently Alters DB Information*****");
                                
                            }
                            Console.WriteLine("\tstats \t\t- \tGet DB statistics commands");
                            Console.WriteLine("\tinfolist \t- \tGet User and Room Information Commands");
                            if (dbEmulationMode == false)
                            {
                                Console.WriteLine("\tmmode \t\t- \tEnter Moderator Mode");
                            } else
                            {
                                Console.WriteLine("\tmodlist \t- \tGet full list of room commands");
                            }
                            Console.WriteLine("\texit \t\t- \tQuit");
                            break;
                    }

                    //If menu mode is on, get the next command then redraw the window
                    Console.Write("\n> ");
                    response = Console.ReadLine();
                    Console.Clear();

                    menuOn = false;
                }

                //UI Top
                Console.WriteLine("==========================================================");
                Console.WriteLine("================Welcome to the POCKETCHAT!!===============");
                Console.WriteLine("===================Admin Client Portal====================");
                Console.WriteLine("==========================================================");
                messageToAdmin = null;

                //This switch statement takes care of all possible commands
                switch (response)
                {
                    //***Menu Commands***
                    case "infolist":
                        menuID = 1;
                        menuOn = true;
                        break;
                    case "modlist":
                        menuID = 2;
                        menuOn = true;
                        break;
                    case "stats":
                        menuID = 3;
                        menuOn = true;
                        break;
                    case "back":
                        menuID = 0;
                        menuOn = true;
                        break;
                    case "menu":
                        menuOn = true;
                        break;
                    //***Activation of Moderator Mode***
                    case "mmode":
                        Console.Write("Please enter the db emulation password: ");
                        password = Console.ReadLine();
                        if (password.CompareTo("3dalkgalbi!") == 0)
                        {
                            messageToAdmin = "\t***Moderator Mode Activated!***";
                            dbEmulationMode = true;
                        } else
                        {
                            messageToAdmin = "\t***Moderator Mode Activation Failed!***";
                        }
                        break;
                    //***User related commands***
                    case "new":         //Create new user
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("Please enter a password: ");
                        password = Console.ReadLine();
                        flag = redis.CreateUser(name, password);
                        if (flag == true)
                        {
                            Console.WriteLine(name + " successfully added to the UserPool.");
                        }
                        else
                        {
                            Console.WriteLine(name + " is already in the UserPool.");
                        }
                        break;
                    case "del":         //Delete User
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("Please enter a password: ");
                        password = Console.ReadLine();
                        flag = redis.DeleteUser(name, password);
                        if (flag == true)
                        {
                            Console.WriteLine(name + " successfully deleted from the UserPool.");
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Username or Password.");
                        }
                        break;
                    case "uexist":      //Check user existence
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        if (redis.DoesUsernameExist(name))
                        {
                            Console.Write(name + " exists in the UserPool.");
                        }
                        else
                        {
                            Console.Write(name + " does not exist in the UserPool.");
                        }
                        break;
                    case "chklogin":    //Check user login status
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        number = redis.IsUserLoggedIn(name);
                        switch (number)
                        {
                            case -1:
                                Console.WriteLine("User does not exist.");
                                break;
                            case 0:
                                Console.WriteLine(name + " is not logged in.");
                                break;
                            case 1:
                                Console.WriteLine(name + " is logged in.");
                                break;
                            default:
                                Console.WriteLine("Unexpected Error");
                                break;
                        }
                        break;
                    case "chkdummy":    //Check user dummy status
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        number = redis.IsUserDummy(name);
                        switch (number)
                        {
                            case -1:
                                Console.WriteLine("User does not exist.");
                                break;
                            case 0:
                                Console.WriteLine(name + " is not a dummy client.");
                                break;
                            case 1:
                                Console.WriteLine(name + " is a dummy client.");
                                break;
                            default:
                                Console.WriteLine("Unexpected Error");
                                break;
                        }
                        break;
                    case "chkblock":    //Check user block status
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        number = redis.IsUserBlocked(name);
                        switch (number)
                        {
                            case -1:
                                Console.WriteLine("User does not exist.");
                                break;
                            case 0:
                                Console.WriteLine(name + " is not blocked.");
                                break;
                            case 1:
                                Console.WriteLine(name + " is blocked.");
                                break;
                            case 2:
                                Console.WriteLine(name + " is no longer blocked.");
                                break;
                            default:
                                Console.WriteLine("Unexpected Error");
                                break;
                        }
                        break;
                    case "chgname":     //Change username
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("Please enter a password: ");
                        password = Console.ReadLine();
                        Console.Write("Please desired new name: ");
                        response = Console.ReadLine();
                        numResult = redis.ChangeUsername(name, response, password);
                        switch (numResult)
                        {
                            case -3:
                                Console.WriteLine(response + " is already in use.");
                                break;
                            case -2:
                                Console.WriteLine("Current username is already " + response + ".");
                                break;
                            case -1:
                                Console.WriteLine(name + " is blocked.");
                                break;
                            case 0:
                                Console.WriteLine("Incorrect Username or Password.");
                                break;
                            case 1:
                                Console.WriteLine("Username successfully changed from " + name + " to " + response + ".");
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "chgpass":     //Change user password
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("Please enter a password: ");
                        password = Console.ReadLine();
                        Console.Write("Please desired new password: ");
                        response = Console.ReadLine();
                        numResult = redis.ChangePassword(name, password, response);
                        switch (numResult)
                        {
                            case -2:
                                Console.WriteLine("Password change unnecessary.");
                                break;
                            case -1:
                                Console.WriteLine(name + " is blocked.");
                                break;
                            case 0:
                                Console.WriteLine("Incorrect Username or Password.");
                                break;
                            case 1:
                                Console.WriteLine("Password successfully changed.");
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "chgconn":     //Change connection ID
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("\nPlease enter a new connection ID: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        numResult = redis.ChangeConnectionID(name, number);
                        if (numResult < 0)
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        else
                        {
                            Console.WriteLine(name + "'s connection ID has been changed from " + numResult + " to " + number + ".");
                        }
                        break;
                    case "blkuser":     //Block User
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("\nPlease enter a time penalty in minutes: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        time = redis.BlockUser(name, (uint)Math.Abs(number));
                        if (time == 0)
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        else
                        {
                            Console.WriteLine(name + " has been blocked by (an additional) " + number + " minute(s)");
                            timeLater = timeNow.AddSeconds(time);
                            Console.WriteLine("Suspension will end at " + timeLater + ".");
                        }
                        break;
                    case "ublkuser":    //Unblock user
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        numResult = redis.UnBlockUser(name);
                        switch (numResult)
                        {
                            case -1:
                                Console.WriteLine("User is not blocked.");
                                break;
                            case 0:
                                Console.WriteLine("User does not exist.");
                                break;
                            case 1:
                                Console.WriteLine(name + " has been unblocked.");
                                break;
                            default:
                                Console.WriteLine("Unknown error.");
                                break;
                        }
                        break;
                    case "getrank":     //Get user's rank
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        number = redis.GetUserRank(name);
                        if (number >= 0)
                        {
                            Console.WriteLine(name + " has a rank of " + number + ".");
                        }
                        else
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        break;
                    case "getuloc":     //Get user location
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        number = redis.GetUserLocation(name);
                        if (number == 0)
                        {
                            Console.WriteLine(name + " is located in the Lobby.");
                        }
                        else if (number > 0)
                        {
                            Console.WriteLine(name + " is located in Room " + number + ".");
                        }
                        else
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        break;
                    case "getconnid":   //Get user connection ID
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        number = redis.GetUserConnectionID(name);
                        if (number < 0)
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        else
                        {
                            Console.WriteLine(name + " has a connection ID of " + number);
                        }
                        break;
                    case "getsusp":     //Get suspension time
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        time = redis.GetUserSuspendTime(name);
                        if (time == 0)
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        else if (time == 1)
                        {
                            Console.WriteLine("User is not blocked.");
                        }
                        else if (time == 2)
                        {
                            Console.WriteLine("User is no longer blocked.");
                        }
                        else
                        {
                            timeLater = timeNow.AddSeconds(time);
                            Console.WriteLine(name + " is suspended until " + timeLater + ".");
                        }
                        break;
                    case "getmsgcnt":   //Get user message count
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        number = redis.GetUserMessageCount(name);
                        if (number < 0)
                        {
                            Console.WriteLine("User does not exist.");
                        }
                        else
                        {
                            Console.WriteLine(name + " has a message count of " + number);
                        }
                        break;
                    case "getrmsgcnt":  //Get message count by rank
                        Console.Write("Please enter a rank value: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        numResult = redis.GetMessageCountAtRank(number);
                        if (numResult < 0)
                        {
                            Console.WriteLine("Rank does not exist.");
                        }
                        else
                        {
                            Console.WriteLine("Rank " + number + " has a message count of " + numResult);
                        }
                        break;
                    case "getruser":    //Get user by rank
                        Console.Write("Please enter a rank value: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        response = redis.GetUsernameAtRank(number);
                        if (response.CompareTo("0") == 0)
                        {
                            Console.WriteLine("Rank does not exist.");
                        }
                        else
                        {
                            Console.WriteLine("Rank " + number + " is held by " + response);
                        }
                        break;
                    case "incmsgcnt":   //Modify message count
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("Please enter an amount to increment by: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        numResult = redis.AddToUserMessageCount(name, number);
                        if (numResult >= 0)
                        {
                            Console.WriteLine(name + "'s message count updated by " + number + " to " + numResult + ".");
                        }
                        else if (numResult == -1)
                        {
                            Console.WriteLine("Incorrect Username or Password.");
                        }
                        else
                        {
                            Console.WriteLine("Unknown Error.");
                        }
                        break;
                    case "login":       //Login attempt
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        Console.Write("Please enter a password: ");
                        password = Console.ReadLine();
                        Console.Write("Is the user a dummy? (y = yes) ");
                        ckey = Console.ReadKey();
                        if (ckey.KeyChar == 'y' || ckey.KeyChar == 'Y')
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                        Console.Write("\nPlease enter a connection ID: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        numResult = redis.Login(name, password, number, flag);
                        switch (numResult)
                        {
                            case -1:
                                Console.WriteLine("User is blocked.");
                                break;
                            case 0:
                                Console.WriteLine("Incorrect Username or Password.");
                                break;
                            default:
                                if (number != numResult)                                    //If the passed connection ID is different than the result, that means the connection was overridden
                                {
                                    Console.WriteLine(name + " was already logged in and their session has been overridden");
                                    Console.WriteLine(name + "'s connection ID has been changed from " + numResult + " to " + number + ".");
                                }
                                Console.WriteLine("Login Successful!");
                                break;
                        }
                        break;
                    case "logout":      //Logout attempt
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("Please enter a username: ");
                        name = Console.ReadLine();
                        numResult = redis.Logout(name);
                        switch (numResult)
                        {
                            case -1:
                                Console.WriteLine("User does not exist.");
                                break;
                            case 0:
                                Console.WriteLine(name + " is not logged in.");
                                break;
                            case 1:
                                Console.WriteLine(name + " has been logged out.");
                                break;
                            default:
                                Console.WriteLine("Unknown error.");
                                break;
                        }
                        break;
                    case "nuser":       //Number of registered users
                        bigNumber = redis.GetUserPoolSize();
                        Console.WriteLine(bigNumber + " user(s) in the UserPool.");
                        break;
                    case "getulist":    //Get user list
                        nameList = redis.GetUserList();
                        for (int i = 0; i < nameList.Length; i++)
                        {
                            Console.Write(nameList[i] + "\t");
                            if ((i + 1) % 5 == 0)
                            {
                                Console.Write("\n");
                            }
                        }
                        Console.Write("\n");
                        break;
                    case "getrlist":    //Get rank listing
                        Console.Write("\nPlease enter the top-n list value (0 for full list): ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        rankList = redis.GetTopList(--number);
                        int x = 0;
                        foreach (KeyValuePair<string, double> keyValuePair in rankList)
                        {
                            x++;
                            Console.WriteLine("Rank " + x + ":\t" + keyValuePair.Key + "\t\t" + keyValuePair.Value);
                        }
                        break;
                    case "nloguser":    //Number of logged in users
                        bigNumber = redis.GetLoginPoolSize();
                        Console.WriteLine(bigNumber + " user(s) logged in.");
                        break;
                    case "getloglist":  //Get login list
                        nameList = redis.GetLoginList();
                        for (int i = 0; i < nameList.Length; i++)
                        {
                            Console.Write(nameList[i] + "\t");
                            if ((i + 1) % 5 == 0)
                            {
                                Console.Write("\n");
                            }
                        }
                        Console.Write("\n");
                        break;
                    case "getsubrlist": //Get ranking sub-list
                        Console.Write("Please enter a sub-pool name (LoginPool, DummyPool, etc): ");
                        name = Console.ReadLine();
                        Console.Write("\nPlease enter the top-n list value (0 for full list): ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        rankList = redis.GetSubTopList(--number, name);
                        int y = 0;
                        foreach (KeyValuePair<string, double> keyValuePair in rankList)
                        {
                            y++;
                            Console.WriteLine("Rank " + y + ":\t" + keyValuePair.Key + "\t\t" + (keyValuePair.Value - 1));
                        }
                        break;
                    case "exit":        //Exit application
                        isAdminFinished = true;
                        menuOn = true;
                        break;
                    case "rnew":        //Create new room
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        Console.Write("Please enter a room title: ");
                        response = Console.ReadLine();
                        Console.Write("Please enter a room owner: ");
                        name = Console.ReadLine();
                        numResult = redis.RoomCreate((uint)number, response, name);
                        switch (numResult)
                        {
                            case -1:
                                Console.WriteLine("Room already exists.");
                                break;
                            case 0:
                                Console.WriteLine("Owner does not exist.");
                                break;
                            case 1:
                                Console.WriteLine("Room " + number + " - \"" + response + "\" successfully created and owned by " + name + ".");
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "rdel":        //Delete room
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.WriteLine("Cannot delete a room manually. The room will delete upon the last user leaving.");
                        break;
                    case "rexist":      //Check room existence
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        if (redis.RoomExist((uint)number))
                        {
                            Console.WriteLine("Room " + number + " exists.");
                        }
                        else
                        {
                            Console.WriteLine("Room " + number + " does not exist.");
                        }
                        break;
                    case "rconuser":    //Check if room contains a user
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        Console.Write("Please enter a name: ");
                        name = Console.ReadLine();
                        numResult = redis.RoomContainsUser((uint)number, name);
                        switch (numResult)
                        {
                            case -2:
                                Console.WriteLine("User does not exist.");
                                break;
                            case -1:
                                Console.WriteLine("Room does not exist.");
                                break;
                            case 0:
                                Console.WriteLine(name + " is not in Room " + number + ".");
                                break;
                            case 1:
                                Console.WriteLine(name + " is in Room " + number + ".");
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "rtitle":      //Check room title
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        if (redis.RoomGetTitle((uint)number, out name))
                        {
                            Console.WriteLine("Room " + number + "'s title is \"" + name + ".");
                        }
                        else
                        {
                            Console.WriteLine("Room does not exist.");
                        }
                        break;
                    case "rucount":     //Get room's user count
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        numResult = redis.RoomGetUserCount((uint)number);
                        if (numResult < 0)
                        {
                            Console.WriteLine("Room does not exist.");
                        }
                        else
                        {
                            Console.WriteLine("Room " + number + " contains " + numResult + " user(s).");
                        }
                        break;
                    case "rowner":      //Get room's owner
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        if (redis.RoomGetOwner((uint)number, out name))
                        {
                            Console.WriteLine("Room " + number + "'s owner is " + name + ".");
                        }
                        else
                        {
                            Console.WriteLine("Room does not exist.");
                        }
                        break;
                    case "rsizerank":   //Get room's size rank
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        numResult = redis.RoomGetSizeRank((uint)number);
                        if (numResult < 0)
                        {
                            Console.WriteLine("Room does not exist.");
                        }
                        else
                        {
                            Console.WriteLine("Room " + number + "'s size ranking is " + numResult + ".");
                        }
                        break;
                    case "rchgtitle":   //Change room title
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        Console.Write("Please enter a new room title: ");
                        response = Console.ReadLine();
                        numResult = redis.RoomChangeTitle((uint)number, response, out name);
                        switch (numResult)
                        {
                            case -1:
                                Console.WriteLine("Room does not exist.");
                                break;
                            case 0:
                                Console.WriteLine("The new room title is not different than the current setting.");
                                break;
                            case 1:
                                Console.WriteLine("Room " + number + "'s title has been changed from \"" + name + "\" to \"" + response + ".\"");
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "rsetown":     //Change room owner
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        Console.Write("Please enter a new room owner name: ");
                        response = Console.ReadLine();
                        numResult = redis.RoomSetOwner((uint)number, response, out name);
                        switch (numResult)
                        {
                            case -1:
                                Console.WriteLine("Room does not exist.");
                                break;
                            case 0:
                                Console.WriteLine("The new room owner is not different than the current setting.");
                                break;
                            case 1:
                                Console.WriteLine("Room " + number + "'s owner has been changed from " + name + " to " + response + ".");
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "radduse":     //Add user to room
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        Console.Write("Please enter a name: ");
                        name = Console.ReadLine();
                        numResult = redis.RoomAddUser((uint)number, name);
                        switch (numResult)
                        {
                            case -2:
                                Console.WriteLine(name + " already in Room " + number + ".");
                                break;
                            case -1:
                                Console.WriteLine("Room does not exist.");
                                break;
                            case 0:
                                Console.WriteLine("User does not exist.");
                                break;
                            case 1:
                                Console.WriteLine(name + " added to Room " + number + ".");
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "rremuse":     //Remove user from room
                        if (dbEmulationMode == false)
                        {
                            messageToAdmin = "***Error: DB Moderator Mode must be activated before using command \'" + response + "\'***";
                            break;
                        }
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        Console.Write("Please enter a name: ");
                        name = Console.ReadLine();
                        numResult = redis.RoomRemoveUser((uint)number, name);
                        switch (numResult)
                        {
                            case -3:
                                Console.WriteLine(name + " is not in Room " + number + ".");
                                break;
                            case -2:
                                Console.WriteLine(name + " removed from Room " + number + ".");
                                Console.WriteLine("Error while trying to close Room " + number + "due to it being empty.");
                                break;
                            case -1:
                                Console.WriteLine("Room does not exist.");
                                break;
                            case 0:
                                Console.WriteLine("User does not exist.");
                                break;
                            case 1:
                                Console.WriteLine(name + " removed from Room " + number + ".");
                                break;
                            case 2:
                                Console.WriteLine(name + " removed from Room " + number + ".");
                                Console.WriteLine("Room " + number + "'s was closed because it became empty.");
                                break;
                            case 3:
                                Console.WriteLine(name + " removed from Room " + number + ".");
                                Console.WriteLine("Error in re-assigning room's owner.");
                                break;
                            case 4:
                                Console.WriteLine(name + " removed from Room " + number + ".");
                                if (redis.RoomGetOwner((uint)number, out response))
                                {
                                    Console.WriteLine("Room " + number + "'s owner changed from " + name + " to " + response + ".");
                                }
                                else
                                {
                                    Console.WriteLine("Error in re-assigning room's owner.");
                                }
                                break;
                            default:
                                Console.WriteLine("Unknown Error.");
                                break;
                        }
                        break;
                    case "rulist":      //Get room's user list
                        Console.Write("\nPlease enter room number: ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        nameList = redis.RoomUserList((uint)number);
                        if (nameList == null)
                        {
                            Console.WriteLine("Room does not exist.");
                        }
                        else
                        {
                            for (int i = 0; i < nameList.Length; i++)
                            {
                                Console.Write(nameList[i] + "\t");
                                if ((i + 1) % 5 == 0)
                                {
                                    Console.Write("\n");
                                }
                            }
                            Console.Write("\n");
                        }
                        break;
                    case "rlist":       //Get room list
                        nameList = redis.RoomList();
                        if (nameList == null)
                        {
                            Console.WriteLine("No rooms exist.");
                        }
                        else
                        {
                            for (int i = 0; i < nameList.Length; i++)
                            {
                                Console.Write(nameList[i] + "\t");
                                if (redis.RoomGetTitle(uint.Parse(nameList[i].Substring(5)), out name))
                                {
                                    Console.Write(" - \"" + name + "\"");
                                }
                                Console.Write("\t");
                                if ((i + 1) % 4 == 0)
                                {
                                    Console.Write("\n");
                                }
                            }
                            Console.Write("\n");
                        }
                        break;
                    case "rranklist":   //Get Room Ranking List
                        Console.Write("\nPlease enter the top-n list value (0 for full list): ");
                        if (!int.TryParse(Console.ReadLine(), out number))
                        {
                            Console.WriteLine("\t---Incorrect Input---");
                            break;
                        }
                        rankList = redis.RoomSizeRankList(--number);
                        int z = 0;
                        foreach (KeyValuePair<string, double> keyValuePair in rankList)
                        {
                            z++;
                            Console.WriteLine("Rank " + z + ":\t" + keyValuePair.Key + "\t\t" + keyValuePair.Value);
                        }
                        break;
                    case "loblist":     //Get lobby list
                        nameList = redis.GetLobbyUserList();
                        if (nameList == null)
                        {
                            Console.WriteLine("The Lobby is empty.");
                        }
                        else
                        {
                            for (int i = 0; i < nameList.Length; i++)
                            {
                                Console.Write(nameList[i] + "\t");
                                if ((i + 1) % 5 == 0)
                                {
                                    Console.Write("\n");
                                }
                            }
                            Console.Write("\n");
                        }
                        break;
                    default:            //Incorrect command, print invalid entry
                        messageToAdmin = "\n\t\t---Invalid Entry---";
                        break;
                }
                
                //If there was a admin message, print it
                Console.WriteLine(messageToAdmin);

                //If the menu wasn't on, get the next command
                if (!menuOn)
                {
                    //Get next command
                    Console.Write("\nTIP: Enter 'menu' to return to the command list menu.");
                    Console.Write("\n> ");
                    response = Console.ReadLine();
                    Console.Clear();
                }
            }

            //Close with a kind message followed by connection closure
            Console.WriteLine("Thank you for using the Redis Test Client. Enter anything to exit.");
            Console.ReadLine();
            redis.CloseConnection();
        }
    }
}
