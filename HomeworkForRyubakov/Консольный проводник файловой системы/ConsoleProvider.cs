using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProvider
{
    class FileExplorer
    {
        private static string currentDirectory;
        private static DriveInfo[] allDrives;

        static void Main(string[] args)
        {
            Initialize();
            ShowMainMenu();
        }

        static void Initialize()
        {
            allDrives = DriveInfo.GetDrives();
            currentDirectory = Directory.GetCurrentDirectory();
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== КОНСОЛЬНЫЙ ПРОВОДНИК ===");
                Console.WriteLine($"Текущая директория: {currentDirectory}");
                Console.WriteLine("1. Просмотр содержимого текущего каталога");
                Console.WriteLine("2. Перейти в подкаталог");
                Console.WriteLine("3. Вернуться в родительский каталог");
                Console.WriteLine("4. Просмотр доступных дисков");
                Console.WriteLine("5. Создать новый каталог");
                Console.WriteLine("6. Создать новый файл");
                Console.WriteLine("7. Просмотреть содержимое файла");
                Console.WriteLine("8. Удалить файл или каталог");
                Console.WriteLine("9. Выход");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowDirectoryContents();
                        break;
                    case "2":
                        EnterSubdirectory();
                        break;
                    case "3":
                        GoToParentDirectory();
                        break;
                    case "4":
                        ShowAvailableDrives();
                        break;
                    case "5":
                        CreateNewDirectory();
                        break;
                    case "6":
                        CreateNewFile();
                        break;
                    case "7":
                        ViewFileContents();
                        break;
                    case "8":
                        DeleteItem();
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        WaitForUser();
                        break;
                }
            }
        }

        static void ShowDirectoryContents()
        {
            Console.Clear();
            Console.WriteLine($"Содержимое каталога: {currentDirectory}");
            Console.WriteLine(new string('-', 50));

            try
            {
                // Показываем подкаталоги
                var directories = Directory.GetDirectories(currentDirectory);
                Console.WriteLine("\n[Каталоги]");
                foreach (var dir in directories)
                {
                    var dirInfo = new DirectoryInfo(dir);
                    Console.WriteLine($"{dirInfo.Name} [Каталог]");
                }

                // Показываем файлы
                var files = Directory.GetFiles(currentDirectory);
                Console.WriteLine("\n[Файлы]");
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    Console.WriteLine($"{fileInfo.Name} ({fileInfo.Length} байт)");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Ошибка: Нет доступа к каталогу.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Ошибка: Каталог не найден.");
            }

            WaitForUser();
        }

        static void EnterSubdirectory()
        {
            Console.Clear();
            Console.WriteLine("Введите имя подкаталога или полный путь:");
            var subdirectory = Console.ReadLine();

            try
            {
                if (Path.IsPathRooted(subdirectory))
                {
                    if (Directory.Exists(subdirectory))
                    {
                        currentDirectory = subdirectory;
                    }
                    else
                    {
                        Console.WriteLine("Каталог не существует.");
                    }
                }
                else
                {
                    var newPath = Path.Combine(currentDirectory, subdirectory);
                    if (Directory.Exists(newPath))
                    {
                        currentDirectory = newPath;
                    }
                    else
                    {
                        Console.WriteLine("Каталог не существует.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            WaitForUser();
        }

        static void GoToParentDirectory()
        {
            try
            {
                var parent = Directory.GetParent(currentDirectory);
                if (parent != null)
                {
                    currentDirectory = parent.FullName;
                }
                else
                {
                    Console.WriteLine("Вы уже в корневом каталоге.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            WaitForUser();
        }

        static void ShowAvailableDrives()
        {
            Console.Clear();
            Console.WriteLine("Доступные диски:");
            Console.WriteLine(new string('-', 50));

            foreach (var drive in allDrives)
            {
                try
                {
                    Console.WriteLine($"Диск {drive.Name}");
                    Console.WriteLine($"  Тип: {drive.DriveType}");
                    if (drive.IsReady)
                    {
                        Console.WriteLine($"  Файловая система: {drive.DriveFormat}");
                        Console.WriteLine($"  Общий объем: {drive.TotalSize / (1024 * 1024 * 1024)} GB");
                        Console.WriteLine($"  Свободно: {drive.TotalFreeSpace / (1024 * 1024 * 1024)} GB");
                    }
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Console.WriteLine($"Не удалось получить информацию о диске {drive.Name}");
                }
            }

            Console.WriteLine("\nХотите перейти на другой диск? (y/n)");
            var choice = Console.ReadLine();
            if (choice?.ToLower() == "y")
            {
                Console.Write("Введите букву диска (например, C:): ");
                var driveLetter = Console.ReadLine();
                if (!string.IsNullOrEmpty(driveLetter) && driveLetter.EndsWith(":"))
                {
                    try
                    {
                        if (Directory.Exists(driveLetter))
                        {
                            currentDirectory = driveLetter;
                        }
                        else
                        {
                            Console.WriteLine("Диск не существует или недоступен.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
            }

            WaitForUser();
        }

        static void CreateNewDirectory()
        {
            Console.Clear();
            Console.WriteLine("Введите имя нового каталога:");
            var dirName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(dirName))
            {
                Console.WriteLine("Имя каталога не может быть пустым.");
                WaitForUser();
                return;
            }

            try
            {
                var newDirPath = Path.Combine(currentDirectory, dirName);
                Directory.CreateDirectory(newDirPath);
                Console.WriteLine($"Каталог '{dirName}' успешно создан.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании каталога: {ex.Message}");
            }

            WaitForUser();
        }

        static void CreateNewFile()
        {
            Console.Clear();
            Console.WriteLine("Введите имя нового файла (с расширением, например, file.txt):");
            var fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Имя файла не может быть пустым.");
                WaitForUser();
                return;
            }

            try
            {
                var filePath = Path.Combine(currentDirectory, fileName);
                Console.WriteLine("Введите содержимое файла (для завершения ввода нажмите Enter дважды):");

                string line;
                var content = new System.Text.StringBuilder();
                while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                {
                    content.AppendLine(line);
                }

                File.WriteAllText(filePath, content.ToString());
                Console.WriteLine($"Файл '{fileName}' успешно создан.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
            }

            WaitForUser();
        }

        static void ViewFileContents()
        {
            Console.Clear();
            Console.WriteLine("Введите имя файла для просмотра:");
            var fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Имя файла не может быть пустым.");
                WaitForUser();
                return;
            }

            try
            {
                var filePath = Path.Combine(currentDirectory, fileName);
                if (File.Exists(filePath))
                {
                    Console.WriteLine($"Содержимое файла '{fileName}':");
                    Console.WriteLine(new string('-', 50));
                    var content = File.ReadAllText(filePath);
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("Файл не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }

            WaitForUser();
        }

        static void DeleteItem()
        {
            Console.Clear();
            Console.WriteLine("Введите имя файла или каталога для удаления:");
            var itemName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(itemName))
            {
                Console.WriteLine("Имя не может быть пустым.");
                WaitForUser();
                return;
            }

            try
            {
                var itemPath = Path.Combine(currentDirectory, itemName);
                Console.WriteLine($"Вы уверены, что хотите удалить '{itemName}'? (y/n)");
                var confirm = Console.ReadLine();

                if (confirm?.ToLower() == "y")
                {
                    if (File.Exists(itemPath))
                    {
                        File.Delete(itemPath);
                        Console.WriteLine($"Файл '{itemName}' успешно удален.");
                    }
                    else if (Directory.Exists(itemPath))
                    {
                        Directory.Delete(itemPath, true);
                        Console.WriteLine($"Каталог '{itemName}' успешно удален.");
                    }
                    else
                    {
                        Console.WriteLine("Файл или каталог не существует.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
            }

            WaitForUser();
        }

        static void WaitForUser()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}