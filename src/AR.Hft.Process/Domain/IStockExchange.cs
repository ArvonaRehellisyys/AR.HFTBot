namespace AR.Hft.Process.Domain
{
    /// <summary>
    /// IStockExchange provides price data for signals.
    /// </summary>
    public interface IStockExchange
    {
        double GetPrice(string stockMessage);
    }
}