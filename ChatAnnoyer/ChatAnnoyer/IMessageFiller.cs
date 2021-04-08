using OpenMod.API.Ioc;

namespace ChatAnnoyer
{
    [Service]
    public interface IMessageFiller
    {
        string FillMessage(string message);
    }
}