using bankAPI.Model;
using bankAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace bankAPI.Controllers
{
    [ApiController]
    [Route("bankAPI/v1/")]
    public class CustomerController :  ControllerBase
    
    {
        private readonly IRepository _repo;

        public CustomerController(IRepository repo) {

            _repo = repo;

        }



        // 1. API : to create account
        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (_repo.find(customer.mobileNumber) != null)
                {
                    return BadRequest(new { message = $" Oops! It seems like there's an issue. " +
                        $" An account with the mobile number " +
                        $"{customer.mobileNumber} already exists." });

                }
                else
                {
                    _repo.Save(customer);
                    return Ok(new { message =customer.mobileNumber });
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Validation failed", errors = errors });
            }
        }


        // 2. API to check if account exist
        [HttpGet]
        [Route("check/{mobileNumber:int}")]
        public ActionResult check(int mobileNumber)
        {
            // check if customer exist
            var existingCustomer = _repo.find(mobileNumber);
    
            if(existingCustomer == null)
            {
                // var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

                //return BadRequest(new { message = "No Account Found", errors = errors });
                return NotFound("No Account Found");

            }
            else
            {
                string acc_num = existingCustomer.account_number;
                return Ok(new { existingCustomer });

                
            }
        }

        //3. API for KYC
        [HttpGet]
        [Route("kyc/{mobileNumber:int}")]

        public ActionResult doKYC(int mobileNumber)
        {
            // check if customer exist
            var existingCustomer = _repo.find(mobileNumber);

            if (existingCustomer != null && existingCustomer.kycStatus == "Pending")
            {
                var kycresult = _repo.doKYC(existingCustomer);
                return Ok(new { kycresult });
             

            }
            else if(existingCustomer != null && existingCustomer.kycStatus != "Pending")
            {
                var msg = existingCustomer;
                var response = new
                {
                    message = msg
                    
                    // You can include additional data in the model if needed
                    // Example: additionalData = "Some additional information"
                };

                return BadRequest(response);

            }
            else
            {
               // No account exist
                return NotFound(); 


            }

            
        }




        
    }
}
