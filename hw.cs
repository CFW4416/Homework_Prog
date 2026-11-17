using System.Text.Encodings.Web;
using System.Text.Json;

/*
задание 1
namespace game
{
    public class QuestEntry
    {
        public string QuestName { get; set; } = string.Empty;
        public string Status { get; set; } = "Active"; // "Active", "Completed", "Failed"
        public string Description { get; set; } = string.Empty;
    }

    public class AdventureJournal
    {
        public string HeroName { get; set; } = string.Empty;
        public List<QuestEntry> Quests { get; set; } = new();
    }

    class Program
    {
        private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "journal.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            AdventureJournal journal = LoadOrCreateJournal();

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n=== Журнал приключений ===");
                Console.WriteLine("1. Добавить квест");
                Console.WriteLine("2. Завершить квест");
                Console.WriteLine("3. Провалить квест");
                Console.WriteLine("4. Показать все квесты");
                Console.WriteLine("5. Показать только активные квесты");
                Console.WriteLine("6. Выход");
                Console.Write("Выберите действие: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddQuest(journal);
                        break;
                    case "2":
                        ChangeQuestStatus(journal, "Completed");
                        break;
                    case "3":
                        ChangeQuestStatus(journal, "Failed");
                        break;
                    case "4":
                        ShowAllQuests(journal);
                        break;
                    case "5":
                        ShowActiveQuests(journal);
                        break;
                    case "6":
                        isRunning = false;
                        Console.WriteLine("Пока, Полина!");
                        break;
                    default:
                        Console.WriteLine("Не тупи, выбери пункт от 1 до 6.");
                        break;
                }
            }
        }

        private static AdventureJournal LoadOrCreateJournal()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var loadedJournal = JsonSerializer.Deserialize<AdventureJournal>(json, JsonOptions);
                if (loadedJournal != null)
                {
                    Console.WriteLine($"Журнал загружен. С возвращением, {loadedJournal.HeroName}!");
                    return loadedJournal;
                }
            }

            Console.Write("Файл журнала не найден. Введи имя героя: ");
            string? heroName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(heroName))
            {
                Console.Write("Имя не может быть пустым. Введите имя героя: ");
                heroName = Console.ReadLine();
            }

            var newJournal = new AdventureJournal
            {
                HeroName = heroName.Trim(),
                Quests = new List<QuestEntry>()
            };

            SaveJournal(newJournal);
            Console.WriteLine("Новый журнал успешно создан!");
            return newJournal;
        }

        private static void SaveJournal(AdventureJournal journal)
        {
            string json = JsonSerializer.Serialize(journal, JsonOptions);
            File.WriteAllText(FilePath, json);
        }

        private static void AddQuest(AdventureJournal journal)
        {
            Console.Write("\nНазвание квеста: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название квеста не может быть пустым.");
                return;
            }

            Console.Write("Введите описание квеста: ");
            string description = Console.ReadLine() ?? string.Empty;

            journal.Quests.Add(new QuestEntry
            {
                QuestName = name.Trim(),
                Description = description.Trim(),
                Status = "Active"
            });

            SaveJournal(journal);
            Console.WriteLine("Квест добавлен и сохранён!");
        }

        private static void ChangeQuestStatus(AdventureJournal journal, string newStatus)
        {
            var activeQuests = journal.Quests.Where(q => q.Status == "Active").ToList();

            if (activeQuests.Count == 0)
            {
                Console.WriteLine("\nНет активных квестов для изменения статуса.");
                return;
            }

            Console.WriteLine("\nАктивные квесты:");
            for (int i = 0; i < activeQuests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {activeQuests[i].QuestName} — {activeQuests[i].Description}");
            }

            Console.Write("Введите номер квеста: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= activeQuests.Count)
            {
                QuestEntry selectedQuest = activeQuests[index - 1];
                selectedQuest.Status = newStatus;

                SaveJournal(journal);
                string actionText = newStatus == "Completed" ? "завершён" : "провален";
                Console.WriteLine($"Квест «{selectedQuest.QuestName}» {actionText}!");
            }
            else
            {
                Console.WriteLine("Некорректный номер квеста.");
            }
        }

        private static void ShowAllQuests(AdventureJournal journal)
        {
            Console.WriteLine("\nВсе квесты:");
            if (journal.Quests.Count == 0)
            {
                Console.WriteLine("В журнале нет ни одного квеста.");
                return;
            }

            foreach (var quest in journal.Quests)
            {
                Console.WriteLine($"[{quest.Status}] {quest.QuestName}: {quest.Description}");
            }
        }

        private static void ShowActiveQuests(AdventureJournal journal)
        {
            var activeQuests = journal.Quests.Where(q => q.Status == "Active").ToList();

            Console.WriteLine("\nАктивные квесты:");
            if (activeQuests.Count == 0)
            {
                Console.WriteLine("Активных квестов нет.");
                return;
            }

            foreach (var quest in activeQuests)
            {
                Console.WriteLine($"- {quest.QuestName}: {quest.Description}");
            }
        }
    }
}
*/

