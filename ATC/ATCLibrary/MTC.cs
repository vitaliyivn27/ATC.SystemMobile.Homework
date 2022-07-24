
namespace ATC.ATCLibrary
{
    public enum TarifType
    {
        Ordinary,
        Unlimited
    }
    public class MTC<T> where T : User
    {
        T[] accounts;

        public string Name { get; private set; }

        public MTC(string name) => this.Name = name;

        public void Open(TarifType tarifType, decimal summury,
            UserStateHandler addSumHandler, UserStateHandler withdrawSumHandler,
            UserStateHandler closeAccountHandler, UserStateHandler openAccountHandler,
            UserCallHandler callingHandler, UserCallHandler calledHandler,
            UserCallHandler busyHandler, UserCallHandler endedHandler, 
            UserCallHandler connectToPortHandler, UserCallHandler disconnectFromPortHandler)
        {
            T? newAccount = null;
            switch (tarifType)
            {
                case TarifType.Ordinary:
                    newAccount = new OrdinaryTarifUser(summury) as T;
                    break;
                case TarifType.Unlimited:
                    newAccount = new UnlimittedTarifUser(summury) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("Error to open new number");
            
            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calling += callingHandler;
            newAccount.Called += calledHandler;
            newAccount.Busy += busyHandler;
            newAccount.Ended += endedHandler;
            newAccount.Connected += connectToPortHandler;
            newAccount.Disconnected += disconnectFromPortHandler;

            newAccount.Open();
        }

        public void Put(decimal summury, long number)
        {
            T account = FindAccount(number);
            if (account == null)
                throw new Exception("Number isn't founded");
            account.Put(summury);
        }

        public void Withdraw(decimal summury, long number)
        {
            T account = FindAccount(number);
            if (account == null)
                throw new Exception("Number isn't founded");
            account.Withdraw(summury);
        }

        public void Close(long number)
        {
            long someNumber;
            T account = FindAccount(number, out someNumber);
            if (account == null)
                throw new Exception("Number isn't founded");

            account.Close();

            if (accounts.Length <= 1)
                accounts = null;
            else
            {
                T[] tempAccounts = new T[accounts.Length - 1];
                for (long i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != someNumber)
                        tempAccounts[j++] = accounts[i];
                }
                accounts = tempAccounts;
            }
        }

        public void CalculateBalance(decimal time)
        {
            if (accounts == null)
                return;
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i] is OrdinaryTarifUser)
                {
                    (accounts[i] as OrdinaryTarifUser).CalculateOrdinary(time);
                }
                else
                {
                    (accounts[i] as UnlimittedTarifUser).CalculateUnlimitted();
                }
            }
        }

        public void ConnectAllUsers()
        {
            if (accounts == null)
                return;
            for (int i = 0; i < accounts.Length; i++)
            {
                accounts[i].ConnectToPort();
            }
        }

        public void FirstTryCallSecond()
        {
            accounts[0].TryToCall();
        }

        public void SecondTryToAnswer()
        {
            accounts[1].AnswerTheCall();
        }

        public void ThirdTryToCallSecond()
        {
            accounts[2].TryToCallOnBusyNumber();
        }

        public decimal EndCall()
        {
            return accounts[0].EndTheCall();
        }

        public T FindAccount(long number)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Number == number)
                    return accounts[i];
            }
            return null;
        }

        public T FindAccount(long number, out long someNumber)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Number == number)
                {
                    someNumber = i;
                    return accounts[i];
                }
            }
            someNumber = -1;
            return null;
        }
    }
}
