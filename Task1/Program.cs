using System;
using System.IO;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            UserDirectory();
        }
        static string EnterDirectory()
        {
            Console.WriteLine("Введите необходимую папку");
            string str = Console.ReadLine();
            return str;
        }
        static bool CheckUser(string comand)
        {

            string direct = DirectoryAdress.Directorys;

            if (Directory.Exists(direct))
            {
                if (comand == "Да" || comand == "да")
                    return true;

                else
                    Console.WriteLine(direct);

                return false;
            }

            else
            {
                Console.WriteLine("Такой папки не существует");
                return false;
            }
        }
        public static void UserDirectory()
        {
            bool start;

            DirectoryAdress.Directorys = EnterDirectory();
            Console.WriteLine("Папка для очистки: " + DirectoryAdress.Directorys);
            Console.WriteLine("Начать очистку файлов? (Да/Нет)");
            string comandToStart = Console.ReadLine();

            start = CheckUser(comandToStart);
            if (start)
                FileManager.GetFiles();
            else
                UserDirectory();

        }
        public class DirectoryAdress
        {
            private static string directory;

            public static string Directorys
            {
                get { return directory; }
                set { directory = value; }
            }
        }

        class FileManager
        {
            public static int howMuchDeletedFiles = 0;
            public static void GetFiles()
            {
                DirectoryInfo dirinfo = new DirectoryInfo(DirectoryAdress.Directorys);
                try
                {
                    if (dirinfo.GetDirectories().Length != 0)
                    {
                        string[] dirs = Directory.GetDirectories(DirectoryAdress.Directorys);
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            var updateTime = File.GetLastWriteTime(dirs[i]);
                            var currentTime = DateTime.Now;
                            if (currentTime - updateTime > TimeSpan.FromMinutes(30))
                            {
                                Console.WriteLine("Файл под названием: {0} удален", dirs[i]);
                                Directory.Delete(dirs[i]);
                                howMuchDeletedFiles++;
                            }
                        }
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
                try
                {
                    if (dirinfo.GetFiles().Length != 0)
                    {
                        string[] files = Directory.GetFiles(DirectoryAdress.Directorys);
                        for (int i = 0; i < files.Length; i++)
                        {
                            var updateTime = File.GetLastWriteTime(files[i]);

                            var currentTime = DateTime.Now;

                            if (currentTime - updateTime > TimeSpan.FromMinutes(30))
                            {
                                Console.WriteLine("Файл под названием: {0} удален", files[i]);
                                File.Delete(files[i]);
                                howMuchDeletedFiles++;
                            }
                        }
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }

                Console.WriteLine("Процедура очистки завершена, всего удалено: {0} файлов", howMuchDeletedFiles);
                Program.UserDirectory();
            }
        }
    }
}
