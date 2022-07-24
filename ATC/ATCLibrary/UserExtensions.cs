
namespace ATC.ATCLibrary
{
    internal static class UserExtensions
    {
        internal static void CalculateOrdinary(this OrdinaryTarifUser tarif, decimal time)
        {
            decimal monthBill = time * 0.1m;
            tarif.Sum -= monthBill;
            tarif.AddOrCloseOrdinaryNumber(tarif.Sum);
        }
        internal static void CalculateUnlimitted(this UnlimittedTarifUser tarif)
        {
            tarif.Sum -= 900;
            tarif.AddOrCloseUnlimittedNumber(tarif.Sum);
        }
    }
}
