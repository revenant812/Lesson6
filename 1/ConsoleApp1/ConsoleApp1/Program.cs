using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {                
                var AllProcess = Process.GetProcesses();
                Console.WriteLine($"Имя процесса                PID");
                Console.WriteLine("================================");
                foreach (var x in AllProcess)
                {
                    Console.WriteLine($"{x.ProcessName} {x.Id}");
                }
                Console.WriteLine("====================================");
                Console.WriteLine("Если Вы хотите завершить нужный процесс по ID, то нажмите 1");
                Console.WriteLine("Если Вы хотите найти нужный процесс по имени, то нажмите 2");
                Console.Write("Наберите нужную цифру: ");
                if (Convert.ToInt32(Console.ReadLine()) == 1)
                {
                    KillProcessById();                   
                }
                else
                {
                    Console.Write("Наберите имя процесса который Вы хотите найти: ");
                    var Name = Console.ReadLine();
                    for (int i = 0; i < Find(Name).Item2; i++)
                    {
                        var FindProccess = Process.GetProcessesByName(Find(Name).Item1[i]);
                        Console.WriteLine($"{FindProccess[i].ProcessName} {FindProccess[i].Id}");
                    }
                    if (Find(Name).Item2 > 1)
                    {
                        Console.WriteLine("найдено несколько процессов с одинаковым именем");
                        KillProcessById();
                    }
                    else
                    {
                        bool CheckProcess = true;
                        Console.Write("Напишите имя процесса который Вы хотите завершить: ");
                        string ResultString = Console.ReadLine();
                        if (string.IsNullOrEmpty(ResultString))
                        {
                            Console.Write("Вы не сделали выбор, программа будет перезапущена");
                        }
                        foreach (var n in AllProcess)
                        {
                            if (n.ProcessName == ResultString)
                            {
                                CheckProcess = false;
                                n.Kill();
                                Console.Write("Процесс завершен, нажмите любую клавижу для продолжения...");
                                Console.ReadKey();
                            }
                        }
                        if(CheckProcess)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Процесс с таким именем не найден, программа будет перезапущена.");
                            Console.Write("Для продолжения нажмите любую клавишу...");
                            Console.ReadKey();
                        }
                    }
                        
                }
            }
        }
        static (string[], int) Find(string str)
        {
            var AllProcess1 = Process.GetProcesses();
            int count = 0;
            int count1 = 0;
            foreach (var p in AllProcess1)
            {
                if (p.ProcessName.Contains(str))
                {
                    count = count + 1;
                }
            }
            string[] array = new string[count];

            foreach (var p in AllProcess1)
            {
                if (p.ProcessName.Contains(str))
                {
                    array[count1] = p.ProcessName;
                    count1 = count1 + 1;
                }
            }
            return (array, count1);
        }

        static void KillProcessById()
        {
            int NumberProcess;
            bool CheckProcess = true;
            var ProcessForKill = Process.GetProcesses();
            Console.Write("Наберите номер ID который Вы хотите завершить: ");           
            string h = Console.ReadLine();
            bool check = int.TryParse(h, out NumberProcess);                    
            if (check)
            {
                
                foreach (var k in ProcessForKill)
                {
                    if (k.Id == NumberProcess)
                    {
                        CheckProcess = false;
                        k.Kill();
                        Console.Write("Процесс завершен, нажмите любую клавижу для продолжения...");
                        Console.ReadKey();
                        break;
                    }                   
                } 
                if(CheckProcess)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Процесс с таким номером не найден, программа будет перезапущена.");
                    Console.Write("Для продолжения нажмите любую клавишу...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Вы ввели неверное значение, программа будет перезапущена.");
                Console.Write("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}
