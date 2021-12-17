using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Table
{
    public enum Type
    {
        K, O
    }

    public enum Actions
    {
        ADD, DELETE, UPDATE
    }
    public class OOP
    {
        

        public struct Log
        {
            public Actions action;
            public DateTime time;
            public string info;
            public string item;

            public Log(Actions action, DateTime time, string info, string item)
            {
                this.action = action;
                this.time = time;
                this.info = info;
                this.item = item;
            }
        }
        public struct Item
        {
            public int number;
            public String ItemName;
            public Type ItemType;
            public double price;
            public int amount;

            public Item(int number, string ItemName, Type ItemType, double price, int amount)
            {
                this.number = number;
                this.ItemName = ItemName;
                this.ItemType = ItemType;
                this.price = price;
                this.amount = amount;
            }

            public void Print()
            {
                Console.WriteLine($"|{this.number}|{this.ItemName,-25}|{this.ItemType,-10}|{this.price,-18}|{this.amount,-17}|");
            }
        }

        static void ShowTable(List<Item> items)
        {
            Console.WriteLine(new String('-', 77));
            Console.WriteLine($"{"|Прайс-лист",-76}|");
            Console.WriteLine(new String('-', 77));
            Console.WriteLine($"|№|{"Наименование товара",-25}|{"Тип товара",-10}|{"Цена за 1 шт (руб)",-18}|{"Количество",-17}|");
            Console.WriteLine(new String('-', 77));
            foreach (Item item in items)
            {
                item.Print();
                Console.WriteLine(new String('-', 77));
            }
            Console.WriteLine($"{"|Перечисляемый тип: К – канцтовары, О - оргтехника",-76}|");
            Console.WriteLine(new String('-', 77));
        }

        static void Add(List<Item> items)
        {
            Console.WriteLine("Введите данные:");

            Console.WriteLine("Наименование товара:");
            string name = Console.ReadLine();

            Type ItemType;
            while (true)
            {
                Console.WriteLine("Тип товара(K, O):");
                string tmp = Console.ReadLine();
                if (tmp.ToUpper() == "K" || tmp.ToUpper() == "К") // обработка русской и английской К
                {
                    ItemType = Type.K;
                    break;
                }
                else if (tmp.ToUpper() == "O" || tmp.ToUpper() == "О") // обработка русской и английской О
                {
                    ItemType = Type.O;
                    break;
                }
                else Console.WriteLine("Некорректный ввод значения. Введите еще раз.");

            }

            Console.WriteLine("Цена за 1 шт (руб)");
            double price = Math.Round(double.Parse(Console.ReadLine()), 2);

            Console.WriteLine("Количество:");
            int amount = Int32.Parse(Console.ReadLine());
            Item value = new(items.Count + 1, name, ItemType, price, amount);
            items.Add(value);
        }

        static void Delete(List<Item> items, int index)
        {
            List<Item> AfterRemove = items;
            AfterRemove.Remove(AfterRemove[index - 1]);

            for (int i = index - 1; i < items.Count; i++)
            {
                Item value = items[i];
                value.number = i + 1;
                items[i] = value;
            }
        }
        static void Update(List<Item> items, int index)
        {
            
            Add(items);
            Item TmpItem = items[items.Count - 1];
            TmpItem.number = index;
            items.Remove(items[items.Count - 1]);
            items.Remove(items[index - 1]);
            items.Insert(index - 1, TmpItem);
        }

        static void Search(List<Item> items)
        {
            List<Item> ResultOfSearch = new();
            Console.WriteLine("Введите номер столбца, к которому будет применен фильтр:\n1 - Тип товара\n2 - Цена за 1 шт (руб)\n3 - Количество");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Введите тип, по которому будут выбраны строки:\n1 - К\n2 - О");
                    string tmp1 = Console.ReadLine();

                    if (tmp1.ToUpper() == "K" || tmp1.ToUpper() == "К" || tmp1.ToUpper() == "O" || tmp1.ToUpper() == "о")
                        foreach (Item item in items)
                        {
                            if (tmp1.ToUpper() == "K" || tmp1.ToUpper() == "К") // обработка русской и английской К
                            {
                                if (item.ItemType == Type.K)
                                    ResultOfSearch.Add(item);

                            }
                            else if (tmp1.ToUpper() == "O" || tmp1.ToUpper() == "О") // обработка русской и английской О
                            {
                                if (item.ItemType == Type.O)
                                    ResultOfSearch.Add(item);
                            }
                        }
                    else
                        Console.WriteLine("Ошибка ввода.");
                    break;

                case "2":
                    Console.WriteLine("Введите значение, по которому будут выбраны строки(после выбора значения будет выбран оператор):");
                    if (int.TryParse(Console.ReadLine(), out int tmp2))
                    {
                        Console.WriteLine("Введите оператор (<, >, =):");
                        string comparison = Console.ReadLine();
                        if (comparison == "<" || comparison == ">" || comparison == "=")
                            foreach (Item item in items)
                            {
                                switch (comparison)
                                {
                                    case ">":
                                        if (item.price > tmp2)
                                            ResultOfSearch.Add(item);
                                        break;

                                    case "<":
                                        if (item.price < tmp2)
                                            ResultOfSearch.Add(item);
                                        break;

                                    case "=":
                                        if (item.price == tmp2)
                                            ResultOfSearch.Add(item);
                                        break;
                                }
                            }
                        else Console.WriteLine("Ошибка распознавания оператора.");
                    }
                    else Console.WriteLine("Ошибка распознавания числа.");
                    break;

                case "3":
                    Console.WriteLine("Введите значение, по которому будут выбраны строки(после выбора значения будет выбран оператор):");
                    if (int.TryParse(Console.ReadLine(), out int tmp3))
                    {
                        Console.WriteLine("Введите оператор (<, >, =):");
                        string comparison = Console.ReadLine();
                        if (comparison == "<" || comparison == ">" || comparison == "=")
                            foreach (Item item in items)
                            {
                                switch (comparison)
                                {
                                    case ">":
                                        if (item.amount > tmp3)
                                            ResultOfSearch.Add(item);
                                        break;

                                    case "<":
                                        if (item.amount < tmp3)
                                            ResultOfSearch.Add(item);
                                        break;

                                    case "=":
                                        if (item.amount == tmp3)
                                            ResultOfSearch.Add(item);
                                        break;
                                }
                            }
                        else Console.WriteLine("Ошибка распознавания оператора.");
                    }
                    else Console.WriteLine("Ошибка распознавания числа.");
                    break;

                default:
                    Console.WriteLine("Ошибка распознавания столбца.");
                    break;
            }
            Console.WriteLine("Таблица, сгенерированная с помощью заданных фильтров:");
            ShowTable(ResultOfSearch);
        }

        static void AddToLog(List<Log> CurrentLog, Actions action, List<TimeSpan> LogTimes, List<Item> items)
        {
            string info = "";
            DateTime NowTime = DateTime.Now;
            TimeSpan SpanTime = DateTime.Now.TimeOfDay;
            LogTimes.Add(SpanTime);

            if (action == Actions.ADD)
            {
                 info = "добавлена запись";
            }
            if (action == Actions.DELETE)
            {
                info = "удалена запись";
            }
            if (action == Actions.UPDATE)
            {
                info = "обновлена запись";
            }
            Item item = items[items.Count - 1];
            Log log = new(action, NowTime, info, item.ItemName);
            CurrentLog.Add(log);
            
        }

        static TimeSpan MaxDifference(List<TimeSpan> LogTimes)
        {
            LogTimes.Sort();
            if (LogTimes.Count == 1)
            {
                return LogTimes[0];
            }
            
            if (LogTimes.Count == 2)
            {
                return LogTimes[1] - LogTimes[0];
            }
            TimeSpan MaxDiff = LogTimes[1] - LogTimes[0];
            for (int i = 2; i < LogTimes.Count; i++)
            {
                if (LogTimes[i] - LogTimes[i - 1] > MaxDiff)
                {
                    MaxDiff = LogTimes[i] - LogTimes[i - 1];
                }
            }

            return MaxDiff;

        }
        static void ShowLog(List<Log> CurrentLog, List<TimeSpan> LogTimes)
        {
            while (CurrentLog.Count > 50)
                CurrentLog.Remove(CurrentLog[0]);

            TimeSpan MaxDiff = MaxDifference(LogTimes);

            Console.WriteLine(new String('=', 50));

            if (CurrentLog.Count == 0)
                Console.WriteLine("В логе пока ничего нет");
            else 
                foreach (Log element in CurrentLog)
                {
                    Console.WriteLine($"{element.time.ToLongTimeString()} - {element.info} \"{element.item}\"");
                }

            Console.WriteLine($"\n{Convert.ToDateTime(MaxDiff.ToString()).ToLongTimeString()} – Самый долгий период бездействия пользователя");
            Console.WriteLine(new String('=', 50));

        }

        private static void Main()
        {
            List<Item> items = new();
            List<Log> CurrentLog = new();
            List<TimeSpan> LogTimes = new();
            int choice = 0;       

            while (choice != 7)
            {
                Console.WriteLine("Выберите операцию:\n" +
                              "1 – Просмотр таблицы\n" +
                              "2 – Добавить запись\n" +
                              "3 – Удалить запись\n" +
                              "4 – Обновить запись\n" +
                              "5 – Поиск записей\n" +
                              "6 – Просмотреть лог\n" +
                              "7 - Выход\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ShowTable(items);
                        break;
                    case 2:
                        Add(items);
                        AddToLog(CurrentLog, Actions.ADD, LogTimes, items);
                        Console.WriteLine("Операция завершена\n" + new String('=', 20));
                        break;
                    case 3:
                        Console.WriteLine("Введите номер записи, которую следует удалить:");
                        int toDelete = int.Parse(Console.ReadLine());
                        AddToLog(CurrentLog, Actions.DELETE, LogTimes, items);
                        Delete(items, toDelete);
                        Console.WriteLine("Операция завершена\n" + new String('=', 20));
                        break;
                    case 4:
                        Console.WriteLine("Введите номер записи, которую следует обновить:");
                        int toUpdate = int.Parse(Console.ReadLine());
                        AddToLog(CurrentLog, Actions.UPDATE, LogTimes, items);
                        Update(items, toUpdate);
                        Console.WriteLine("Операция завершена\n" + new String('=', 20));
                        break;
                    case 5:
                        Search(items);
                        break;
                    case 6:
                        ShowLog(CurrentLog, LogTimes);
                        break;
                    case 7:
                        Console.WriteLine("Выход из программы...");
                        Console.WriteLine("Операция завершена\n" + new String('=', 20));
                        break;
                }
            }
        }

    }
}