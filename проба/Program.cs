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
                    new ("1", 10,true,1,1),
                    new ("2", 10,true,1,1),
                    new ("3", 10,false,2,2),
                    new ("4", 10,false,2,2)
                }; 
                for (int i = 0; i < characterCount; i++)
                {
                    Console.WriteLine("Введите информацию о персонаже " + (i + 1));
                    GameCharacter character = new GameCharacter();
                    int a = 0;
                    while (character.InputInformation() != (characterCount == a))
                        a++;
                    characters.Add(character);
                } 
                while (true)
                {
                    Console.Write("Выберете персонажа: ");
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