//задание 2
/*
namespace game
{
    public class Monster
    {
        public string Name { get; set; } = string.Empty;
        public int Health { get; set; }
        public int Damage { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string csvPath = Path.Combine(AppContext.BaseDirectory, "monsters.csv");
            string jsonPath = Path.Combine(AppContext.BaseDirectory, "monsters.json");

            if (!File.Exists(csvPath))
            {
                Console.WriteLine("Файл monsters.csv не найден.");
                return;
            }

            // прочитала строки из csv
            string[] lines = File.ReadAllLines(csvPath);
            List<Monster> monsters = new List<Monster>();

            // пропустила заголовок и распарсила остальных монстров
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] parts = lines[i].Split(';');

                Monster monster = new Monster
                {
                    Name = parts[0].Trim(),
                    Health = int.Parse(parts[1]),
                    Damage = int.Parse(parts[2]),
                    Type = parts[3].Trim()
                };

                monsters.Add(monster);
            }

            // сохранила список в json с отступами
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonOutput = JsonSerializer.Serialize(monsters, jsonOptions);
            File.WriteAllText(jsonPath, jsonOutput);

            // прочитала json обратно и вывела список
            string jsonInput = File.ReadAllText(jsonPath);
            List<Monster> deserializedMonsters = JsonSerializer.Deserialize<List<Monster>>(jsonInput) ?? new List<Monster>();

            foreach (Monster m in deserializedMonsters)
            {
                Console.WriteLine($"[{m.Type}] {m.Name} — HP: {m.Health}, DMG: {m.Damage}");
            }

            // посчитала статистику по максимумам
            if (deserializedMonsters.Count > 0)
            {
                Monster maxHpMonster = deserializedMonsters[0];
                Monster maxDmgMonster = deserializedMonsters[0];

                foreach (Monster m in deserializedMonsters)
                {
                    if (m.Health > maxHpMonster.Health)
                    {
                        maxHpMonster = m;
                    }

                    if (m.Damage > maxDmgMonster.Damage)
                    {
                        maxDmgMonster = m;
                    }
                }

                Console.WriteLine("\nСтатистика:");
                Console.WriteLine($"Всего монстров: {deserializedMonsters.Count}");
                Console.WriteLine($"Больше всего HP: {maxHpMonster.Name} ({maxHpMonster.Health} HP)");
                Console.WriteLine($"Больше всего урона: {maxDmgMonster.Name} ({maxDmgMonster.Damage} DMG)");
            }

            Console.WriteLine("\nПока, Полина!");
        }
    }
}
*/

//задание 3
using System.Globalization;


namespace game
{
    public class PlayerProfile
    {
        public string Nickname { get; set; } = string.Empty;
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public double WinRate { get; set; }
    }

