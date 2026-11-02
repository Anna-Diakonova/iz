using System;

namespace ShopProject
{
    public delegate void ShoppingEventHandler(int count);
    public delegate void OutOfStockEventHandler();

    public class Shop
    {
        public static int ItemCount;

        public static void OnEvent1(int count)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Товара в магазине {ItemCount}");
            Console.ResetColor();
        }

        public static void OnEvent2()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Заказать товар на базе");
            Console.ResetColor();
        }
    }

    public class Buyer
    {
        public event ShoppingEventHandler Event1;
        public event OutOfStockEventHandler Event2;

        private int itemsToBuy;

        private bool hasFailedToBuy = false;

        public bool HasFailedToBuy
        {
            get { return hasFailedToBuy; }
        }

        public Buyer(int itemsToBuy)
        {
            this.itemsToBuy = itemsToBuy;
        }

        public void MakePurchase()
        {
            if (Shop.ItemCount >= itemsToBuy)
            {
                Shop.ItemCount -= itemsToBuy;
                Event1?.Invoke(itemsToBuy);
            }
            else
            {
                hasFailedToBuy = true;
                Event2?.Invoke();
            }
        }

        public void OnEvent1(int count)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Купил товара={count}");
            Console.ResetColor();
        }

        public void OnEvent2()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Не смог купить товара={itemsToBuy}");
            Console.ResetColor();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Shop.ItemCount = random.Next(10, 21);

            while (true)
            {
                int wantedItems = random.Next(1, 8);

                Buyer buyer = new Buyer(wantedItems);

                buyer.Event1 += buyer.OnEvent1;
                buyer.Event1 += Shop.OnEvent1;

                buyer.Event2 += buyer.OnEvent2;
                buyer.Event2 += Shop.OnEvent2;

                buyer.MakePurchase();

                if (buyer.HasFailedToBuy)
                {
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}