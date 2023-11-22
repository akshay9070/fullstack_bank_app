using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace bankAPI.Model
{
    public class Customer
    {

        public Customer()
        {
            GenerateAccountNumber();
            kycStatus = "Pending";
            kycInfo = "KYC Pending";
            kycDate = "0000-00-00 00:00:00";
        }

        public string kycStatus { get; set; }

        [Key]
        public int userID { get; set; }

        [Required]
        public int mobileNumber { get; set; }

        // account number
        public string account_number {  get; private set; }
        
        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string address { get; set; }

        // for kyc
       
        public string kycInfo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        [DataType(DataType.DateTime)]
        public string kycDate { get; set; }

        [Required]
        public string country { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        // Generate an 11-digit account number
        private void GenerateAccountNumber()
        {        
            Random random = new Random();
            account_number = random.Next(100000000, 999999999).ToString() + random.Next(0, 9).ToString();
        }


    }

    
}

// create 3 api

// 1. to check existig customer based on mobile number
// 2. KYC using above para/ age cal
// 3. create account and return account number 