    class Program
    {
        private static readonly string LogPath = Path.Combine(AppContext.BaseDirectory, "actions.log");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n=== Управление профилями ===");
                Console.WriteLine("1. Создать профиль");
                Console.WriteLine("2. Записать результат игры");
                Console.WriteLine("3. Показать профиль");
                Console.WriteLine("4. Показать лог действий");
                Console.WriteLine("5. Выход");
                Console.Write("Выбери действие: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateProfile();
                        break;
                    case "2":
                        RecordGameResult();
                        break;
                    case "3":
                        ShowProfile();
                        break;
                    case "4":
                        ShowLog();
                        break;
                    case "5":
                        isRunning = false;
                        Console.WriteLine("Пока, Полина!");
                        break;
                    default:
                        Console.WriteLine("Не тупи, выбери пункт от 1 до 5.");
                        break;
                }
            }
        }

        // собрала путь к файлу 
        private static string GetProfilePath(string nickname)
        {
            return Path.Combine(AppContext.BaseDirectory, $"profile_{nickname}.json");
        }

        // сохранила профиль в json 
        private static void SaveProfile(PlayerProfile profile)
        {
            string json = JsonSerializer.Serialize(profile, JsonOptions);
            File.WriteAllText(GetProfilePath(profile.Nickname), json);
        }

        // дописала строку в общий лог 
        private static void WriteLog(string message)
        {
            string timestamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            File.AppendAllText(LogPath, $"[{timestamp}] {message}\n");
        }

        private static void CreateProfile()
        {
            Console.Write("\nВведи никнейм: ");
            string? nickname = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                Console.WriteLine("Никнейм не может быть пустым.");
                return;
            }

            string path = GetProfilePath(nickname);

            //  не занят ли уже такой профиль
            if (File.Exists(path))
            {
                Console.WriteLine("Профиль уже существует!");
                return;
            }

            var profile = new PlayerProfile
            {
                Nickname = nickname,
                GamesPlayed = 0,
                Wins = 0,
                WinRate = 0
            };

            SaveProfile(profile);
            WriteLog($"Создан профиль: {nickname}");
            Console.WriteLine("Профиль создан!");
        }

        private static void RecordGameResult()
        {
            Console.Write("\nВведи никнейм: ");
            string? nickname = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                Console.WriteLine("Никнейм не может быть пустым.");
                return;
            }

            string path = GetProfilePath(nickname);

            if (!File.Exists(path))
            {
                Console.WriteLine("Профиль не найден");
                return;
            }

            Console.Write("Введи результат (win или loss): ");
            string? result = Console.ReadLine()?.Trim().ToLower();

            if (result != "win" && result != "loss")
            {
                Console.WriteLine("Нужно ввести win или loss.");
                return;
            }

            // загрузила профиль
            string json = File.ReadAllText(path);
            var profile = JsonSerializer.Deserialize<PlayerProfile>(json);

            if (profile == null)
                return;

            profile.GamesPlayed++;
            if (result == "win")
            {
                profile.Wins++;
            }

            profile.WinRate = profile.Wins * 100.0 / profile.GamesPlayed;

            SaveProfile(profile);

            string winRateFormatted = profile.WinRate.ToString("0.0", CultureInfo.InvariantCulture);
            WriteLog($"{profile.Nickname}: {result} (Всего игр: {profile.GamesPlayed}, Побед: {profile.Wins}, WinRate: {winRateFormatted}%)");

            Console.WriteLine("Результат записан!");
        }

        private static void ShowProfile()
        {
            Console.Write("\nВведи никнейм: ");
            string? nickname = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nickname))
            {
                Console.WriteLine("Никнейм не может быть пустым.");
                return;
            }

            string path = GetProfilePath(nickname);

            if (!File.Exists(path))
            {
                Console.WriteLine("Профиль не найден");
                return;
            }

            // прочитала профиль  и вывела данные
            string json = File.ReadAllText(path);
            var profile = JsonSerializer.Deserialize<PlayerProfile>(json);

            if (profile != null)
            {
                string winRateFormatted = profile.WinRate.ToString("0.0", CultureInfo.InvariantCulture);
                Console.WriteLine($"\nНикнейм: {profile.Nickname}");
                Console.WriteLine($"Всего игр: {profile.GamesPlayed}");
                Console.WriteLine($"Побед: {profile.Wins}");
                Console.WriteLine($"WinRate: {winRateFormatted}%");
            }
        }

        private static void ShowLog()
        {
            Console.WriteLine("\n Лог действий ");
            if (!File.Exists(LogPath))
            {
                Console.WriteLine("Лог пока пуст.");
                return;
            }

            // прочитала и вывела весь файл actions.log
            Console.Write(File.ReadAllText(LogPath));
        }
    }
}
