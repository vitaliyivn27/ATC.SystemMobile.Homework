using ATC.ATCLibrary;

namespace ATC
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startAtc = DateTime.Now;
            MTC<User> mtc = new MTC<User>("MTC");
            OpenOrdinaryAccount(mtc);
            OpenOrdinaryAccount(mtc);
            OpenUnlimittedAccount(mtc);
            mtc.ConnectAllUsers();
            mtc.FirstTryCallSecond();
            mtc.SecondTryToAnswer();
            mtc.ThirdTryToCallSecond();
            var timeOfCall = mtc.EndCall();
            Console.WriteLine("\nThe first month of using MTC mobile has passed");            
            DateTime endFirstMonth = startAtc.AddDays(45);
            Console.WriteLine($"Today : {endFirstMonth}");
            mtc.CalculateBalance(timeOfCall);
        }

        private static void OpenOrdinaryAccount(MTC<User> mtc)
        {
            Console.WriteLine("Put some money on number:");
            decimal summury = Convert.ToDecimal(Console.ReadLine());
            TarifType tarifType;
            tarifType = TarifType.Ordinary;

            mtc.Open(tarifType, summury,
                AddSumHandler, WithdrawSumHandler,
                CloseAccountHandler, OpenAccountHandler,
                CallingHandler, CalledHandler,
                BusyHandler, EndedHandler, 
                ConnectToPortHandler, DisconnectFromPortHandler);
        }
        private static void OpenUnlimittedAccount(MTC<User> mtc)
        {
            Console.WriteLine("Put some money on number:");
            decimal summury = Convert.ToDecimal(Console.ReadLine());
            TarifType tarifType;
            tarifType = TarifType.Unlimited;

            mtc.Open(tarifType, summury,
                AddSumHandler, WithdrawSumHandler,
                CloseAccountHandler, OpenAccountHandler,
                CallingHandler, CalledHandler,
                BusyHandler, EndedHandler, 
                ConnectToPortHandler, DisconnectFromPortHandler);
        }

        private static void Withdraw(MTC<User> mtc)
        {
            Console.WriteLine("Write how much money you want to withdraw:");

            decimal summury = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Write number:");
            long number = Convert.ToInt32(Console.ReadLine());

            mtc.Withdraw(summury, number);
        }

        private static void Put(MTC<User> mtc)
        {
            Console.WriteLine("Write how much money you want to put:");
            decimal summury = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Write number:");
            long number = Convert.ToInt32(Console.ReadLine());
            mtc.Put(summury, number);
        }

        private static void CloseAccount(MTC<User> mtc)
        {
            Console.WriteLine("Write number you need to close:");
            long number = Convert.ToInt32(Console.ReadLine());

            mtc.Close(number);
        }

        private static void OpenAccountHandler(object sender, UserEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void AddSumHandler(object sender, UserEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void WithdrawSumHandler(object sender, UserEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void CloseAccountHandler(object sender, UserEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void CallingHandler(object sender, UserCallArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void CalledHandler(object sender, UserCallArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void BusyHandler(object sender, UserCallArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void EndedHandler(object sender, UserCallArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void ConnectToPortHandler(object sender, UserCallArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void DisconnectFromPortHandler(object sender, UserCallArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}