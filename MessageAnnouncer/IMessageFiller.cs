using OpenMod.API.Ioc;

namespace MessageAnnouncer
{
    [Service]
    public interface IMessageFiller
    {
        string FillMessage(string message);
    }
}