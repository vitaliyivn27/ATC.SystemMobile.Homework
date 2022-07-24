using ATC;

namespace ATC.ATCLibrary
{
    public class OrdinaryTarifUser : User
    {
        public OrdinaryTarifUser(decimal summury) : base(summury)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new UserEventArgs($"Opened new account with ordinary tarif! Number: {this.Number}", this.Sum));
        }

        public void AddOrCloseOrdinaryNumber(decimal summury)
        {
            if (Sum >= 0)
            {
                Console.WriteLine($"Money balance +{Number} after paying last month, {Sum}, continue using");
            }
            else if (Sum < 0)
            {
                Console.WriteLine($"You need to cover {Sum} or your +{Number} will be closed");
                Console.WriteLine($"Do you want to put money on +{Number}? Y/N");
                string? input = Console.ReadLine();
                string? option = input.ToLower();
                switch (option)
                {
                    case "y":
                        Console.WriteLine("Put some money :");
                        var edditedMoney = Convert.ToDecimal(Console.ReadLine());
                        Put(edditedMoney);
                        if (Sum < 0)
                        {
                            Close();
                        }
                        else
                        {
                            Console.WriteLine($"Money balance +{Number} after paying last month, {Sum}, continue using");
                        }
                        break;
                    case "n":
                        Close();
                        break;
                    default:
                        Close();
                        break;
                }
            }
        }
    }
}
