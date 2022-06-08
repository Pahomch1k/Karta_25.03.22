using System;
using System.Collections.Generic;
using static System.Console;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections;  


namespace Dz_22_Karta
{
    class Program
    {
        static void Main(string[] args)
        {
            int flag = 0; 
            while (flag == 0)
            {
                WriteLine("1. Старт\n2. Выход");
                int choise = 0;
                choise = Convert.ToInt32(ReadLine());

                switch (choise)
                {
                    case 1:
                        { 
                            Game game = new Game();

                            int f = 0;
                            while (f == 0)
                            {
                                Write("Сколько игроков? ");
                                choise = Convert.ToInt32(ReadLine());
                                if (choise < 2 || choise > 6) Write("Некоректное кол-во игроков");
                                else f++;
                            }
                             
                            for (int i = 0; i < choise; i++)
                            {
                                string n;
                                Player pl = new Player();
                                Write("Имя? ");
                                n = ReadLine();
                                pl.name = n;

                                game.players.Add(pl);
                            }

                            game.StartGame();
                        }
                        break;
                    case 2: flag++; break;
                    default: WriteLine("Try again"); break;
                } 
            }
        }
    }

    class Karta<T>
    {
        T karta; 
        public Karta() {  }

        public Karta(T x)
        {
            karta = x; 
        } 

        public string GetKartaString()
        {
            string n = $"{karta}";
            return n;
        } 
    }

    class Player
    {
        public string name { get; set; }
        public List<Karta<int>> Koloda1 = new List<Karta<int>>();
        public List<Karta<string>> Koloda2 = new List<Karta<string>>(); 
        public int count { get; set; }

        public Player()
        {
            count = 0;
            name = "";
        }
    }

    class Game
    {
        public List<Player> players = new List<Player>();
        int[,] K = new int[9 , 4]; 

        public Game()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 4; j++)
                    K[i, j] = 1; 
        }

        public void StartGame()
        {
            int[] countGame = new int[6];

            InitKart();

            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].Koloda1.Count != 0)
                    {
                        WriteLine($"{players[i].name} - {players[i].Koloda1[j].GetKartaString()}"); 
                        players[i].Koloda1.RemoveAt(j);
                    }
                    else if (players[i].Koloda2.Count != 0)
                    {  
                        WriteLine($"{players[i].name} - {players[i].Koloda2[j].GetKartaString()}");
                        players[i].Koloda2.RemoveAt(j);
                    }
                    
                }
            } 
        }

        public void InitKart()
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int fl = 0;
                    while (fl == 0)
                    {
                        int x, x1 = 0;
                        Random f = new Random();
                        x = f.Next(6, 14);

                        for (int g = 0; g < 4; g++)
                        {
                            if (K[x - 6, g] != 0)
                            {
                                if (x < 11) 
                                { 
                                    Karta<int> k = Kart2(x);
                                    players[i].Koloda1.Add(k);
                                    K[x - 6, g] = 0;
                                    x1++;
                                    break;
                                }
                                else 
                                { 
                                    Karta<string> k = Kart1(x);
                                    players[i].Koloda2.Add(k);
                                    K[x - 6, g] = 0;
                                    x1++;
                                    break;
                                }   
                            } 
                        }
                        if (x1 > 0) fl++;
                    }  
                } 
            }
            WriteLine("Конец Init");
        }

        public Karta<string> Kart1(int x)
        {
            string[] s = new string[4] { "valet", "dama", "korol", "tyz" };
            Karta<string> k = new Karta<string>("");

            switch (x)
            {
                case 11: k = new Karta<string>(s[0]); break;
                case 12: k = new Karta<string>(s[1]); break;
                case 13: k = new Karta<string>(s[2]); break;
                case 14: k = new Karta<string>(s[3]); break; 
                default: break;
            }
            WriteLine("Karta1 заполнена");
            return k;
        }

        public Karta<int> Kart2(int x)
        {
            int[] s = new int[5] { 6, 7, 8, 9, 10 }; 
            Karta<int> k = new Karta<int>(0);

            switch (x)
            {
                case 6: k = new Karta<int>(s[0]); break;
                case 7: k = new Karta<int>(s[1]); break;
                case 8: k = new Karta<int>(s[2]); break;
                case 9: k = new Karta<int>(s[3]); break;
                case 10: k = new Karta<int>(s[4]); break;
                default: break;
            }
            WriteLine("Karta2 заполнена ");
            return k;
        } 
    }
}
