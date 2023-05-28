using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize(80, 27);

            int score = 4;

            int xOffset = 1;
            int yOffset = 24;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("score:4", xOffset, yOffset++);

            WriteText("+", 0, 25);
            WriteText("+", 78, 25);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            // Отрисовка точек			
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            sound mäng = new sound();
            ConsoleKeyInfo nupp = new ConsoleKeyInfo();
            _ = mäng.Tagaplaanis_Mangida("../../../back.wav");                 

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    _ = mäng.Natuke_mangida("../../../eat.wav");
                    food = foodCreator.CreateFood();
                    food.Draw();
                    score++;
                    WriteScore(score);
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            WriteGameOver();
            Console.ReadLine();
        }

        static void WriteScore(int score)
        {
            int xOffset = 7;
            int yOffset = 24;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteNum(score.ToString(), xOffset, yOffset++);
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("  m ä n g   o n   l ä b i   ", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("Autor: fngpr                ", xOffset + 2, yOffset++);
            WriteText(" Eesti valitsuse tellimusel", xOffset + 1, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            sound over = new sound();
            _ = over.Natuke_mangida("../../../over.wav");
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

        static void WriteNum(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

    }
}