using bankAPI.Context;
using bankAPI.Model;
using KYC;

namespace bankAPI.Repository
{
    public class CustomerRepository : IRepository
    {
        private readonly applicationDbContext _context;
        // readonly kycDetails _kycdetails;

        public CustomerRepository(applicationDbContext context) {

            _context = context;
            //_kycdetails = kycdetails;

        }

        

        public Customer? find(int mobileNumber)
        {
            return _context.customers.FirstOrDefault(c => c.mobileNumber == mobileNumber);
           // return _context.customers.Find(mobNum);
        }

        public void Save(Customer customer)
        {
            _context.customers.Add(customer);
            _context.SaveChanges();

        }


        public Customer doKYC(Customer customer)
        {
            //kycDetails _kycdetails = new kycDetails();
            //_kycdetails.mobileNumber = customer.mobileNumber;

            // check age if 18 or not
            bool a = customerKYC.CheckAge(customer.DOB);
            bool b = customerKYC.checkRegion(customer.country);
            if (!a)
            {

                customer.kycStatus = "Failed";
                customer.kycInfo = "KYC failed!!! Age must be 18+";
                customer.kycDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //customer.kycStatus = _kycdetails.kycStatus;

            }
            else if (!b) {
                customer.kycStatus = "Failed";
                customer.kycInfo = "KYC failed!!! Country must be India";
                customer.kycDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                //customer.kycStatus = _kycdetails.kycStatus;

            }
            else
            {
                customer.kycStatus = "Success";
                customer.kycInfo = "KYC successful!!!";
                customer.kycDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //customer.kycStatus = _kycdetails.kycStatus;

            }

            _context.SaveChanges();
            return customer;
          

        }
    }
}
