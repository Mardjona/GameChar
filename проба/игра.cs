namespace проба;
class GameCharacter
{
    
    // Свойства класса
    private string name { get; set; }
    private int MaxHealth { get; set; }
    private double CurrentHealth { get; set; }
    private bool IsAlly { get; set; }
    private int CoorX { get; set; }
    private int CoorY { get; set; }
    private int Wins { get; set; }
    private double Damage;
    public bool alive = true;
    public string Name { get { return name; } }

    //Создание персонажа
    public bool InputInformation()
    {
        Console.Write("Имя персонажа: ");
        name = Console.ReadLine();
        Console.Write("Максимальное здоровье: ");
        MaxHealth = int.Parse(Console.ReadLine());
        Damage = MaxHealth * 0.4;
        Console.Write("Координата X: ");
        CoorX = int.Parse(Console.ReadLine());
        Console.Write("Координата Y: ");
        CoorY = int.Parse(Console.ReadLine());
        CurrentHealth = MaxHealth; // Устанавливаем текущее здоровье равным максимальному при создании персонажа
        Console.Write("Принадлежность к лагерю (+/-): ");
        string s = Console.ReadLine();
        if (s == "+")
        {
            IsAlly = true;
            return false;
        }
        else if (s == "-")
        {
            IsAlly = false;
            return false;
        }
        else
            Console.WriteLine("Ошибка");
        return true;
    }
    // Вывод информации о персонаже 
    private void DisplayInformation()
    {
        Console.WriteLine(" Информация о персонаже:");
        Console.WriteLine(" Имя: " + name);
        Console.WriteLine(" Максимальное здоровье: " + MaxHealth);
        Console.WriteLine(" Текущее здоровье: " + CurrentHealth);
        Console.WriteLine(" Принадлежность к лагерю: " + (IsAlly ? "Команда 1" : "Команда 2"));
        Console.WriteLine(" Координаты: (" + CoorX + ", " + CoorY + ")");
        Console.WriteLine(" Количество побед  " + Wins);
    }
    // Метод начало игры
    private void StartGame(List<GameCharacter> persons)
    {
        Console.Write($"На вашем пути обнаружен враг! Что собираетесь делать " +
        $"  \n1.  Нанести урон " +
        $"  \n2.  Убить" +
        $"  \n3.  Переместиться" +
        $"  \n    ");
        switch (Convert.ToInt32(Console.ReadLine()))
        {
            case 1: Fight(persons); break;
            case 2: Dead(persons); break;
            case 3: Move(persons); break;
        }
    }
    //  Метод передвижения по полю 
    private void Move(List<GameCharacter> persons)
    {
        Console.Write("\n Введите новую координату X: ");
        int x = int.Parse(Console.ReadLine());
        Console.Write("\n Введите новую координату Y: ");
        int y = int.Parse(Console.ReadLine());
        int previousX = CoorX;   // временная переменная
        int previousY = CoorY;   // временная переменная
        CoorX = x;
        CoorY = y;
        Console.WriteLine($"\n Персонаж {name} переместился с координат {previousX},{previousY} на {CoorX},{CoorY}");
        foreach (GameCharacter p in persons)
        {
            if (x == p.CoorX && y == p.CoorY && p!=this)
            {
                if (p.alive == true)
                {
                    if (p.IsAlly != this.IsAlly)
                        Fight(persons);
                    else
                        Console.WriteLine("\n Это ваш тиммейт");
                }
            }
            else
            {
                Console.WriteLine("\n На этих координатах никого нет");
            }
        }
    }
    // Метод сражения 
    private void Fight(List<GameCharacter> persons)
    {
        List<GameCharacter> Team1 = new List<GameCharacter>();
        List<GameCharacter> Team2 = new List<GameCharacter>();
        // количество игроков каждой команды
        foreach (GameCharacter p in persons)
        {
            if (p.IsAlly == IsAlly)
                Team1.Add(p);
            else
                Team2.Add(p);
        }
        Console.WriteLine($" \n В команде Team1 {Team1.Count} игроков.  ");
        Console.WriteLine($"В \n В команде Team2 {Team2.Count} игроков ");
        // Нанесение урона
        while (true)
        {
            //создание переменных урона
            double Team1Dem = 0;
            double Team2Dem = 0;
            //суммирование урона  членов команд
            foreach (GameCharacter p in Team1)
                Team1Dem += p.Damage;
            foreach (GameCharacter p in Team2)
                Team2Dem += p.Damage;
            //деление суммарного урона на количество противников 
            Team1Dem /= Team2.Count;
            Team2Dem /= Team1.Count;

            //нанесение урона
            Console.WriteLine(" ДРАКА НАЧАЛАСЬ!");
            foreach (GameCharacter p in Team1)
            {
                if (CoorX == p.CoorX && CoorY == p.CoorY)
                {
                    if (p.alive == true)
                    {
                        if (IsAlly != p.IsAlly)
                        {
                            p.CurrentHealth -= Team2Dem;
                            Console.WriteLine($"\n Имя игрока {p.name} \tКоличество его здоровья {p.CurrentHealth}");
                            if (p.CurrentHealth <= 0)
                            {
                                p.alive = false;
                                Console.WriteLine("\n Игрок" + " " + p.name + " " + "умер");
                                Wins++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Team1.Count; i++)
            {
                if (Team1[i].alive == false)
                {
                    Team1.Remove(Team1[i]);
                }
            }

            foreach (GameCharacter p in Team2)
            {
                if (CoorX == p.CoorX && CoorY == p.CoorY)
                {
                    if (p.alive == true)
                    {
                        if (IsAlly != p.IsAlly)
                        {
                            p.CurrentHealth -= Team1Dem;
                            Console.WriteLine($"\n Имя игрока {p.name} \tКоличество здоровья {p.CurrentHealth}");
                            if (p.CurrentHealth <= 0)
                            {
                                p.alive = false;
                                Console.WriteLine("\n Игрок" + " " + p.name + " " + "умер");
                                Wins++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Team2.Count; i++)
            {
                if (Team2[i].alive == false)
                {
                    Team2.Remove(Team2[i]);
                }
            }
            // проверяем жив ли игрок
            if (alive == false)
            {
                Console.WriteLine("Вы умерли и выбываете из игры ");
                Environment.Exit(0);
            }
            
            bool isTeam1Alive = false; // Отсутствуют живые игроки
            bool isTeam2Alive = false; // Отсутствуют живые игроки

            foreach (GameCharacter person in Team1)
                if (person.alive && person.IsAlly == IsAlly)
                {
                    isTeam1Alive = true;
                    break;
                }
            foreach (GameCharacter person in Team1)
                if (person.alive && person.IsAlly != IsAlly)
                {
                    isTeam2Alive = true;
                    break;
                }
            // Если живых игроков не осталось игра прекращается
            if (isTeam1Alive == false)   return;
            if (isTeam2Alive == false)   return;
            Console.WriteLine("Враг атакован ");
            Console.WriteLine($"Что будете делать " +
                $"1. Продолжить атаку" +
                $"2. Восстановить свое здоровье" +
                $"3. Восстановить здоровье своему тиммейту ");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1: break;
                case 2: RestoreHealth(); break;
                case 3: Heal(persons); break;
            }
            //выбор дествий в бою
            while (true)
            {
                Console.Write
                ("\n--------------------\n" +
                 "Что будете делать?\n" +
                 "1. Сражаться дальше\n" +
                 "2. Восстановить очик здоровья\n" +
                 "3. Лечить союзников\n" +
                 "4. Применить ульт\n" +
                 "5. Бежать\n" 
                 );
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1: break;
                    case 2: RestoreHealth(); break;
                    case 3: Heal(persons); break;
                    case 4: Dead(Team2); break;
                    case 5: Move(persons); break;
                }
                break;
            }
            //проверка кто победил
            int aliveCount = 0;
            foreach (GameCharacter p in Team2)
            {
                if (p.alive)
                    aliveCount++;
            }
            if (aliveCount == 0)
            {
                Console.WriteLine(" Игроков не осталось ");
            }
            Console.WriteLine(" Битва продолжается");
        }
    }
     
    //Ультимативная способность
    private void Dead(List<GameCharacter> persons)
    {
        if (Wins > 10)
            foreach (GameCharacter p in persons)
            {
                if (CoorX == p.CoorX && CoorY == p.CoorY)
                    if (p.IsAlly != IsAlly)
                        p.alive = false;
                Console.WriteLine($"\n Игрок {p.name} погиб.");
            }
    }
    // Самолечение 
    private void RestoreHealth()
    {
        if (Wins >= 5)
        {
            Console.WriteLine($"\n Персонаж {name} использовал полное лечение.");
            CurrentHealth = MaxHealth;
        }
        else
        {
            Console.WriteLine("\n Недостаточно побед для использования полного лечения.");
        }
    }
    // лечение команды 
    private void Heal(List<GameCharacter> persons) 
    {
        foreach (GameCharacter p in persons)
        {
            Console.WriteLine("Введите имя товарища, которого  хотите полечить ");
            string name = Console.ReadLine();
            if (name == p.name && p.IsAlly == IsAlly && p.alive == alive)
            {
                Console.WriteLine("Какое количество здоровья хотите передать?");
                int hp = Convert.ToInt32(Console.ReadLine());
                if (hp < CurrentHealth)
                {
                    if (hp < p.MaxHealth)
                    {
                        p.CurrentHealth += hp;
                        CurrentHealth -= hp;
                        Console.WriteLine(" Вы не бросили товарища в беде");
                        Console.WriteLine($" Ваше здоровье равно {CurrentHealth}");
                    }
                }
                else
                    Console.WriteLine("\n Недостаточно здоровья для передачи товарищу");
            }
            else
                Console.WriteLine($"\n Персонаж {p.name} не является тиммейтом ");
        }
    }
     //Меню
    public void Menu(List<GameCharacter> persons)
    {
        //Проверка встречного игрока 
        foreach (GameCharacter p in persons)
            if (CoorX == p.CoorX && CoorY == p.CoorY)
                if (p.alive == true)
                    if (IsAlly != p.IsAlly)
                        StartGame(persons);
        while (true)
        {
            if (persons.Count(person => person.alive == true && person.IsAlly != IsAlly) == 0 &&
                persons.Count(person => person.alive == true && person.IsAlly == IsAlly) == 0)
            {
                Console.WriteLine("\n Ничья \n"); return;
            }
            else if (persons.Count(person => person.alive == true && person.IsAlly != IsAlly) == 0)
            {
                Console.WriteLine("\n  Победу одержала КОМАНДА 1 \n"); return;
            }
            else if (persons.Count(person => person.alive == true && person.IsAlly == IsAlly) == 0)
            {
                Console.WriteLine("\n победу одержала команда 2 \n"); return;
            }
            else
            {
                if (alive == false)
                {
                    Console.WriteLine("Персонаж мертв, нажмите Enter для выхода");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.Write
                    ("--------------------------------\n" +
                        "Выберете необходимое действие:\n" +
                        "1. Показать данные персонажа\n" +
                        "2. Передвижение\n" +
                        "3. Восстановить ОЗ\n" +
                        "4. Лечить союзников\n" +
                        "5. Выход\n" +
                        ">");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1: DisplayInformation(); break;
                        case 2: Move(persons); break;
                        case 3: RestoreHealth(); break;
                        case 4: Heal(persons); break;
                        case 5: return;
                    }
                }
            }
        }
    }
}














