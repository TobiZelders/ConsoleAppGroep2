﻿using System;

namespace cinemaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Films.FilmMain();
            Navigation.ManagerNavigation();
            User user = new User();
            Console.WriteLine("Please pick a option.\n1: Login.\n2: Register.\n3: Continue as guest.\n0: exit");
            int choice = ChoiceInput(0, 3);
            if (choice == 1) {
               Login(user);
            } else if (choice == 2) {
                user.CreateAccount();
            } else if (choice == 3) {
                user.ContinueAsGuest();
            } else if (choice == 0) {
                Environment.Exit(0);
            }

            if (user.username == "owner") {
                Navigation.OwnerNavigation();
            } else if (user.username == "manager") {
                Navigation.ManagerNavigation();
            } else if (user.username == "caterer") {
                Navigation.CatererNavigation();
            } else {
                Navigation.CustomerNavigation();
            }
            //Login(user);
            //Register(user);
            //ReserveTickets.ReserveTicketsMain(user);
            //UserData.UserDataMain();
            //RoomOptions.RoomOptionsMain();
            //Films.FilmMain();
            //Filters.GenreFilter(user);
            Console.Read();
        }

        static void Login(User user)
        {
            while (user.accountVerified == false)
            {
                user.VerifyLogin();
            }
        }

        public static int ChoiceInput(int min, int max) {
            int choice;
            string choiceInput = Console.ReadLine();
            int.TryParse(choiceInput, out choice);
            while (max < choice || choice < min || string.IsNullOrWhiteSpace(choiceInput)) {
                Console.WriteLine("Invalid Input! Please enter your option:");
                choiceInput = Console.ReadLine();
                int.TryParse(choiceInput, out choice);
            }
            return choice;
        }

        public static string StringCheck() {
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input)) {
                Console.WriteLine("Invalid Input! Please try again.");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}
