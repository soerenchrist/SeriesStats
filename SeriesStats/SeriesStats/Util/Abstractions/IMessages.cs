namespace SeriesStats.Util.Abstractions
{
    public interface IMessages
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}