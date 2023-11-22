using bankAPI.Context;
using bankAPI.Model;

namespace bankAPI.Repository
{
    public interface IRepository
    {
        public void Save(Customer customer);
        public Customer find(int mobNum);
        public Customer doKYC(Customer customer);
    }
}
