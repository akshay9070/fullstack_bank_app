
namespace KYC
{
    public class customerKYC
    {

        // check if age is 18+ or not
        public static bool CheckAge(string birthDate)
        {
            DateTime dob = DateTime.Parse(birthDate);
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - dob.Year;
            DateTime do11b=currentDate.AddYears(-age);

            // Check if the birthday has occurred this year
            if (dob.Date > currentDate.AddYears(-age))
            {
                age--;
            }

            return age>=18;

        }

        
        // check the nationality
        public static bool checkRegion(string region)
        {
            return region.ToLower() == "india";
        }
    }

}