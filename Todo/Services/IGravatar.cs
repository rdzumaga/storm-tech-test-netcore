using System.Threading.Tasks;

namespace Todo.Services
{
    public interface IGravatar
    {
        Task<string> GetUserDetailsAsync(string emailAddress);
    }
}
