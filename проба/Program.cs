namespace проба;

using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите количество персонажей: ");
        int characterCount = int.Parse(Console.ReadLine());
        {
            if (characterCount >= 2)
            {
                List<GameCharacter> characters = new List<GameCharacter>()
                {
                    new("111", 10, true, 0, 0),
                    new("222", 10, true, 2, 2),
                    new("333", 10, false, 0, 1),
                    new("444", 10, false, 0, 1)
                };
                /*for (int i = 0; i < characterCount; i++)
                {
                    Console.WriteLine("Введите информацию о персонаже " + (i + 1));
                    GameCharacter character = new GameCharacter();
                    int a = 0;
                    while (character.InputInformation() != (characterCount == a))
                        a++;
                    characters.Add(character);
                } */
                while (true)
                {
                    Console.Write("Выберете персонажа(По индексу): ");
                    int ans = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\nИгра начинается!");
                    characters[ans].Menu(characters); 
                }
            } 
            else
            {
                Console.WriteLine("Для начала игры нужно создать минимум двух игроков!");
                Console.ReadLine();
            }
        }
    }
}
